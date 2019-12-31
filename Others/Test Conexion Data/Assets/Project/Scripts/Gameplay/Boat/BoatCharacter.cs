﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
using ProjetPirate.Physic;
using ProjetPirate.Data;
using UnityEngine.Networking;
using ProjetPirate.IA;

namespace ProjetPirate.Boat
{
    public enum ShipType
    {
        Military,
        Merchant,
        Player_Level_1,
        Player_Level_2,
        Player_Level_3,
    }

    public enum StructureState
    {
        Normal,
        Weakened,
        Endangered
    }

    public enum BoatMovementState
    {
        IDLE, // DON'T MOVE
        ACCELERATE,
        DECELERATE,
        CRUISE_SPEED // VITESSE DE CROISIERE
    }

    /// <summary>
    /// use for the future animations of the boat
    /// </summary>
    public enum BoatRotationState
    {
        FORWARD, //DON'T TURN
        BABORD, //TURN LEFT 
        TRIBORD //TURN RIGHT
    }

    ///// <summary>
    ///// Base class of the boat posess al the main data
    ///// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(FloatingObject))]
    [RequireComponent(typeof(AttractObject))]
    public class BoatCharacter : ProjetPirate.IA.Character
    {
        private Data_Boat _data_Boat = new Data_Boat();

        //Components
        private Rigidbody _rigidbody;
        private AttractObject _attractObject;

        //isMovingForward
        private bool _isMovingForward = false;
        private float _stoppingDistance;

        [Header("FALL DEATH")]
        [SerializeField]
        [Range(-100, -10)]
        private float _HeightFallingDeath = -10;

        
        [Header("BOAT STATES (DON'T TOUCH)")]
        [SerializeField]
        private BoatMovementState _boatMovementState;
        [SerializeField]
        private BoatRotationState _boatRotationState;

        #region PERFORM MOVEMENT DIRECTION

        Vector3 _normalizeTarget_MovementDirection;
        Vector3 _targetPosition_MovementDirection;
        Vector3 _direction_MovementDirection;
        Vector3 _currentVelocity_MovementDirection;

        [Header("FORWARD MOVEMENT ")]
        //[SerializeField]
        private float _minSpeedForward = 0f;
        [SerializeField]
        private float _accelerationSpeedForward = 1;
        [SerializeField]
        private float _decelerationSpeedForward = 1;


        [Header("ROTATION")]
        [SerializeField]
        private JoystickController _joystickController;
        //[SerializeField]
        private float _angleToDestination;

        #endregion PERFORM MOVEMENT DIRECTION

        private Vector3 _currentForwardDirection;
        private Vector3 _currentForwardVelocity; // _currentVelocity

        //private float _zInputMovement; // input vertical 
        //private float _xInputMovement; // input horizontal

        private bool _ControllerIsMoving = false; // set depuis le controller (boatController ou IAController)

        [Header("CANNONS")]
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;


        [SerializeField] private List<Transform> _larboardCannonPositions;
        [SerializeField] private List<Transform> _starboardCannonPositions;
        [SerializeField] private int _defaultCannonNumberBySide;

        [SerializeField] private GameObject _larboardCannonPrefab;
        [SerializeField] private GameObject _starboardCannonPrefab;


        public int DefaultCannonNumberBySide
        {
            get { return _defaultCannonNumberBySide; }
        }

        public List<Transform> LarboardCannonPositions
        {
            get { return _larboardCannonPositions; }
        }

        public List<Transform> StarboardCannonPositions
        {
            get { return _starboardCannonPositions; }
        }

