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
        [SerializeField] public CannonHarpoon _prowCannonHarpoon;

        /// <summary>
        /// TEST SEB
        /// </summary>
        //int _maxCannonsPerSide = 2;
        //int _startCannonsNumberLeft = 1;
        //int _startCannonsNumberRight = 1;
        //int _currentCannnonsNumberLeft = 1;
        //int _currentCannnonsNumberRight = 1;
        // END TEST SEB //
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
        [SerializeField] private ParticleSystem _goldFX;

        private bool _goldFXIsPlaying = false;
        private Vector3 _goldFXStartPosition;
        private Vector3 _goldFXEndPosition;
        private float _goldFXCurrentTime = 0;

        public bool Safe = false;

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
            _deathAnimationCurrentRotationTime = -_deathAnimationRotationDelay / _deathAnimationRotationTime;
            _deathAnimationCurrentMovementTime = -_deathAnimationMovementDelay / _deathAnimationMovementTime;
        }


        bool _asUpdateDatas = false;

        void Update()
        {
            _isMovingForward = false;

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
            else
            {
                //BOAT STATES
                this.ManageBoatMovementState();
                //UPDATE SPEED
                this.UpdateSpeedForwardForDirection();
            }

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

            //if (!_isDocking)
            //{
            //    if (!CheckLeftInvisibleWall())
            //    {
            //        CheckRightInvisibleWall();
            //    }
            //}


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
                    this.CmdAddCannons(true, false);
                    if ((player._data.Boat.CurrentCanonLeft < player._data.Boat.MaxCanonPerSide))
                    {
                        player._data.Boat.CurrentCanonLeft++;
                    }

                    player.CmdSendCurrentCanonLeft(player._data.Boat.CurrentCanonLeft);

                    this.CmdUpdateActiveCanons();

                }
                if (Input.GetKeyDown(KeyCode.F6))
                {
                    if ((player._data.Boat.CurrentCanonRight < player._data.Boat.MaxCanonPerSide))
                    {
                        player._data.Boat.CurrentCanonRight++;
                    }

                    player.CmdSendCurrentCanonRight(player._data.Boat.CurrentCanonRight);

                    this.CmdUpdateActiveCanons();
                }
            }
            // END TEST
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
            player._data.Boat.Stats.Life = _maxLifePoint;
            player.CmdSendLife(player._data.Boat.Stats.Life);

            isDying = true;

            CmdAddPlank(this._plankDroppedByDeath, this.transform.position);

            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponentInParent<Player>().Death();
  

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

                this.GetComponentInParent<Player>().Disappear();
                this.GetComponent<BoxCollider>().enabled = true;

                _currentMovingSpeed = _maxMovingSpeed;
                _respawninfAnimationIsPlaying = true;
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

            if ((player._data.Boat.CurrentCanonLeft < player._data.Boat.MaxCanonPerSide) && left == true)
            {
                player._data.Boat.CurrentCanonLeft++;
            }
            if ((player._data.Boat.CurrentCanonRight < player._data.Boat.MaxCanonPerSide) && right == true)
            {
                player._data.Boat.CurrentCanonRight++;
            }
            this.RpcAddCannons(left, right);
        }

        [ClientRpc]
        public void RpcAddCannons(bool left, bool right)
        {
            if (( player._data.Boat.CurrentCanonLeft <  player._data.Boat.MaxCanonPerSide) && left == true)
            {
                 player._data.Boat.CurrentCanonLeft++;
            }
            if (( player._data.Boat.CurrentCanonRight <  player._data.Boat.MaxCanonPerSide) && right == true)
            {
                 player._data.Boat.CurrentCanonRight++;
            }
        }


        public void SetActiveCannons()
        {
            if(player == null)
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
                if (i <  player._data.Boat.CurrentCanonLeft)
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
                if (i <  player._data.Boat.CurrentCanonRight)
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
            if ( player._data.Boat.Stats.Speed == 0)
            {
                _boatMovementState = BoatMovementState.IDLE;
            }
            else
            {
                //acceleration
                //if (_currentSpeedForward > 0 && (_zInputMovement != 0 || _xInputMovement != 0))
                if ( player._data.Boat.Stats.Speed > 0 && _ControllerIsMoving == true)
                {
                    _boatMovementState = BoatMovementState.ACCELERATE;
                }
                //deceleration
                //else if (_currentSpeedForward > 0 && (_zInputMovement == 0 && _xInputMovement == 0))
                else if ( player._data.Boat.Stats.Speed > 0 && _ControllerIsMoving == false)
                {
                    _boatMovementState = BoatMovementState.DECELERATE;
                }
                //cruise_speed
                if ( player._data.Boat.Stats.Speed >= _maxMovingSpeed)
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

        //Shoot at Prow (Proue)
        public void ShootProwHarpoon()
        {
            if (!_prowdCannonHarpoonInCooldown & !Safe)
            {
                _prowCannonHarpoon._FireCannonHarpoon();
                _prowdCannonHarpoonInCooldown = true;
            }
        }

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward *  player._data.Boat.Stats.Speed * Time.deltaTime;
            //pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        public void Accelerate()
        {
            //Debug.Log(this.name + "je suis dans le accelerate");
             player._data.Boat.Stats.Speed += _accelerationSpeedForward * Time.deltaTime;
            if ( player._data.Boat.Stats.Speed > _maxMovingSpeed)
            {
                 player._data.Boat.Stats.Speed = _maxMovingSpeed;
            }
            //Debug.Log(this.name + " --> Acclerate /  player._data.Boat.dStats.Speed : " +  player._data.Boat.dStats.Speed + " _accelerationSpeedForward : " + _accelerationSpeedForward
            //+ " Time.deltaTime : " + Time.deltaTime);
            _stoppingDistance = (( player._data.Boat.Stats.Speed / 10) * ( player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
        }

        public void Decelerate()
        {
             player._data.Boat.Stats.Speed -= _decelerationSpeedForward * Time.deltaTime;
            if ( player._data.Boat.Stats.Speed < 0)
            {
                 player._data.Boat.Stats.Speed = 0;
            }
            _stoppingDistance = (( player._data.Boat.Stats.Speed / 10) * ( player._data.Boat.Stats.Speed / 10)) * 50 / _decelerationSpeedForward;
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

            if ( player._data.Boat.Stats.Speed > 0)
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
            //Debug.Log(this.name + " --> performMovement /  player._data.Boat.dStats.Speed : " +  player._data.Boat.dStats.Speed + " _currentAngularSpeed : " + _currentAngularSpeed + " this.transform.position : " + this.transform.position);
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
            return  player._data.Boat.Stats.Speed;
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
                /*if (_damageFX != null)
                {
                    Vector3 vec = pDamageLocation.eulerAngles;
                    vec.y += 180;
                    _damageFX.transform.eulerAngles = vec;
                    vec = pDamageLocation.position;
                    vec += pDamageLocation.forward * 0.112f;
                    _damageFX.transform.position = vec;
                    _damageFX.Play();
                }*/

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
    }
}
