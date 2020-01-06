using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Boat
{
    //move the Require component floating object to the BoatMovement Script
    [RequireComponent(typeof(BoatMovement))]
    public class BoatController : MonoBehaviour
    {
        //Move the Movements variables to the BoatMovement Script

        //the Movement Script
        private BoatMovement _boatMovement;
        [SerializeField] private Transform _cameraPivot;
        [SerializeField] private Transform _cameraPosition;
        //[SerializeField] private bool _cameraAffectedBySpeed = false;

        private float _verticalInput;
        private float _horizontalInput;
        private float _zoom = 1;
        [SerializeField] float _minZoom = 0.5f;
        [SerializeField] float _maxZoom = 2f;
        private float _zoomIntensity = 1;


        private void Start()
        {
            _boatMovement = this.GetComponent<BoatMovement>();
        }

        private void Update()
        {
            //get Inputs
            //_verticalInput = Input.GetAxis("Vertical");
            //_horizontalInput = Input.GetAxis("Horizontal");

            //_boatMovement.Movement(_verticalInput);
            //_boatMovement.Steer(_horizontalInput);
            //_boatMovement.PerformMovement(_verticalInput);
            //_boatMovement.PerformRotation(_horizontalInput);

            //this.PerformMovement(_verticalInput);
            //this.PerformRotation(_horizontalInput);

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
            //if (_cameraAffectedBySpeed)
            //{
            //    Camera.main.transform.position = _cameraPosition.position - _boatMovement.transform.forward * (_boatMovement.getSpeedForward() / _boatMovement.getMaxSpeedForward()) * 10 * _zoom;
            //}
        }

        /// <summary>
        /// For the controllers call this function in the playerController class
        /// use to perform the forward movement of the boat
        /// </summary>
        public void PerformMovement(float pVerticalInput)
        {
            _boatMovement.PerformMovement(pVerticalInput);

        }

        /// <summary>
        /// For the controllers call this function in the playerController class
        /// use to perform the rotation movement of the boat
        /// </summary>
        public void PerformRotation(float pHorizontalInput)
        {
            _boatMovement.PerformRotation(pHorizontalInput);
        }

        public void PerformMovementDirection(float pVerticalInput, float pHorizontalInput)
        {
            //test PerformMovementDirection
            _boatMovement.PerformMovementDirection(pVerticalInput, pHorizontalInput);
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

        public BoatMovement getBoatMovement()
        {
            return _boatMovement;
        }

    }
}