        void Update()
        {

            _isMovingForward = false;

            //GoldFxAnimation();
            //verify death falling character
            //FallDeath();

            //if ((float)_currentLifePoint / (float)_maxLifePoint > _weakenedStateLifeRatio)
            //{
            //    _structureState = StructureState.Normal;
            //}
            //else if ((float)_currentLifePoint / (float)_maxLifePoint > _endangeredStateLifeRatio)
            //{
            //    _structureState = StructureState.Weakened;
            //}
            //else
            //{
            //    _structureState = StructureState.Endangered;
            //}
            //_currentLarboardShootCooldownTime += Time.deltaTime;
            //_currentStarboardShootCooldownTime += Time.deltaTime;
            //if (_currentLarboardShootCooldownTime > _shootCooldown)
            //{
            //    _larboardCannonInCooldown = false;
            //    _currentLarboardShootCooldownTime = 0;
            //}
            //if (_currentStarboardShootCooldownTime > _shootCooldown)
            //{
            //    _starboardCannonInCooldown = false;
            //    _currentStarboardShootCooldownTime = 0;
            //}

            //if (Input.GetKeyDown(KeyCode.Keypad0))
            //{
            //    Death();
            //}

            //if (_deathAnimationIsPlaying)
            //{
            //    DeathAnimation();
            //}
            //else if (_respawninfAnimationIsPlaying)
            //{
            //    RespawnAnimation();
            //}
            //else if (_fallAnimationIsPlaying)
            //{
            //    FallAnimation();
            //}
            //else
            //{

                //BOAT STATES
                Debug.Log("update boatCharacter");
                this.ManageBoatMovementState();
                //UPDATE SPEED
                this.UpdateSpeedForwardForDirection();

                //if (!_isDocking)
                //{
                //    if (!CheckLeftInvisibleWall())
                //    {
                //        CheckRightInvisibleWall();
                //    }
                //}
            //}

            //for (int i = 0; i < _waterTrails.Count; i++)
            //{
            //    ParticleSystem.MainModule main = _waterTrails[i].main;
            //    main.startLifetime = _currentMovingSpeed / _maxMovingSpeed;
            //}
            _data_Boat.ReverseReloadTransform(this.gameObject);
        }

        //public void SetUpBoat(Player _player)
        //{
        //    this.gameObject.transform.SetParent(_player.gameObject.transform);
        //    this.transform.localPosition = new Vector3(0, 0, 0);
        //    for (int i = 0; i < this.DefaultCannonNumberBySide; i++)
        //    {
        //        this.AddLarboardCannon();
        //        this.AddStarboardCannon();
        //    }
        //    _controller = _player;
        //}

        //public void SetUpBoat(Ship_Controller pController)
        //{
        //    _controller = pController;
        //}

