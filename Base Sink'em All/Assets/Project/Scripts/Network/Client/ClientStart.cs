using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using ProjetPirate.Data;
namespace ProjetPirate.Network
{




    public class ClientStart : NetworkManager
    {
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



        //Message Type for Sending et Receiving data. 
        //It should be the same Message Type than the server.
        public const short RegisterClientLoginMsgId = 888;
        public const short StateConnectionClientLoginMsgId = 777;
        public const short StateConnectionToGameClientMsgId = 666;

        [System.NonSerialized]
        public bool tryToLogin;

        [System.NonSerialized]
        public bool tryToRegister;

        private ClientData data;

        [SerializeField]
        ProjetPirate.UI.Menu.MenuManager _menuManager;

        float timeSinceLastAttemptReconnection;
        float timeLastAttemptReconnection;

        bool isConnectedToServer;

        void Start()
        {
            StartClient();
            RegisterHandlers();
        }
        
        public override void OnClientConnect(NetworkConnection conn)
        {
            isConnectedToServer = true;
            base.OnClientConnect(conn);
        }
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            isConnectedToServer = false;
            base.OnClientDisconnect(conn);
        }

        public void RegisterHost(StateConnectionMode _stateConnectionMode)
        {
            if (_menuManager != null)
            {
                //Register a new Message who can be sent to the server 
                RegisterClientLogin msg = new RegisterClientLogin();
                Debug.Log("test");
                msg.stateConnectionMode = _stateConnectionMode;
                string passwordBuffer;
                if (_stateConnectionMode == StateConnectionMode.LOGIN)
                {
                    msg.username = _menuManager._userName.text;
                    passwordBuffer = _menuManager._password.text;
                }
                else if (_stateConnectionMode == StateConnectionMode.REGISTER)
                {
                    msg.username = _menuManager._userNameInscrire.text;
                    passwordBuffer = _menuManager._passwordInscrire.text;
                }
                else
                {

                    msg.username = _menuManager._userName.text;
                    passwordBuffer = _menuManager._password.text;
                }
                //Check if the username and the password are on a good format
                //if (CheckIsCorrectFormat(msg.username, passwordBuffer, 4, 12, 7, 16) == true)
                {
                    //Encryption of the password
                    msg.password = passwordBuffer.GetHashCode();

                    //Send Login of Client (Username and Password) to the Server.
                    if (client.Send(RegisterClientLoginMsgId, msg) == true)
                    {
                        Debug.Log("Message is Send");

                    }
                    else
                    {
                        Debug.LogWarning("Message isn't Send");
                    }
                }
            }
        }

        public void ReceiveMessageServer(NetworkMessage _msg)
        {

            ConnectionState recvMessage = _msg.ReadMessage<ConnectionState>();
            Debug.Log("Message is received" + recvMessage.stateConnectionMessage.ToString());
            CheckRegisterLoginServer(recvMessage.stateConnectionMessage.ToString());
        }

        //Check Connection State
        void CheckRegisterLoginServer(string _stateMessage)
        {
            if (_menuManager != null)
            {
                Debug.Log(_stateMessage);
                if (_stateMessage == StateConnectionMessage.REGISTER_USERNAME_NON_AVAILABLE.ToString())
                {
                    _menuManager.launchScreenStateMessage = UI.Menu.MenuManager.LaunchStateMessage.LAUNCH_USERNAME_NON_AVAILABLE;

                }
                else if (_stateMessage == StateConnectionMessage.LOGIN_USERNAME_DOES_NOT_EXIST.ToString())
                {
                    _menuManager.launchScreenStateMessage = UI.Menu.MenuManager.LaunchStateMessage.LAUNCH_USERNAME_DOES_NOT_EXIST;

                }
                else if (_stateMessage == StateConnectionMessage.LOGIN_USERNAME_PASSWORD_DOES_NOT_CORRESPOND.ToString())
                {
                    _menuManager.launchScreenStateMessage = UI.Menu.MenuManager.LaunchStateMessage.LAUNCH_PASSWORD_DOES_NOT_CORRESPOND;

                }
                else if (_stateMessage == StateConnectionMessage.REGISTER_SUCCESSFUL.ToString())
                {
                    Debug.Log("Register Succesful");
                    _menuManager.launchScreenStateMessage = UI.Menu.MenuManager.LaunchStateMessage.LAUNCH_REGISTER_SUCCESFUL;

                    _menuManager._LogIn.SetActive(true);
                    _menuManager._sInscrire.SetActive(false);

                }
                else if (_stateMessage == StateConnectionMessage.LOGIN_USERNAME_PASSWORD_CORRESPOND_AND_IS_NOT_CONNECTED.ToString())
                {
                    _menuManager.launchScreenStateMessage = UI.Menu.MenuManager.LaunchStateMessage.LAUNCH_NOTHING;

                    _menuManager._mainMenu.SetActive(true);
                    _menuManager._LogIn.SetActive(false);
                    _menuManager._sInscrire.SetActive(false);
                }

                _menuManager.SetScreenRegisterLoginServer();
            }
        }

