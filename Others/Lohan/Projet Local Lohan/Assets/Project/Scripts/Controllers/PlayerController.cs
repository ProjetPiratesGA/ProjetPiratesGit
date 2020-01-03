﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Boat;

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

        //[SerializeField] // pour que le test fonctionne je prend les infos 
        private GameObject _player;//On récupère le player afin que le controller possede ses info 

        //Variable  pour gérer le reload notement l'animation des bouttons
        private bool canReloadUp = false;
        public bool _canReloadUp
        {
            get { return canReloadUp; }
        }
        private bool canReloadLeft = false;
        public bool _canReloadLeft
        {
            get { return canReloadLeft; }
        }
        private bool canReloadRight = false;
        public bool _canReloadRight
        {
            get { return canReloadRight; }
        }


        //Temps de reload pour chaque canon (au cas ou il diffère selon le canon)
        float currentReloadTimeUp = 0;
        float currentReloadTimeLeft = 0;
        float currentReloadTimeRight = 0;

        //Il faudra ajuster cette variable en fonction du reload des canons
        float reloadingTime;

        // Use this for initialization
        void Start()
        {
            _player = Player.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = Player.Instance;
            }

            ReloadTimeButton();
            GetJoystickInput();
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

        /// <summary>
        /// Check the joystick input and send it to the player
        /// </summary>
        public Vector2 GetJoystickInput()
        {
            return _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition();

            //if (_player != null)
            //{

            //    _player.GetComponentInChildren<BoatController>().PerformMovementDirection(
            //    _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().y,
            //    _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().x);
            //}
            //else
            //{
            //    Debug.LogWarning("PlayerController --> '_player' est null");
            //}
        }

        /// <summary>
        /// Fonction tir vers Tribord
        /// </summary>
        public void ShootRight()
        {
            //Debug.Log("ShootRight / canReloadRight : " + canReloadRight);
            if (!_player.GetComponentInChildren<BoatCharacter>().Safe)
            {
                if (!canReloadRight)
                {
                    _player.GetComponentInChildren<BoatCharacter>().ShootStarboard();
                    reloadingTime = _player.GetComponentInChildren<BoatCharacter>().getShootCoolDown();
                    _buttonShootRight.GetComponent<Image>().fillAmount = 0;
                    canReloadRight = true;
                }
            }

        }

        /// <summary>
        /// Fonction Tir Babord
        /// </summary>
        public void ShootLeft()
        {
            //Debug.Log("ShootLeft / canReloadLeft : " + canReloadLeft);
            if (!_player.GetComponentInChildren<BoatCharacter>().Safe)
            {
                if (!canReloadLeft)
                {
                    _player.GetComponentInChildren<BoatCharacter>().ShootLarboard();
                    reloadingTime = _player.GetComponentInChildren<BoatCharacter>().getShootCoolDown();
                    _buttonShootLeft.GetComponent<Image>().fillAmount = 0;
                    canReloadLeft = true;
                }
            }

        }

        /// <summary>
        /// Fonction Tir Proue
        /// </summary>
        public void ShootUp()
        {
            if (!_player.GetComponentInChildren<BoatCharacter>().Safe)
            {
                if (!canReloadUp)
                {
                    _player.GetComponentInChildren<BoatCharacter>().ShootProwHarpoon();
                    reloadingTime = _player.GetComponentInChildren<BoatCharacter>().getShootCoolDown();
                    _buttonShootUp.GetComponent<Image>().fillAmount = 0;
                    canReloadUp = true;
                }
            }
        }
    }
}