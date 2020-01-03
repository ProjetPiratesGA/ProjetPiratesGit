using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ProjetPirate.Data;
using ProjetPirate.Boat;

namespace Project.Network
{
    public class ServerNetworkManager : NetworkManager
    {
        private List<BoatController> _boatList = new List<BoatController>();
        private List<Player> _playerList = new List<Player>();

        public List<BoatController> boatList
        {
            get { return _boatList; }
            set { _boatList = value; }
        }

        public List<Player> playerList
        {
            get { return _playerList; }
            set { _playerList = value; }
        }

        Data_server data = new Data_server();

        ///////////////////////////////////////////////////////////////
        ///Error can appears if two players try to connect on the same time. 
        ///

        #region State Message Register + Login

        public enum StateConnectionMode
        {
            REGISTER,
            LOGIN,
        }

        public enum StateConnectionMessage
        {
            REGISTER_USERNAME_NON_AVAILABLE = -1,
            LOGIN_USERNAME_DOES_NOT_EXIST = 0,
            LOGIN_USERNAME_PASSWORD_DOES_NOT_CORRESPOND = 1,
            REGISTER_SUCCESSFUL = 2,
            LOGIN_USERNAME_PASSWORD_CORRESPOND_AND_IS_NOT_CONNECTED = 3,
            TRY_TO_CONNECT_GAME = 4,
            ACCOUNT_ALREADY_CONNECT = 5,

        }

        public class RegisterClientLogin : MessageBase
        {
            public StateConnectionMode stateConnectionMode;
            public string username;
            public int password;
        }
        public class ConnectionState : MessageBase
        {
            public StateConnectionMessage stateConnectionMessage;
        }

        const string constConnectToGameClientMsg = "CanConnectToGame";
        public class ConnectToGameClient : MessageBase
        {
            public string connectToGameClientMsg = constConnectToGameClientMsg;
        }

        #endregion

        /*Message Type (like channel)*/
        public const short RegisterClientLoginMsgId = 888;
        public const short StateConnectionClientLoginMsgId = 777;
        public const short StateConnectionToGameClientMsgId = 666;

        float fTimeSinceLastSaveServer;
        float fTimeLastSaveServer;

        bool sendStateLoginRegister;

        NetworkConnection _connBuffer;

        StateConnectionMessage stateConnectionBuffer;

        //A opti
        [System.NonSerialized]
        public ClientData tmpDataBufferPlayerRegister;

        public byte[] _usernameBuffer;

        public Data_Player tmpDataBufferPlayerEnterOnGame;
        public byte[] byteDataUpdatePlayerEnterOnGame;

        void Start()
        {
            RegisterHandlers();
            fTimeLastSaveServer = Time.time;
        }

