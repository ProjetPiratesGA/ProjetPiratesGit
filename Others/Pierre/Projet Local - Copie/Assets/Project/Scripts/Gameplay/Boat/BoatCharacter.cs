using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
using ProjetPirate.Physic;
using ProjetPirate.Data;
using ProjetPirate.IA;

namespace ProjetPirate.Boat
{
    public enum ShipType
    {
        Military = -1,
        Merchant = -2,
        Player_Level_1 = 1,
        Player_Level_2 = 2,
        Player_Level_3 = 3,
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

        [Header("LIFE")]
        [SerializeField] private ShipType _shipType;

        [SerializeField]
        private float _weakenedStateLifeRatio = 0.5f;
        [SerializeField]
        private float _endangeredStateLifeRatio = 0.1f;
        public StructureState _structureState;

        [SerializeField] public int _plankDroppedByDeath;
        [SerializeField] public int _moneyDroppedByDeath;

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

        private bool _isMovingForward = false;
        private float _stoppingDistance;


        private bool _deathAnimationIsPlaying = false;
        private float _deathAnimationCurrentRotationTime = 0;
        private float _deathAnimationRotationDelay = 0;
        private float _deathAnimationCurrentMovementTime = 0;
        private float _deathAnimationMovementDelay = 2;
        private float _deathAnimationRotationTime = 4;
        private float _deathAnimationMovementTime = 2;
        private Vector3 _deathAnimationStartRotation;
        private Vector3 _deathAnimationEndRotation;
        private Vector3 _deathAnimationStartPosition;
        private Vector3 _deathAnimationEndPosition;
        private Vector3 _deathAnimationPositionOffset = new Vector3(0, -10, 0);
        private Vector3 _deathAnimationRotationOffset = new Vector3(-80, 0, 0);

        private bool _fallAnimationIsPlaying = false;
        private float _fallAnimationCurrentRotationTime = 0;
        private float _fallAnimationRotationDelay = 0;
        private float _fallAnimationCurrentMovementTime = 0;
        private float _fallAnimationMovementDelay = 0;
        private float _fallAnimationRotationTime = 2;
        private float _fallAnimationMovementTime = 2;
        private Vector3 _fallAnimationStartRotation;
        private Vector3 _fallAnimationEndRotation;
        private Vector3 _fallAnimationStartPosition;
        private Vector3 _fallAnimationEndPosition;
        private Vector3 _fallAnimationPositionOffset = new Vector3(0, -150, -100);
        private float _fallAnimationRotationOffset = 100;

        private bool _respawninfAnimationIsPlaying = false;
        private float _respawnAnimationCurrentTime = 0;
        private float _respawnAnimationTime = 1.5f;

        private int _nextDockingCheckpointId;


        private bool _goldFXIsPlaying = false;
        private Vector3 _goldFXStartPosition;
        private Vector3 _goldFXEndPosition;
        private float _goldFXCurrentTime = 0;


        public ShipType ShipType
        {
            get { return _shipType; }
        }

        public float StoppingDistance
        {
            get { return _stoppingDistance; }
        }

        public float Deceleration
        {
            get { return _decelerationSpeedForward; }
        }

