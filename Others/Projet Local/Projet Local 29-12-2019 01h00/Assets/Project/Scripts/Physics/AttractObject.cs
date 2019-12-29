using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Physic
{

    /// <summary>
    /// object who's attract by the edges of the map
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class AttractObject : MonoBehaviour
    {

        [Header("FALL VARIABLES")]
        [SerializeField]
        private float _fallingForceFactor = 0.1f;
        [SerializeField]
        private float _fallingTorqueFactor = 0.01f;

        private Rigidbody _rigidbody;
        [Header("FOR THE TEST")]
        [SerializeField]
        public float _distToTheEdge;
        [SerializeField]
        public float _distToCenter;
        [SerializeField]
        public Vector3 _directionToCenterSea;
        [SerializeField]
        public Vector3 _directionToApplyForce;
        [SerializeField]
        public Vector3 _velocityForce;
        [SerializeField]
        public Vector3 _vectorForceFalling;
        [SerializeField]
        public bool _isFalling = false;

        public float FallingForceFactor
        {
            get
            {
                return _fallingForceFactor;
            }

        }

        public float FallingTorqueFactor
        {
            get
            {
                return _fallingTorqueFactor;
            }

        }



        // Use this for initialization
        void Start()
        {
            Sea._attractObjects.Add(this);
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {

            //if (_isFalling == true)
            //{
            //    _rigidbody.constraints = RigidbodyConstraints.None;

            //    //add a falling force to the boat


            //    ///TEST WITH "_velocityForce"

            //    if (_velocityForce != null)
            //    {
            //        _vectorForceFalling = new Vector3(_velocityForce.x, _velocityForce.y, _velocityForce.z) * FallingForceFactor;
            //        _rigidbody.AddForce(_vectorForceFalling, ForceMode.VelocityChange);

            //        _vectorForceFalling = new Vector3(0, -_velocityForce.y, -_velocityForce.z) * FallingTorqueFactor;
            //        _rigidbody.AddTorque(_vectorForceFalling, ForceMode.VelocityChange);
            //    }
            //}
            //else
            //{
            //    _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            //}

        }

        public bool AttractMethod(Transform _seaTransform, float _seaRadius, float _attractForce, float _seaZoneLimit)
        {
            //calcul the dist between the boat & the center of the sea
            _directionToCenterSea = (_seaTransform.position - this.transform.position).normalized;
            _distToCenter = Vector3.Distance(_seaTransform.position, this.transform.position);
            _distToTheEdge = _seaRadius - _distToCenter;

            //if the object is near the edge the force is add to the boat

            //calcul direction force
            _directionToApplyForce = -_directionToCenterSea;
            _velocityForce = _directionToApplyForce * _attractForce * Time.fixedDeltaTime;
            _velocityForce *= (_distToCenter * 0.01f);

            if (_isFalling == false && _distToTheEdge < _seaZoneLimit)
            {
                //move the object
                this.transform.position += _velocityForce;
                //_rigidbody.MovePosition(this.transform.position + _velocityForce);
                Debug.DrawLine(this.transform.position, _seaTransform.position, Color.blue);
                Debug.DrawRay(this.transform.position, _velocityForce, Color.red);
            }

            //if the object pass the edge set the falling variable
            if (_distToTheEdge < 10)
            {
                _isFalling = true;
                return true;
            }
            else
            {
                _isFalling = false;
                return false;
            }
        }
    }
}