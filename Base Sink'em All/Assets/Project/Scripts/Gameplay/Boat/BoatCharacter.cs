using System.Collections;
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

        /// <summary>
        /// TEST SEB
        /// </summary>
        //int _maxCannonsPerSide = 2;
        //int _startCannonsNumberLeft = 1;
        //int _startCannonsNumberRight = 1;
        //int _currentCannnonsNumberLeft = 1;
        //int _currentCannnonsNumberRight = 1;
        // END TEST SEB //


        [SerializeField] private float _shootCooldown;

        [SerializeField] private int _defaultCannonNumberBySide;

        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;

        [SerializeField] private GameObject _larboardCannonPrefab;
        [SerializeField] private GameObject _starboardCannonPrefab;

        public Data_Boat Data
        {
            get { return _data_Boat; }
        }

        public List<Cannon> larboardCannons
        {
            get { return _larboardCannons; }
            set { _larboardCannons = value ; }
        }

        public List<Cannon> starboardCannons
        {
            get { return _starboardCannons; }
            set { _starboardCannons = value; }
        }
        public int DefaultCannonNumberBySide
        {
            get { return _defaultCannonNumberBySide; }
        }


        public float getShootCoolDown()
        {
            return _shootCooldown;
        }

        void Start()
        {
            _data_Boat.dStats = _data;

        }

        bool _asUpdateDatas = false;

        void Update()
        {


            _isMovingForward = false;

            //GoldFxAnimation();
            //verify death falling character
            //FallDeath();

            //if ((float)_data_Boat.dStats.Life / (float)_maxLifePoint > _weakenedStateLifeRatio)
            //{
            //    _structureState = StructureState.Normal;
            //}
            //else if ((float)_data_Boat.dStats.Life / (float)_maxLifePoint > _endangeredStateLifeRatio)
            //{
            //    _structureState = StructureState.Weakened;
            //}
            //else
            //{
            //    _structureState = StructureState.Endangered;
            //}
            _currentLarboardShootCooldownTime += Time.deltaTime;
            _currentStarboardShootCooldownTime += Time.deltaTime;
            if (_currentLarboardShootCooldownTime > _shootCooldown)
            {
                _larboardCannonInCooldown = false;
                _currentLarboardShootCooldownTime = 0;
            }
            if (_currentStarboardShootCooldownTime > _shootCooldown)
            {
                _starboardCannonInCooldown = false;
                _currentStarboardShootCooldownTime = 0;
            }

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
            //    main.startLifetime = _data_Boat.dStats.Speed / _maxMovingSpeed;
            //}
            _data_Boat.ReverseReloadTransform(this.gameObject);


            //TEST DEBUG ADD CANNON
            if(this.hasAuthority)
            {
                if(Input.GetKeyDown(KeyCode.F5))
                {
                    this.CmdAddCannons(true, false);
                    this.CmdUpdateActiveCanons();
                }
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    this.CmdAddCannons(false, true);
                    this.CmdUpdateActiveCanons();
                }
            }
            // END TEST
        }



        [Command]
        public void CmdSetUpBoat(GameObject player)
        {
            
            this.gameObject.transform.SetParent(player.transform);
            //this.transform.localPosition = new Vector3(0, 0, 0);
            _controller = player.GetComponent<Controller>();

            player.GetComponent<Player>().SetDataBoat(this);
            TargetSetParent(player.GetComponent<Player>().connectionToClient, player.gameObject);


            /// <summary>
            /// TEST SEB
            /// </summary>
            this.SetActiveCannons();

            this.RpcUpdateActiveCannons();
            // END TEST SEB //
        }

        /// <summary>
        /// TEST SEB
        /// </summary>
        [Command]
        public void CmdAddCannons(bool left, bool right)
        {

            if ((_data_Boat.CurrentCanonLeft < _data_Boat.MaxCanonPerSide) && left == true)
            {
                _data_Boat.CurrentCanonLeft++;
            }
            if ((_data_Boat.CurrentCanonRight < _data_Boat.MaxCanonPerSide) && right == true)
            {
                _data_Boat.CurrentCanonRight++;
            }
            this.RpcAddCannons(left, right);
        }

        [ClientRpc]
        public void RpcAddCannons(bool left, bool right)
        {
            if ((_data_Boat.CurrentCanonLeft < _data_Boat.MaxCanonPerSide) && left == true)
            {
                _data_Boat.CurrentCanonLeft++;
            }
            if ((_data_Boat.CurrentCanonRight < _data_Boat.MaxCanonPerSide) && right == true)
            {
                _data_Boat.CurrentCanonRight++;
            }
        }


        public void SetActiveCannons()
        {
            for (int i = 0; i < _larboardCannons.Count; i++)
            {
                if (i < _data_Boat.CurrentCanonLeft)
                {
                    _larboardCannons[i].gameObject.SetActive(true);
                }
                else
                {
                    _larboardCannons[i].gameObject.SetActive(false);
                }
            }

            for (int i = 0; i < _starboardCannons.Count; i++)
            {
                if (i < _data_Boat.CurrentCanonRight)
                {
                    _starboardCannons[i].gameObject.SetActive(true);
                }
                else
                {
                    _starboardCannons[i].gameObject.SetActive(false);
                }
            }
        }

        [TargetRpc]
        public void TargetSetParent(NetworkConnection target,GameObject player)
        {
            this.gameObject.transform.SetParent(player.transform);
            //this.transform.localPosition = new Vector3(0, 0, 0);
            _controller = player.GetComponent<Controller>();
            player.GetComponent<Player>().SetDataBoat(this);

        }

        /// <summary>
        /// TEST SEB
        /// </summary>
        [Command]
        public void CmdUpdateActiveCanons()
        {
            SetActiveCannons();
            RpcUpdateActiveCannons();
        }



        [ClientRpc]
        public void RpcUpdateActiveCannons()
        {
            SetActiveCannons();
        }



        [TargetRpc]
        public void TargetUpdateActiveCannons(NetworkConnection target)
        {
            SetActiveCannons();
        }

        // END TEST SEB //

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
            if (_data_Boat.dStats.Speed == 0)
            {
                _boatMovementState = BoatMovementState.IDLE;
            }
            else
            {
                //acceleration
                //if (_currentSpeedForward > 0 && (_zInputMovement != 0 || _xInputMovement != 0))
                if (_data_Boat.dStats.Speed > 0 && _ControllerIsMoving == true)
                {
                    _boatMovementState = BoatMovementState.ACCELERATE;
                }
                //deceleration
                //else if (_currentSpeedForward > 0 && (_zInputMovement == 0 && _xInputMovement == 0))
                else if (_data_Boat.dStats.Speed > 0 && _ControllerIsMoving == false)
                {
                    _boatMovementState = BoatMovementState.DECELERATE;
                }
                //cruise_speed
                if (_data_Boat.dStats.Speed >= _maxMovingSpeed)
                {
                    _boatMovementState = BoatMovementState.CRUISE_SPEED;
                }
            }




            #endregion MANAGE BOAT MOVEMENT STATE
        }

        //Shoot at Larboard (Babord)
        public void ShootLarboard()
        {
            if (!_larboardCannonInCooldown /*& !Safe*/)
            {
                CmdFireLeft();

                _larboardCannonInCooldown = true;
            }
        }

        [Command]
        private void CmdFireLeft()
        {
            RpcFireLeft();
        }

        [ClientRpc]
        private void RpcFireLeft()
        {
            for (int i = 0; i < _larboardCannons.Count; i++)
            {
                //TEST SEB (Condition if seulement)
                if (_larboardCannons[i].gameObject.activeSelf)
                {
                    _larboardCannons[i].FireCannon();
                }
            }
        }


        //Shoot at Starboard (Tribord)
        public void ShootStarboard()
        {
            if (!_starboardCannonInCooldown /*& !Safe*/)
            {
                CmdFireRight();

                _starboardCannonInCooldown = true;
            }
        }

        [Command]
        private void CmdFireRight()
        {
            RpcFireRight();
        }

        [ClientRpc]
        private void RpcFireRight()
        {

            for (int i = 0; i < _starboardCannons.Count; i++)
            {
                //TEST SEB (Condition if seulement)
                if (_starboardCannons[i].gameObject.activeSelf)
                {
                    _starboardCannons[i].FireCannon();
                }
            }
        }


        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * _data_Boat.dStats.Speed * Time.deltaTime;
            pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        public void Accelerate()
        {
            Debug.Log(this.name + "je suis dans le accelerate");
            _data_Boat.dStats.Speed += _accelerationSpeedForward * Time.deltaTime;
            if (_data_Boat.dStats.Speed > _maxMovingSpeed)
            {
                _data_Boat.dStats.Speed = _maxMovingSpeed;
            }
            Debug.Log(this.name + " --> Acclerate / _data_Boat.dStats.Speed : " + _data_Boat.dStats.Speed + " _accelerationSpeedForward : " + _accelerationSpeedForward
                + " Time.deltaTime : " + Time.deltaTime);
            _stoppingDistance = ((_data_Boat.dStats.Speed / 10) * (_data_Boat.dStats.Speed / 10)) * 50 / _decelerationSpeedForward;
        }

        public void Decelerate()
        {
            _data_Boat.dStats.Speed -= _decelerationSpeedForward * Time.deltaTime;
            if (_data_Boat.dStats.Speed < 0)
            {
                _data_Boat.dStats.Speed = 0;
            }
            _stoppingDistance = ((_data_Boat.dStats.Speed / 10) * (_data_Boat.dStats.Speed / 10)) * 50 / _decelerationSpeedForward;
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
            Debug.Log(this.name + " --> performMovement / _data_Boat.dStats.Speed : " + _data_Boat.dStats.Speed + " _currentAngularSpeed : " + _currentAngularSpeed + " this.transform.position : " + this.transform.position);
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
            return _data_Boat.dStats.Speed;
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
            return _data_Boat.dStats.Life;
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
