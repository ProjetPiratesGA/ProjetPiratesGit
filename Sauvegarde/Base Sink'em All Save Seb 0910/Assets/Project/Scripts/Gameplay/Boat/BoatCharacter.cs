using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjetPirate.Controllers;
using ProjetPirate.Physic;
using ProjetPirate.Data;
using UnityEngine.Networking;
using ProjetPirate.IA;
using Project.Network;

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
        [SerializeField] private ShipType _shipType;

        //Components
        private Rigidbody _rigidbody;
        private AttractObject _attractObject;

        //isMovingForward
        private bool _isMovingForward = false;
        private float _stoppingDistance;

        private bool isDying = false;

        [SerializeField] public int _plankDroppedByDeath;
        [SerializeField] public int _moneyDroppedByDeath;

        [Header("FALL DEATH")]
        [SerializeField]
        [Range(-100, -10)]
        private float _HeightFallingDeath = -10;

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
        private Vector3 _deathAnimationRotationOffset = new Vector3(0, 0, -80);

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

        public bool _respawnUI = false;
        public bool _respawningAnimationIsPlaying = false;
        private float _respawnAnimationCurrentTime = 0;
        private float _respawnAnimationTime = 1.5f;

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
        private float _accelerationSpeedRotation = 1;
        [SerializeField]
        private float _decelerationSpeedRotation = 1;
        [SerializeField]
        private JoystickController _joystickController; // je l'utilise pour obtenir la dead zone du joystick
        [SerializeField]
        private float _angleToDestination;
        [SerializeField]
        private bool _isRotatingLarboard = false;
        [SerializeField]
        private bool _isRotatingStarboard = false;
        [SerializeField]
        private bool _isRotating = false;


        #endregion PERFORM MOVEMENT DIRECTION

        private Vector3 _currentForwardDirection;
        private Vector3 _currentForwardVelocity; // _currentVelocity

        //private float _zInputMovement; // input vertical 
        //private float _xInputMovement; // input horizontal

        private bool _ControllerIsMoving = false; // set depuis le controller (boatController ou IAController)

        [Header("CANNONS")]
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;
        [SerializeField] public CannonHarpoon _prowCannonHarpoon;
        [SerializeField] int _maxCannonsPerSide = 2;

        [SerializeField] private List<Transform> _larboardCannonPositions;
        [SerializeField] private List<Transform> _starboardCannonPositions;
        [SerializeField] private Transform _prowCannonHarpoonPosition;

        [SerializeField] private GameObject _larboardCannonPrefab;
        [SerializeField] private GameObject _starboardCannonPrefab;
        [SerializeField] private GameObject _prowCannonHarpoonPrefab;

        [SerializeField] private float _shootCooldown;

        [SerializeField] private int _defaultCannonNumberBySide;

        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;
        private bool _prowdCannonHarpoonInCooldown = false;
        private float _prowCannonHarpoonShootCooldownTime = 0;


        [SerializeField]
        private float _weakenedStateLifeRatio = 0.5f;
        [SerializeField]
        private float _endangeredStateLifeRatio = 0.1f;
        public StructureState _structureState;

        [SerializeField] public GameObject _droppedPlank;
        [SerializeField] public GameObject _droppedChest;

        private bool _goldFXIsPlaying = false;
        private Vector3 _goldFXStartPosition;
        private Vector3 _goldFXEndPosition;
        private float _goldFXCurrentTime = 0;

        public bool Safe = false;
        [SerializeField] public Dock _dock;
        private int _nextDockingCheckpointId;
        public bool _canDock = false;

        [SerializeField] bool _isDocking = false;
        [SerializeField] public bool _isDocked = false;
        [SerializeField] bool _isLeavingDock = false;
        private float _dockingAngularSpeed = 90;

        [SerializeField] bool _isPushedByIsland = false;
        [SerializeField] List<InvisibleWallPoint> _leftPoints;
        [SerializeField] List<InvisibleWallPoint> _rightPoints;

        [SerializeField] List<ParticleSystem> _waterTrails;
        [SerializeField] ParticleSystem _damageFX;
        [SerializeField] Material _damageFXColor;
        [SerializeField] private ParticleSystem _goldFX;
        [SerializeField] private TestSmokeDeath _smokeDeathFX;
        [SerializeField] private ParticleSystem _explosionFX;

        public bool _isDying
        {
            get { return isDying; }
            set { isDying = value; }
        }

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

        public List<Cannon> larboardCannons
        {
            get { return _larboardCannons; }
            set { _larboardCannons = value; }
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
        public List<Transform> LarboardCannonPositions
        {
            get { return _larboardCannonPositions; }
        }

        public GameObject DroppedChest
        {
            get { return _droppedChest; }
        }

        public List<Transform> StarboardCannonPositions
        {
            get { return _starboardCannonPositions; }
        }

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
            return player._data.Boat.Stats.Life;
        }

        void Start()
        {
            _joystickController = FindObjectOfType<JoystickController>();
            if (_joystickController == null)
            {
                Debug.Log("JoystickController Not Assigned");
            }


            _deathAnimationCurrentRotationTime = -_deathAnimationRotationDelay / _deathAnimationRotationTime;
            _deathAnimationCurrentMovementTime = -_deathAnimationMovementDelay / _deathAnimationMovementTime;

            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.gameObject.name = "DirectionLocator";
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;
            Vector3 vec = _directionLocator.position;
            vec.y = 0;
            _directionLocator.position = vec;
            vec = this.transform.position;
            vec.y = 0;
            this.transform.position = vec;

            //SEB 0910
            if(_prowCannonHarpoonPrefab !=null && hasAuthority)
            {
                this._prowCannonHarpoon.gameObject.SetActive(player._data.Boat.AsHarpoon);
                CmdSetHarpoon();
            }
            //END SEB 0910

        }


        bool _asUpdateDatas = false;
        private bool _hasExploded;

        void OnEnable()
        {
            if (_damageFX != null)
            {
                _damageFX.Stop();
                ParticleSystem.MainModule main = _damageFX.main;
                if (_damageFXColor != null)
                {
                    main.startColor = _damageFXColor.color;
                }
            }
            if (_goldFX != null)
            {
                _goldFX.Stop();
            }
            if (_explosionFX)
            {
                _explosionFX.Stop();
            }
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Death();
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                if (!_isDocked & !_isDocking)
                {
                    StartDocking();

                }
                else if (_isDocked)
                {
                    _isDocked = false;
                    _isLeavingDock = true;
                }
            }

            if (_deathAnimationIsPlaying)
            {
                DeathAnimation();
            }
            else if (_respawningAnimationIsPlaying)
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
                this.ManageBoatRotationState();

                //UPDATE SPEED
                this.UpdateSpeedForwardForDirection();
                this.UpdateSpeedRotation();

            }

            GoldFxAnimation();
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
            _prowCannonHarpoonShootCooldownTime += Time.deltaTime;
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
            if (_prowCannonHarpoonShootCooldownTime > _shootCooldown)
            {
                _prowdCannonHarpoonInCooldown = false;
                _prowCannonHarpoonShootCooldownTime = 0;
            }

            if (!_isDocking)
            {
                if (!CheckLeftInvisibleWall())
                {
                    CheckRightInvisibleWall();
                }
            }

            for (int i = 0; i < _waterTrails.Count; i++)
            {
                ParticleSystem.MainModule main = _waterTrails[i].main;
                main.startLifetime = player._data.Boat.Stats.Speed / _maxMovingSpeed;
            }

            //for (int i = 0; i < _waterTrails.Count; i++)
            //{
            //    ParticleSystem.MainModule main = _waterTrails[i].main;
            //    main.startLifetime = _data_Boat.dStats.Speed / _maxMovingSpeed;
            //}
            player._data.Boat.UpdateTransform(this.gameObject);


            //TEST DEBUG ADD CANNON
            if (this.hasAuthority)
            {
                if (Input.GetKeyDown(KeyCode.F5))
                {
                    if ((player._data.Boat.CurrentCanonLeft < _maxCannonsPerSide))
                    {
                        player._data.Boat.CurrentCanonLeft++;
                    }

                    player.CmdSendCurrentCanonLeft(player._data.Boat.CurrentCanonLeft);

                    this.CmdUpdateActiveCanons();

                }
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    if ((player._data.Boat.CurrentCanonRight < _maxCannonsPerSide))
                    {
                        player._data.Boat.CurrentCanonRight++;
                    }

                    player.CmdSendCurrentCanonRight(player._data.Boat.CurrentCanonRight);

                    this.CmdUpdateActiveCanons();
                }
                //SEB 0910
                if (Input.GetKeyDown(KeyCode.F7))
                {

                    player._data.Boat.AsHarpoon = true;
                    player.CmdSendHarpoon(player._data.Boat.AsHarpoon);
                    this._prowCannonHarpoon.gameObject.SetActive(player._data.Boat.AsHarpoon);
                    CmdSetHarpoon();
                }
                //END SEB 0910
            }
            // END TEST
        }

        //SEB 08
        [Command]
        public void CmdUpdatePosition(GameObject boat)
        {
            player._data.Boat.UpdateTransform(boat);
            RpcUpdatePosition(boat);
        }

        [ClientRpc]
        public void RpcUpdatePosition(GameObject boat)
        {
            player._data.Boat.UpdateTransform(boat);
        }
        //FIN SEB 08


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

        [Command]
        public void CmdDestroyPlank(GameObject _plank)
        {
            List<PlankOnSea> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().plankList;

            tempList.Remove(_plank.GetComponent<PlankOnSea>());
            Destroy(_plank);
        }

        [Command]
        private void CmdAddPlank(int nbPlank, Vector3 _position)
        {
            //NetworkServer.SpawnWithClientAuthority(plank.gameObject, this.connectionToClient);

            List<PlankOnSea> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().plankList;

            for (int i = 0; i < nbPlank; i++)
            {
                GameObject plank = Instantiate(_droppedPlank);

                plank.transform.position = _position + new Vector3(0, 0.712f, 0);
                plank.GetComponent<PlankOnSea>().SetDestination();

                tempList.Add(plank.GetComponent<PlankOnSea>());

                //TargetSpawnPlank(this.connectionToClient, plank.gameObject);
                NetworkServer.Spawn(plank);
            }
        }

        public override void Death()
        {
            AudioManager.Stop(this.gameObject.GetComponent<AudioSource>());
            AudioManager.Play(this.gameObject.GetComponent<AudioSource>(), "DeathBoat");

            player._data.Boat.Stats.Life = _maxLifePoint;
            player.CmdSendLife(player._data.Boat.Stats.Life);

            isDying = true;

            CmdAddPlank(this._plankDroppedByDeath, this.transform.position);

            this.GetComponent<BoxCollider>().enabled = false;
            player.Death();

            /*ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }*/

            _deathAnimationStartPosition = this.transform.position;
            _deathAnimationStartRotation = this.transform.eulerAngles;
            _deathAnimationEndPosition = _deathAnimationStartPosition + (_deathAnimationPositionOffset.x * this.transform.right) + (_deathAnimationPositionOffset.y * this.transform.up) + (_deathAnimationPositionOffset.z * this.transform.forward);
            _deathAnimationEndRotation = _deathAnimationStartRotation + _deathAnimationRotationOffset;
            _deathAnimationIsPlaying = true;

            if (_smokeDeathFX != null)
            {
                _smokeDeathFX.Play();
            }
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

            if (_deathAnimationCurrentMovementTime > 0.8f & !_hasExploded)
            {
                if (_explosionFX != null)
                {
                    _explosionFX.transform.position = this.transform.position;
                    _explosionFX.Play();
                }
                _hasExploded = true;
            }

            if (_deathAnimationCurrentRotationTime >= 1 & _deathAnimationCurrentMovementTime >= 1)
            {
                _deathAnimationIsPlaying = false;
                _deathAnimationCurrentRotationTime = -_deathAnimationRotationDelay / _deathAnimationRotationTime;
                _deathAnimationCurrentMovementTime = -_deathAnimationMovementDelay / _deathAnimationMovementTime;

                this.player.Disappear();
                this.GetComponent<BoxCollider>().enabled = true;

                player._data.Boat.Stats.Speed = _maxMovingSpeed;
                _respawnUI = true;
                //_respawningAnimationIsPlaying = true;
                _hasExploded = false;
            }
        }

        public void Fall()
        {
            Debug.Log("Start");
            this.GetComponent<BoxCollider>().enabled = false;
            player.Death();
            ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }
            _fallAnimationStartPosition = this.transform.position;
            _fallAnimationStartRotation = player.transform.eulerAngles;
            _fallAnimationEndPosition = _fallAnimationStartPosition + (_fallAnimationPositionOffset.x * this.transform.right) + (_fallAnimationPositionOffset.y * this.transform.up) + (_fallAnimationPositionOffset.z * player.transform.forward);
            float angle = Mathf.Abs(player.transform.eulerAngles.y);
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
            _fallAnimationEndRotation = _fallAnimationStartRotation + (offsetX * player.transform.right) + (offsetZ * player.transform.forward);
            _fallAnimationIsPlaying = true;
            Debug.Log("End");
        }

        public void FallAnimation()
        {
            Debug.Log("Anim");
            _fallAnimationCurrentRotationTime += Time.deltaTime / _fallAnimationRotationTime;
            _fallAnimationCurrentMovementTime += Time.deltaTime / _fallAnimationMovementTime;

            if (_fallAnimationCurrentRotationTime > 0 & _fallAnimationCurrentRotationTime < 1)
            {
                //Vector3 offset = DirectionLocator.eulerAngles;
                player.transform.eulerAngles = Vector3.Lerp(_fallAnimationStartRotation, _fallAnimationEndRotation, _fallAnimationCurrentRotationTime);
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
                player.transform.eulerAngles = Vector3.zero;
                this.transform.eulerAngles = Vector3.zero;
                player.Disappear();
                this.GetComponent<BoxCollider>().enabled = true;
                _currentMovingSpeed = _maxMovingSpeed;
                _respawnUI = true;
                this.GetComponent<AttractObject>()._isFalling = false;
                player._data.Boat.Stats.Life = _maxLifePoint;
            }
        }

        public void RespawnAnimation()
        {
            _respawnAnimationCurrentTime += Time.deltaTime;
            MoveForward();
            if (_respawnAnimationCurrentTime >= _respawnAnimationTime)
            {
                _respawningAnimationIsPlaying = false;
                _respawnAnimationCurrentTime = 0;
            }
        }

        public void StartDocking()
        {
            if (_dock != null)
            {
                if (!_isDocked & !_isDocking)
                {
                    this.gameObject.layer = 9;
                    _dock._isAvailable = false;
                    _isDocking = true;
                }
                else if (_isDocked)
                {
                    _isDocked = false;
                    _isLeavingDock = true;
                }
            }

        }
        public void Docking()
        {
            bool willWait = false;
            // Wait a bit at each checkpoint
            {
                // If the entity reached the checkpoint
                if (Vector3.Distance(this.transform.position, _dock._dockCheckpoints[_nextDockingCheckpointId].position) < _maxMovingSpeed/* * Time.deltaTime*/)
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
                        player._data.Boat.Stats.Speed = 0;
                    }
                }

                if (_nextDockingCheckpointId == _dock._dockCheckpoints.Count - 1)
                {

                    willWait = true;

                }
                GoToDestination(_dock._dockCheckpoints[_nextDockingCheckpointId].position, willWait);
                if (player._data.Boat.Stats.Speed > _maxMovingSpeed / 4)
                {
                    player._data.Boat.Stats.Speed = _maxMovingSpeed / 4;
                    _stoppingDistance = ((player._data.Boat.Stats.Speed / 10) * (player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
                }
            }
        }

        public void LeaveDock()
        {
            // Wait a bit at each checkpoint
            {
                // If the entity reached the checkpoint
                if (Vector3.Distance(this.transform.position, _dock._dockCheckpoints[_nextDockingCheckpointId].position) < _maxMovingSpeed)
                {
                    {
                        _nextDockingCheckpointId--;
                    }

                    // Check if the last checkpoint has been reached

                    if (_nextDockingCheckpointId == -1)
                    {
                        _nextDockingCheckpointId = 0;
                        _isLeavingDock = false;
                        _dock._isAvailable = true;
                        this.gameObject.layer = 0;
                    }
                }

                GoToDestination(_dock._dockCheckpoints[_nextDockingCheckpointId].position);
                if (player._data.Boat.Stats.Speed > _maxMovingSpeed / 4)
                {
                    player._data.Boat.Stats.Speed = _maxMovingSpeed / 4;
                    _stoppingDistance = ((player._data.Boat.Stats.Speed / 10) * (player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
                }

            }
        }

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

        [Command]
        public void CmdSetUpBoat(GameObject player)
        {

            this.gameObject.transform.SetParent(player.transform);
            //this.transform.localPosition = new Vector3(0, 0, 0);
            //_controller = player.GetComponent<Controller>();
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

            if ((player._data.Boat.CurrentCanonLeft < _maxCannonsPerSide) && left == true)
            {
                player._data.Boat.CurrentCanonLeft++;
            }
            if ((player._data.Boat.CurrentCanonRight < _maxCannonsPerSide) && right == true)
            {
                player._data.Boat.CurrentCanonRight++;
            }
            this.RpcAddCannons(left, right);
        }

        [ClientRpc]
        public void RpcAddCannons(bool left, bool right)
        {
            if ((player._data.Boat.CurrentCanonLeft < _maxCannonsPerSide) && left == true)
            {
                player._data.Boat.CurrentCanonLeft++;
            }
            if ((player._data.Boat.CurrentCanonRight < _maxCannonsPerSide) && right == true)
            {
                player._data.Boat.CurrentCanonRight++;
            }
        }

        //SEB 0910
        [Command]
        public void CmdSetHarpoon()
        {
            this._prowCannonHarpoon.gameObject.SetActive(player._data.Boat.AsHarpoon);
        }

        [ClientRpc]
        public void RpcSetHarpoon()
        {
            this._prowCannonHarpoon.gameObject.SetActive(player._data.Boat.AsHarpoon);
        }
        //END SEB 0910


        public void SetActiveCannons()
        {
            if (player == null)
            {
                Debug.Log("PLAYER NULL");
                Debug.Break();
            }
            else
                Debug.Log("PLAYER OK");

            if (player._data == null)
            {
                Debug.Log("DATA NULL");
                Debug.Break();
            }
            else
                Debug.Log("DATA OK");

            if (player._data.Boat == null)
            {
                Debug.Log("DATA_BOAT NULL");
                Debug.Break();
            }
            else
                Debug.Log("DATA_BOAT OK");

            for (int i = 0; i < _larboardCannons.Count; i++)
            {
                if (i < player._data.Boat.CurrentCanonLeft)
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
                if (i < player._data.Boat.CurrentCanonRight)
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
        public void TargetSetParent(NetworkConnection target, GameObject player)
        {
            this.gameObject.transform.SetParent(player.transform);
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
            if (player._data.Boat.Stats.Speed == 0)
            {
                _boatMovementState = BoatMovementState.IDLE;
            }
            else
            {
                //acceleration
                //if (_currentSpeedForward > 0 && (_zInputMovement != 0 || _xInputMovement != 0))
                if (player._data.Boat.Stats.Speed > 0 && _ControllerIsMoving == true)
                {
                    _boatMovementState = BoatMovementState.ACCELERATE;
                }
                //deceleration
                //else if (_currentSpeedForward > 0 && (_zInputMovement == 0 && _xInputMovement == 0))
                else if (player._data.Boat.Stats.Speed > 0 && _ControllerIsMoving == false)
                {
                    _boatMovementState = BoatMovementState.DECELERATE;
                }
                //cruise_speed
                if (player._data.Boat.Stats.Speed >= _maxMovingSpeed)
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
                if (_isRotatingLarboard)
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
            /*
            for (int i = 0; i < _larboardCannons.Count; i++)
            {
                //TEST SEB (Condition if seulement)
                if (_larboardCannons[i].gameObject.activeSelf)
                {
                    _larboardCannons[i].FireCannon();
                }
            }
            */
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
            /*
            for (int i = 0; i < _starboardCannons.Count; i++)
            {
                //TEST SEB (Condition if seulement)
                if (_starboardCannons[i].gameObject.activeSelf)
                {
                    _starboardCannons[i].FireCannon();
                }
            }
            */
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

        //Shoot at Prow (Proue)
        public void ShootProwHarpoon()
        {
            if (!_prowdCannonHarpoonInCooldown & !Safe)
            {
                _prowCannonHarpoon._FireCannonHarpoon();
                _prowdCannonHarpoonInCooldown = true;
            }
        }



        #region MOVEMENTS

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * player._data.Boat.Stats.Speed * Time.deltaTime;
            //pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        public void Accelerate()
        {
            //Debug.Log(this.name + "je suis dans le accelerate");
            player._data.Boat.Stats.Speed += _accelerationSpeedForward * Time.deltaTime;
            if (player._data.Boat.Stats.Speed > _maxMovingSpeed)
            {
                player._data.Boat.Stats.Speed = _maxMovingSpeed;
            }
            //Debug.Log(this.name + " --> Acclerate /  player._data.Boat.Stats.Speed : " +  player._data.Boat.Stats.Speed + " _accelerationSpeedForward : " + _accelerationSpeedForward
            //+ " Time.deltaTime : " + Time.deltaTime);
            _stoppingDistance = ((player._data.Boat.Stats.Speed / 10) * (player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
        }

        public void Decelerate()
        {
            player._data.Boat.Stats.Speed -= _decelerationSpeedForward * Time.deltaTime;
            if (player._data.Boat.Stats.Speed < 0)
            {
                player._data.Boat.Stats.Speed = 0;
            }
            _stoppingDistance = ((player._data.Boat.Stats.Speed / 10) * (player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
        }

        /// <summary>
        /// a ranger pour apres
        /// </summary>
        private void UpdateSpeedForwardForDirection()
        {
            //acceleration
            //if (_zInputMovement != 0 || _xInputMovement != 0)
            //Debug.Log(this.name + " --> UpdateSpeedForwardForDirection / _ControllerIsMoving" + _ControllerIsMoving);
            if (_ControllerIsMoving == true)
            {
                //Debug.Log(this.name + "je suis passere");
                Accelerate();
            }
            //deceleration speed
            //else if (this.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() == null)
            if (_ControllerIsMoving == false)
            {
                Decelerate();
            }

            if (player._data.Boat.Stats.Speed > 0)
            {
                _isMovingForward = true;
            }
            else
            {
                _isMovingForward = false;
            }
        }

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
            if ((_angleToDestination < 0 && _angleToDestination > -180)
                && _currentAngularSpeed > 0)
            {
                _isRotatingLarboard = true;
            }
            else
                _isRotatingLarboard = false;
            if ((_angleToDestination > 0 && _angleToDestination < 180)
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


        }

        private void AccelerateRotation()
        {
            _currentAngularSpeed += _accelerationSpeedRotation * Time.deltaTime;
            //Debug.Log("AccelerateRotation : _currentAngularSpeed  " + _currentAngularSpeed + " _accelerationSpeedRotation : " + _accelerationSpeedRotation);
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

        public void PerformMovement(float pInputVertical, float pInputHorizontal)
        {
            //define direction with the inputs
            if (pInputHorizontal > _joystickController._joystickDeadZone || pInputHorizontal < -_joystickController._joystickDeadZone
                        || pInputVertical > _joystickController._joystickDeadZone || pInputVertical < -_joystickController._joystickDeadZone)
            {
                //LA DIRECTION EST MAINTENANT SEULEMENT UTILISER POUR LA ROTATION CAR LE MOUVEMENT DU BATEAU SE FAIT TOUJOURS VERS L'AVANT
                _normalizeTarget_MovementDirection = new Vector3(pInputHorizontal, 0, pInputVertical);
                _targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
                _direction_MovementDirection = _targetPosition_MovementDirection - this.transform.position;

                //je set l'angle a la direction ici
                _angleToDestination = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);

            }
            else
            {
                _angleToDestination = Vector3.SignedAngle(this.transform.forward, this.transform.forward, this.transform.up);
            }
            //VELOCITY
            //_currentVelocity_MovementDirection = this.transform.forward * _currentSpeedForward * Time.deltaTime;
            //if (_attractObject._isFalling == false)
            //{
            MoveForward();
            //this.transform.position += _currentVelocity_MovementDirection;
            //}

            //ROTATION
            //check the direction of the destination (with angle)

            //check if the forward direction of the boat is near the target direction
            if (Mathf.Abs(_angleToDestination) <= _maxAngularSpeed * Time.deltaTime)
            {
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
                if (!_isPushedByIsland)
                {
                    this.TurnLarboard();
                }
            }
            //turn right
            else if (_angleToDestination > 0 && _angleToDestination < 180)
            {
                if (!_isPushedByIsland)
                {
                    this.TurnStarboard();
                }

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
            return player._data.Boat.Stats.Speed;
        }

        public float getMaxSpeedForward()
        {
            return _maxMovingSpeed;
        }

        public float getRotateSpeed()
        {
            return _maxAngularSpeed;
        }

        public int getCurrentXp()
        {
            if (this.GetComponent<BoatController>().player != null)
            {
                return (int)this.GetComponent<BoatController>().player._data.Ressource.Reputation;
            }
            else
            {
                return 0;
            }
        }

        //public float getShootCoolDown()
        //{
        //    return _shootCooldown;
        //}

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

        public void SetUpBoat(Ship_Controller pController)
        {
            _controller = pController;
        }

        public override int Damage(int _damage)
        {
            if (!Safe)
            {
                player._data.Boat.Stats.Life -= _damage;
                if (player._data.Boat.Stats.Life <= 0)
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

                player._data.Boat.Stats.Life -= _damage;
                player.CmdSendLife(player._data.Boat.Stats.Life);
                if (player._data.Boat.Stats.Life <= 0)
                {
                    Death();
                    return _xpEarned;
                }
            }
            return 0;
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
                        Debug.Log("Accelerate");
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
                //TurnLarboard();
                //player._data.Boat.Stats.Speed = 0;
                if (_isMovingForward)
                {
                    this.transform.Rotate(0, -_dockingAngularSpeed * Time.deltaTime, 0);
                }
            }
            else
            {
                //If the destination si to the right, turn right.
                if (_isMovingForward)
                {
                    this.transform.Rotate(0, +_dockingAngularSpeed * Time.deltaTime, 0);
                }
                //player._data.Boat.Stats.Speed = 0;
            }
        }
    }
}
