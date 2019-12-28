using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Boat
{


    public class AnimationBoat : MonoBehaviour
    {
        /// <summary>
        /// envoyer le game Object ou est le script boatMovement
        /// </summary>
        private BoatMovement _boatMovement;

        private Transform _transform;

        private float _angleInclineAccelerate = 5f;
        private float _speedInclineAccelerate = 0.1f;
        private Vector3 _currentAngle;
        // Use this for initialization
        void Start()
        {
            _transform = this.GetComponent<Transform>();

            _boatMovement = this.GetComponent<BoatMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            //_currentAngle = this.transform.rotation.eulerAngles;

            ////test
            //if (Input.GetKey(KeyCode.F))
            //{
            //}

            //if(_boatMovement != null)
            //{
            //    Debug.Log("AnimationBoat --> Update / _boatMovement.getBoatMovementState() : " + _boatMovement.getBoatMovementState());

            //    //InclineAcceleration
            //    if(_boatMovement.getBoatMovementState() == BoatMovementState.ACCELERATE)
            //    {
            //        if(_currentAngle.x > -_angleInclineAccelerate)
            //        {
            //            _currentAngle.x -= _speedInclineAccelerate;
            //            this.transform.rotation = Quaternion.Euler(_currentAngle);
            //        }
            //    }

            //    if (_boatMovement.getBoatMovementState() == BoatMovementState.IDLE)
            //    {
            //        if (_currentAngle.x < 0)
            //        {
            //            _currentAngle.x += _speedInclineAccelerate;
            //            this.transform.rotation = Quaternion.Euler(_currentAngle);
            //        }
            //    }

            //    Debug.Log("AnimationBoat --> Update / _currentAngle.x : " + _currentAngle.x);

            //}
            //else
            //{
            //    Debug.LogError("AnimationBoat --> Update : _boatMovement est null");
            //}
        }

    }
}
