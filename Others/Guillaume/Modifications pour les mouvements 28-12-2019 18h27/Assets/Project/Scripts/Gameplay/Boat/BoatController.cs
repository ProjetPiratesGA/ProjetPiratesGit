using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region DEBUG
using ProjetPirate.Controllers;
#endregion DEBUG


namespace ProjetPirate.Boat
{
    //move the Require component floating object to the BoatMovement Script
    public class BoatController : MonoBehaviour
    {
        //Scripts
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
        //[SerializeField] private bool _cameraAffectedBySpeed = false;

        //Pour le moment je récupère les inputs du player controller afin de set la variable _isMoving du boatCharacter
        private float _verticalInput;
        private float _horizontalInput;

        private float _zoom = 1;
        [SerializeField] float _minZoom = 0.5f;
        [SerializeField] float _maxZoom = 2f;
        private float _zoomIntensity = 1;

        #region DEBUG


        #endregion DEBUG

        private void Start()
        {
            Debug.Log("BoatController : Start");

            //je récup le joystick
            _joystick = FindObjectOfType<PlayerController>();
            if(_joystick == null)
            {
                Debug.LogError("_joystick est null");
            }
            else
            {
                Debug.Log("_joystick est bien instancier");
            }

            _boatCharacter = this.GetComponent<BoatCharacter>();

            //_cameraPivot = CameraPivot.Instance.transform;
            //_cameraPosition = CameraPosition.Instance.transform;
        }

        private void Update()
        {
            //TEMPORAIRE : pour le test je chope les inputs clavier
            //float verticalInput = Input.GetAxis("Vertical");
            //float horizontalInput = Input.GetAxis("Horizontal");
            //PerformMovementDirection(verticalInput, horizontalInput);

            //this.PerformMovementDirection(
            //_joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().y,
            //_joystick.GetComponent<JoystickController>().GetNormalizeJoystickPosition().x);

            this.PerformMovementDirection();

            //isMoving
            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                _boatCharacter.setControllerIsMoving(true);
            }
            else
            {
                _boatCharacter.setControllerIsMoving(false);
            }


            if (Input.GetKeyDown(KeyCode.Space))
                this.GetComponent<BoatCharacter>().ShootLarboard();
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                this.GetComponent<BoatCharacter>().ShootStarboard();

            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                Zoom(0.01f);
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Zoom(-0.01f);
            }

            _cameraPivot.position = this.transform.position;

            //SHOOTS
            if(_joystick._canReloadLeft)
            {
                _boatCharacter.ShootLarboard();
            }
            if (_joystick._canReloadRight)
            {
                _boatCharacter.ShootStarboard();
            }
            //if (_joystick._canReloadUp)
            //{
            //    //Shoot Up a mettre quand il y aura la fonction
            //    _boatCharacter.Shoot;
            //}

            //CAMERA
            //if (_cameraAffectedBySpeed)
            //{
            //    Camera.main.transform.position = _cameraPosition.position - _boatMovement.transform.forward * (_boatMovement.getSpeedForward() / _boatMovement.getMaxSpeedForward()) * 10 * _zoom;
            //}
        }

        public void PerformMovementDirection()
        {

            _horizontalInput = _joystick.GetJoystickInput().x;
            _verticalInput = _joystick.GetJoystickInput().y;

            _boatCharacter.PerformMovement(_verticalInput, _horizontalInput);

        }

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