        public float Acceleration
        {
            get { return _accelerationSpeedForward; }
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

        //DATAS
        private Data_Boat _data_Boat = new Data_Boat();

        private Rigidbody _rigidbody;

        private AttractObject _attractObject;

        public bool Safe = false;
        [SerializeField] private Dock _dock;

        [SerializeField] bool _isDocking = false;
        [SerializeField] bool _isDocked = false;
        [SerializeField] bool _isLeavingDock = false;

        [SerializeField] bool _isPushedByIsland = false;
        [SerializeField] List<InvisibleWallPoint> _leftPoints;
        [SerializeField] List<InvisibleWallPoint> _rightPoints;

        [SerializeField] List<ParticleSystem> _waterTrails;
        [SerializeField] ParticleSystem _damageFX;
        [SerializeField] Material _damageFXColor;
        [SerializeField] private ParticleSystem _goldFX;

        [Header("BOAT STATES (DON'T TOUCH)")]
        [SerializeField]
        private BoatMovementState _boatMovementState;
        [SerializeField]
        private BoatRotationState _boatRotationState;

        #region (TEST)MANAGE COLLISION
        //[Header("MANAGE COLLISION")]
        //[SerializeField]
        //private bool BoatCollide = false; // true if the boat collide an object
        //[SerializeField]
        //private LayerMask _layerMaskSea; //Made to ignore raycast collision with the sea
        //private Vector3 _directionVerifyOnTheSea;
        //private Vector3 _travelVerifyOnTheSea;
        //private Vector3 _startPositionVerifyOnTheSea;
        //[SerializeField]
        //private float _distanceVerifyOnTheSea = 10f;
        //[SerializeField]
        //private Transform[] _DetectionCollisionTransform;

        //private struct DetectionCollisionStruct
        //{
        //    public Transform _transform;
        //    public bool _isCollide;
        //}
        //[Header("JUST TO SEE")]
        //[SerializeField]
        //private DetectionCollisionStruct[] _detectionCollisionStruct;

        #endregion (TEST)MANAGE COLLISION

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

        // Use this for initialization
        void Start()
        {
            //set the currentLife of the boat when it appear
            _joystickController = FindObjectOfType<JoystickController>();
            if (_joystickController == null)
            {
                Debug.LogError("JoystickController Not Assigned");
            }
            if (_damageFX != null)
            {
                _damageFX.Stop();
                ParticleSystem.MainModule main = _damageFX.main;
                main.startColor = _damageFXColor.color;
            }
            if (_goldFX != null)
            {
                _goldFX.Stop();
            }

            _currentLifePoint = _maxLifePoint;
            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;

            _deathAnimationCurrentRotationTime = -_deathAnimationRotationDelay / _deathAnimationRotationTime;
            _deathAnimationCurrentMovementTime = -_deathAnimationMovementDelay / _deathAnimationMovementTime;


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

            _isMovingForward = false;
            GoldFxAnimation();
            //verify death falling character
            //FallDeath();

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

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Death();
            }

            if (_deathAnimationIsPlaying)
            {
                DeathAnimation();
            }
            else if (_respawninfAnimationIsPlaying)
            {
                RespawnAnimation();
            }
            else if (_fallAnimationIsPlaying)
            {
                FallAnimation();
            }
            else if (_isDocking)
            {
                Docking();
            }
            else if (_isDocked)
            {

            }
            else if (_isLeavingDock)
            {
                LeaveDock();
            }
            else
            {

                //BOAT STATES
                this.ManageBoatMovementState();

                //UPDATE SPEED
                this.UpdateSpeedForwardForDirection();

                if (!_isDocking)
                {
                    if (!CheckLeftInvisibleWall())
                    {
                        CheckRightInvisibleWall();
                    }
                }
            }

            for (int i = 0; i < _waterTrails.Count; i++)
            {
                ParticleSystem.MainModule main = _waterTrails[i].main;
                main.startLifetime = _currentMovingSpeed / _maxMovingSpeed;
            }
            _data_Boat.ReverseReloadTransform(this.gameObject);
        }

        public void SetUpData(Data_Boat pNewData)
        {
            _data_Boat = pNewData;
            _currentMovingSpeed = _data_Boat.dStats.Speed;
            _currentLifePoint = (int)_data_Boat.dStats.Life;
        }

