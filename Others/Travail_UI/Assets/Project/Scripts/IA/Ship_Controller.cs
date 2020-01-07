using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{

    [RequireComponent(typeof(Ship_Character))]
    public class Ship_Controller : IA_Controller
    {
        [Header("Ship Controller")]
        [SerializeField] private float _maxRange = 15;
        [SerializeField] private float _minRange = 10;

        // Use this for initialization
        void Start()
        {
            _character = this.gameObject.GetComponent<Ship_Character>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isOnAlert)
            {
                if (this.GetComponent<Patrol_Static>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Static>().Patrol());
                }
                if (this.GetComponent<Patrol_Wander>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Wander>().Patrol());
                }
                if (this.GetComponent<Patrol_Checkpoint>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Checkpoint>().Patrol());
                }
            }
            else
            {
                if (Vector3.Distance(this.transform.position, _target.transform.position) > _maxRange)
                {
                    switch (_behaviour)
                    {
                        case Behaviour.Aggressive:
                            GoToDestination(_target.transform.position);
                            break;
                        case Behaviour.Punisher:
                            GoToDestination(_target.transform.position);
                            break;
                        case Behaviour.Careful:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        default:
                            break;
                    }
                }
                else if (Vector3.Distance(this.transform.position, _target.transform.position) < _minRange)
                {
                    switch (_behaviour)
                    {
                        case Behaviour.Aggressive:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        case Behaviour.Punisher:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        case Behaviour.Careful:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    GoToDestination(_target.transform.position, 90);
                    _character.GetComponent<Ship_Character>().ShootLarboard();
                }
            }
        }

        public void GoToDestination(Vector3 _destination, float _turningAngle = 0)
        {
            // Make the DirectionLocator faces the destination
            _character.DirectionLocator.LookAt(_destination);
            _character.DirectionLocator.Rotate(0, _turningAngle, 0);
            Vector3 rotation = _character.DirectionLocator.eulerAngles;
            rotation.x = Mathf.Abs(rotation.x);
            rotation.y = Mathf.Abs(rotation.y);
            rotation.z = Mathf.Abs(rotation.z);
            _character.DirectionLocator.eulerAngles = rotation;

            //Check the distance to the destination
            if (Vector3.Distance(this.transform.position, _destination) > _character.MovingSpeed * Time.deltaTime)
            {
                //If the destination is too far, move forward
                _character.MoveForward();
            }
            else
            {
                //If the destination is close enough, the character take the destination's position
                this.transform.position = _destination;
            }

            //Check in which direction is the destination
            if (Mathf.Abs(_character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y) <= _character.AngularSpeed * Time.deltaTime)
            {
                //If the direction is close enough, take the exact rotation to face the destination
                this.transform.eulerAngles = _character.DirectionLocator.eulerAngles;
            }
            else if ((_character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y < 0 & _character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y > -180) | _character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y > 180)
            {
                //If the destination si to the left, turn left.
                _character.TurnLarboard();
            }
            else
            {
                //If the destination si to the right, turn right.
                _character.TurnStarboard();
            }
        }

    }
}