        void Update()
        {
            SendErrorLoginRegister();
            SaveServerEvery(1);
        }
        public override void OnStartServer()
        {
            Debug.Log("On Start Server : Reinitialisation Account is used");

            data = SaveSystem.LoadServer();
            for (int j = 0; j < data.ClientRegistered.Count; j++)
            {
                data.ClientRegistered[j].AccountIsUsed = false;
            }
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Player playerInstance = Instantiate(playerPrefab.GetComponent<Player>());
            _playerList.Add(playerInstance);

            _playerList[_playerList.Count - 1].InitPlayer();

            _connBuffer = conn;

            byte[] dataSend = formateToByte(_playerList[_playerList.Count - 1]._data);


            _playerList[_playerList.Count - 1].RpcRefreshPlayersDatas(dataSend);
            Debug.Log("Add Player, New Client");



            NetworkServer.AddPlayerForConnection(conn, _playerList[_playerList.Count - 1].gameObject, playerControllerId);

            //Rafraichissement des liens de parenté ainsi que des paramétres des clients déja spawn pour le nouveau client
            for (int i = 0; i < _boatList.Count; i++)
            {
                _boatList[i].gameObject.GetComponent<BoatCharacter>().TargetSetParent(conn, _boatList[i].player.gameObject);

                _boatList[i].gameObject.GetComponent<BoatCharacter>().TargetUpdateActiveCannons(conn);
            }

        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            Debug.LogError("Player Disconnect");

            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].connectionToClient == conn)
                {
                    Debug.LogError("Connection Player Correspond to Player Disconnect");

                    for (int j = 0; j < data.ClientRegistered.Count; j++)
                    {
                        if (data.ClientRegistered[j].Username == playerList[i]._username)
                        {
                            data.ClientRegistered[j].AccountIsUsed = false;
                        }
                    }
                    for (int k = 0; k < _boatList.Count; k++)
                    {
                        if(_boatList[k].player.connectionToClient == conn)
                        {
                            _boatList.Remove(_boatList[k]);
                            break;
                        }
                    }
                    playerList.Remove(playerList[i]);
                }
            }
            Debug.LogWarning("Player list size = " + playerList.Count + " , Boat list size = " + _boatList.Count);


            base.OnServerDisconnect(conn);
        }

        public byte[] formateToByte(Data_Player _dataReceive)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, _dataReceive);

            //This gives you the byte array.
            return mStream.ToArray();
        }

        public byte[] formateToByte(string _dataReceive)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, _dataReceive);

            //This gives you the byte array.
            return mStream.ToArray();
        }

        public byte[] formateToByte(ClientData _dataReceive)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, _dataReceive);

            //This gives you the byte array.
            return mStream.ToArray();
        }
        ///////////////////////////////////////////////////////////

        public void SendErrorLoginRegister()
        {
            if (sendStateLoginRegister == true)
            {
                sendStateLoginRegister = false;

                ConnectionState msgStateConnection = new ConnectionState();
                msgStateConnection.stateConnectionMessage = stateConnectionBuffer;

                Debug.Log("Message : " + msgStateConnection.stateConnectionMessage.ToString());

                NetworkServer.SendToClient(_connBuffer.connectionId, StateConnectionClientLoginMsgId, msgStateConnection);
            }
        }

        #region Receipt Message State and Treat it

        public void TreatRecvMessageByClient(NetworkMessage _msg)
        {

            //Read and stock the message send by the Client
            RegisterClientLogin objectMessage = _msg.ReadMessage<RegisterClientLogin>();
            _connBuffer = _msg.conn;

            Debug.Log("Object Message : " + objectMessage.username);

            CheckPlayerCanLogin(objectMessage);
            CheckPlayerCanRegister(objectMessage);

            if (objectMessage.stateConnectionMode != StateConnectionMode.LOGIN
                && objectMessage.stateConnectionMode != StateConnectionMode.REGISTER)
            {

            }

        }

        private void CheckPlayerCanRegister(RegisterClientLogin _objectMessage)
        {
            if (_objectMessage.stateConnectionMode == StateConnectionMode.REGISTER)
            {

                if (CheckUsernameExist(_objectMessage.username) == false)
                {
                    Debug.Log("Username Exist");

                    tmpDataBufferPlayerRegister = new ClientData(_objectMessage.username, _objectMessage.password, data.CountIDUnique);

                    stateConnectionBuffer = StateConnectionMessage.REGISTER_SUCCESSFUL;
                    sendStateLoginRegister = true;

                    if (SaveSystem.RegisterPlayer(tmpDataBufferPlayerRegister, data) == true)
                    {
                        Debug.Log("Password Is Set : " + _objectMessage.password);
                        stateConnectionBuffer = StateConnectionMessage.REGISTER_SUCCESSFUL;
                        sendStateLoginRegister = true;

                    }
                    else
                    {
                        Debug.Log("Password Is Not Set : " + _objectMessage.password);

                    }


                }
                else
                {
                    Debug.Log("Username Is Not Available.");
                    //Send a message to a client
                    sendStateLoginRegister = true;
                    stateConnectionBuffer = StateConnectionMessage.REGISTER_USERNAME_NON_AVAILABLE;




                }

            }
        }

        private void CheckPlayerCanLogin(RegisterClientLogin _objectMessage)
        {
            if (CheckUsernameExist(_objectMessage.username) == true)
            {

                Debug.Log("Username exist");
                if (CheckPasswordCorrespond(_objectMessage.username, _objectMessage.password) == true)
                {
                    if (CheckAccountIsAlreadyConnect(_objectMessage.username) == true)
                    {
                        Debug.Log("Password Correspond");

                        for (int i = 0; i < _playerList.Count; i++)
                        {
                            if (_playerList[i].connectionToClient == _connBuffer)
                            {
                                _playerList[i]._username = _objectMessage.username;

                            }
                        }

                        stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_PASSWORD_CORRESPOND_AND_IS_NOT_CONNECTED;
                        sendStateLoginRegister = true;
                        Debug.Log("Username Before Set Data" + _objectMessage.username);
                        _usernameBuffer = formateToByte(_objectMessage.username);

                        //Load Data Login
                        LoadDataLoginClient(_connBuffer);
                    }
                    else
                    {
                        stateConnectionBuffer = StateConnectionMessage.ACCOUNT_ALREADY_CONNECT;

                    }
                }
                else
                {
                    sendStateLoginRegister = true;

                    Debug.Log("Password doesn't Correspond");
                    stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_PASSWORD_DOES_NOT_CORRESPOND;
                }
            }
            else
            {
                Debug.Log("Username doesn't exist");
                stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_DOES_NOT_EXIST;
                sendStateLoginRegister = true;

            }
        }

        private bool CheckPasswordCorrespond(string _username, int _password)
        {
            //Check On the List who contains username and password. Use Data Structure in the future

            for (int i = 0; i < data.ClientRegistered.Count; i++)
            {
                if (data.ClientRegistered[i].Username == _username)
                {
                    if (data.ClientRegistered[i].Password == _password)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        private bool CheckAccountIsAlreadyConnect(string _username)
        {
            for (int i = 0; i < data.ClientRegistered.Count; i++)
            {
                if (data.ClientRegistered[i].Username == _username)
                {
                    if (data.ClientRegistered[i].AccountIsUsed == false)
                    {
                        data.ClientRegistered[i].AccountIsUsed = true;
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckUsernameExist(string _username)
        {
            if (data != null)
            {
                if (data.ClientRegistered != null)
                {
                    for (int i = 0; i < data.ClientRegistered.Count; i++)
                    {
                        if (data.ClientRegistered[i].Username == _username)
                            return true;
                    }
                }
                else
                {
                    Debug.LogError("data Registered NULL");
                }
            }
            else
            {
                Debug.LogError("Data NULL");
            }
            return false;
        }

        public void ReceiveStateConnectToGame(NetworkMessage _msg)
        {
            ConnectToGameClient objectMessage = _msg.ReadMessage<ConnectToGameClient>();
            if (objectMessage.connectToGameClientMsg == constConnectToGameClientMsg)
            {
                Debug.Log("Client can Connect to Game, Try to Resend a Msg to the Client");
                ConnectToGameClient msgStateConnectionToGame = new ConnectToGameClient();

                Debug.Log("Message : " + msgStateConnectionToGame.connectToGameClientMsg);

                NetworkServer.SendToClient(_msg.conn.connectionId, StateConnectionToGameClientMsgId, msgStateConnectionToGame);

                //Set Data
                LoadDataEnterGameClient(_msg.conn);

            }
        }

        #endregion

        //Register a Handler, to read Client Sending Message
        public void RegisterHandlers()
        {
            //Login Register Connection State Message
            NetworkServer.RegisterHandler(RegisterClientLoginMsgId, TreatRecvMessageByClient);
            NetworkServer.RegisterHandler(StateConnectionToGameClientMsgId, ReceiveStateConnectToGame);

        }

        private void LoadDataLoginClient(NetworkConnection _conn)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                if (_playerList[i].connectionToClient == _connBuffer)
                {
                    Debug.Log("Load Data Login -> Set isConnected");
                    _playerList[i]._isConnected = true;

                }
            }
        }

        public void LoadDataEnterGameClient(NetworkConnection _conn)
        {
            for (int i = 0; i < _playerList.Count; i++)
            {
                Debug.Log("USERNAME : " + _playerList[i]._isConnected);
                if (_playerList[i].connectionToClient == _conn)
                {
                    for (int j = 0; j < data.ClientRegistered.Count; j++)
                    {
                        if (data.ClientRegistered[j].Username == _playerList[i]._username)
                        {
                            byteDataUpdatePlayerEnterOnGame = formateToByte(data.ClientRegistered[j].Player);

                            Debug.Log("Load Data Enter On Game -> Set isEnteringGame");

                            _playerList[i]._isEnteringGame = true;

                        }
                    }

                }
            }

        }

        public void SaveServerEvery(int _timeBetweenEachServer)
        {
            fTimeSinceLastSaveServer = Time.time - fTimeLastSaveServer;

            if (fTimeSinceLastSaveServer >= _timeBetweenEachServer
                && ThereIsAccountUsed())
            {
                fTimeLastSaveServer = Time.time;
                SaveServer();
            }
        }

        public bool ThereIsAccountUsed()
        {
            for (int i = 0; i < data.ClientRegistered.Count; i++)
            {
                if (data.ClientRegistered[i].AccountIsUsed == true)
                    return true;
            }
            return false;
        }

        public void SaveServer()
        {
            Debug.Log("Save Server !");
            SaveSystem.SaveServer(data);
        }
    }
}