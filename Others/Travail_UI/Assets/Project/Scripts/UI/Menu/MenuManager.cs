using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        [Header("Message Erreur")]
        public GameObject _messageErreur;
        public GameObject _messageErreurInvalidUserName;
        public GameObject _messageErreurInvalidPassword;
        public GameObject _messageErreurPasswordNotEntered;

        [Header("Other")]
        public GameObject _buttonReturnMainMenu;

        bool blockInput = false;
        float timerBlockInput = 0;

        //On récupère l'Id du LvL à charger afin de pouvoir le réutiliser dans le load.
        static public int _iDLevelToLoad;


        // Use this for initialization
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

        // Update is called once per frame
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

        /// <summary>
        /// La fonction nous connecte au serveur et nous emmène au menu plrincipal
        /// </summary>
        public void ConnectingToServor()
        {
            //check des identifiants
            //if(IDUser == true)
            {
                //On accede au serveur
               _LogIn.SetActive(false);
                _mainMenu.SetActive(true);
            }
            //else
            {
                _messageErreur.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
        }

        public void Inscription()
        {
            bool _inscriptionConfirmed = true;

            //if (_userNameInscrire.text == _userName deja existant)
            //{
            //    _messageErreurInvalidUserName.SetActive(true);
            //    this.GetComponent<GraphicRaycaster>().enabled = false;
            //    timerBlockInput = 0;
            //    blockInput = true;
            //_inscriptionConfirmed = false;
            //}


            if (_passwordInscrire.text != _confirmPasswordInscrire.text)
            {
                _messageErreurInvalidPassword.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
                _inscriptionConfirmed = false;
            }

            if (_passwordInscrire.text == string.Empty)
            {
                _messageErreurPasswordNotEntered.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
                _inscriptionConfirmed = false;
            }

            if (_inscriptionConfirmed)
            {
                //fonction pour enregistrer les données
                //Connection au serveur
                _userName.text = "";
                _password.text = "";
                _LogIn.SetActive(true);
                _sInscrire.SetActive(false);
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

        public void GoToGame()
        {
            SceneManager.LoadScene(1);
        }

        //Cette fonction nous permet d'aller à la scene de chargement
        public void GoToLoadingScene(int _iDLvLToLoad)
        {
            _iDLevelToLoad = _iDLvLToLoad; //On set la variable static _iDLevelToLoad à l'index de la scene qu'on voudra charger dans le loadingScene.
            SceneManager.LoadScene(1);
        }
    }
}
