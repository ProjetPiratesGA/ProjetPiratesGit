using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Boat;

namespace ProjetPirate.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [Header("MOVMENT")]
        public GameObject _joystick;

        [Header("SHOOT")]
        public GameObject _buttonShootUp;
        public GameObject _buttonShootLeft;
        public GameObject _buttonShootRight;
        public GameObject _buttonShootRepa;


        private GameObject _player;//On récupère le player afin que le controller possede ses info 

        //Variable  pour gérer le reload notement l'animation des bouttons
        bool canReloadUp = false;
        bool canReloadLeft = false;
        bool canReloadRight = false;
        bool canReloadRepa = false;

        //Temps de reload pour chaque canon (au cas ou il diffère selon le canon)
        float currentReloadTimeUp = 0;
        float currentReloadTimeLeft = 0;
        float currentReloadTimeRight = 0;
        float currentReloadTimeRepa = 0;

        //Il faudra ajuster cette variable en fonction du reload des canons
        float reloadingTime = 2;
        float reloadingTimeRepa = 5;

        // Use this for initialization
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
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
            if (canReloadRepa)
            {
                currentReloadTimeRepa += Time.deltaTime / reloadingTimeRepa;
                _buttonShootRepa.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, currentReloadTimeRepa);
                if (_buttonShootRepa.GetComponent<Image>().fillAmount == 1)
                {
                    currentReloadTimeRepa = 0;
                    canReloadRepa = false;
                }
            }
        }

        /// <summary>
        /// Check the joystick input and send it to the player
        /// </summary>
        void GetJoystickInput()
        {
            //_player.GetComponent<BoatController>().getBoatMovement().PerformMovement(_joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().y);
            //_player.GetComponent<BoatController>().getBoatMovement().PerformRotation(_joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().x);
            _player.GetComponent<BoatController>().getBoatMovement().PerformMovementDirection(
            _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().y,
            _joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().x);
        }

        /// <summary>
        /// Fonction tir vers Tribord
        /// </summary>
        public void ShootRight()
        {
            if (!canReloadRight)
            {
                //_player.GetComponent<BoatCharacter>().ShootStarboard();
                reloadingTime = _player.GetComponent<BoatCharacter>().getShootCoolDown();
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
                //_player.GetComponent<BoatCharacter>().ShootLarboard();
                reloadingTime = _player.GetComponent<BoatCharacter>().getShootCoolDown();
                _buttonShootLeft.GetComponent<Image>().fillAmount = 0;
                canReloadLeft = true;
            }

        }

        /// <summary>
        /// Fonction Tir Proue
        /// </summary>
        public void ShootUp()
        {
            if (!canReloadUp)
            {
                reloadingTime = _player.GetComponent<BoatCharacter>().getShootCoolDown();
                _buttonShootUp.GetComponent<Image>().fillAmount = 0;
                canReloadUp = true;
            }
        }

        public void Repa()
        {
            if (!canReloadRepa)
            {
               // reloadingTimeRepa = _player.GetComponent<BoatCharacter>().getShootCoolDown();

                _buttonShootRepa.GetComponent<Image>().fillAmount = 0;
                canReloadRepa = true;
            }
        }


    }
}