        //Shoot at Larboard (Babord)
        public void ShootLarboard()
        {
            if (!_larboardCannonInCooldown & !Safe)
            {
                if (this.GetComponent<Ship_Controller>() != null)
                {
                    this.GetComponent<Ship_Controller>().ResetTime();
                }
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
            if (!_starboardCannonInCooldown & !Safe)
            {
                if (this.GetComponent<Ship_Controller>() != null)
                {
                    this.GetComponent<Ship_Controller>().ResetTime();
                }
                for (int i = 0; i < _starboardCannons.Count; i++)
                {
                    _starboardCannons[i]._FireCannon();
                }
                _starboardCannonInCooldown = true;
            }
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
            _currentMovingSpeed += _accelerationSpeedForward * Time.deltaTime;
            if (_currentMovingSpeed > _maxMovingSpeed)
            {
                _currentMovingSpeed = _maxMovingSpeed;
            }
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



        public override int Damage(int _damage)
        {
            if (!Safe)
            {
                _currentLifePoint -= _damage;
                if (_currentLifePoint <= 0)
                {
                    Death();
                    return _xpEarned;
                }
            }
            return 0;
        }

        public override int Damage(int _damage, Transform pDamageLocation)
        {
            if (!Safe)
            {
                if (_damageFX != null)
                {
                    Vector3 vec = pDamageLocation.eulerAngles;
                    vec.y += 180;
                    _damageFX.transform.eulerAngles = vec;
                    vec = pDamageLocation.position;
                    vec += pDamageLocation.forward * 0.112f;
                    _damageFX.transform.position = vec;
                    _damageFX.Play();
                }
                _controller.Damage();
                _currentLifePoint -= _damage;
                if (_currentLifePoint <= 0)
                {
                    Death();
                    return _xpEarned;
                }
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
            this.GetComponent<BoxCollider>().enabled = false;
            _controller.Death();
            ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }
            _deathAnimationStartPosition = this.transform.position;
            _deathAnimationStartRotation = this.transform.eulerAngles;
            _deathAnimationEndPosition = _deathAnimationStartPosition + (_deathAnimationPositionOffset.x * this.transform.right) + (_deathAnimationPositionOffset.y * this.transform.up) + (_deathAnimationPositionOffset.z * this.transform.forward);
            _deathAnimationEndRotation = _deathAnimationStartRotation + _deathAnimationRotationOffset;
            _deathAnimationIsPlaying = true;
        }

        public void DeathAnimation()
        {
            _deathAnimationCurrentRotationTime += Time.deltaTime / _deathAnimationRotationTime;
            _deathAnimationCurrentMovementTime += Time.deltaTime / _deathAnimationMovementTime;

            if (_deathAnimationCurrentRotationTime > 0 & _deathAnimationCurrentRotationTime < 1)
            {
                this.transform.eulerAngles = Vector3.Lerp(_deathAnimationStartRotation, _deathAnimationEndRotation, _deathAnimationCurrentRotationTime);
            }
            if (_deathAnimationCurrentMovementTime > 0 & _deathAnimationCurrentMovementTime < 1)
            {
                this.transform.position = Vector3.Lerp(_deathAnimationStartPosition, _deathAnimationEndPosition, _deathAnimationCurrentMovementTime);
            }
            else if (_deathAnimationCurrentMovementTime < 0)
            {
                this.transform.position = _deathAnimationStartPosition;
            }
            else
            {
                this.transform.position = _deathAnimationEndPosition;

            }

            if (_deathAnimationCurrentRotationTime >= 1 & _deathAnimationCurrentMovementTime >= 1)
            {
                _deathAnimationIsPlaying = false;
                _deathAnimationCurrentRotationTime = -_deathAnimationRotationDelay / _deathAnimationRotationTime;
                _deathAnimationCurrentMovementTime = -_deathAnimationMovementDelay / _deathAnimationMovementTime;
                _controller.Disappear();
                this.GetComponent<BoxCollider>().enabled = true;
                _currentMovingSpeed = _maxMovingSpeed;
                _respawninfAnimationIsPlaying = true;
            }
        }

        public void Fall()
        {
            _currentLifePoint = _maxLifePoint;
            this.GetComponent<BoxCollider>().enabled = false;
            //_controller.Death();
            ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }
            _fallAnimationStartPosition = this.transform.position;
            _fallAnimationStartRotation = Controller.transform.eulerAngles;
            _fallAnimationEndPosition = _fallAnimationStartPosition + (_fallAnimationPositionOffset.x * this.transform.right) + (_fallAnimationPositionOffset.y * this.transform.up) + (_fallAnimationPositionOffset.z * Controller.transform.forward);
            float angle = Mathf.Abs(Controller.transform.eulerAngles.y);
            float offsetX = 0;
            float offsetZ = 0;
            if (angle > 270 | angle < 90)
            {
                float tempAngle = angle;
                if (tempAngle > 270)
                {
                    tempAngle -= 270;
                    tempAngle = -90 + tempAngle;
                }
                offsetX = Mathf.Abs(0 - tempAngle);
                Debug.Log("Down : " + offsetX);
                offsetX = 90 - offsetX;
                Debug.Log("Down : " + offsetX);
                offsetX /= 90;
                Debug.Log("Down : " + offsetX);
                offsetX *= -_fallAnimationRotationOffset;
                Debug.Log("Down : " + offsetX);
            }
            if (angle > 0 & angle < 180)
            {
                offsetZ = Mathf.Abs(90 - angle);
                Debug.Log("Left : " + offsetZ);
                offsetZ = 90 - offsetZ;
                Debug.Log("Left : " + offsetZ);
                offsetZ /= 90;
                Debug.Log("Left : " + offsetZ);
                offsetZ *= -_fallAnimationRotationOffset;
                //offsetZ = -80 * (angle / ((90 - (Mathf.Abs(90 - angle))) / 90));
                Debug.Log("Left : " + offsetZ);
            }
            if (angle > 90 & angle < 270)
            {
                offsetX = Mathf.Abs(180 - angle);
                Debug.Log("Up : " + offsetX);
                offsetX = 90 - offsetX;
                Debug.Log("Up : " + offsetX);
                offsetX /= 90;
                Debug.Log("Up : " + offsetX);
                offsetX *= _fallAnimationRotationOffset;
                Debug.Log("Up : " + offsetX);
            }
            if (angle > 180 & angle < 360)
            {
                offsetZ = Mathf.Abs(270 - angle);
                Debug.Log("Right : " + offsetZ);
                offsetZ = 90 - offsetZ;
                Debug.Log("Right : " + offsetZ);
                offsetZ /= 90;
                Debug.Log("Right : " + offsetZ);
                offsetZ *= _fallAnimationRotationOffset;
                Debug.Log("Right : " + offsetZ);
            }
            Debug.Log(new Vector2(offsetX, offsetZ));
            //Z Left Right
            //X Up Down
            _fallAnimationEndRotation = _fallAnimationStartRotation + (offsetX * Controller.transform.right) + (offsetZ * Controller.transform.forward);
            _fallAnimationIsPlaying = true;
        }

        public void FallAnimation()
        {
            _fallAnimationCurrentRotationTime += Time.deltaTime / _fallAnimationRotationTime;
            _fallAnimationCurrentMovementTime += Time.deltaTime / _fallAnimationMovementTime;

            if (_fallAnimationCurrentRotationTime > 0 & _fallAnimationCurrentRotationTime < 1)
            {
                //Vector3 offset = DirectionLocator.eulerAngles;
                Controller.transform.eulerAngles = Vector3.Lerp(_fallAnimationStartRotation, _fallAnimationEndRotation, _fallAnimationCurrentRotationTime);
                //offset = DirectionLocator.eulerAngles - offset;
                //this.transform.Rotate(offset);
                //DirectionLocator.Rotate(-offset);
            }
            if (_fallAnimationCurrentMovementTime > 0 & _fallAnimationCurrentMovementTime < 1)
            {
                this.transform.position = Vector3.Lerp(_fallAnimationStartPosition, _fallAnimationEndPosition, _fallAnimationCurrentMovementTime);
            }
            else if (_fallAnimationCurrentMovementTime < 0)
            {
                this.transform.position = _fallAnimationStartPosition;
            }
            else
            {
                this.transform.position = _fallAnimationEndPosition;

            }

            if (_fallAnimationCurrentRotationTime >= 1 & _fallAnimationCurrentMovementTime >= 1)
            {
                _fallAnimationIsPlaying = false;
                _fallAnimationCurrentRotationTime = -_fallAnimationRotationDelay / _fallAnimationRotationTime;
                _fallAnimationCurrentMovementTime = -_fallAnimationMovementDelay / _fallAnimationMovementTime;
                Controller.transform.eulerAngles = Vector3.zero;
                this.transform.eulerAngles = Vector3.zero;
                _controller.Disappear();
                this.GetComponent<BoxCollider>().enabled = true;
                _currentMovingSpeed = _maxMovingSpeed;
                _respawninfAnimationIsPlaying = true;
                this.GetComponent<AttractObject>()._isFalling = false;
            }
        }

        public void RespawnAnimation()
        {
            _respawnAnimationCurrentTime += Time.deltaTime;
            MoveForward();
            if (_respawnAnimationCurrentTime >= _respawnAnimationTime)
            {
                _respawninfAnimationIsPlaying = false;
                _respawnAnimationCurrentTime = 0;
            }
        }

        public void StartDocking(Dock pDock)
        {
            _dock = pDock;
            _isDocking = true;
        }
        public void Docking()
        {
            bool willWait = false;
            // Wait a bit at each checkpoint
            {
                // If the entity reached the checkpoint
                if (Vector3.Distance(this.transform.position, _dock._dockCheckpoints[_nextDockingCheckpointId].position) < _currentMovingSpeed * Time.deltaTime)
                {
                    {
                        _nextDockingCheckpointId++;
                    }

                    // Check if the last checkpoint has been reached

                    if (_nextDockingCheckpointId == _dock._dockCheckpoints.Count)
                    {
                        _nextDockingCheckpointId--;
                        _isDocking = false;
                        _isDocked = true;
                        _currentMovingSpeed = 0;
                    }
                }

                if (_nextDockingCheckpointId == _dock._dockCheckpoints.Count - 1)
                {

                    willWait = true;

                }
                GoToDestination(_dock._dockCheckpoints[_nextDockingCheckpointId].position, willWait);
                if (_currentMovingSpeed > _maxMovingSpeed / 4)
                {
                    _currentMovingSpeed = _maxMovingSpeed / 4;
                    _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
                }
            }
        }

        public void LeaveDock()
        {
            // Wait a bit at each checkpoint
            {
                // If the entity reached the checkpoint
                if (Vector3.Distance(this.transform.position, _dock._dockCheckpoints[_nextDockingCheckpointId].position) < _currentMovingSpeed * Time.deltaTime)
                {
                    {
                        _nextDockingCheckpointId--;
                    }

                    // Check if the last checkpoint has been reached

                    if (_nextDockingCheckpointId == -1)
                    {
                        _nextDockingCheckpointId = 0;
                        _isLeavingDock = false;
                    }
                }

                GoToDestination(_dock._dockCheckpoints[_nextDockingCheckpointId].position);
                if (_currentMovingSpeed > _maxMovingSpeed / 4)
                {
                    _currentMovingSpeed = _maxMovingSpeed / 4;
                    _stoppingDistance = ((_currentMovingSpeed / 10) * (_currentMovingSpeed / 10)) * 50 / _decelerationSpeedForward;
                }

            }
        }

        public void GoldFx(Vector3 pChestLocation)
        {
            if (_goldFX != null)
            {
                _goldFXStartPosition = pChestLocation;
                _goldFXEndPosition = this.transform.position;
                _goldFXIsPlaying = true;
                _goldFX.transform.position = _goldFXStartPosition;
                _goldFXCurrentTime = 0;
                _goldFX.Play();
            }
        }

        public void GoldFx(Vector3 pChestLocation, int pContainedMoney)
        {
            if (_goldFX != null)
            {
                _goldFXStartPosition = pChestLocation;
                _goldFXEndPosition = this.transform.position;
                _goldFXIsPlaying = true;
                _goldFX.transform.position = _goldFXStartPosition;
                _goldFXCurrentTime = 0;
                ParticleSystem.EmissionModule module = _goldFX.emission;
                module.rateOverTime = pContainedMoney * 25;
                _goldFX.Play();
            }
        }

        public void GoldFxAnimation()
        {
            if (_goldFXIsPlaying)
            {
                _goldFXCurrentTime += Time.deltaTime;
                _goldFXEndPosition = this.transform.position;
                _goldFX.transform.position = Vector3.Lerp(_goldFXStartPosition, _goldFXEndPosition, _goldFXCurrentTime);
            }
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
            _controller = _player;
        }

        public void SetUpBoat(Ship_Controller pController)
        {
            _controller = pController;
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
            if (_controller.GetComponent<Player>() != null)
            {
                return _controller.GetComponent<Player>()._currentXp;
            }
            else
            {
                return 0;
            }
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

        /// <summary>
        /// a ranger pour apres
        /// </summary>
        private void UpdateSpeedForwardForDirection()
        {
            //acceleration
            //if (_zInputMovement != 0 || _xInputMovement != 0)
            if (_ControllerIsMoving == true)
            {
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

        #endregion ACCESSORS

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

        public void GoToDestination(Vector3 _destination, bool pMustStopToDestination = false, float _turningAngle = 0)
        {
            // Make the DirectionLocator faces the destination
            DirectionLocator.LookAt(_destination);
            DirectionLocator.Rotate(0, _turningAngle, 0);
            Vector3 rotation = DirectionLocator.eulerAngles;
            rotation.x = Mathf.Abs(rotation.x);
            rotation.y = Mathf.Abs(rotation.y);
            rotation.z = Mathf.Abs(rotation.z);
            DirectionLocator.eulerAngles = rotation;
            //Check the distance to the destination
            if (Vector3.Distance(this.transform.position, _destination) > StoppingDistance)
            {
                //If the destination is too far, move forward
                GetComponent<BoatCharacter>().setControllerIsMoving(true);
                Accelerate();
                MoveForward();
            }
            else if (Vector3.Distance(this.transform.position, _destination) < StoppingDistance)
            {
                Debug.Log("StoppingDistance");
                if (pMustStopToDestination)
                {
                Debug.Log("MustStop");
                    if (Vector3.Distance(this.transform.position, _destination) > Deceleration * Time.deltaTime)
                    {
                Debug.Log("Decelerate");
                       setControllerIsMoving(false);
                        Decelerate();
                        MoveForward();
                    }
                    else
                    {
                Debug.Log("Stop");
                        this.transform.position = _destination;
                    }
                }
                else
                {
                    if (Vector3.Distance(this.transform.position, _destination) > getMaxSpeedForward() * Time.deltaTime)
                    {
                        setControllerIsMoving(true);
                        Accelerate();
                        MoveForward();
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
            if (Mathf.Abs(DirectionLocator.eulerAngles.y - transform.eulerAngles.y) <= GetComponent<BoatCharacter>().getRotateSpeed() * Time.deltaTime)
            {
                //If the direction is close enough, take the exact rotation to face the destination
                this.transform.eulerAngles = DirectionLocator.eulerAngles;
            }
            else if ((DirectionLocator.eulerAngles.y - transform.eulerAngles.y < 0 & DirectionLocator.eulerAngles.y - transform.eulerAngles.y > -180) | DirectionLocator.eulerAngles.y - transform.eulerAngles.y > 180)
            {
                //If the destination si to the left, turn left.
                TurnLarboard();
            }
            else
            {
                //If the destination si to the right, turn right.
                TurnStarboard();
            }
        }
    }
}
