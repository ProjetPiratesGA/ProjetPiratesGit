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

        //Data_server data = new Data_server();

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
            LOGIN_USERNAME_PASSWORD_CORRESPOND = 3,
            TRY_TO_CONNECT_GAME = 4,

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


        bool sendStateLoginRegister;

        bool playerCanConnectOnGame;
        bool playerCanCreateAccount;

        NetworkConnection _connBuffer;

        StateConnectionMessage stateConnectionBuffer;

        //A opti
        [System.NonSerialized]
        public ClientData tmpDataBuffer;
        public byte[] dataUpdatePlayer;

        void Start()
        {
            RegisterHandlers();
        }

        void Update()
        {
            SendErrorLoginRegister();

        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        {
            Player playerInstance = Instantiate(playerPrefab.GetComponent<Player>());
            _playerList.Add(playerInstance);

            _playerList[_playerList.Count - 1].idClientBuffer = _playerList.Count - 1;
            _playerList[_playerList.Count - 1].InitPlayer();

            _connBuffer = conn;


            byte[] dataSend = formateToByte(_playerList[_playerList.Count - 1]._data);


            _playerList[_playerList.Count - 1].RpcRefreshPlayersDatas(dataSend);



            NetworkServer.AddPlayerForConnection(conn, _playerList[_playerList.Count - 1].gameObject, playerControllerId);

            for (int i = 0; i < _boatList.Count; i++)
            {
                //_boatList[i].GetComponent<BarLife>().TargetRefreshLifeBar(conn);
            }

        }

        public byte[] formateToByte(Data_Player _dataReceive)
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
                playerCanCreateAccount = false;
                playerCanConnectOnGame = false;
            }

        }

        private void CheckPlayerCanRegister(RegisterClientLogin _objectMessage)
        {
            if (_objectMessage.stateConnectionMode == StateConnectionMode.REGISTER)
            {
                playerCanConnectOnGame = false;

                if (CheckUsernameExist(_objectMessage.username) == false)
                {
                    Debug.Log("Username Exist");

                    tmpDataBuffer = new ClientData(_objectMessage.username, _objectMessage.password);

                    playerCanCreateAccount = true;
                    stateConnectionBuffer = StateConnectionMessage.REGISTER_SUCCESSFUL;
                    sendStateLoginRegister = true;

                    if (SaveSystem.RegisterPlayer(tmpDataBuffer, Data_server.ClientRegistered) == true)
                    {
                        Debug.Log("Password Is Set : " + _objectMessage.password);
                        playerCanCreateAccount = true;
                        stateConnectionBuffer = StateConnectionMessage.REGISTER_SUCCESSFUL;
                        sendStateLoginRegister = true;

                        //Load Data
                        dataUpdatePlayer = formateToByte(tmpDataBuffer.Player);
                        LoadDataRegisterClient(_connBuffer);

                        //LoadData(_msg.conn);
                    }
                    else
                    {
                        Debug.Log("Password Is Not Set : " + _objectMessage.password);

                        playerCanCreateAccount = false;
                    }


                }
                else
                {
                    Debug.Log("Username Is Not Available.");
                    //Send a message to a client
                    sendStateLoginRegister = true;
                    stateConnectionBuffer = StateConnectionMessage.REGISTER_USERNAME_NON_AVAILABLE;


                    playerCanConnectOnGame = false;
                    playerCanCreateAccount = false;

                }

            }
        }

        private void CheckPlayerCanLogin(RegisterClientLogin _objectMessage)
        {
            playerCanCreateAccount = false;
            if (CheckUsernameExist(_objectMessage.username) == true)
            {

                Debug.Log("Username exist");
                if (CheckPasswordCorrespond(_objectMessage.username, _objectMessage.password) == true)
                {

                    Debug.Log("Password Correspond");

                    //tmpDataBuffer = new ClientData(objectMessage.username, objectMessage.password);


                    playerCanConnectOnGame = true;
                    stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_PASSWORD_CORRESPOND;
                    sendStateLoginRegister = true;
                    if (tmpDataBuffer != null)
                    {
                        Debug.Log("1 - Try to Load Data (TargetRPC)");

                    }
                    else
                    {
                        Debug.Log("NUll tmpData");
                    }


                }
                else
                {
                    playerCanConnectOnGame = false;
                    sendStateLoginRegister = true;

                    Debug.Log("Password doesn't Correspond");
                    stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_PASSWORD_DOES_NOT_CORRESPOND;
                }
            }
            else
            {
                playerCanConnectOnGame = false;
                Debug.Log("Username doesn't exist");
                stateConnectionBuffer = StateConnectionMessage.LOGIN_USERNAME_DOES_NOT_EXIST;
                sendStateLoginRegister = true;

            }
        }

        private bool CheckPasswordCorrespond(string _username, int _password)
        {
            //Check On the List who contains username and password. Use Data Structure in the future

            for (int i = 0; i < Data_server.ClientRegistered.Count; i++)
            {
                if (Data_server.ClientRegistered[i].Username == _username)
                {
                    if (Data_server.ClientRegistered[i].Password == _password)
                    {
                        return true;
                    }
                }
                else
                    return false;
            }
            return false;
        }

        private bool CheckUsernameExist(string _username)
        {

            for (int i = 0; i < Data_server.ClientRegistered.Count; i++)
            {
                if (Data_server.ClientRegistered[i].Username == _username)
                    return true;
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
            }
        }

        private bool SetPassword(string _username, int _password)
        {

            for (int i = 0; i < Data_server.ClientRegistered.Count; i++)
            {
                if (Data_server.ClientRegistered[i].Username == _username)
                {
                    Data_server.ClientRegistered[i].Password = _password;
                    return true;
                }
            }
            return false;
        }

        #endregion

        //Register a Handler, to read Client Sending Message
        public void RegisterHandlers()
        {
            //Login Register Connection State Message
            NetworkServer.RegisterHandler(RegisterClientLoginMsgId, TreatRecvMessageByClient);
            NetworkServer.RegisterHandler(StateConnectionToGameClientMsgId, ReceiveStateConnectToGame);

        }

        private void LoadDataRegisterClient(NetworkConnection _conn)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].connectionToClient == _conn)
                {

                    playerList[i].CallCmdLoadData();

                }
            }

        }

        private void LoadDataLoginClient(NetworkConnection _conn)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].connectionToClient == _conn)
                {

                    //playerList[i].CallCmdLoadData();

                }
            }

        }

    }
}