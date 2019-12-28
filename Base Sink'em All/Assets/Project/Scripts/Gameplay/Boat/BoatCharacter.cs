using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
//using ProjetPirate.Physic;
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

    ///// <summary>
    ///// Base class of the boat posess al the main data
    ///// </summary>
    //[RequireComponent(typeof(Rigidbody))]
    //[RequireComponent(typeof(FloatingObject))]
    //[RequireComponent(typeof(AttractObject))]
    public class BoatCharacter : ProjetPirate.IA.Character
    {
        //    private Controller _owner;

        //    [Header("LIFE")]
        //    [SerializeField] private ShipType _shipType;

        //    [SerializeField]
        //    private float _weakenedStateLifeRatio = 0.5f;
        //    [SerializeField]
        //    private float _endangeredStateLifeRatio = 0.1f;
        //    public StructureState _structureState;

        //    [SerializeField] public int _plankDroppedByDeath;
        //    [SerializeField] public int _moneyDroppedByDeath;

        //    [Header("CANNONS")]
        //    [SerializeField] private List<Cannon> _larboardCannons;
        //    [SerializeField] private List<Cannon> _starboardCannons;

        //    [SerializeField] private float _shootCooldown;
        //    [SerializeField] public GameObject _droppedPlank;
        //    [SerializeField] public GameObject _droppedChest;

        //    [SerializeField] private List<GameObject> _structuresPerLevel;
        //    [SerializeField] private GameObject _larboardCannonPrefab;
        //    [SerializeField] private GameObject _starboardCannonPrefab;

        //    [SerializeField] private List<Transform> _larboardCannonPositions;
        //    [SerializeField] private List<Transform> _starboardCannonPositions;
        //    [SerializeField] private int _defaultCannonNumberBySide;

        //    private bool _larboardCannonInCooldown = false;
        //    private bool _starboardCannonInCooldown = false;
        //    private float _currentLarboardShootCooldownTime = 0;
        //    private float _currentStarboardShootCooldownTime = 0;

        //    private bool _isMovingForward = false;
        //    private float _stoppingDistance;

        //    public Controller Owner
        //    {
        //        get
        //        {
        //            return _owner;
        //        }
        //    }

        //    public float StoppingDistance
        //    {
        //        get { return _stoppingDistance; }
        //    }

        //    public float Deceleration
        //    {
        //        get { return _decelerationSpeedForward; }
        //    }

        //    public List<Transform> LarboardCannonPositions
        //    {
        //        get { return _larboardCannonPositions; }
        //    }

        //    public List<Transform> StarboardCannonPositions
        //    {
        //        get { return _starboardCannonPositions; }
        //    }

        //    public int DefaultCannonNumberBySide
        //    {
        //        get { return _defaultCannonNumberBySide; }
        //    }

        //    public GameObject DroppedChest
        //    {
        //        get { return _droppedChest; }
        //    }

        //    [Header("FALL DEATH")]
        //    [SerializeField]
        //    [Range(-100, -10)]
        //    private float _HeightFallingDeath = -10;

        //    //DATAS
        //    private Data_Boat _data_Boat = new Data_Boat();

        //    private Rigidbody _rigidbody;

        //    private AttractObject _attractObject;

        //    [SerializeField] bool _isDocking = false;
        //    [SerializeField] bool _isPushedByIsland = false;
        //    [SerializeField] List<InvisibleWallPoint> _leftPoints;
        //    [SerializeField] List<InvisibleWallPoint> _rightPoints;

        //    [Header("BOAT STATES (DON'T TOUCH)")]
        //    [SerializeField]
        //    private BoatMovementState _boatMovementState;
        //    [SerializeField]
        //    private BoatRotationState _boatRotationState;

        //    #region (TEST)MANAGE COLLISION
        //    //[Header("MANAGE COLLISION")]
        //    //[SerializeField]
        //    //private bool BoatCollide = false; // true if the boat collide an object
        //    //[SerializeField]
        //    //private LayerMask _layerMaskSea; //Made to ignore raycast collision with the sea
        //    //private Vector3 _directionVerifyOnTheSea;
        //    //private Vector3 _travelVerifyOnTheSea;
        //    //private Vector3 _startPositionVerifyOnTheSea;
        //    //[SerializeField]
        //    //private float _distanceVerifyOnTheSea = 10f;
        //    //[SerializeField]
        //    //private Transform[] _DetectionCollisionTransform;

        //    //private struct DetectionCollisionStruct
        //    //{
        //    //    public Transform _transform;
        //    //    public bool _isCollide;
        //    //}
        //    //[Header("JUST TO SEE")]
        //    //[SerializeField]
        //    //private DetectionCollisionStruct[] _detectionCollisionStruct;

        //    #endregion (TEST)MANAGE COLLISION

        //    #region PERFORM MOVEMENT DIRECTION

        //    Vector3 _normalizeTarget_MovementDirection;
        //    Vector3 _targetPosition_MovementDirection;
        //    Vector3 _direction_MovementDirection;
        //    Vector3 _currentVelocity_MovementDirection;

        //    [Header("FORWARD MOVEMENT ")]
        //    //[SerializeField]
        //    private float _minSpeedForward = 0f;
        //    [SerializeField]
        //    private float _accelerationSpeedForward = 1;
        //    [SerializeField]
        //    private float _decelerationSpeedForward = 1;


        //    [Header("ROTATION")]
        //    [SerializeField]
        //    private JoystickController _joystickController;
        //    //[SerializeField]
        //    private float _angleToDestination;

        //    #endregion PERFORM MOVEMENT DIRECTION

        //    private Vector3 _currentForwardDirection;
        //    private Vector3 _currentForwardVelocity; // _currentVelocity

        //    //private float _zInputMovement; // input vertical 
        //    //private float _xInputMovement; // input horizontal

        //    private bool _isMoving = false; // set depuis le controller (boatController ou IAController)

        //    // Use this for initialization
        //    void Start()
        //    {
        //        //set the currentLife of the boat when it appear
        //        _currentLifePoint = _maxLifePoint;
        //        _directionLocator = Instantiate(new GameObject()).transform;
        //        _directionLocator.SetParent(this.transform);
        //        _directionLocator.localPosition = Vector3.zero;


        //        //verify if the boat have the attrctObject Script
        //        if (this.GetComponent<AttractObject>() != null)
        //        {
        //            _attractObject = this.GetComponent<AttractObject>();
        //        }
        //        else
        //        {
        //            Debug.LogWarning("BoatMovement --> Start / This boat doesn't have the attract object component");
        //        }

        //        //BOAT MOVEMENT STATE
        //        _boatMovementState = BoatMovementState.IDLE;

        //        //BOAT ROTATION STATE
        //        _boatRotationState = BoatRotationState.FORWARD;

        //        //RIGIDBODY
        //        _rigidbody = this.GetComponent<Rigidbody>();
        //        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        //    }

        //    // Update is called once per frame
        //    void Update()
        //    {

        //        _isMovingForward = false;

        //        //verify death falling character
        //        FallDeath();

        //        if ((float)_currentLifePoint / (float)_maxLifePoint > _weakenedStateLifeRatio)
        //        {
        //            _structureState = StructureState.Normal;
        //        }
        //        else if ((float)_currentLifePoint / (float)_maxLifePoint > _endangeredStateLifeRatio)
        //        {
        //            _structureState = StructureState.Weakened;
        //        }
        //        else
        //        {
        //            _structureState = StructureState.Endangered;
        //        }
        //        _currentLarboardShootCooldownTime += Time.deltaTime;
        //        _currentStarboardShootCooldownTime += Time.deltaTime;
        //        if (_currentLarboardShootCooldownTime > _shootCooldown)
        //        {
        //            _larboardCannonInCooldown = false;
        //            _currentLarboardShootCooldownTime = 0;
        //        }
        //        if (_currentStarboardShootCooldownTime > _shootCooldown)
        //        {
        //            _starboardCannonInCooldown = false;
        //            _currentStarboardShootCooldownTime = 0;
        //        }

        //        //BOAT STATES
        //        this.ManageBoatMovementState();

        //        //UPDATE SPEED
        //        this.UpdateSpeedForwardForDirection();

        //        if (!_isDocking)
        //        {
        //            if (!CheckLeftInvisibleWall())
        //            {
        //                CheckRightInvisibleWall();
        //            }
        //        }

        //        _data_Boat.ReverseReloadTransform(this.gameObject);
        //    }

        //    //Shoot at Larboard (Babord)
        //    public void ShootLarboard()
        //    {
        //        if (!_larboardCannonInCooldown)
        //        {
        //            for (int i = 0; i < _larboardCannons.Count; i++)
        //            {
        //                _larboardCannons[i]._FireCannon();
        //            }
        //            _larboardCannonInCooldown = true;
        //        }
        //    }


        //    //Shoot at Starboard (Tribord)
        //    public void ShootStarboard()
        //    {
        //        if (!_starboardCannonInCooldown)
        //        {
        //            for (int i = 0; i < _starboardCannons.Count; i++)
        //            {
        //                _starboardCannons[i]._FireCannon();
        //            }
        //            _starboardCannonInCooldown = true;
        //        }
        //    }

        //    // Move forward based on _movingSpeed
        //    public override void MoveForward()
        //    {
        //        Vector3 pos = this.transform.position;
        //        pos += this.transform.forward * _currentMovingSpeed * Time.deltaTime;
        //        pos.y = 0;
        //        this.transform.position = pos;
        //        _isMovingForward = true;
        //    }

        //    public void Accelerate()
        //    {
        //        _currentMovingSpeed += _accelerationSpeedForward * Time.deltaTime;
        //        if (_currentMovingSpeed > _maxMovingSpeed)
        //        {
        //            _currentMovingSpeed = _maxMovingSpeed;
        //        }
        //        _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        //    }

        //    public void Decelerate()
        //    {
        //        _currentMovingSpeed -= _decelerationSpeedForward * Time.deltaTime;
        //        if (_currentMovingSpeed < 0)
        //        {
        //            _currentMovingSpeed = 0;
        //        }
        //        _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
        //    }

        //    public override int Damage(int _damage)
        //    {
        //        _currentLifePoint -= _damage;
        //        if (_currentLifePoint <= 0)
        //        {
        //            Death();
        //            return _xpEarned;
        //        }
        //        return 0;
        //    }

        //    private void FallDeath()
        //    {
        //        if (this.transform.position.y < _HeightFallingDeath)
        //        {
        //            Debug.Log("IS FALLING ");
        //            Death();
        //        }
        //    }

        //    public override void Death()
        //    {
        //        _currentLifePoint = _maxLifePoint;
        //        _owner.Death();
        //        this.GetComponent<BoxCollider>().enabled = false;
        //        ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
        //        for (int i = 0; i < enemies.Length; i++)
        //        {
        //            if (enemies[i].Target == this.gameObject)
        //            {
        //                enemies[i].RemoveAlert();
        //            }
        //        }
        //        this.GetComponent<BoxCollider>().enabled = true;

        //    }

        //    public void SetUpBoat(Player _player)
        //    {
        //        this.gameObject.transform.SetParent(_player.gameObject.transform);
        //        this.transform.localPosition = new Vector3(0, 0, 0);
        //        for (int i = 0; i < this.DefaultCannonNumberBySide; i++)
        //        {
        //            this.AddLarboardCannon();
        //            this.AddStarboardCannon();
        //        }
        //        _owner = _player;
        //    }

        //    public void AddLarboardCannon()
        //    {
        //        if (_larboardCannons.Count < LarboardCannonPositions.Count)
        //        {
        //            Cannon cannon = Instantiate(_larboardCannonPrefab).GetComponent<Cannon>();
        //            cannon.gameObject.transform.SetParent(LarboardCannonPositions[_larboardCannons.Count]);
        //            cannon.gameObject.transform.localPosition = Vector3.zero;
        //            cannon.SetOwner(this);
        //            _larboardCannons.Add(cannon);
        //        }
        //    }

        //    public void RemoveLarboardCannon()
        //    {
        //        if (_larboardCannons.Count > 0)
        //        {
        //            Cannon cannon = _larboardCannons[_larboardCannons.Count - 1];
        //            _larboardCannons.RemoveAt(_larboardCannons.Count - 1);
        //            Destroy(cannon.gameObject);
        //        }
        //    }

        //    public void AddStarboardCannon()
        //    {
        //        if (_starboardCannons.Count < StarboardCannonPositions.Count)
        //        {
        //            Cannon cannon = Instantiate(_starboardCannonPrefab).GetComponent<Cannon>();
        //            cannon.gameObject.transform.SetParent(StarboardCannonPositions[_starboardCannons.Count]);
        //            cannon.gameObject.transform.localPosition = Vector3.zero;
        //            cannon.SetOwner(this);
        //            _starboardCannons.Add(cannon);
        //        }
        //    }

        //    public void RemoveStarboardCannon()
        //    {
        //        if (_starboardCannons.Count > 0)
        //        {
        //            Cannon cannon = _starboardCannons[_starboardCannons.Count - 1];
        //            _larboardCannons.RemoveAt(_starboardCannons.Count - 1);
        //            Destroy(cannon.gameObject);
        //        }
        //    }



        //    #region ACCESSORS

        //    public float getShootCoolDown()
        //    {
        //        return _shootCooldown;
        //    }


        //    public int getMaxLife()
        //    {
        //        return _maxLifePoint;
        //    }

        //    public float getCurrentLife()
        //    {
        //        return _currentLifePoint;
        //    }


        //    public int getCurrentXp()
        //    {
        //        if (_owner.GetComponent<Player>() != null)
        //        {
        //            return _owner.GetComponent<Player>()._currentXp;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }

        //    #endregion ACCESSORS

        //    #region MUTATORS

        //    /// <summary>
        //    /// must be call in controllers
        //    /// </summary>
        //    public void setIsMoving(bool pIsMoving)
        //    {
        //        _isMoving = pIsMoving;
        //    }

        //    #endregion MUTATORS

        //    private void ManageBoatMovementState()
        //    {
        //        #region MANAGE BOAT MOVEMENT STATE

        //        //use the input to define the state of the boat
        //        if (_currentMovingSpeed == 0)
        //        {
        //            _boatMovementState = BoatMovementState.IDLE;
        //        }
        //        else
        //        {
        //            //acceleration
        //            //if (_currentSpeedForward > 0 && (_zInputMovement != 0 || _xInputMovement != 0))
        //            if (_currentMovingSpeed > 0 && _isMoving == true)
        //            {
        //                _boatMovementState = BoatMovementState.ACCELERATE;
        //            }
        //            //deceleration
        //            //else if (_currentSpeedForward > 0 && (_zInputMovement == 0 && _xInputMovement == 0))
        //            else if (_currentMovingSpeed > 0 && _isMoving == false)
        //            {
        //                _boatMovementState = BoatMovementState.DECELERATE;
        //            }
        //            //cruise_speed
        //            if (_currentMovingSpeed >= _maxMovingSpeed)
        //            {
        //                _boatMovementState = BoatMovementState.CRUISE_SPEED;
        //            }
        //        }




        //        #endregion MANAGE BOAT MOVEMENT STATE
        //    }

        //    /// <summary>
        //    /// a ranger pour apres
        //    /// </summary>
        //    private void UpdateSpeedForwardForDirection()
        //    {
        //        //acceleration
        //        //if (_zInputMovement != 0 || _xInputMovement != 0)
        //        if(_isMoving == true)
        //        {
        //            Accelerate();
        //        }
        //        //deceleration speed
        //        //else if (this.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() == null)
        //        if(_isMoving == false)
        //        {
        //            Decelerate();
        //        }

        //        _data_Boat.dStats.Speed = _currentMovingSpeed;
        //        if (_data_Boat.dStats.Speed > 0)
        //        {
        //            _isMovingForward = true;
        //        }
        //        else
        //        {
        //            _isMovingForward = false;
        //        }
        //    }

        //    #region MOVEMENTS

        //    public void PerformMovement(float pInputVertical, float pInputHorizontal)
        //    {
        //        //GET AXIS
        //        //_xInputMovement = pInputHorizontal;
        //        //_zInputMovement = pInputVertical;

        //        //define direction with the inputs
        //        _normalizeTarget_MovementDirection = new Vector3(pInputHorizontal, 0, pInputVertical);
        //        _targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
        //        _direction_MovementDirection = _targetPosition_MovementDirection - this.transform.position;

        //        //VELOCITY
        //        //_currentVelocity_MovementDirection = this.transform.forward * _currentSpeedForward * Time.deltaTime;
        //        //move the position by the velocity
        //        if (_attractObject._isFalling == false)
        //        {
        //            MoveForward();
        //            //this.transform.position += _currentVelocity_MovementDirection;

        //            //_rigidbody.MovePosition(this.transform.position + _currentVelocity_MovementDirection);
        //        }

        //        //ROTATION

        //        //check the direction of the destination (with angle)
        //        _angleToDestination = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);

        //        //check if the forward direction of the boat is near the target direction
        //        if (Mathf.Abs(_angleToDestination) <= _maxAngularSpeed * Time.deltaTime)
        //        {
        //            if (_joystickController != null)
        //            {
        //                if (pInputHorizontal > _joystickController._joystickDeadZone || pInputHorizontal < -_joystickController._joystickDeadZone
        //                    || pInputVertical > _joystickController._joystickDeadZone || pInputVertical < -_joystickController._joystickDeadZone)
        //                {
        //                    this.transform.LookAt(this.transform.position + (_direction_MovementDirection * 10));
        //                    _angleToDestination = 0;
        //                    _boatRotationState = BoatRotationState.FORWARD;
        //                    //Debug.LogError("AAAAAAAAAAAAA : "+"this.transform.position : " + this.transform.position +  "_targetPosition_MovementDirection" + _targetPosition_MovementDirection);
        //                }
        //            }
        //        }
        //        //turn left
        //        else if (_angleToDestination < 0 && _angleToDestination > -180)
        //        {
        //            this.TurnLarboard();
        //            _boatRotationState = BoatRotationState.BABORD;
        //        }
        //        //turn right
        //        else if (_angleToDestination > 0 && _angleToDestination < 180)
        //        {
        //            this.TurnStarboard();
        //            _boatRotationState = BoatRotationState.TRIBORD;
        //        }


        //    }

        //    /// <summary>
        //    /// turn left (babord)
        //    /// </summary>
        //    public override void TurnLarboard()
        //    {
        //        if (_isMovingForward)
        //        {
        //            this.transform.Rotate(0, -_maxAngularSpeed * Time.deltaTime, 0);
        //        }
        //    }

        //    /// <summary>
        //    /// turn right (starboard)
        //    /// </summary>
        //    public override void TurnStarboard()
        //    {
        //        if (_isMovingForward)
        //        {
        //            this.transform.Rotate(0, +_maxAngularSpeed * Time.deltaTime, 0);
        //        }
        //    }

        //    /// <summary>
        //    /// pour le debug
        //    /// </summary>
        //    //private void OnDrawGizmos()
        //    //{
        //    //    //draw sphere at the target position
        //    //    Gizmos.color = Color.blue;
        //    //    Gizmos.DrawSphere(this.transform.position + this.transform.forward * 10, 2f);
        //    //    Gizmos.color = Color.red;
        //    //    Gizmos.DrawSphere(this.transform.position + _direction_MovementDirection * 10, 2f);
        //    //    Gizmos.color = Color.magenta;
        //    //    Gizmos.DrawSphere(this.transform.position + (_direction_MovementDirection * 10), 2f);
        //    //    Debug.DrawLine(this.transform.position, this.transform.position + (_direction_MovementDirection * 10));
        //    //}


        //    #endregion MOVEMENTS

        //    #region ACCESSORS
        //    public BoatMovementState getBoatMovementState()
        //    {
        //        return _boatMovementState;
        //    }

        //    public BoatRotationState getBoatRotationState()
        //    {
        //        return _boatRotationState;
        //    }

        //    public float getSpeedForward()
        //    {
        //        return _currentMovingSpeed;
        //    }

        //    public float getMaxSpeedForward()
        //    {
        //        return _maxMovingSpeed;
        //    }

        //    public float getRotateSpeed()
        //    {
        //        return _maxAngularSpeed;
        //    }

        //    #endregion ACCESSORS

        //    private bool CheckLeftInvisibleWall()
        //    {
        //        //Debug.Log("Peach");
        //        for (int i = 0; i < _leftPoints.Count; i++)
        //        {
        //            //Debug.Log(i);
        //            if (_leftPoints[i].InvisibleWallIsOn())
        //            {
        //                Debug.Log("Touch it");
        //                TurnStarboard();
        //                _isPushedByIsland = true;
        //                return true;
        //            }
        //        }
        //        _isPushedByIsland = false;
        //        return false;
        //    }

        //    private bool CheckRightInvisibleWall()
        //    {
        //        for (int i = 0; i < _rightPoints.Count; i++)
        //        {
        //            if (_rightPoints[i].InvisibleWallIsOn())
        //            {
        //                TurnLarboard();
        //                _isPushedByIsland = true;
        //                return true;
        //            }
        //        }
        //        _isPushedByIsland = false;
        //        return false;
        //    }
    }
}
