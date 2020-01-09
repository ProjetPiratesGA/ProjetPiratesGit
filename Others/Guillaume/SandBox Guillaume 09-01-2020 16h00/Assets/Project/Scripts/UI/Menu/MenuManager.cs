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
        // Use this for initialization
        void Start()
        {
            _mainMenu.SetActive(true);
            _LogIn.SetActive(false);
            _messageErreur.SetActive(false);
            _choix.SetActive(false);
            _sInscrire.SetActive(false);
            _buttonReturnMainMenu.SetActive(false);
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


        public void GoToChoice()
        {
            _choix.SetActive(true);
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
            //check des identifiants
            //if(IDUser == true)
            {
                //On accede au serveur
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
        }
    }
}
