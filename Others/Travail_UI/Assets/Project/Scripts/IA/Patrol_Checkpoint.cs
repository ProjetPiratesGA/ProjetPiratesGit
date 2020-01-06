using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Patrol_Checkpoint : IA_Patrol
    {
        [Header("Patrol Checkpoint")]
        [SerializeField] private List<Transform> _checkpoints;
        [SerializeField] private float _standardWaitingTime;
        [SerializeField] private float _finalWaitingTime;
        [SerializeField] private bool _isCycled = false;
        
        private float _currentWaitingTime;
        private bool _isWaiting;
        private bool _isWaitingFinal;
        private bool _isReversed;
        private int _nextCheckpointId = 0;

        // Use this for initialization
        void Start()
        {
            //Check if a IA_Character class is associated to that Script and what type it is.
            base.SetUpCharacter();
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override Vector3 Patrol()
        {
            // Wait a bit at each checkpoint
            if (_isWaiting)
            {
                _currentWaitingTime += Time.deltaTime;

                // If final checkpoint
                if (_isWaitingFinal)
                {
                    if (_currentWaitingTime >= _finalWaitingTime)
                    {
                        _isWaiting = _isWaitingFinal = false;
                        _currentWaitingTime = 0;
                    }
                }
                // If standard checkpoint
                else
                {
                    if (_currentWaitingTime >= _standardWaitingTime)
                    {
                        _isWaiting = _isWaitingFinal = false;
                        _currentWaitingTime = 0;
                    }
                }
                return this.transform.position;
            }
            else
            {
                // If the entity reached the checkpoint
                if (this.transform.position == _checkpoints[_nextCheckpointId].position)
                {
                    if (_isReversed)
                    {
                        _nextCheckpointId--;
                    }
                    else
                    {
                        _nextCheckpointId++;
                    }

                    // Check if the last checkpoint has been reached
                    if(_nextCheckpointId == _checkpoints.Count | (_isReversed & _nextCheckpointId == -1))
                    {
                        if (_isCycled)
                        {
                            //Go back to first checkpoint if cycled pattern
                            _nextCheckpointId = 0;
                        }
                        else
                        {
                            //Reverse checkpoints if not a cycled pattern
                            _isReversed = !_isReversed;
                            if (_isReversed)
                            {
                                _nextCheckpointId = _checkpoints.Count - 2;
                            }
                            else
                            {
                                _nextCheckpointId = 1;
                            }
                        }
                        _isWaitingFinal = true;
                    }
                    _isWaiting = true;
                }
                return _checkpoints[_nextCheckpointId].position;
            }
            
        }
    }
}