        public void AddLarboardCannon(NetworkConnection conn)
        {
            if (_larboardCannons.Count < LarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_larboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(LarboardCannonPositions[_larboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _larboardCannons.Add(cannon);

                NetworkServer.SpawnWithClientAuthority(cannon.gameObject, conn);
                this.TargetSetLardboardCanon(conn, cannon.gameObject);
            }
        }

        //public void RemoveLarboardCannon()
        //{
        //    if (_larboardCannons.Count > 0)
        //    {
        //        Cannon cannon = _larboardCannons[_larboardCannons.Count - 1];
        //        _larboardCannons.RemoveAt(_larboardCannons.Count - 1);
        //        Destroy(cannon.gameObject);
        //    }
        //}

        public void AddStarboardCannon(NetworkConnection conn)
        {
            if (_starboardCannons.Count < StarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_starboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(StarboardCannonPositions[_starboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _starboardCannons.Add(cannon);

                NetworkServer.SpawnWithClientAuthority(cannon.gameObject, conn);
                this.TargetSetStarboardCanon(conn, cannon.gameObject);

            }
        }

        [Command]
        public void CmdSetUpBoat(GameObject player)
        {
            this.gameObject.transform.SetParent(player.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            _controller = player.GetComponent<Controller>();


            TargetSetParent(player.GetComponent<Player>().connectionToClient, player.gameObject);

            for (int i = 0; i < this.DefaultCannonNumberBySide; i++)
            {
                this.AddLarboardCannon(player.GetComponent<Player>().connectionToClient);
                this.AddStarboardCannon(player.GetComponent<Player>().connectionToClient);
            }
        }


        [TargetRpc]
        public void TargetSetParent(NetworkConnection target,GameObject player)
        {
            this.gameObject.transform.SetParent(player.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            _controller = player.GetComponent<Controller>();
        }

        [TargetRpc]
        public void TargetSetLardboardCanon(NetworkConnection target, GameObject cannon)
        {
            cannon.transform.SetParent(LarboardCannonPositions[_larboardCannons.Count]);
            cannon.transform.localPosition = Vector3.zero;
            cannon.GetComponent<Cannon>().SetOwner(this);
            _larboardCannons.Add(cannon.GetComponent<Cannon>());
        }

        [TargetRpc]
        public void TargetSetStarboardCanon(NetworkConnection target, GameObject cannon)
        {
            cannon.transform.SetParent(StarboardCannonPositions[_starboardCannons.Count]);
            cannon.transform.localPosition = Vector3.zero;
            cannon.GetComponent<Cannon>().SetOwner(this);
            _starboardCannons.Add(cannon.GetComponent<Cannon>());
        }

        #region MUTATORS

        /// <summary>
        /// must be call in controllers
        /// </summary>
        public void setControllerIsMoving(bool pControllerIsMoving)
        {
            _ControllerIsMoving = pControllerIsMoving;
        }

        #endregion MUTATORS

        private void ManageBoatMovementState()
        {
            #region MANAGE BOAT MOVEMENT STATE

            //use the input to define the state of the boat
            if (_currentMovingSpeed == 0)
            {
                _boatMovementState = BoatMovementState.IDLE;
            }
            else
            {
                //acceleration
                //if (_currentSpeedForward > 0 && (_zInputMovement != 0 || _xInputMovement != 0))
                if (_currentMovingSpeed > 0 && _ControllerIsMoving == true)
                {
                    _boatMovementState = BoatMovementState.ACCELERATE;
                }
                //deceleration
                //else if (_currentSpeedForward > 0 && (_zInputMovement == 0 && _xInputMovement == 0))
                else if (_currentMovingSpeed > 0 && _ControllerIsMoving == false)
                {
                    _boatMovementState = BoatMovementState.DECELERATE;
                }
                //cruise_speed
                if (_currentMovingSpeed >= _maxMovingSpeed)
                {
                    _boatMovementState = BoatMovementState.CRUISE_SPEED;
                }
            }




            #endregion MANAGE BOAT MOVEMENT STATE
        }

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * _currentMovingSpeed * Time.deltaTime;
            pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        public void Accelerate()
        {
            Debug.Log(this.name + "je suis dans le accelerate");
            _currentMovingSpeed += _accelerationSpeedForward * Time.deltaTime;
            if (_currentMovingSpeed > _maxMovingSpeed)
            {
                _currentMovingSpeed = _maxMovingSpeed;
            }
            Debug.Log(this.name + " --> Acclerate / _currentMovingSpeed : " + _currentMovingSpeed + " _accelerationSpeedForward : " + _accelerationSpeedForward
                + " Time.deltaTime : " + Time.deltaTime);
            _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        }

        public void Decelerate()
        {
            _currentMovingSpeed -= _decelerationSpeedForward * Time.deltaTime;
            if (_currentMovingSpeed < 0)
            {
                _currentMovingSpeed = 0;
            }
            _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        }

        /// <summary>
        /// a ranger pour apres
        /// </summary>
        private void UpdateSpeedForwardForDirection()
        {
            //acceleration
            //if (_zInputMovement != 0 || _xInputMovement != 0)
            Debug.Log(this.name + " --> UpdateSpeedForwardForDirection / _ControllerIsMoving" + _ControllerIsMoving);
            if (_ControllerIsMoving == true)
            {
                Debug.Log(this.name + "je suis passere");
                Accelerate();
            }
            //deceleration speed
            //else if (this.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() == null)
            if (_ControllerIsMoving == false)
            {
                Decelerate();
            }

            _data_Boat.dStats.Speed = _currentMovingSpeed;
            if (_data_Boat.dStats.Speed > 0)
            {
                _isMovingForward = true;
            }
            else
            {
                _isMovingForward = false;
            }
        }

        #region MOVEMENTS

        public void PerformMovement(float pInputVertical, float pInputHorizontal)
        {
            Debug.Log(this.name + " --> performMovement / _currentMovingSpeed : " + _currentMovingSpeed + " _currentAngularSpeed : " + _currentAngularSpeed + " this.transform.position : " + this.transform.position);
            _joystickController = FindObjectOfType<JoystickController>();
            if (_joystickController == null)
            {
                Debug.LogWarning("JoystickController Not Assigned");
            }
            else
            {
                //define direction with the inputs
                if (pInputHorizontal > _joystickController._joystickDeadZone || pInputHorizontal < -_joystickController._joystickDeadZone
                            || pInputVertical > _joystickController._joystickDeadZone || pInputVertical < -_joystickController._joystickDeadZone)
                {
                    _normalizeTarget_MovementDirection = new Vector3(pInputHorizontal, 0, pInputVertical);
                    _targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
                    _direction_MovementDirection = _targetPosition_MovementDirection - this.transform.position;
                }
            }

            //VELOCITY
            //_currentVelocity_MovementDirection = this.transform.forward * _currentSpeedForward * Time.deltaTime;
            //if(_attractObject != null)
            //{
            //    if (_attractObject._isFalling == false)
            //    {
                    MoveForward();
                    //this.transform.position += _currentVelocity_MovementDirection;
            //    }
            //}
            //else
            //{
            //    if(this.GetComponent<AttractObject>() == null)
            //    {
            //        Debug.LogError(this.name + " --> _attractObject est null");
            //    }
            //    else
            //    {
            //        _attractObject = this.GetComponent<AttractObject>();
            //        Debug.Log(this.name + " --> _attractObject is find");
            //    }
            //}

            //ROTATION
            //check the direction of the destination (with angle)
            _angleToDestination = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);

            //check if the forward direction of the boat is near the target direction
            if (Mathf.Abs(_angleToDestination) <= _maxAngularSpeed * Time.deltaTime)
            {
                _boatRotationState = BoatRotationState.FORWARD;
                if (_joystickController != null)
                {
                    if (pInputHorizontal > _joystickController._joystickDeadZone || pInputHorizontal < -_joystickController._joystickDeadZone
                        || pInputVertical > _joystickController._joystickDeadZone || pInputVertical < -_joystickController._joystickDeadZone)
                    {
                        this.transform.LookAt(this.transform.position + (_direction_MovementDirection * 10));
                        _angleToDestination = 0;
                    }
                }
            }
            //turn left
            else if (_angleToDestination < 0 && _angleToDestination > -180)
            {
                this.TurnLarboard();
                _boatRotationState = BoatRotationState.BABORD;
            }
            //turn right
            else if (_angleToDestination > 0 && _angleToDestination < 180)
            {
                this.TurnStarboard();
                _boatRotationState = BoatRotationState.TRIBORD;
            }

        }

        /// <summary>
        /// turn left (babord)
        /// </summary>
        public override void TurnLarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, -_maxAngularSpeed * Time.deltaTime, 0);
            }
        }

        /// <summary>
        /// turn right (starboard)
        /// </summary>
        public override void TurnStarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, +_maxAngularSpeed * Time.deltaTime, 0);
            }
        }

