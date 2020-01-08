﻿using ProjetPirate.Boat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using ProjetPirate.Boat;

namespace ProjetPirate.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVEMENT")]
        public GameObject _joystick;

        [Header("SHOOT")]
        public GameObject _buttonShootUp;
        public GameObject _buttonShootLeft;
        public GameObject _buttonShootRight;

        private BoatCharacter _boatCharacter = null;


        private GameObject _player;//On récupère le player afin que le controller possede ses info 

        public GameObject player
        {
            get { return _player; }
        }

        //Variable  pour gérer le reload notement l'animation des bouttons
        bool canReloadUp = false;
        bool canReloadLeft = false;
        bool canReloadRight = false;

        //Temps de reload pour chaque canon (au cas ou il diffère selon le canon)
        float currentReloadTimeUp = 0;
        float currentReloadTimeLeft = 0;
        float currentReloadTimeRight = 0;

        //Il faudra ajuster cette variable en fonction du reload des canons
        float reloadingTime = 2;

        // Use this for initialization
        //void Start()
        //{
        //    _player = GameObject.FindGameObjectWithTag("Player");
        //    if(_player == null)
        //    {
        //        Debug.LogError("_player est null");
        //    }
        //}

        // Update is called once per frame
        void Update()
        {
            //if (_player == null)
            //{
            //    _player = GameObject.FindGameObjectWithTag("Player");
            //}
            ReloadTimeButton();
            //GetJoystickInput();

        }

        /// <summary>
        /// Cette fonction permet d'effectuer l'animation de reload sur le boutton en influençant la variable fillAmount qui est comprise entre 0 et 1
        /// On e=fait donc un Lerp avec le temps de reload du canon sur cette variable
        /// </summary>
        public void ReloadTimeButton()
        {
            if (canReloadUp)
            {
                currentReloadTimeUp += Time.deltaTime / reloadingTime;
                _buttonShootUp.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, currentReloadTimeUp);
                if (_buttonShootUp.GetComponent<Image>().fillAmount == 1)
                {
                    currentReloadTimeUp = 0;
                    canReloadUp = false;
                }
            }

            if (canReloadLeft)
            {
                currentReloadTimeLeft += Time.deltaTime / reloadingTime;
                _buttonShootLeft.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, currentReloadTimeLeft);
                if (_buttonShootLeft.GetComponent<Image>().fillAmount == 1)
                {
                    currentReloadTimeLeft = 0;
                    canReloadLeft = false;
                }
            }

            if (canReloadRight)
            {
                currentReloadTimeRight += Time.deltaTime / reloadingTime;
                _buttonShootRight.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, currentReloadTimeRight);
                if (_buttonShootRight.GetComponent<Image>().fillAmount == 1)
                {
                    currentReloadTimeRight = 0;
                    canReloadRight = false;
                }
            }
        }


        //Guillaume 30-12-2019 11h26 --> mise a jour de la fonction GetJoystickInput pour qu'elle corresponde a celle du projet Local
        /// <summary>
        /// Get The Joystick Input
        /// </summary>
        public Vector2 GetJoystickInput()
        {
            return _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition();
        }

        /// <summary>
        /// Fonction tir vers Tribord
        /// </summary>
        public void ShootRight()
        {
            if (!canReloadRight)
            {
                _boatCharacter.ShootStarboard();
                reloadingTime = _boatCharacter.getShootCoolDown();
                _buttonShootRight.GetComponent<Image>().fillAmount = 0;
                canReloadRight = true;
            }
        }

        /// <summary>
        /// Fonction Tir Babord
        /// </summary>
        public void ShootLeft()
        {
            if (!canReloadLeft)
            {
                _boatCharacter.ShootLarboard();
                reloadingTime = _boatCharacter.getShootCoolDown();
                _buttonShootLeft.GetComponent<Image>().fillAmount = 0;
                canReloadLeft = true;
            }

        }

        ///// <summary>
        ///// Fonction Tir Proue
        ///// </summary>
        public void ShootUp()
        {
            if (!canReloadUp)
            {
                _boatCharacter.ShootProwHarpoon();
                 reloadingTime = _boatCharacter.getShootCoolDown();
                _buttonShootUp.GetComponent<Image>().fillAmount = 0;
                canReloadUp = true;
            }
        }


        ///Ajout seb
        public void SetBoatCharacterReference(BoatCharacter currentBoat)
        {
            _boatCharacter = currentBoat;
        }
    }
}