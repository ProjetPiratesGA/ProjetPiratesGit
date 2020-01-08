using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ProjetPirate.Network;


namespace ProjetPirate.UI.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Header("GroupButton")]
        public GameObject _mainMenu;
        public GameObject _LogIn;
        public GameObject _choix;
        public GameObject _sInscrire;
        public GameObject _option;


        [Header("InputField")]
        public InputField _userName;
        public InputField _password;
        public InputField _userNameInscrire;
        public InputField _passwordInscrire;
        public InputField _confirmPasswordInscrire;

        [Header("Message")]
        public GameObject _messageErreur;
        public GameObject _messageErreurInvalidUserName;
        public GameObject _messageErreurInvalidPassword;
        public GameObject _messageErreurPasswordNotEntered;
        //public GameObject _messageRegisterSuccessful;


        [Header("Other")]
        public GameObject _buttonReturnMainMenu;

        bool blockInput = false;
        float timerBlockInput = 0;

        [SerializeField]
        ClientStart _clientStart;

        public enum LaunchStateMessage
        {
            LAUNCH_USERNAME_NON_AVAILABLE,
            LAUNCH_USERNAME_DOES_NOT_EXIST,
            LAUNCH_PASSWORD_DOES_NOT_CORRESPOND,
            LAUNCH_REGISTER_SUCCESFUL,
            LAUNCH_NOTHING,
        }

        public LaunchStateMessage launchScreenStateMessage;

        [System.NonSerialized]
        public bool isTryingToConnectClientOnGame;

        void Start()
        {

            _mainMenu.SetActive(false);
            _LogIn.SetActive(false);
            _messageErreur.SetActive(false);
            _choix.SetActive(true);
            _sInscrire.SetActive(false);
            _buttonReturnMainMenu.SetActive(false);
            _option.SetActive(false);

        }

        void Update()
        {
            if (blockInput)
            {
                timerBlockInput += Time.deltaTime;
                if (timerBlockInput >= 2)
                {
                    _messageErreur.SetActive(false);
                    _messageErreurInvalidUserName.SetActive(false);
                    _messageErreurInvalidPassword.SetActive(false);
                    _messageErreurPasswordNotEntered.SetActive(false);
                    this.GetComponent<GraphicRaycaster>().enabled = true;
                    _userName.text = "";
                    _password.text = "";
                    _userNameInscrire.text = "";
                    _passwordInscrire.text = "";
                    _confirmPasswordInscrire.text = "";
                    blockInput = false;
                }
            }
        }

        public void GoToLogIn()
        {
            _userName.text = "";
            _password.text = "";
            _choix.SetActive(false);
            _LogIn.SetActive(true);
        }


        public void GoToOption()
        {
            _option.SetActive(true);
            _mainMenu.SetActive(false);
            _buttonReturnMainMenu.SetActive(true);
        }

        public void GoToChoice()
        {
            Debug.Log("Create Player");
            //SceneManager.LoadScene("Game");
            _choix.SetActive(false);
            _mainMenu.SetActive(false);
            _buttonReturnMainMenu.SetActive(true);
        }

        public void GoToInscrire()
        {
            _userNameInscrire.text = "";
            _passwordInscrire.text = "";
            _confirmPasswordInscrire.text = "";
            _choix.SetActive(false);
            _sInscrire.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ConnectingToServor()
        {

            _clientStart.tryToLogin = true;

            this.GetComponent<GraphicRaycaster>().enabled = false;
            timerBlockInput = 0;
            blockInput = true;
            /*
            if (launchUsernameDoesNotExist == true)
            {
                _messageErreur.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
            else if(launchPasswordDoesNotCorrespond == true)
            {
                _messageErreur.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
            else
            {
                Debug.Log("Try to CONNECT !!!");
            }
            */
        }

        public void SetScreenRegisterLoginServer()
        {
            if (launchScreenStateMessage == LaunchStateMessage.LAUNCH_USERNAME_NON_AVAILABLE)
            {
                _messageErreurInvalidUserName.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                Debug.Log("NON AVAILABLE");
                blockInput = true;
            }
            else if (launchScreenStateMessage == LaunchStateMessage.LAUNCH_USERNAME_DOES_NOT_EXIST)
            {
                _messageErreur.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
            else if (launchScreenStateMessage == LaunchStateMessage.LAUNCH_PASSWORD_DOES_NOT_CORRESPOND)
            {
                _messageErreur.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
        }

        public void Inscription()
        {
            Debug.Log("Register Ready");
            _clientStart.tryToRegister = true;

            if (_passwordInscrire.text != _confirmPasswordInscrire.text)
            {
                _messageErreurInvalidPassword.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
                _clientStart.tryToRegister = false;

            }

            if (_passwordInscrire.text == string.Empty)
            {
                _messageErreurPasswordNotEntered.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
                _clientStart.tryToRegister = false;

            }

        }

        public void ReturnMainMenu()
        {
            _mainMenu.SetActive(true);
            _LogIn.SetActive(false);
            _messageErreur.SetActive(false);
            _choix.SetActive(false);
            _sInscrire.SetActive(false);
            _buttonReturnMainMenu.SetActive(false);
            _option.SetActive(false);

        }

        private void LoadGame()
        {
            isTryingToConnectClientOnGame = true;
        }


        public void GoToChoiceBeginMenu()
        {
            if (_sInscrire.activeSelf)
            {
                _sInscrire.SetActive(false);
                _choix.SetActive(true);
            }

            if (_LogIn.activeSelf)
            {
                _LogIn.SetActive(false);
                _choix.SetActive(true);
            }
        }
    }
}
