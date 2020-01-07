using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Controllers;


namespace ProjetPirate.Boat
{

    /// <summary>
    /// use for the future animations of the boat
    /// </summary>
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
    /// this class posess the functions for the movement of the boat
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(FloatingObject))]
    public class BoatMovement : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField] bool _isDocking = false;
        [SerializeField] bool _isPushedByIsland = false;
        [SerializeField] List<InvisibleWallPoint> _leftPoints;
        [SerializeField] List<InvisibleWallPoint> _rightPoints;

        #region MANAGE COLLISION
        [Header("MANAGE COLLISION")]
        [SerializeField]
        private bool BoatCollide = false; // true if the boat collide an object
        [SerializeField]
        private LayerMask _layerMaskSea; //Made to ignore raycast collision with the sea
        private Vector3 _directionVerifyOnTheSea;
        private Vector3 _travelVerifyOnTheSea;
        private Vector3 _startPositionVerifyOnTheSea;
        [SerializeField]
        private float _distanceVerifyOnTheSea = 10f;
        [SerializeField]
        private Transform[] _DetectionCollisionTransform;

        private struct DetectionCollisionStruct
        {
            public Transform _transform;
            public bool _isCollide;
        }
        [Header("JUST TO SEE")]
        [SerializeField]
        private DetectionCollisionStruct[] _detectionCollisionStruct;

        #endregion MANAGE COLLISION

        #region PERFORM MOVEMENT DIRECTION

        [Header("PERFORM MOVEMENT FOR DIRECTION")]
        [SerializeField]
        Vector3 _normalizeTarget_MovementDirection;
        [SerializeField]
        Vector3 _targetPosition_MovementDirection;
        [SerializeField]
        Vector3 _direction_MovementDirection;
        [SerializeField]
        Vector3 _currentVelocity_MovementDirection;


        [Header("FORWARD MOVEMENT FOR DIRECTION")]
        [SerializeField]
        [Range(0f, 25f)]
        private float _maxSpeedForwardForDirection = 10f;
        [SerializeField]
        private float _minSpeedForwardForDirection = 0f;
        [SerializeField]
        private float _currentSpeedForwardForDirection = 0f; // VITESSE VERS L'AVANT
        [SerializeField]
        private float _accelerationSpeedForwardForDirection = 1;
        [SerializeField]
        private float _decelerationSpeedForwardForDirection = 1;

        [Header("ANGLE VARIANCE FOR DIRECTION")]
        [SerializeField]
        private float _angleSpeed = 0.1f;
        [SerializeField]
        private JoystickController _joystickController;
        //[SerializeField]
        //private float _currentAngle_ForDirection; // angle entre le forward et la direction voulue
        //[SerializeField]
        //private float _currentAngleVariance_ForDirection;
        //[SerializeField]
        //private float _maxAngleVariance_ForDirection = 1f; // maximum d'angle pour tourner par frame (essayer entre 0.1f et 1f sinon c'est trop rapide)
        //[SerializeField]
        //private float _minAngleVariance_ForDirection = 0f;
        //[SerializeField]
        //private float _accelerationAngleVariance_ForDirection = 0.05f;
        //[SerializeField]
        //private bool ResetAngleVarianceWantedLeft_ForDireciton = false; // (pour l'orientation gauche) false when the player controle the angle, true when the angle must be reset to 0 (when the player don't touch)
        //[SerializeField]
        //private bool ResetAngleVarianceWantedRight_ForDirection = false; // (pour l'orientation droite) false when the player controle the angle, true when the angle must be reset to 0 (when the player don't touch)



        #endregion PERFORM MOVEMENT DIRECTION

        //OLD MOVEMENTS
        #region OLD MOVEMENTS
        //[Header("OLD")]//to delete
        //[SerializeField]
        //private float _movementTreshold = 10.0f;
        //[SerializeField]
        //private float _speed;
        //private float _currentSpeed;
        //[SerializeField]
        //private float _steerSpeed = 1.0f; // VITESSE DE ROTATION
        //private float _steerFactor; //use with the horizontal input

        #endregion OLD MOVEMENTS

        //FORWARD MOVEMENT
        #region FORWARD MOVEMENT
        //FORWARD MOVEMENT
        [Header("FORWARD MOVEMENT")]
        [SerializeField]
        [Range(0f, 50f)]
        private float _maxSpeedForward = 10f;
        [SerializeField]
        private float _minSpeedForward = 0f;
        //[SerializeField]
        private float _currentSpeedForward = 0f; // VITESSE VERS L'AVANT
        [SerializeField]
        [Range(0.01f, 1f)]
        private float _accelerationSpeedForward = 0.1f;
        [SerializeField]
        [Range(0.01f, 1f)]
        private float _decelerationSpeedForward = 0.1f;


        private float _zInputMovement; // input vertical récupérer
        private Vector3 _currentForwardDirection;
        private Vector3 _currentForwardVelocity; // _currentVelocity

        #endregion FORWARD MOVEMENT

        //ROTATION MOVEMENT
        #region ROTATION MOVEMENT
        [Header("ROTATION MOVEMENT (DON'T TOUCH IN EDITOR)")]
        [SerializeField]
        private BoatMovementState _boatMovementState;
        [SerializeField]
        private BoatRotationState _boatRotationState;

        //DEPRECIATE
        [Header("ROTATION (SPEED)")]
        //[SerializeField]
        //private Vector3 _currentVelocityRotation;
        //[SerializeField]
        //private Vector3 _currentDirectionRotation;
        //[SerializeField]
        //private float _maxSpeedRotation = 10f;
        //private float _maxSpeedRotationUpdating;
        //[SerializeField]
        //private float _minSpeedRotation = 0f;
        //[SerializeField]
        //private float _increaseValueSpeedRotation = 0.1f;
        //[SerializeField]
        //private float _decreaseValueSpeedRotation = 0.1f;
        //[SerializeField]
        //private float _currentSpeedRotation = 1f; // ROTATION SPEED

        //NOTE PERSO J'AI CHANGER DE CONFIGURATION SPEED --> ANGLE (car m'orienter avec le up du bateau alors que je vais tout droit je ne comprend pas)
        [Header("ROTATION (ANGLE)")]
        [SerializeField]
        [Range(0f, 2f)]
        private float _maxAngleVarianceWanted; // angleVariance is the angle add at each frame for the rotation on the y axe
        //[SerializeField]
        private float _currentAngleVarianceWanted;

        [SerializeField]
        [Range(0.01f, 0.1f)]
        private float _angleAcceleration;
        //[SerializeField]
        private Vector3 _targetRotation; // traget Rotation 
        private float _angleRotationFactor = 100f;


        private float _xInputMovement;
        //[SerializeField]
        private bool ResetAngleVarianceWantedLeft = false; // (pour l'orientation gauche) false when the player controle the angle, true when the angle must be reset to 0 (when the player don't touch)
        //[SerializeField]
        private bool ResetAngleVarianceWantedRight = false; // (pour l'orientation droite) false when the player controle the angle, true when the angle must be reset to 0 (when the player don't touch)

        #endregion ROTATION MOVEMENT

        // Use this for initialization
        void Start()
        {
            //LAYER MASK
            //_layerMaskSea = 10;

            //DETECTION COLLISION TAB
            _detectionCollisionStruct = new DetectionCollisionStruct[_DetectionCollisionTransform.Length];
            for (int i = 0; i < _detectionCollisionStruct.Length; i++)
            {
                //je dois instancier les struct
                _detectionCollisionStruct[i]._transform = _DetectionCollisionTransform[i];
                _detectionCollisionStruct[i]._isCollide = false;
            }

            //BOAT MOVEMENT STATE
            _boatMovementState = BoatMovementState.IDLE;

            //BOAT ROTATION STATE
            _boatRotationState = BoatRotationState.FORWARD;

            //RIGIDBODY
            _rigidbody = this.GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY;

            
        }

        private void Update()
        {
            //Debug.Log("BoatMovement --> Update" + "zInput : " + _zInputMovement);
            //Debug.Log("BoatMovement --> Update" + "xInput : " + _xInputMovement);

            //BOAT STATES
            this.ManageBoatMovementState();
            //this.ManageBoatRotationState();

            //UPDATES SPEED
            //this.UpdateSpeedForward();
            //this.UpdateSpeedRotation();
            this.UpdateSpeedForwardForDirection();

            //UPDATE ANGLE
            //this.UpdateAngle();
            //this.UpdateAngleForDirection();

            //TEST
            //set the maxSpeedRotation proportionate to the maxSpeedForward 
            //_maxSpeedRotationUpdating = _currentSpeedForward * _maxSpeedForward / 2; 

            //COLLISIONS
            //this.VerifyCollide();
            //this.ManageCollide();

            //if (!_isDocking)
            //{
            //    if (!CheckLeftInvisibleWall())
            //    {
            //        CheckRightInvisibleWall();
            //    }
            //}

        }

        #region COLLISIONS

        private void VerifyCollide()
        {
            if (_detectionCollisionStruct.Length > 0)
            {
                for (int i = 0; i < _detectionCollisionStruct.Length; i++)
                {
                    _startPositionVerifyOnTheSea = _detectionCollisionStruct[i]._transform.position;
                    _directionVerifyOnTheSea = -_detectionCollisionStruct[i]._transform.up; // verify under the boat
                    _travelVerifyOnTheSea = _directionVerifyOnTheSea * _distanceVerifyOnTheSea;

                    Ray ray = new Ray(_startPositionVerifyOnTheSea, _directionVerifyOnTheSea);
                    RaycastHit hitInfo;

                    Debug.DrawRay(_startPositionVerifyOnTheSea, _travelVerifyOnTheSea, Color.red);

                    _layerMaskSea = ~(1 << LayerMask.NameToLayer("Sea"));
                    if (Physics.Raycast(ray, out hitInfo, _distanceVerifyOnTheSea, _layerMaskSea))
                    {
                        //Debug.Log("THE DETECTION RAYCAST : " + i + " TOUCH : " + hitInfo.collider.tag);
                        if (hitInfo.collider.tag == "Island")
                        {
                            //Debug.Log("THE DETECTION RAYCAST: " + i + " TOUCH ISLAND");
                            BoatCollide = true;
                            _detectionCollisionStruct[i]._isCollide = true;
                        }
                    }
                    else
                    {
                        _detectionCollisionStruct[i]._isCollide = false;
                    }

                    //Debug.Log("_detectionCollisionStruct : " + i + " _isCollide" + _detectionCollisionStruct[i]._isCollide);
                }
            }
            else
            {
                //Debug.LogError("_detectionCollisionStruct.Length est null");
            }

            //_startPositionVerifyOnTheSea = this.transform.position;
            //_directionVerifyOnTheSea = -this.transform.up; // verify under the boat
            //_travelVerifyOnTheSea = _directionVerifyOnTheSea * _distanceVerifyOnTheSea;

            //Ray ray = new Ray(_startPositionVerifyOnTheSea, _directionVerifyOnTheSea);
            //RaycastHit hitInfo;

            //Debug.DrawRay(_startPositionVerifyOnTheSea, _travelVerifyOnTheSea, Color.red);

            //_layerMaskSea = ~(1 << LayerMask.NameToLayer("Sea"));
            //if (Physics.Raycast(ray, out hitInfo, _distanceVerifyOnTheSea, _layerMaskSea))
            //{
            //    Debug.Log("TOUCH : " + hitInfo.collider.tag);
            //    if (hitInfo.collider.tag == "Island")
            //    {
            //        Debug.Log("TOUCH ISLAND ");
            //        BoatOnTheSea = true;
            //    }
            //}

        }

        private void ManageCollide()
        {
            if (BoatCollide == true)
            {
                if (_detectionCollisionStruct.Length > 0)
                {
                    for (int i = 0; i < _detectionCollisionStruct.Length; i++)
                    {
                        if (_detectionCollisionStruct[i]._isCollide == true)
                        {
                            //calcul the direction to apply force
                            Vector3 directionVector = (_detectionCollisionStruct[i]._transform.position - this.transform.position).normalized;
                            Vector3 travelVector = directionVector * 100;
                            Debug.DrawRay(_detectionCollisionStruct[i]._transform.position, directionVector, Color.magenta);
                            //Calcul the force vector
                            //Vector3 forceVector = 
                        }
                    }
                }

            }
        }

        #endregion COLLISIONS

        #region MANAGE STATE

        private void ManageBoatMovementState()
        {
            #region MANAGE BOAT MOVEMENT STATE

            //use the input to define the state of the boat
            if (_currentSpeedForward == 0)
            {
                _boatMovementState = BoatMovementState.IDLE;
            }
            else
            {
                if (_currentSpeedForward > 0 && _zInputMovement == 0)
                {
                    _boatMovementState = BoatMovementState.DECELERATE;
                }

                if (_currentSpeedForward > 0 && _zInputMovement != 0)
                {
                    _boatMovementState = BoatMovementState.ACCELERATE;
                }

                if (_currentSpeedForward >= _maxSpeedForward)
                {
                    _boatMovementState = BoatMovementState.CRUISE_SPEED;
                }
            }



            #endregion MANAGE BOAT MOVEMENT STATE
        }

        private void ManageBoatRotationState()
        {
            #region MANAGE BOAT ROTATION STATE

            //A REVOIR POUR L'ORIENTATION
            //FORWARD

            //SPEED
            //#region TEST 1 & 2
            //if (_currentSpeedRotation == 0)
            //{
            //    _boatRotationState = BoatRotationState.FORWARD;
            //}
            //else if(_currentSpeedRotation > 0)
            //{

            //    //TRIBORD
            //    if (_xInputMovement > 0)
            //    {
            //        _boatRotationState = BoatRotationState.TRIBORD;
            //    }
            //    //BABORD
            //    else if(_xInputMovement < 0)
            //    {
            //        _boatRotationState = BoatRotationState.BABORD;
            //    }
            //}
            //#endregion TEST 1 & 2

            //ANGLE
            #region TEST 3

            //FORWARD
            if (_currentAngleVarianceWanted == 0)
            {
                _boatRotationState = BoatRotationState.FORWARD;
            }
            //TRIBORD
            if (_currentAngleVarianceWanted > 0)
            {
                _boatRotationState = BoatRotationState.TRIBORD;
            }
            //BABORD
            else if (_currentAngleVarianceWanted < 0)
            {
                _boatRotationState = BoatRotationState.BABORD;
            }


            #endregion TEST 3

            //Debug.Log(" ROTATION STATE : " + _boatRotationState);

            #endregion MANAGE BOAT ROTATION STATE
        }

        #endregion MANAGE STATE

        #region UPDATE SPEED

        private void UpdateSpeedForward()
        {
            //acceleration
            if ((_zInputMovement > 0 || _zInputMovement < 0) && (_xInputMovement > 0 || _xInputMovement < 0))
            {
                if (_currentSpeedForward <= _maxSpeedForward)
                {
                    _currentSpeedForward += _accelerationSpeedForward;
                }
            }
            //deceleration speed
            else
            {
                if (_currentSpeedForward > _minSpeedForward)
                {
                    _currentSpeedForward -= _decelerationSpeedForward;
                }

            }

            //secure speed
            if (_currentSpeedForward < 0)
            {
                _currentSpeedForward = 0;
            }
        }



        private void UpdateSpeedRotation()
        {
            ////acceleration (si j'avance et que je tourne)
            //if (_zInputMovement > 0 && (_xInputMovement > 0 || _xInputMovement < 0))
            //{
            //    if (_currentSpeedRotation <= _maxSpeedRotationUpdating)
            //    {
            //        _currentSpeedRotation += _increaseValueSpeedRotation;
            //    }
            //}
            ////deceleration speed
            //else
            //{
            //    if (_currentSpeedRotation > _minSpeedRotation)
            //    {
            //        _currentSpeedRotation -= _decreaseValueSpeedRotation;
            //    }

            //}

            ////secure speed
            //if (_currentSpeedRotation < 0)
            //{
            //    _currentSpeedRotation = 0;
            //}
        }

        #endregion UPDATE SPEED

        #region UPDATE ANGLE

        private void UpdateAngle()
        {
            //CALCULATE ANGLE VARIANCE
            #region CALCULATE ANGLE VARIANCE
            if ((_zInputMovement > 0 || _zInputMovement < 0) && (_xInputMovement > 0 || _xInputMovement < 0))
            {
                if (_xInputMovement > 0)
                {
                    //_targetDirection = Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up) * _rigidbody.rotation.eulerAngles;
                    //Debug.Log("Quaternion.AngleAxis : " + Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up).eulerAngles);
                    //Debug.Log("_rigidbody.rotation.eulerAngles : " + _rigidbody.rotation.eulerAngles);

                    //INCREASE ANGLE
                    if (_currentAngleVarianceWanted < _maxAngleVarianceWanted)
                    {
                        _currentAngleVarianceWanted += _angleAcceleration;
                    }
                    //secure
                    if (_currentAngleVarianceWanted > _maxAngleVarianceWanted)
                    {
                        _currentAngleVarianceWanted = _maxAngleVarianceWanted;
                    }
                }
                else if (_xInputMovement < 0)
                {
                    //_targetDirection = Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up) * _rigidbody.rotation.eulerAngles;
                    //_targetDirection = Quaternion.AngleAxis(-_angleVarianceWanted, Vector3.up).eulerAngles;

                    //DECREASE ANGLE
                    if (_currentAngleVarianceWanted > -_maxAngleVarianceWanted)
                    {
                        _currentAngleVarianceWanted -= _angleAcceleration;
                    }
                    //secure
                    if (_currentAngleVarianceWanted < -_maxAngleVarianceWanted)
                    {
                        _currentAngleVarianceWanted = -_maxAngleVarianceWanted;
                    }
                }

                //Set booleans to false because wa want to secure the angle when the player not move the boat
                ResetAngleVarianceWantedLeft = false;
                ResetAngleVarianceWantedRight = false;
            }
            else
            {
                if (_currentAngleVarianceWanted > 0)
                {
                    ResetAngleVarianceWantedRight = true;
                }
                else if (_currentAngleVarianceWanted < 0)
                {
                    ResetAngleVarianceWantedLeft = true;
                }
            }

            //SECURE ANGLE RESET LEFT
            if (ResetAngleVarianceWantedLeft == true)
            {
                if (_currentAngleVarianceWanted < 0)
                {
                    _currentAngleVarianceWanted += _angleAcceleration;
                }
                else if (_currentAngleVarianceWanted > 0)
                {
                    _currentAngleVarianceWanted = 0;
                    ResetAngleVarianceWantedLeft = false;
                }
            }
            //SECURE ANGLE RESET RIGHT
            if (ResetAngleVarianceWantedRight == true)
            {
                if (_currentAngleVarianceWanted > 0)
                {
                    _currentAngleVarianceWanted -= _angleAcceleration;
                }
                if (_currentAngleVarianceWanted < 0)
                {
                    _currentAngleVarianceWanted = 0;
                    ResetAngleVarianceWantedRight = false;
                }
            }

            #endregion CALCULATE ANGLE VARIANCE
        }

        #endregion UPDATE ANGLE

        #region EN TEST FOR DIRECTION
        /// <summary>
        /// a ranger pour apres
        /// </summary>
        private void UpdateSpeedForwardForDirection()
        {
            //acceleration
            if (_zInputMovement != 0 || _xInputMovement != 0)
            {
                if (_currentSpeedForwardForDirection <= _maxSpeedForwardForDirection)
                {
                    _currentSpeedForwardForDirection += _accelerationSpeedForwardForDirection;
                }
            }
            //deceleration speed
            else
            {
                if (_currentSpeedForwardForDirection > _minSpeedForwardForDirection)
                {
                    _currentSpeedForwardForDirection -= _decelerationSpeedForwardForDirection;
                }

            }

            //secure speed
            if (_currentSpeedForwardForDirection < 0)
            {
                _currentSpeedForwardForDirection = 0;
            }
        }

        private void UpdateAngleForDirection()
        {
            //CALCULATE ANGLE VARIANCE
            #region CALCULATE ANGLE VARIANCE
            //if (_zInputMovement != 0 || _xInputMovement != 0)
            //{
            //    if (_xInputMovement > 0)
            //    {
            //        //_targetDirection = Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up) * _rigidbody.rotation.eulerAngles;
            //        //Debug.Log("Quaternion.AngleAxis : " + Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up).eulerAngles);
            //        //Debug.Log("_rigidbody.rotation.eulerAngles : " + _rigidbody.rotation.eulerAngles);

            //        //INCREASE ANGLE
            //        if (_currentAngleVariance_ForDirection < _maxAngleVariance_ForDirection)
            //        {
            //            _currentAngleVariance_ForDirection += _accelerationAngleVariance_ForDirection;
            //        }
            //        //secure
            //        if (_currentAngleVariance_ForDirection > _maxAngleVariance_ForDirection)
            //        {
            //            _currentAngleVariance_ForDirection = _maxAngleVariance_ForDirection;
            //        }
            //    }
            //    else if (_xInputMovement < 0)
            //    {
            //        //_targetDirection = Quaternion.AngleAxis(+_angleVarianceWanted, Vector3.up) * _rigidbody.rotation.eulerAngles;
            //        //_targetDirection = Quaternion.AngleAxis(-_angleVarianceWanted, Vector3.up).eulerAngles;

            //        //DECREASE ANGLE
            //        if (_currentAngleVariance_ForDirection > -_maxAngleVariance_ForDirection)
            //        {
            //            _currentAngleVariance_ForDirection -= _accelerationAngleVariance_ForDirection;
            //        }
            //        //secure
            //        if (_currentAngleVariance_ForDirection < -_maxAngleVariance_ForDirection)
            //        {
            //            _currentAngleVariance_ForDirection = -_maxAngleVariance_ForDirection;
            //        }
            //    }

            //    //Set booleans to false because wa want to secure the angle when the player not move the boat
            //    ResetAngleVarianceWantedLeft_ForDireciton = false;
            //    ResetAngleVarianceWantedRight_ForDirection = false;
            //}
            //else
            //{
            //    if (_currentAngleVariance_ForDirection > 0)
            //    {
            //        ResetAngleVarianceWantedRight_ForDirection = true;
            //    }
            //    else if (_currentAngleVariance_ForDirection < 0)
            //    {
            //        ResetAngleVarianceWantedLeft_ForDireciton = true;
            //    }
            //}

            ////SECURE ANGLE RESET LEFT
            //if (ResetAngleVarianceWantedLeft_ForDireciton == true)
            //{
            //    if (_currentAngleVariance_ForDirection < 0)
            //    {
            //        _currentAngleVariance_ForDirection += _accelerationAngleVariance_ForDirection;
            //    }
            //    else if (_currentAngleVariance_ForDirection > 0)
            //    {
            //        _currentAngleVariance_ForDirection = 0;
            //        ResetAngleVarianceWantedLeft_ForDireciton = false;
            //    }
            //}
            ////SECURE ANGLE RESET RIGHT
            //if (ResetAngleVarianceWantedRight_ForDirection == true)
            //{
            //    if (_currentAngleVariance_ForDirection > 0)
            //    {
            //        _currentAngleVariance_ForDirection -= _accelerationAngleVariance_ForDirection;
            //    }
            //    if (_currentAngleVariance_ForDirection < 0)
            //    {
            //        _currentAngleVariance_ForDirection = 0;
            //        ResetAngleVarianceWantedRight_ForDirection = false;
            //    }
            //}

            #endregion CALCULATE ANGLE VARIANCE


        }


        public void PerformMovementDirection(float pInputVertical, float pInputHorizontal)
        {
            _xInputMovement = pInputHorizontal;
            _zInputMovement = pInputVertical;

            _normalizeTarget_MovementDirection = new Vector3(pInputHorizontal, 0, pInputVertical);
            //define target
            _targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
            //_targetPosition_MovementDirection = this.transform.position + _normalizeTarget_MovementDirection;
            //CALCUL DIRECTION
            _direction_MovementDirection = _targetPosition_MovementDirection - this.transform.position;

            //CALCUL VELOCITY
            _currentVelocity_MovementDirection = this.transform.forward * _currentSpeedForwardForDirection * Time.deltaTime;

            //move the position by the velocity
            this.transform.position += _currentVelocity_MovementDirection;

            //ROTATION
            //this.transform.LookAt(_targetPosition_MovementDirection);

            //check the direction of the destination (with angle)
            //float angle = Mathf.Abs(_direction_MovementDirection.);
            //float angle = Vector3.Angle(this.transform.forward, _direction_MovementDirection);
            float angle = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);
            //Debug.Log("angle : " + angle);

            if (!_isPushedByIsland)
            {
                if (Mathf.Abs(angle) <= _angleSpeed * Time.deltaTime)
                {
                    if (_joystickController != null)
                    {
                        if (_xInputMovement > _joystickController._joystickDeadZone || _xInputMovement < -_joystickController._joystickDeadZone
                            || _zInputMovement > _joystickController._joystickDeadZone || _zInputMovement < -_joystickController._joystickDeadZone)
                        {
                            this.transform.LookAt(_targetPosition_MovementDirection);
                        }
                    }
                }
                else if (angle < 0 && angle > -180)
                {
                    this.TurnLarboard();
                }
                else if (angle > 0 && angle < 180)
                {
                    this.TurnStarboard();
                }
            }




            //SignedAngle pour obtenir l'angle négatif
            //_currentAngle_ForDirection = Vector3.SignedAngle(this.transform.forward, _direction_MovementDirection, this.transform.up);
            //if (_currentAngle_ForDirection > 0 || _currentAngle_ForDirection < 0)
            //{
            //    //il faut tourner a droite
            //    if (_currentAngle_ForDirection > 0)
            //    {
            //        _currentAngleVariance_ForDirection += _accelerationAngleVariance_ForDirection;
            //    }

            //    Vector3 rotationToApply = this.transform.rotation.eulerAngles + new Vector3(0, _currentAngleVariance_ForDirection, 0);

            //    //this.transform.Rotate(rotationToApply);

            //    Debug.Log("PerformMovementDirection / "
            //        + " this.transform.rotation.eulerAngles : " + this.transform.rotation.eulerAngles
            //        + " Vector3 : " + new Vector3(0, _currentAngle_ForDirection, 0)
            //        + " rotationToApply : " + rotationToApply
            //        + " _currentAngle_ForDirection : " + _currentAngle_ForDirection
            //        );
            //    //_rigidbody.MoveRotation(Quaternion.Euler(_rigidbody.rotation.eulerAngles + _targetRotation));
            //}


            //_rigidbody.MovePosition(this.transform.position + _currentForwardVelocity);
            //Debug.Log("BoatMovement --> PerformMovementDirection / " 
            //     + " _targetPosition_MovementDirection : " + _targetPosition_MovementDirection
            //     + " _direction_MovementDirection" + _direction_MovementDirection
            //     + " _currentVelocity_MovementDirection" + _currentVelocity_MovementDirection
            //     + " this.transform.position : " + this.transform.position);

            //       Debug.Log("PerformMovementDirection / "
            //+ " _targetPosition_MovementDirection : " + _targetPosition_MovementDirection
            //+ " this.transform.position : " + this.transform.position
            //);
        }

        /// <summary>
        /// turn left (babord)
        /// </summary>
        private void TurnLarboard()
        {
            this.transform.Rotate(0, -_angleSpeed * Time.deltaTime, 0);
        }

        /// <summary>
        /// turn right (starboard)
        /// </summary>
        private void TurnStarboard()
        {
            this.transform.Rotate(0, +_angleSpeed * Time.deltaTime, 0);
        }

        private void OnDrawGizmos()
        {
            //draw sphere at the target position
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(this.transform.position + this.transform.forward * 10, 2f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(this.transform.position + _direction_MovementDirection * 10, 2f);
        }
        #endregion  EN TEST FOR DIRECTION

        #region MOVEMENTS
        /// <summary>
        /// Mouvement vers l'avant
        /// </summary>
        /// <param name="pInputVertical"></param>
        public void PerformMovement(float pInputVertical)
        {
            //GET MOVEMENT (do a separate function)
            _zInputMovement = pInputVertical;


            //DEFINE DIRECTION
            _currentForwardDirection = this.transform.forward;

            //CALCUL VELOCITY
            _currentForwardVelocity = _currentForwardDirection * _currentSpeedForward * Time.deltaTime;

            //move the position by the velocity
            _rigidbody.MovePosition(_rigidbody.position + _currentForwardVelocity);
        }

        public void PerformRotation(float pInputHorizontal)
        {
            //GET MOVEMENT (do a separate function)
            _xInputMovement = pInputHorizontal;

            //DEFINE ROTATION (Direction of the rotation)
            //the boat can rotate only if the speed of the boat is superior to 0
            if (_currentSpeedForward > 0)
            {
                //WITH INPUT
                #region TEST 1
                //Compute Direcion
                //if(_xInputMovement > 0 || _xInputMovement < 0)
                //{
                //    _currentRotation = _xInputMovement * this.transform.up;
                //}
                //else
                //{
                //    _currentRotation = this.transform.up;
                //}
                //_currentVelocityRotation = _currentDirectionRotation * _currentSpeedRotation * Time.deltaTime;
                #endregion TEST 1

                //TRANSFORM.UP
                #region TEST 2
                //pour l'instant ça fonctionne mais a modifier car c'ets pas clair de prendre le up pour modifier la rotation
                //définir un angle target affiché la direction de l'angle target par rapport a 0, 0 étant le forward
                //if(_xInputMovement > 0)
                //{
                //    _currentRotation = this.transform.up;
                //}
                //else if(_xInputMovement < 0)
                //{
                //    _currentRotation = -this.transform.up;
                //}
                //_currentVelocityRotation = _currentDirectionRotation * _currentSpeedRotation * Time.deltaTime;
                #endregion TEST 2
            }

            //TARGET DIRECTION
            #region TEST 3

            ///CALCULATE TARGET ROTATION
            if (_currentAngleVarianceWanted > 0)
            {
                _targetRotation = Quaternion.AngleAxis(_currentAngleVarianceWanted * _angleRotationFactor * Time.deltaTime, Vector3.up).eulerAngles;
            }
            else if (_currentAngleVarianceWanted < 0)
            {
                _targetRotation = Quaternion.AngleAxis(_currentAngleVarianceWanted * _angleRotationFactor * Time.deltaTime, Vector3.up).eulerAngles;
            }
            else
            {
                _targetRotation = new Vector3(0, 0, 0);
            }

            #endregion TEST 3

            //APPLY ROTATION
            //test 1 & 2
            //_rigidbody.MoveRotation(Quaternion.Euler(_currentVelocityRotation));

            //test 3 (TARGET ROTATION)
            _rigidbody.MoveRotation(Quaternion.Euler(_rigidbody.rotation.eulerAngles + _targetRotation));


        }




        #region OLD FUNCTIONS

        ///// <summary>
        ///// *DEPRECIATE
        ///// Mouvement vers l'avant
        ///// </summary>
        ///// <param name="pInputVertical"></param>
        //public void Movement(float pInputVertical)
        //{
        //    //Calcul the current speed
        //    _currentSpeed = Mathf.Lerp(_currentSpeed, pInputVertical, Time.deltaTime);

        //    Debug.Log("BoatMovement --> Movement / pInputVertical : " + pInputVertical
        //        + " _movementFactor : " + _currentSpeed);

        //    //this.transform.position += new Vector3(0, 0, _movementFactor * _speed);
        //    this.transform.position += this.transform.forward * (_currentSpeed * _speed);

        //    //Debug.Log("VerticalInput : " + _verticalInput + " _movementFactor : " + _movementFactor + " _movementTreshold : " + _movementTreshold);
        //}

        ///// <summary>
        ///// *DEPRECIATE
        ///// mouvement de rotation
        ///// </summary>
        ///// <param name="pInputHorizontal"></param>
        //public void Steer(float pInputHorizontal)
        //{

        //    //Computes
        //    _steerFactor = Mathf.Lerp(_steerFactor, pInputHorizontal, Time.deltaTime / _movementTreshold);
        //    transform.Rotate(0.0f, _steerFactor * _steerSpeed, 0.0f);
        //}

        #endregion OLD FUNCTIONS

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
            return _currentSpeedForward;
        }

        public float getMaxSpeedForward()
        {
            return _maxSpeedForward;
        }

        #endregion ACCESSORS

        private bool CheckLeftInvisibleWall()
        {
            for (int i = 0; i < _leftPoints.Count; i++)
            {
                if (_leftPoints[i].InvisibleWallIsOn())
                {
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
    }
}
