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
        [Header("SEA VARIABLES")]
        [SerializeField]
        private Transform _seaTransform;
        [SerializeField]
        private float _seaRadius = 2600f;
        [SerializeField]
        private float _attractForce = 10f;
        [SerializeField]
        private float _seaZoneLimit = 100f;

        [Header("FALL VARIABLES")]
        [SerializeField]
        private float _fallingForceFactor = 0.1f;
        [SerializeField]
        private float _fallingTorqueFactor = 0.01f;

        private Rigidbody _rigidbody;
        [Header("FOR THE TEST")]
        [SerializeField]
        private float _distToTheEdge;
        [SerializeField]
        private float _distToCenter;
        [SerializeField]
        private Vector3 _directionToCenterSea;
        [SerializeField]
        private Vector3 _directionToApplyForce;
        [SerializeField]
        private Vector3 _velocityForce;
        [SerializeField]
        private Vector3 _vectorForceFalling;
        [SerializeField]
        public bool _isFalling = false;



        // Use this for initialization
        void Start()
        {
            if (Sea.Instance != null)
            {
                _seaTransform = Sea.Instance.transform;

            }
            _rigidbody = this.GetComponent<Rigidbody>();

            if (_seaTransform == null)
            {
                Debug.LogError("AttractObject --> Start / _SeaTransform est null");
            }

        }

        private void Update()
        {
            if (_seaTransform == null)
            {
                _seaTransform = Sea.Instance.transform;

            }
        }

        private void FixedUpdate()
        {
            this.AttractMethod();

            if (_isFalling == true)
            {
                _rigidbody.constraints = RigidbodyConstraints.None;

                //add a falling force to the boat


                ///TEST WITH "_velocityForce"

                if (_velocityForce != null)
                {
                    _vectorForceFalling = new Vector3(_velocityForce.x, _velocityForce.y, _velocityForce.z) * _fallingForceFactor;
                    _rigidbody.AddForce(_vectorForceFalling, ForceMode.VelocityChange);

                    _vectorForceFalling = new Vector3(0, -_velocityForce.y, -_velocityForce.z) * _fallingTorqueFactor;
                    _rigidbody.AddTorque(_vectorForceFalling, ForceMode.VelocityChange);
                }
            }
            else
            {
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

        }

        private void AttractMethod()
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
            }
            else
            {
                _isFalling = false;
            }
        }
    }
}