        void Update()
        {
            //Debug.Log(isConnectedToServer);
            SendUserInfoToServer();
            SendMsgConnectOnGameToServer();
            ReconnectToServer(2);

        }

        public void SendUserInfoToServer()
        {
            if (tryToLogin == true && tryToRegister == false)
            {

                tryToLogin = false;
                tryToRegister = false;

                Debug.Log("Try To Login");
                RegisterHost(StateConnectionMode.LOGIN);
            }
            //Call this function when the player press REGISTER button
            else if (tryToRegister == true && tryToLogin == false)
            {

                tryToRegister = false;
                tryToLogin = false;

                Debug.Log("Try To Register");

                RegisterHost(StateConnectionMode.REGISTER);
            }
        }

        public void SendMsgConnectOnGameToServer()
        {
            if (_menuManager.isTryingToConnectClientOnGame == true)
            {
                _menuManager.isTryingToConnectClientOnGame = false;

                //Send Message Connection to Server

                ConnectToGameClient msgConnectToGameClient = new ConnectToGameClient();

                if (client.Send(StateConnectionToGameClientMsgId, msgConnectToGameClient) == true)
                {
                    Debug.Log("Message Connect To Game is Send");

                }
                else
                {
                    Debug.LogWarning("Message Connect To Game isn't Send");
                }


            }
        }

        public void ReceiveMessageConnectToGame(NetworkMessage _msgConnectToGame)
        {
            ConnectToGameClient _ReceiveMessageConnectToGame = _msgConnectToGame.ReadMessage<ConnectToGameClient>();
            Debug.Log("Message is received" + _ReceiveMessageConnectToGame.connectToGameClientMsg);
            if (_ReceiveMessageConnectToGame.connectToGameClientMsg == constConnectToGameClientMsg)
            {
                SceneManager.LoadScene("Game");
            }
        }

        private bool CheckIsCorrectFormat(string _username, string _password, int minCharacterUsername, int maxCharacterUsername, int minCharacterPassword, int maxCharacterPassword)
        {
            Debug.Log("Username Lenght : " + _username.Length + " | " + "Password Lenght : " + _password.Length);

            //Check if username and password are similar
            if(_username.Length < _password.Length)
            {
                if (!CheckSimilarLetter(_password, _username))
                    return false;
            }
            else
            {
                if (!CheckSimilarLetter(_username, _password))
                    return false;

            }

            //Check Correct Size
            if (_username.Length >= minCharacterUsername
                && _username.Length <= maxCharacterUsername
                && _password.Length >= minCharacterPassword
                && _password.Length <= maxCharacterPassword)
            {
                //Check Correct Type Character
                for (int i = 0; i < _username.Length; i++)
                {
                    if (!char.IsLetterOrDigit(_username, i))
                    {
                        return false;
                    }
                }
                for (int i = 0; i < _password.Length; i++)
                {
                    if (!char.IsLetterOrDigit(_password, i))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }

        }

        private bool CheckSimilarLetter(string _biggestWord, string _lowestWord)
        {
            int _countSimilarLetter = 0;
            for (int i = 0; i < _lowestWord.Length; i++)
            {
                if (_lowestWord[i] != _biggestWord[i])
                {

                    _countSimilarLetter++;
                }
            }
            if (_countSimilarLetter > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RegisterHandlers()
        {
            Debug.Log("Register Handlers");
            client.RegisterHandler(StateConnectionClientLoginMsgId, ReceiveMessageServer);
            client.RegisterHandler(StateConnectionToGameClientMsgId, ReceiveMessageConnectToGame);

        }

        public void ReconnectToServer(float _timeBetweenAttemptReconnection)
        {
            if (!isConnectedToServer)
            {
                timeSinceLastAttemptReconnection = Time.time - timeLastAttemptReconnection;
                if (timeSinceLastAttemptReconnection >= _timeBetweenAttemptReconnection)
                {
                    if (client != null)
                    {
                        client.Disconnect();
                        client.Shutdown();
                        client = null;
                        timeLastAttemptReconnection = Time.time;
                        Debug.Log("Attempt to Reconnect to SERVER");
                        StartClient();
                    }
                    else if (client == null)
                    {
                        StartClient();
                    }
                }
            }
        }
    }
}
