using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Controllers;

namespace ProjetPirate.Boat
{
    //move the Require component floating object to the BoatMovement Script
    public class BoatController : MonoBehaviour
    {
        //Move the Movements variables to the BoatMovement Script

        //ATTNETION CA VA CHANGER
        private PlayerController _joystick; // le joystick posséde le scipt "joystick" qui est actuellement "playerController"
        public PlayerController joystick
        {
            get { return _joystick; }
        }

        //the Movement Script
        private BoatCharacter _boatCharacter;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Transform _cameraPosition;
        [SerializeField] private bool _cameraAffectedBySpeed = true;


        //Pour le moment je récupère les inputs du player controller afin de set la variable _isMoving du boatCharacter
        private float _verticalInput;
        private float _horizontalInput;

        [SerializeField] private float _zoom = 2;
        [SerializeField] float _minZoom = 1f;
        [SerializeField] float _maxZoom = 4f;
        private float _zoomIntensity = 1;


        private void Start()
        {
            _joystick = FindObjectOfType<PlayerController>();
            if (_joystick == null)
            {
                Debug.LogError("_joystick est null");
            }
            else
            {
                Debug.Log("_joystick est bien instancier");
            }

            _boatCharacter = this.GetComponent<BoatCharacter>();
            _cameraPivot = CameraPivot.Instance.transform;
            _cameraPosition = CameraPosition.Instance.transform;
            Vector3 pos = _cameraPosition.localPosition;
            pos *= _zoom;
            Camera.main.transform.localPosition = pos;
        }

        private void Update()
        {
            Zoom(0);
            //isMoving

            this.PerformMovementDirection();

            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                _boatCharacter.setControllerIsMoving(true);
            }
            else
            {
                _boatCharacter.setControllerIsMoving(false);
            }

            //if (Input.GetKeyDown(KeyCode.Space))
            //    this.GetComponent<BoatCharacter>().ShootLarboard();
            //if (Input.GetKeyDown(KeyCode.KeypadEnter))
            //    this.GetComponent<BoatCharacter>().ShootStarboard();

            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                Zoom(0.01f);
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Zoom(-0.01f);
            }

            _cameraPivot.position = this.transform.position;
            //if (_cameraAffectedBySpeed)
            //{
            //    Vector3 pos = _cameraPosition.localPosition;
            //    pos *= _zoom;
            //    Camera.main.transform.localPosition = pos - _boatCharacter.transform.forward * (_boatCharacter.getSpeedForward() / _boatCharacter.getMaxSpeedForward() * 10);
            //}
        }

        public void PerformMovementDirection()
        {
            //TEST AVEC ZQSD
            //_verticalInput = Input.GetAxis("Vertical");
            //_horizontalInput = Input.GetAxis("Horizontal");
            //Debug.Log("AXIS VERTICAL : " + Input.GetAxis("Vertical") + " AXIS HORIZONTAL : " + Input.GetAxis("Horizontal"));
            //_boatCharacter.PerformMovement(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            //AVEC LES INPUTS DES CONTROLLEURS
            _horizontalInput = _joystick.GetJoystickInput().x;
            _verticalInput = _joystick.GetJoystickInput().y;
            //Debug.Log("_verticalInput : " + Input.GetAxis("Vertical") + " _horizontalInput : " + Input.GetAxis("Horizontal"));
            _boatCharacter.PerformMovement(_verticalInput, _horizontalInput);

        }

        /// <summary>
        /// For the controllers call this function in the playerController class
        /// use to perform the forward movement of the boat
        /// </summary>
        //public void PerformMovement(float pVerticalInput)
        //{
        //    _boatMovement.PerformMovement(pVerticalInput);

        //}

        /// <summary>
        /// For the controllers call this function in the playerController class
        /// use to perform the rotation movement of the boat
        /// </summary>
        //public void PerformRotation(float pHorizontalInput)
        //{
        //    _boatMovement.PerformRotation(pHorizontalInput);
        //}

        //public void PerformMovementDirection(float pVerticalInput, float pHorizontalInput)
        //{
        //    _verticalInput = pVerticalInput;
        //    _horizontalInput = pHorizontalInput;
        //    //Debug.Log("PETIT TEST : --> _verticalInput : " + _verticalInput + " _horizontalInput : " + _horizontalInput);
            
        //    _boatCharacter.PerformMovement(pVerticalInput, pHorizontalInput);

        //}

        public void Zoom(float pZoom)
        {
            _zoom += pZoom;
            if (_zoom < _minZoom)
            {
                _zoom = _minZoom;
            }
            if (_zoom > _maxZoom)
            {
                _zoom = _maxZoom;
            }
            Vector3 pos = _cameraPosition.localPosition;
            pos *= _zoom;
            Camera.main.transform.localPosition = pos;
        }

        public BoatCharacter GetBoatCharacter()
        {
            return _boatCharacter;
        }

    }
}