        /// <summary>
        /// pour le debug
        /// </summary>
        //private void OnDrawGizmos()
        //{
        //    //draw sphere at the target position
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawSphere(this.transform.position + this.transform.forward * 10, 2f);
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(this.transform.position + _direction_MovementDirection * 10, 2f);
        //    Gizmos.color = Color.magenta;
        //    Gizmos.DrawSphere(this.transform.position + (_direction_MovementDirection * 10), 2f);
        //    Debug.DrawLine(this.transform.position, this.transform.position + (_direction_MovementDirection * 10));
        //}


        #endregion MOVEMENTS

        #region ACCESSORS
        public BoatMovementState getBoatMovementState()
        {
            return _boatMovementState;
        }

        public BoatRotationState getBoatRotationState()
        {
            return _boatRotationState;
        }

        public float getSpeedForward()
        {
            return _currentMovingSpeed;
        }

        public float getMaxSpeedForward()
        {
            return _maxMovingSpeed;
        }

        public float getRotateSpeed()
        {
            return _maxAngularSpeed;
        }

        //public float getShootCoolDown()
        //{
        //    return _shootCooldown;
        //}


        public int getMaxLife()
        {
            return _maxLifePoint;
        }

        public float getCurrentLife()
        {
            return _currentLifePoint;
        }


        //public int getCurrentXp()
        //{
        //    if (_controller.GetComponent<Player>() != null)
        //    {
        //        return _controller.GetComponent<Player>()._currentXp;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        #endregion ACCESSORS
    }
}