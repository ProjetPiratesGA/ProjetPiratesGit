using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
using ProjetPirate.Physic;
using ProjetPirate.Data;

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

    /// <summary>
    /// Base class of the boat posess al the main data
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(FloatingObject))]
    [RequireComponent(typeof(AttractObject))]
    public class BoatCharacter : ProjetPirate.IA.Character
    {


        private Controller _owner;

        //datas
        private Data_Boat _data_Boat = new Data_Boat();

        private Rigidbody _rigidbody;

        private AttractObject _attractObject;

        #region MOVEMENT 
        [Header("BOAT STATES (DON'T TOUCH)")]
        [SerializeField]
        private BoatMovementState _boatMovementState;
        [SerializeField]
        private BoatRotationState _boatRotationState;


        Vector3 _normalizeTarget_MovementDirection;
        Vector3 _targetPosition_MovementDirection;
        Vector3 _direction_MovementDirection;
        Vector3 _currentVelocity_MovementDirection;

        [Header("FORWARD MOVEMENT")]
        [SerializeField]
        private float _accelerationSpeedForward = 1;
        [SerializeField]
        private float _decelerationSpeedForward = 1;
        private bool _isMovingForward = false;

        [Header("ROTATION")]
        [SerializeField]
        private float _accelerationSpeedRotation = 1;
        [SerializeField]
        private float _decelerationSpeedRotation = 1;
        [SerializeField]
        private JoystickController _joystickController;
        private float _angleToDestination;
        private bool _isRotatingLarboard = false;
        private bool _isRotatingStarboard = false;
        private bool _isRotating = false;


        private Vector3 _currentForwardDirection;
        private Vector3 _currentForwardVelocity; // _currentVelocity

        //private float _zInputMovement; // input vertical 
        //private float _xInputMovement; // input horizontal

        private bool _ControllerIsMoving = false; // set depuis le controller (boatController ou IAController)
        #endregion MOVEMENT

        [Header("SHIP")]
        [SerializeField] private ShipType _shipType;

        [SerializeField]
        private float _weakenedStateLifeRatio = 0.5f;
        [SerializeField]
        private float _endangeredStateLifeRatio = 0.1f;
        public StructureState _structureState;

        [SerializeField] public int _plankDroppedByDeath;
        [SerializeField] public int _moneyDroppedByDeath;

        #region CANNONS
        [Header("CANNONS")]
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;

        [SerializeField] private float _shootCooldown;
        [SerializeField] public GameObject _droppedPlank;
        [SerializeField] public GameObject _droppedChest;

        [SerializeField] private List<GameObject> _structuresPerLevel;
        [SerializeField] private GameObject _larboardCannonPrefab;
        [SerializeField] private GameObject _starboardCannonPrefab;

        [SerializeField] private List<Transform> _larboardCannonPositions;
        [SerializeField] private List<Transform> _starboardCannonPositions;
        [SerializeField] private int _defaultCannonNumberBySide;

        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;

        #endregion CANNONS

        private float _stoppingDistance;
        
        public Controller Owner
        {
            get
            {
                return _owner;
            }
        }

        public float StoppingDistance
        {
            get { return _stoppingDistance; }
        }

        public float Deceleration
        {
            get { return _decelerationSpeedForward; }
        }

        public List<Transform> LarboardCannonPositions
        {
            get { return _larboardCannonPositions; }
        }

        public List<Transform> StarboardCannonPositions
        {
            get { return _starboardCannonPositions; }
        }

        public int DefaultCannonNumberBySide
        {
            get { return _defaultCannonNumberBySide; }
        }

        public GameObject DroppedChest
        {
            get { return _droppedChest; }
        }

        [Header("FALL DEATH")]
        [SerializeField]
        [Range(-100, -10)]
        private float _HeightFallingDeath = -10;


        [Header("DOCKING")]
        [SerializeField] bool _isDocking = false;

        [Header("COLLISIONS")]
        [SerializeField] bool _isPushedByIsland = false;
        [SerializeField] List<InvisibleWallPoint> _leftPoints;
        [SerializeField] List<InvisibleWallPoint> _rightPoints;


        // Use this for initialization
        void Start()
        {
            Debug.Log("BoatCharacter : Start");

            //set the currentLife of the boat when it appear
            _currentLifePoint = _maxLifePoint;
            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;


            //verify if the boat have the attrctObject Script
            if (this.GetComponent<AttractObject>() != null)
            {
                _attractObject = this.GetComponent<AttractObject>();
            }
            else
            {
                Debug.LogWarning("BoatMovement --> Start / This boat doesn't have the attract object component");
            }

            //BOAT MOVEMENT STATE
            _boatMovementState = BoatMovementState.IDLE;

            //BOAT ROTATION STATE
            _boatRotationState = BoatRotationState.FORWARD;

            //RIGIDBODY
            _rigidbody = this.GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Update is called once per frame
        void Update()
        {

            //_isMovingForward = false;

            //verify death falling character
            FallDeath();

            //manage structure state
            if ((float)_currentLifePoint / (float)_maxLifePoint > _weakenedStateLifeRatio)
            {
                _structureState = StructureState.Normal;
            }
            else if ((float)_currentLifePoint / (float)_maxLifePoint > _endangeredStateLifeRatio)
            {
                _structureState = StructureState.Weakened;
            }
            else
            {
                _structureState = StructureState.Endangered;
            }

            //Cannons
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

            //BOAT STATES
            this.ManageBoatMovementState();
            this.ManageBoatRotationState();

            //UPDATE SPEED
            this.UpdateSpeedForward();
            this.UpdateSpeedRotation();

            //docking
            if (!_isDocking)
            {
                if (!CheckLeftInvisibleWall())
                {
                    CheckRightInvisibleWall();
                }
            }

            //data boat
            _data_Boat.ReverseReloadTransform(this.gameObject);
        }


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

        private void ManageBoatRotationState()
        {
            #region MANAGE BOAT ROTATION STATE 

            //use the input to define the state of the boat
            if (_isRotating)
            {
                if(_isRotatingLarboard)
                {
                    _boatRotationState = BoatRotationState.BABORD;
                }
                if (_isRotatingStarboard)
                {
                    _boatRotationState = BoatRotationState.TRIBORD;
                }
            }
            else
            {
                _boatRotationState = BoatRotationState.FORWARD;
            }
            


            #endregion MANAGE BOAT ROTATION STATE 
        }

        public override int Damage(int _damage)
        {
            _currentLifePoint -= _damage;
            if (_currentLifePoint <= 0)
            {
                Death();
                return _xpEarned;
            }
            return 0;
        }

        private void FallDeath()
        {
            if (this.transform.position.y < _HeightFallingDeath)
            {
                Debug.Log("IS FALLING ");
                Death();
            }
        }

        public override void Death()
        {
            _currentLifePoint = _maxLifePoint;
            _owner.Death();
            this.GetComponent<BoxCollider>().enabled = false;
            ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }
            this.GetComponent<BoxCollider>().enabled = true;

        }

        public void SetUpBoat(Player _player)
        {
            this.gameObject.transform.SetParent(_player.gameObject.transform);
            this.transform.localPosition = new Vector3(0, 0, 0);
            for (int i = 0; i < this.DefaultCannonNumberBySide; i++)
            {
                this.AddLarboardCannon();
                this.AddStarboardCannon();
            }
            _owner = _player;
        }

        public void AddLarboardCannon()
        {
            if (_larboardCannons.Count < LarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_larboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(LarboardCannonPositions[_larboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _larboardCannons.Add(cannon);
            }
        }

        public void RemoveLarboardCannon()
        {
            if (_larboardCannons.Count > 0)
            {
                Cannon cannon = _larboardCannons[_larboardCannons.Count - 1];
                _larboardCannons.RemoveAt(_larboardCannons.Count - 1);
                Destroy(cannon.gameObject);
            }
        }

        public void AddStarboardCannon()
        {
            if (_starboardCannons.Count < StarboardCannonPositions.Count)
            {
                Cannon cannon = Instantiate(_starboardCannonPrefab).GetComponent<Cannon>();
                cannon.gameObject.transform.SetParent(StarboardCannonPositions[_starboardCannons.Count]);
                cannon.gameObject.transform.localPosition = Vector3.zero;
                cannon.SetOwner(this);
                _starboardCannons.Add(cannon);
            }
        }

        public void RemoveStarboardCannon()
        {
            if (_starboardCannons.Count > 0)
            {
                Cannon cannon = _starboardCannons[_starboardCannons.Count - 1];
                _larboardCannons.RemoveAt(_starboardCannons.Count - 1);
                Destroy(cannon.gameObject);
            }
        }

        //Shoot at Larboard (Babord)
        public void ShootLarboard()
        {
            
                if (!_larboardCannonInCooldown)
                {
                    for (int i = 0; i < _larboardCannons.Count; i++)
                    {
                        _larboardCannons[i]._FireCannon();
                    }
                    _larboardCannonInCooldown = true;
                }
            
        }


        //Shoot at Starboard (Tribord)
        public void ShootStarboard()
        {
            
                if (!_starboardCannonInCooldown)
                {
                    for (int i = 0; i < _starboardCannons.Count; i++)
                    {
                        _starboardCannons[i]._FireCannon();
                    }
                    _starboardCannonInCooldown = true;
                }
            

        }


        #region ACCESSORS

        public float getShootCoolDown()
        {
            return _shootCooldown;
        }


        public int getMaxLife()
        {
            return _maxLifePoint;
        }

        public float getCurrentLife()
        {
            return _currentLifePoint;
        }


        public int getCurrentXp()
        {
            if (_owner.GetComponent<Player>() != null)
            {
                return _owner.GetComponent<Player>()._currentXp;
            }
            else
            {
                return 0;
            }
        }

        public bool getControllerIsMoving()
        {
            return _ControllerIsMoving;
        }

        #endregion ACCESSORS

        #region MUTATORS

        /// <summary>
        /// must be call in controllers
        /// </summary>
        public void setControllerIsMoving(bool pControllerIsMoving)
        {

            _ControllerIsMoving = pControllerIsMoving;
        }

        #endregion MUTATORS

        #region MOVEMENTS

        #region FORWARD


        /// <summary>
        /// Update the CurrentMoving speed
        /// - verify if the boat is moving forward
        /// </summary>
        private void UpdateSpeedForward()
        {
            //acceleration
            if(_ControllerIsMoving == true)
            {
                AccelerateForward();
            }
            //deceleration speed
            //else if (this.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() == null)
            if(_ControllerIsMoving == false)
            {
                DecelerateForward();
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

        /// <summary>
        /// use to move the boat in is forward direction
        /// - take the forward as a direction & multiply by the currentMovingSpeed
        /// - set he _isMovingForward variable to true
        /// </summary>
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * _currentMovingSpeed * Time.deltaTime;
            pos.y = 0;
            this.transform.position = pos;

            //_isMovingForward = true;
        }

        /// <summary>
        /// use to accelerate the boat
        /// - accelerate the currentMovingSpeed of the boat by the acceleration speed forward variable
        /// - if the currentMovingSpeed is superior the the max moving speed reset the speed to the max moving speed
        /// - determine the stopping distance for the IA (use to make a precise stop)
        /// </summary>
        public void AccelerateForward()
        {
            _currentMovingSpeed += _accelerationSpeedForward * Time.deltaTime;
            Debug.Log("ACCELERATE : _currentMovingSpeed  " + _currentMovingSpeed + " _accelerationSpeedForward : " + _accelerationSpeedForward);
            if (_currentMovingSpeed > _maxMovingSpeed)
            {
                _currentMovingSpeed = _maxMovingSpeed;
            }
            _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        }

        /// <summary>
        /// use to decelerate the boat
        /// - decelerate the currentMovingSpeed of the boat by the decelerationSpeedForward variable
        /// - if the currentMovingSpeed is superior the the max moving speed reset the speed to the max moving speed
        /// - determine the stopping distance for the IA (use to make a precise stop)
        /// </summary>
        public void DecelerateForward()
        {
            _currentMovingSpeed -= _decelerationSpeedForward * Time.deltaTime;
            if (_currentMovingSpeed < 0)
            {
                _currentMovingSpeed = 0;
            }
            _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        }

        #endregion FORWARD


        #region ROTATION

        /// <summary>
        /// Update the CurrentAngularSpeed
        /// - verify where the boat is rotating
        /// /// </summary>
        private void UpdateSpeedRotation()
        {
            //acceleration
            if (_ControllerIsMoving == true)
            {
                AccelerateRotation();
            }
            //deceleration speed
            //else if (this.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() == null)
            if (_ControllerIsMoving == false)
            {
                DecelerateRotation();
            }

            //Booleans rotation
            if ( (_angleToDestination < 0 && _angleToDestination > -180)
                && _currentAngularSpeed > 0)
            {
                _isRotatingLarboard = true;
            }
            else
                _isRotatingLarboard = false;
            if ( (_angleToDestination > 0 && _angleToDestination < 180)
                 && _currentAngularSpeed > 0)
            {
                _isRotatingStarboard = true;
            }
            else
                _isRotatingStarboard = false;

            if (_isRotatingStarboard || _isRotatingLarboard)
                _isRotating = true;
            else
                _isRotating = false;

            //_data_Boat.dStats.Speed = _currentMovingSpeed;
            //if (_data_Boat.dStats.Speed > 0)
            //{
            //    _isMovingForward = true;
            //}
            //else
            //{
            //    _isMovingForward = false;
            //}
        }

        private void AccelerateRotation()
        {
            _currentAngularSpeed += _accelerationSpeedRotation * Time.deltaTime;
            Debug.Log("AccelerateRotation : _currentAngularSpeed  " + _currentAngularSpeed + " _accelerationSpeedRotation : " + _accelerationSpeedRotation);
            if (_currentAngularSpeed > _maxAngularSpeed)
            {
                _currentAngularSpeed = _maxAngularSpeed;
            }
            //_stoppingDistance = ((_currentAngularSpeed / 10) * (_currentAngularSpeed / 10)) * 50 / _decelerationAngularSpeed;
        }

        private void DecelerateRotation()
        {
            _currentAngularSpeed -= _decelerationSpeedRotation * Time.deltaTime;
            if (_currentAngularSpeed < 0)
            {
                _currentAngularSpeed = 0;
            }
            //_stoppingDistance = ((_currentAngularSpeed / 10) * (_currentAngularSpeed / 10)) * 50 / _decelerationSpeedRotation;
        }

        #endregion ROTATION

        /// <summary>
        /// perform the movement of the boat for the BoatController (player)
        /// - define the direction with the sending inputs (not sure)
        /// - call the MoveForward
        /// - manage the rotation of the boat & the enum BoatRotationState
        /// </summary>
        /// <param name="pInputVertical"></param>
        /// <param name="pInputHorizontal"></param>
        public void PerformMovement(float pInputVertical, float pInputHorizontal)
        {

            //define direction with the inputs
            if (pInputHorizontal > _joystickController._joystickDeadZone || pInputHorizontal < -_joystickController._joystickDeadZone
                        || pInputVertical > _joystickController._joystickDeadZone || pInputVertical < -_joystickController._joystickDeadZone)
            {
                _normalizeTarget_MovementDirection = new Vector3(pInputHorizontal, 0, pInputVertical);
                _targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
                _direction_MovementDirection = _targetPosition_MovementDirection - this.transform.position;
            }
            //VELOCITY
            //_currentVelocity_MovementDirection = this.transform.forward * _currentSpeedForward * Time.deltaTime;
            if (_attractObject._isFalling == false)
            {
                MoveForward();
                //this.transform.position += _currentVelocity_MovementDirection;
            }

            //ROTATION
            //check the direction of the destination (with angle)
            _angleToDestination = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);

            //check if the forward direction of the boat is near the target direction
            if (Mathf.Abs(_angleToDestination) <= _maxAngularSpeed * Time.deltaTime)
            {
                //_boatRotationState = BoatRotationState.FORWARD;
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
                //_boatRotationState = BoatRotationState.BABORD;
            }
            //turn right
            else if (_angleToDestination > 0 && _angleToDestination < 180)
            {
                this.TurnStarboard();
                //_boatRotationState = BoatRotationState.TRIBORD;
            }

        }

        /// <summary>
        /// turn left (babord)
        /// </summary>
        public override void TurnLarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, -_currentAngularSpeed * Time.deltaTime, 0);
            }
        }

        /// <summary>
        /// turn right (starboard)
        /// </summary>
        public override void TurnStarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, +_currentAngularSpeed * Time.deltaTime, 0);
            }
        }

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

        #endregion ACCESSORS


        //Collisions
        private bool CheckLeftInvisibleWall()
        {
            //Debug.Log("Peach");
            for (int i = 0; i < _leftPoints.Count; i++)
            {
                //Debug.Log(i);
                if (_leftPoints[i].InvisibleWallIsOn())
                {
                    Debug.Log("Touch it");
                    TurnStarboard();
                    _isPushedByIsland = true;
                    return true;
                }
            }
            _isPushedByIsland = false;
            return false;
        }

        private bool CheckRightInvisibleWall()
        {
            for (int i = 0; i < _rightPoints.Count; i++)
            {
                if (_rightPoints[i].InvisibleWallIsOn())
                {
                    TurnLarboard();
                    _isPushedByIsland = true;
                    return true;
                }
            }
            _isPushedByIsland = false;
            return false;
        }

        #region DEBUG METHOD
        /// <summary>
        /// pour le debug
        /// </summary>
        //private void OnDrawGizmos()
        //{
        //    //draw sphere at the target position

        //    //forward point
        //    Gizmos.color = Color.blue;
        //    Gizmos.DrawSphere(this.transform.position + this.transform.forward * 10, 2f);

        //    //direction point
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(this.transform.position + _direction_MovementDirection * 10, 2f);
        //    Debug.DrawLine(this.transform.position, this.transform.position + _direction_MovementDirection * 10);

        //}

        #endregion DEBUG METHOD
    }
}
