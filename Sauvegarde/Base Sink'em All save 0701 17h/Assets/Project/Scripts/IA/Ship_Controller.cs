using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Boat;
using ProjetPirate.Data;
namespace ProjetPirate.IA
{

    [RequireComponent(typeof(BoatCharacter))]
    public class Ship_Controller : IA_Controller
    {
        [Header("Ship Controller")]
        [SerializeField] private float _maxRange = 15;
        [SerializeField] private float _minRange = 10;


        private Data_BoatEnemy data_boatenemy = new Data_BoatEnemy();
        public BoatCharacter Character
        {
            get { return (BoatCharacter)_character; }
        }
        // Use this for initialization
        void Start()
        {
            _character = this.gameObject.GetComponent<BoatCharacter>();
            Character.SetUpBoat(this);
        }

        // Update is called once per frame
        void Update()
        {
            CheckIfTargetIsSafe();
            CheckResetHealth();
            if (!_isOnAlert)
            {
                if (this.GetComponent<Patrol_Static>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Static>().Patrol(), true);
                }
                if (this.GetComponent<Patrol_Wander>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Wander>().Patrol(), this.GetComponent<Patrol_Wander>().WillWait);
                }
                if (this.GetComponent<Patrol_Checkpoint>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Checkpoint>().Patrol(), this.GetComponent<Patrol_Checkpoint>().WillWait);
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
                        case Behaviour.Careful:
                        case Behaviour.Fearful:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        case Behaviour.Peaceful:
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
                        case Behaviour.Careful:
                        case Behaviour.Fearful:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        case Behaviour.Peaceful:
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (_behaviour)
                    {
                        case Behaviour.Aggressive:
                            GoToDestination(_target.transform.position, false, 90);
                            _character.GetComponent<BoatCharacter>().ShootLarboard();
                            break;
                        case Behaviour.Careful:
                        case Behaviour.Fearful:
                            _character.DirectionLocator.LookAt(_target.transform);
                            GoToDestination(this.transform.position - _character.DirectionLocator.forward);
                            break;
                        case Behaviour.Peaceful:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void GoToDestination(Vector3 _destination, bool pMustStopToDestination = false, float _turningAngle = 0)
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
            if (Vector3.Distance(this.transform.position, _destination) > _character.GetComponent<BoatCharacter>().StoppingDistance)
            {
                //If the destination is too far, move forward
                _character.GetComponent<BoatCharacter>().setControllerIsMoving(true);
                _character.MoveForward();
            }
            else if (Vector3.Distance(this.transform.position, _destination) < _character.GetComponent<BoatCharacter>().StoppingDistance)
            {
                if (pMustStopToDestination)
                {
                    if (Vector3.Distance(this.transform.position, _destination) > _character.GetComponent<BoatCharacter>().Deceleration * Time.deltaTime)
                    {
                        _character.GetComponent<BoatCharacter>().setControllerIsMoving(false);
                        _character.MoveForward();
                    }
                    else
                    {
                        this.transform.position = _destination;
                    }
                }
                else
                {
                    if (Vector3.Distance(this.transform.position, _destination) > _character.GetComponent<BoatCharacter>().getMaxSpeedForward() * Time.deltaTime)
                    {
                        _character.GetComponent<BoatCharacter>().setControllerIsMoving(true);
                        _character.MoveForward();
                    }
                    else
                    {
                        this.transform.position = _destination;
                    }
                }
            }
            else
            {
                //If the destination is close enough, the character take the destination's position
            }

            //Check in which direction is the destination
            if (Mathf.Abs(_character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y) <= _character.GetComponent<BoatCharacter>().getRotateSpeed() * Time.deltaTime)
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

        public override void Death()
        {
            //PlankOnSea.CmdSpawnPlankOnServer(_character.GetComponent<BoatCharacter>()._droppedPlank, this.transform.position, _character.GetComponent<BoatCharacter>()._plankDroppedByDeath);
            //Chest.SpawnChest(_character.GetComponent<BoatCharacter>().DroppedChest, this.transform.position, _character.GetComponent<BoatCharacter>()._moneyDroppedByDeath, 0);

        }

        public override void Disappear()
        {
            Destroy(this.gameObject);
        }
    }
}