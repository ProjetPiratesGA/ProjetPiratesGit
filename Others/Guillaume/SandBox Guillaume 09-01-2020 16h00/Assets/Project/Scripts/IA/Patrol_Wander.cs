using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Patrol_Wander : IA_Patrol
    {
        [Header("Wandering Area")]
        [SerializeField] private Transform _center;
        [SerializeField] private Transform _destination;
        [SerializeField] private float _radius;

        [Header("Patrol Wander")]
        [SerializeField] private float _waitingTime;
        [SerializeField] private float _forcedResetTime;
        private float _currentWaitingTime;
        private float _currentForcedResetTime;
        private bool _willWait;

        public bool WillWait
        {
            get { return _willWait; }
        }

        // Use this for initialization
        void Start()
        {
            //Check if a IA_Character class is associated to that Script and what type it is.
            base.SetUpCharacter();
            _destination.position = _center.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override Vector3 Patrol()
        {

            _currentForcedResetTime += Time.deltaTime;

            // Change destination if the ai reached it or if a timeout occured
            if (_destination.position == this.transform.position | _currentForcedResetTime > _forcedResetTime)
            {
                _currentWaitingTime += Time.deltaTime;

                // Wait before changing destination
                if (_currentWaitingTime >= _waitingTime | _currentForcedResetTime > _forcedResetTime)
                {
                    //Debug.Log("Reset_Destination");
                    // Draw a random position until it is far enough from the entity
                    while (Vector3.Distance(this.transform.position, _destination.position) < _radius / 4)
                    {
                        Vector3 vec = _destination.localPosition;
                        vec.z = Random.Range(0, _radius);
                        _destination.localPosition = vec;
                        vec = _center.eulerAngles;
                        vec.y = Random.Range(0, 360);
                    }
                    _currentWaitingTime = 0;
                    _currentForcedResetTime = 0;
                }

            }
            else
            {

                _willWait = (_waitingTime != 0);

            }
            return _destination.position;
        }

    }
}