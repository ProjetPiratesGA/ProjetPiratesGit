using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Tools;
using ProjetPirate.Data;

namespace ProjetPirate.Boat
{

    public class AnimationBoat : MonoBehaviour
    {        

        /// <summary>
        /// envoyer le game Object ou est le script boatMovement
        /// </summary>
        private BoatCharacter _boatCharacter;

        [SerializeField]
        private Transform _transformParent; // must be set with the parent object
        private Transform _transformMesh;
        [SerializeField] private bool _canDoAnimation; // false si le bateau ne doit pas faire les animation (quand il n'y a pas de transform de parent ou de script BoatCharacter dans le parent

        //animation Idle
        [Header("(JUST TO SEE)")]
        [SerializeField] private Vector3 _currentAngleRotation; //utiliser pour vérifier les valeurs de this.transform.rotation.eulerAngles
        [SerializeField] private BoatRotationState _tempBoatRotationState_ForRoll_Z; // a utiliser seulement pour sauvegarder le boat RotationState pour la fonction de rollZ
        [SerializeField] private BoatRotationState _tempBoatRotationState_ForRoll_X; // a utiliser seulement pour sauvegarder le boat RotationState pour la fonction de rollX

        [Header("CURRENT_TARGET_Z(JUST TO SEE)")]
        [SerializeField] private float _currentTargetInclinaison_Z;
        [SerializeField] private bool _currentCanVerifyReachTargetInclinaison_Z = true; // doit être a true quand le reach targetInclinaison peut être vérifié // doit être reste a false quand on change de BoatRotationState
        [SerializeField] private bool _currentReachTargetInclinaison_Z; // doit être a true quand la current target est atteinte
        [SerializeField] private float _tempTargetAngleForRoll_Z;

        [Header("CURRENT_TARGET_X(JUST TO SEE)")]
        [SerializeField] private float _currentTargetInclinaison_X;
        [SerializeField] private bool _currentCanVerifyReachTargetInclinaison_X = true; // doit être a true quand le reach targetInclinaison peut être vérifié // doit être reste a false quand on change de BoatRotationState
        [SerializeField] private bool _currentReachTargetInclinaison_X; // doit être a true quand la current target est atteinte
        [SerializeField] private float _tempTargetAngleForRoll_X;

        [Header("CURRENT_ROLL_Z(JUST TO SEE)")]
        [SerializeField] private float _currentAngleMaxInclinaison_Z;
        [SerializeField] private float _currentRollSpeed_Z;
        [SerializeField] private bool _currentIsRotating_Larboard;
        [SerializeField] private bool _currentIsRotating_Starboard;

        [Header("CURRENT_ROLL_X(JUST TO SEE)")]
        [SerializeField] private float _currentAngleMaxInclinaison_X;
        [SerializeField] private float _currentRollSpeed_X;
        [SerializeField] private bool _currentIsRotating_Forward;
        [SerializeField] private bool _currentIsRotating_Backward;

        //LES VARIABLES POUR L'EFFET DE TANGAGE
        #region ROLL MOVEMENT
        [Header("ROLL_IDLE_MOVEMENT_Z")]
        [SerializeField] private float _RollIdleMovement_AngleMaxInclinaison_Z; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollIdleMovement_SpeedRotation_Z; // speed de rotation
        private float _RollIdleMovement_TargetInclinaison_Z = 0; // doit être set a 0 au démarrage car c'est la target de l'idle
        private bool _RollIdleMovement_IsRotatingLarboard_Z; // true quand rotation vers la gauche (babord)
        private bool _RollIdleMovement_IsRotatingStarboard_Z; // true quand rotation vers la droite (tribord)


        [Header("ROLL_FORWARD_MOVEMENT_Z")]
        [SerializeField] private float _RollForwardMovement_AngleMaxInclinaison_Z; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollForwardMovement_SpeedRotation_Z; // speed de rotation
        private float _RollForwardMovement_TargetInclinaison_Z = 0; // doit être set a 0 au démarrage car c'est la target de l'idle
        private bool _RollForwardMovement_IsRotatingLarboard_Z; // true quand rotation vers la gauche (babord)
        private bool _RollForwardMovement_IsRotatingStarboard_Z; // true quand rotation vers la droite (tribord)

        [Header("ROLL_LARBOARD_MOVEMENT_Z")]
        [SerializeField] private float _RollLarboardMovement_AngleMaxInclinaison_Z; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollLarboardMovement_SpeedRotation_Z; // speed de rotation
        private float _RollLarboardMovement_TargetInclinaison_Z; // correspond dans l'animation au centre de la target
        private bool _RollLarboardMovement_IsRotatingLarboard_Z; // true quand rotation vers la gauche (babord)
        private bool _RollLarboardMovement_IsRotatingStarboard_Z; // true quand rotation vers la droite (tribord)

        [Header("ROLL_STARBOARD_MOVEMENT_Z")]
        [SerializeField] private float _RollStarboardMovement_AngleMaxInclinaison_Z; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollStarboardMovement_SpeedRotation_Z; // speed de rotation
        private float _RollStarboardMovement_TargetInclinaison_Z; // correspond dans l'animation au centre de la target
        private bool _RollStarboardMovement_IsRotatingLarboard_Z; // true quand rotation vers la gauche (babord)
        private bool _RollStarboardMovement_IsRotatingStarboard_Z; // true quand rotation vers la droite (tribord)

        [Header("ROLL_IDLE_MOVEMENT_X")]
        [SerializeField] private float _RollIdleMovement_AngleMaxInclinaison_X; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollIdleMovement_SpeedRotation_X; // speed de rotation 
        private float _RollIdleMovement_TargetInclinaison_X = 0; // doit être set a 0 au démarrage car c'est la target de l'idle
        private bool _RollIdleMovement_IsRotatingForward_X; // true quand rotation vers l'avant
        private bool _RollIdleMovement_IsRotatingBackward_X; // true quand rotation vers l'arrière

        [Header("ROLL_FORWARD_MOVEMENT_X")]
        [SerializeField] private float _RollForwardMovement_AngleMaxInclinaison_X; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollForwardMovement_SpeedRotation_X; // speed de rotation 
        private float _RollForwardMovement_TargetInclinaison_X; // // correspond dans l'animation au centre de la target
        private bool _RollForwardMovement_IsRotatingForward_X; // true quand rotation vers l'avant
        private bool _RollForwardMovement_IsRotatingBackward_X; // true quand rotation vers l'arrière

        [Header("ROLL_LARBOARD_MOVEMENT_X")]
        [SerializeField] private float _RollLarboardMovement_AngleMaxInclinaison_X; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollLarboardMovement_SpeedRotation_X; // speed de rotation 
        private float _RollLarboardMovement_TargetInclinaison_X; // // correspond dans l'animation au centre de la target
        private bool _RollLarboardMovement_IsRotatingForward_X; // true quand rotation vers l'avant
        private bool _RollLarboardMovement_IsRotatingBackward_X; // true quand rotation vers l'arrière

        [Header("ROLL_STARBOARD_MOVEMENT_X")]
        [SerializeField] private float _RollStarboardMovement_AngleMaxInclinaison_X; // angle d'inclinaison pour le roll autour de la cible target
        [SerializeField] private float _RollStarboardMovement_SpeedRotation_X; // speed de rotation 
        private float _RollStarboardMovement_TargetInclinaison_X; // // correspond dans l'animation au centre de la target
        private bool _RollStarboardMovement_IsRotatingForward_X; // true quand rotation vers l'avant
        private bool _RollStarboardMovement_IsRotatingBackward_X; // true quand rotation vers l'arrière

        #endregion ROLL MOVEMENT

        #region TARGET

        //Z

        [Header("TARGET_FORWARD_Z")]
        [SerializeField] private float _Forward_SpeedRotationToTarget_Z; // vitesse a laquelle la rotation du bateau s'effectue pour revenir au centre
        [SerializeField] private bool _Forward_ReachAngleInclinaisonTarget_Z; // true quand le bateau a atteint l'angle d'inclinaison // doit repssaer a false quand je ne suis pas en BoatRotationState Forward
        private float _Forward_TargetInclinaison_Z = 0; // doit être set a 0 au début

        [Header("TARGET_LARBOARD_Z")]
        [SerializeField] private float _Larboard_SpeedRotationToTarget_Z; // vitesse a laquelle la rotation du bateau s'effectue pour aller a la target larboard
        [SerializeField] private bool _Larboard_ReachAngleInclinaisonTarget_Z; // true quand le bateau a atteint l'angle d'inclinaison // doit repaaser a false quand je ne suis pas en BoatRotationState Babord
        [Range(0, 20)]
        [SerializeField] private float _Larboard_TargetInclinaison_Z; // centre d'orientation du bateau en position Larboard

        [Header("TARGET_STARBOARD_Z")]
        [SerializeField] private float _Starboard_SpeedRotationToTarget_Z; // vitesse a laquelle la rotation du bateau s'effectue pour aller a la target starboard
        [SerializeField] private bool _Starboard_ReachAngleInclinaisonTarget_Z; // true quand le bateau a atteint l'angle d'inclinaison // doit repssaer a false quand je ne suis pas en BoatRotationState Tribord
        [Range(360, 340)]
        [SerializeField] private float _Starboard_TargetInclinaison_Z; // centre d'orientation du bateau en position starboard

        //X

        [Header("TARGET_FORWARD_X")]
        [SerializeField] private float _Forward_SpeedRotationToTarget_X; // vitesse a laquelle la rotation du bateau s'effectue pour revenir au centre
        [SerializeField] private bool _Forward_ReachAngleInclinaisonTarget_X; // true quand le bateau a atteint l'angle d'inclinaison // doit repssaer a false quand je ne suis pas en BoatRotationState Forward
        private float _Forward_TargetInclinaison_X = 0; // doit être set a 0 au début

        [Header("TARGET_LARBOARD_X")]
        [SerializeField] private float _Larboard_SpeedRotationToTarget_X; // vitesse a laquelle la rotation du bateau s'effectue pour aller a la target larboard
        [SerializeField] private bool _Larboard_ReachAngleInclinaisonTarget_X; // true quand le bateau a atteint l'angle d'inclinaison // doit repaaser a false quand je ne suis pas en BoatRotationState Babord
        [Range(0, 20)]
        [SerializeField] private float _Larboard_TargetInclinaison_X; // centre d'orientation du bateau en position Larboard

        [Header("TARGET_STARBOARD_Z")]
        [SerializeField] private float _Starboard_SpeedRotationToTarget_X; // vitesse a laquelle la rotation du bateau s'effectue pour aller a la target starboard
        [SerializeField] private bool _Starboard_ReachAngleInclinaisonTarget_X; // true quand le bateau a atteint l'angle d'inclinaison // doit repssaer a false quand je ne suis pas en BoatRotationState Tribord
        [Range(360, 340)]
        [SerializeField] private float _Starboard_TargetInclinaison_X; // centre d'orientation du bateau en position starboard

        #endregion TARGET

        // Use this for initialization
        void Start()
        {

            //SET VARIABLES
            _canDoAnimation = true;

            //REFERENCES (PARENT)
            //if (this.GetComponent<BoatCharacter>() != null)
            //{
            //    _boatCharacter = this.GetComponent<BoatCharacter>();
            //}
            //else
            //{
            //    Debug.LogError("AnimationBoat --> Start / there is no BoatCharacter");
            //    _canDoAnimation = false;
            //}

            //REFERENCES (ENFANT)
            if (this.GetComponentInParent<BoatCharacter>() != null)
            {
                _boatCharacter = this.GetComponentInParent<BoatCharacter>();
            }
            else
            {
                Debug.LogError("AnimationBoat --> Start / there is no BoatCharacter in parent");
                _canDoAnimation = false;
            }
            if (_transformParent == null)
            {
                Debug.LogError("AnimationBoat --> Start / transformParent est null");
                _canDoAnimation = false;
            }

            _transformMesh = this.GetComponent<Transform>();
            Debug.Log("Transform Name : " + this.GetComponent<Transform>().name);


        }

        // Update is called once per frame
        void Update()
        {
            if (_canDoAnimation == true)
            {
                //get the current Rotation
                _currentAngleRotation = this.transform.rotation.eulerAngles;

                //I only want positive numbers
                if (_currentAngleRotation.z < 0)
                {
                    _currentAngleRotation.z = MathTools.invert(_currentAngleRotation.z);
                    //Debug.Log("AnimationBoat --> Update / convert into positive : " + _currentAngleRotation.z);
                }
                if (_currentAngleRotation.x < 0)
                {
                    _currentAngleRotation.x = MathTools.invert(_currentAngleRotation.x);
                    //Debug.Log("AnimationBoat --> Update / convert into positive : " + _currentAngleRotation.x);
                }
                Debug.Log(this.name + " this.transform.rotation.eulerAngles : " + this.transform.rotation.eulerAngles + " _currentAngleRotation : " + _currentAngleRotation);

                ///En priorité je cale la rotation du bateau sur la target et aprés je fais le roll
                switch (_boatCharacter.getBoatRotationState())
                {
                    case BoatRotationState.FORWARD:
                        //si je suis en forward je rotate mon bateau en target forward
                        this.AnimationTargetForward();

                        //si je suis en IDLE je fais l'animation de roll
                        if (_boatCharacter.getBoatMovementState() == BoatMovementState.IDLE)
                        {
                            this.AnimationRollIdleMovement();
                        }
                        //si je suis pas en Idle je fais l'autre animation (la même mais avec d'autres valeurs)
                        else if (_boatCharacter.getBoatMovementState() == BoatMovementState.ACCELERATE
                        || _boatCharacter.getBoatMovementState() == BoatMovementState.DECELERATE
                        || _boatCharacter.getBoatMovementState() == BoatMovementState.CRUISE_SPEED)
                        {
                            this.AnimationRollForwardMovement();
                        }
                        break;
                    case BoatRotationState.BABORD:
                        //si je suis en BABORD je rotate mon bateau en target Larboard
                        this.AnimationTargetLarboard();
                        //je ne dois pas être en idle pour faire l'animation de roll larboard
                        if (_boatCharacter.getBoatMovementState() != BoatMovementState.IDLE)
                        {
                            this.AnimationRollLarboardMovement();
                        }

                        break;
                    case BoatRotationState.TRIBORD:
                        //si je suis en TRIBORD je rotate mon bateau en target Starboard
                        this.AnimationTargetStarboard();
                        //je ne dois pas être en idle pour faire l'animation de roll starboard
                        if (_boatCharacter.getBoatMovementState() != BoatMovementState.IDLE)
                        {
                            this.AnimationRollStarboardMovement();
                        }
                        break;
                    default:
                        break;
                }

                //RESET BOOLEANS
                if (_boatCharacter.getBoatRotationState() != BoatRotationState.FORWARD)
                {
                    _Forward_ReachAngleInclinaisonTarget_Z = false;
                    _Forward_ReachAngleInclinaisonTarget_X = false;
                }
                if (_boatCharacter.getBoatRotationState() != BoatRotationState.BABORD)
                {
                    _Larboard_ReachAngleInclinaisonTarget_Z = false;
                    _Larboard_ReachAngleInclinaisonTarget_X = false;
                }
                if (_boatCharacter.getBoatRotationState() != BoatRotationState.TRIBORD)
                {
                    _Starboard_ReachAngleInclinaisonTarget_Z = false;
                    _Starboard_ReachAngleInclinaisonTarget_X = false;
                }

                //A COMMENTEZ EN PARENT
                Vector3 _rotationeulerToApply = new Vector3(
                    _transformMesh.eulerAngles.x,
                    _transformParent.eulerAngles.y,
                    _transformMesh.eulerAngles.z);

                _transformMesh.eulerAngles = _rotationeulerToApply;

                _boatCharacter.Data_Boat.ChildTransform.Rotation = new myVector3(_rotationeulerToApply.x, _rotationeulerToApply.y, _rotationeulerToApply.z);
            }
        }


        #region ANIMATION ROLL

        private void AnimationRollIdleMovement()
        {
            //_RollIdleMovement_TargetInclinaison_Z = _Idle_TargetInclinaison_Z // mettre un _Idle_TargetInclinaison_Z
            this.RollZ(_RollIdleMovement_SpeedRotation_Z, _RollIdleMovement_AngleMaxInclinaison_Z, _RollIdleMovement_TargetInclinaison_Z);
            this.RollX(_RollIdleMovement_SpeedRotation_X, _RollIdleMovement_AngleMaxInclinaison_X, _RollForwardMovement_TargetInclinaison_X);
        }

        private void AnimationRollForwardMovement()
        {
            //_RollForwardMovement_TargetInclinaison_Z = _Forward_TargetInclinaison_Z // mettre un _Forward_TargetInclinaison_Z
            this.RollZ(_RollForwardMovement_SpeedRotation_Z, _RollForwardMovement_AngleMaxInclinaison_Z, _RollForwardMovement_TargetInclinaison_Z);
            this.RollX(_RollForwardMovement_SpeedRotation_X, _RollForwardMovement_AngleMaxInclinaison_X, _RollForwardMovement_TargetInclinaison_X);
        }

        private void AnimationRollLarboardMovement()
        {
            _RollLarboardMovement_TargetInclinaison_Z = _Larboard_TargetInclinaison_Z;
            _RollLarboardMovement_TargetInclinaison_X = _Larboard_TargetInclinaison_X;
            this.RollZ(_RollLarboardMovement_SpeedRotation_Z, _RollLarboardMovement_AngleMaxInclinaison_Z, _RollLarboardMovement_TargetInclinaison_Z);
            this.RollX(_RollLarboardMovement_SpeedRotation_X, _RollLarboardMovement_AngleMaxInclinaison_X, _RollLarboardMovement_TargetInclinaison_X);
        }

        private void AnimationRollStarboardMovement()
        {
            _RollStarboardMovement_TargetInclinaison_Z = _Starboard_TargetInclinaison_Z;
            _RollStarboardMovement_TargetInclinaison_X = _Starboard_TargetInclinaison_X;
            this.RollZ(_RollStarboardMovement_SpeedRotation_Z, _RollStarboardMovement_AngleMaxInclinaison_Z, _RollStarboardMovement_TargetInclinaison_Z);
            this.RollX(_RollStarboardMovement_SpeedRotation_X, _RollStarboardMovement_AngleMaxInclinaison_X, _RollStarboardMovement_TargetInclinaison_X);

        }

        #endregion ANIMATION IDLE

        #region ANIMATION TARGET

        private void AnimationTargetLarboard()
        {
            //TARGET_LARBOARD_Z
            #region TARGET_LARBOARD_Z
            //si je n'ai pas atteint la cible
            if (_Larboard_ReachAngleInclinaisonTarget_Z == false)
            {
                //je rotate
                this.RotateToTargetZ(_Larboard_TargetInclinaison_Z, _Larboard_SpeedRotationToTarget_Z);
            }
            //vérification Target
            if (_currentAngleRotation.z < _Larboard_TargetInclinaison_Z + _Larboard_SpeedRotationToTarget_Z
                && _currentAngleRotation.z > _Larboard_TargetInclinaison_Z - _Larboard_SpeedRotationToTarget_Z)
            {
                _Larboard_ReachAngleInclinaisonTarget_Z = true;
            }
            #endregion TARGET_LARBOARD_Z

            //TARGET_LARBOARD_X
            #region TARGET_LARBOARD_X
            //si je n'ai pas atteint la cible
            if (_Larboard_ReachAngleInclinaisonTarget_X == false)
            {
                //je rotate
                this.RotateToTargetX(_Larboard_TargetInclinaison_X, _Larboard_SpeedRotationToTarget_X);
            }
            //vérification Target
            if (_currentAngleRotation.x < _Larboard_TargetInclinaison_X + _Larboard_SpeedRotationToTarget_X
                && _currentAngleRotation.x > _Larboard_TargetInclinaison_X - _Larboard_SpeedRotationToTarget_X)
            {
                _Larboard_ReachAngleInclinaisonTarget_X = true;
            }
            #endregion TARGET_LARBOARD_X
        }

        private void AnimationTargetStarboard()
        {
            //TARGET_STARBOARD_Z
            #region TARGET_STARBOARD_Z
            //si je n'ai pas atteint la cible
            if (_Starboard_ReachAngleInclinaisonTarget_Z == false)
            {
                //je rotate
                this.RotateToTargetZ(_Starboard_TargetInclinaison_Z, _Starboard_SpeedRotationToTarget_Z);
            }
            //vérification Target
            if (_currentAngleRotation.z < _Starboard_TargetInclinaison_Z + _Starboard_SpeedRotationToTarget_Z
                && _currentAngleRotation.z > _Starboard_TargetInclinaison_Z - _Starboard_SpeedRotationToTarget_Z)
            {
                _Starboard_ReachAngleInclinaisonTarget_Z = true;
            }
            #endregion TARGET_STARBOARD_Z

            //TARGET_STARBOARD_X
            #region TARGET_STARBOARD_X
            //si je n'ai pas atteint la cible
            if (_Starboard_ReachAngleInclinaisonTarget_X == false)
            {
                //je rotate
                this.RotateToTargetX(_Starboard_TargetInclinaison_X, _Starboard_SpeedRotationToTarget_X);
            }
            //vérification Target
            if (_currentAngleRotation.x < _Starboard_TargetInclinaison_X + _Starboard_SpeedRotationToTarget_X
                && _currentAngleRotation.x > _Starboard_TargetInclinaison_X - _Starboard_SpeedRotationToTarget_X)
            {
                _Starboard_ReachAngleInclinaisonTarget_X = true;
            }
            #endregion TARGET_STARBOARD_X
        }

        private void AnimationTargetForward()
        {
            //TARGET_FORWARD_Z
            #region TARGET_FORWARD_Z
            //si je n'ai pas atteint la cible
            if (_Forward_ReachAngleInclinaisonTarget_Z == false)
            {
                //je rotate
                this.RotateToTargetZ(_Forward_TargetInclinaison_Z, _Forward_SpeedRotationToTarget_Z);
            }
            //vérification Target
            if (_currentAngleRotation.z < _Forward_TargetInclinaison_Z + _Forward_SpeedRotationToTarget_Z
                && _currentAngleRotation.z > _Forward_TargetInclinaison_Z - _Forward_SpeedRotationToTarget_Z)
            {
                _Forward_ReachAngleInclinaisonTarget_Z = true;
            }

            #endregion TARGET_FORWARD_Z

            //TARGET_FORWARD_X
            #region TARGET_FORWARD_X
            //si je n'ai pas atteint la cible
            if (_Forward_ReachAngleInclinaisonTarget_X == false)
            {
                //je rotate
                this.RotateToTargetX(_Forward_TargetInclinaison_X, _Forward_SpeedRotationToTarget_X);
            }
            //vérification Target
            if (_currentAngleRotation.x < _Forward_TargetInclinaison_X + _Forward_SpeedRotationToTarget_X
                && _currentAngleRotation.x > _Forward_TargetInclinaison_X - _Forward_SpeedRotationToTarget_X)
            {
                _Forward_ReachAngleInclinaisonTarget_X = true;
            }

            #endregion TARGET_FORWARD_X
        }

        #endregion ANIMATION TARGET

        #region ROLL

        /// <summary>
        /// - "pCenterOfRotation" correspond au centre de rotation pour l'animation de roll (c'est le target inclinaison)
        /// </summary>
        /// <param name="pSpeedRotating"></param>
        /// <param name="pAngleMaxInclinaison"></param>
        /// <param name="pCenterOfRotation"></param>
        private void RollZ(float pSpeedRotating, float pAngleMaxInclinaison, float pCenterOfRotation)
        {
            //get parameters
            _currentRollSpeed_Z = pSpeedRotating;
            _currentAngleMaxInclinaison_Z = pAngleMaxInclinaison;
            _currentTargetInclinaison_Z = pCenterOfRotation;

            #region VERIFICATION_Z

            //CAN VERIFY REACH
            if (_currentCanVerifyReachTargetInclinaison_Z == false)
            {
                if (_boatCharacter.getBoatRotationState() != _tempBoatRotationState_ForRoll_Z)
                {
                    _currentCanVerifyReachTargetInclinaison_Z = true;
                }
            }

            //verify if the current target is reach
            if (_currentCanVerifyReachTargetInclinaison_Z == true)
            {
                //if the current target is achieve
                if (((_currentAngleRotation.z < _currentTargetInclinaison_Z + _currentRollSpeed_Z) && (_currentAngleRotation.z > _currentTargetInclinaison_Z - _currentRollSpeed_Z)))
                {
                    _currentReachTargetInclinaison_Z = true;

                    //CAN VERIFY
                    _currentCanVerifyReachTargetInclinaison_Z = false;
                    _tempBoatRotationState_ForRoll_Z = _boatCharacter.getBoatRotationState();
                }
                else
                {
                    _currentReachTargetInclinaison_Z = false;
                    //je rotate
                    this.RotateToTargetZ(_currentTargetInclinaison_Z, _currentRollSpeed_Z);
                }
            }

            #endregion VERIFICATION_Z

            #region DO_THE_ROLL_Z

            //DO THE ROLL
            if (_currentReachTargetInclinaison_Z == true)
            {
                //LE SOUCIS EST QUE JE REPASSE PAS EN LARBOARD

                //define in which way start the  animation
                if (_currentIsRotating_Larboard == false && _currentIsRotating_Starboard == false)
                {
                    _currentIsRotating_Larboard = true;
                    _currentIsRotating_Starboard = false;
                }
                //set the the tempTargetAngle
                if (_currentIsRotating_Larboard == true)
                {
                    _tempTargetAngleForRoll_Z = _currentTargetInclinaison_Z + _currentAngleMaxInclinaison_Z;
                    //Debug.Log("DEFINE TEMP LARBOARD --> _tempTargetAngleForRoll_Z : " + _tempTargetAngleForRoll_Z + " _currentAngleRotation.z" + _currentAngleRotation.z);
                }
                else if (_currentIsRotating_Starboard == true)
                {
                    _tempTargetAngleForRoll_Z = _currentTargetInclinaison_Z - _currentAngleMaxInclinaison_Z;
                    //Debug.Log("DEFINE TEMP STARBOARD --> _tempTargetAngleForRoll_Z : " + _tempTargetAngleForRoll_Z + " _currentAngleRotation.z" + _currentAngleRotation.z);
                }
                if (_tempTargetAngleForRoll_Z < 0)
                {
                    _tempTargetAngleForRoll_Z = MathTools.invert(_tempTargetAngleForRoll_Z);
                    _tempTargetAngleForRoll_Z = 360 - _tempTargetAngleForRoll_Z;
                    //Debug.Log("DEFINE TEMP INVERT --> _tempTargetAngleForRoll_Z : " + _tempTargetAngleForRoll_Z + " _currentAngleRotation.z" + _currentAngleRotation.z);

                }

                //Debug.Log("tempTargetAngle : " + _tempTargetAngle + " _currentAngleRotation.z" + _currentAngleRotation.z);

                //si l'angle target est a gauche et que mon angle est a droite
                if (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_Z) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == false)
                {
                    //je met mon angle a gauche
                    this.RotateLarboardZ(_currentRollSpeed_Z);
                }
                //si l'angle target est a droite et que mon agnle est a gauche
                else if (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_Z) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == true)
                {
                    //je met mon angle a droite
                    this.RotateStarboardZ(_currentRollSpeed_Z);
                }
                //si l'angle target est mon angle sont du même coté
                else if ((MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_Z) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == true)
                || (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_Z) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == false))
                {
                    float tempConvertForPlus = _tempTargetAngleForRoll_Z + _currentRollSpeed_Z;
                    float tempConvertForMoins = _tempTargetAngleForRoll_Z - _currentRollSpeed_Z;
                    //JE VEUT QUE DU POSITIF!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (tempConvertForPlus < 0)
                    {
                        tempConvertForPlus = MathTools.invert(tempConvertForPlus);
                        tempConvertForPlus = 360 - tempConvertForPlus;
                        //Debug.Log("RotateToTargetZ --> invert & convert to 360 --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.z" + _currentAngleRotation.z);
                    }
                    if (tempConvertForMoins < 0)
                    {
                        tempConvertForMoins = MathTools.invert(tempConvertForMoins);
                        tempConvertForMoins = 360 - tempConvertForMoins;
                        //Debug.Log("RotateToTargetZ --> invert & convert to 360 --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.z" + _currentAngleRotation.z);
                    }
                    //Debug.Log("LE PUTAIN DE TEST : _currentAngleRotation.z : " + _currentAngleRotation.z
                    //    + " _tempTargetAngleForRoll_Z + _currentRollSpeed_Z : " + tempConvertForPlus
                    //    + " _tempTargetAngleForRoll_Z - _currentRollSpeed_Z : " + tempConvertForMoins
                    //    );
                    //POUQUOIIIIIIIIIIIIIII::::::

                    //EN LARBOARD
                    if (_currentIsRotating_Larboard)
                    {
                        //je rotate
                        if (_currentAngleRotation.z < _tempTargetAngleForRoll_Z)
                        {
                            //Debug.Log("ROTATE LABOARD");
                            //je rotate vers la gauche
                            this.RotateLarboardZ(_currentRollSpeed_Z);
                        }
                        //si j'ai atteint la target
                        else if (_currentAngleRotation.z >= _tempTargetAngleForRoll_Z)
                        {
                            //Debug.Log("ATTEINT TARGET LARBOARD");
                            _currentIsRotating_Larboard = false;
                            _currentIsRotating_Starboard = true;
                        }
                    }
                    //EN STARBOARD
                    else if (_currentIsRotating_Starboard == true)
                    {
                        //je rotate
                        if (_currentAngleRotation.z > _tempTargetAngleForRoll_Z)
                        {
                            //Debug.Log("ROTATE STARBOARD");
                            //je rotate vers la droite
                            this.RotateStarboardZ(_currentRollSpeed_Z);
                        }
                        //si j'ai atteint la target
                        else if (_currentAngleRotation.z <= _tempTargetAngleForRoll_Z)
                        {
                            //Debug.Log("ATTEINT TARGET STARBOARD");
                            _currentIsRotating_Larboard = true;
                            _currentIsRotating_Starboard = false;
                        }
                    }
                }
            }
            #endregion DO_THE_ROLL_Z
        }

        /// <summary>
        /// - "pCenterOfRotation" correspond au centre de rotation pour l'animation de roll (c'est le target inclinaison)
        /// </summary>
        /// <param name="pSpeedRotating"></param>
        /// <param name="pAngleMaxInclinaison"></param>
        /// <param name="pCenterOfRotation"></param>
        private void RollX(float pSpeedRotating, float pAngleMaxInclinaison, float pCenterOfRotation)
        {
            //get parameters
            _currentRollSpeed_X = pSpeedRotating;
            _currentAngleMaxInclinaison_X = pAngleMaxInclinaison;
            _currentTargetInclinaison_X = pCenterOfRotation;

            #region VERIFICATION_X

            //CAN VERIFY REACH
            if (_currentCanVerifyReachTargetInclinaison_X == false)
            {
                if (_boatCharacter.getBoatRotationState() != _tempBoatRotationState_ForRoll_X)
                {
                    _currentCanVerifyReachTargetInclinaison_X = true;
                }
            }

            //verify if the current target is reach
            if (_currentCanVerifyReachTargetInclinaison_X == true)
            {
                //if the current target is achieve
                if (((_currentAngleRotation.x < _currentTargetInclinaison_X + _currentRollSpeed_X) && (_currentAngleRotation.x > _currentTargetInclinaison_X - _currentRollSpeed_X)))
                {
                    _currentReachTargetInclinaison_X = true;

                    //CAN VERIFY
                    _currentCanVerifyReachTargetInclinaison_X = false;
                    _tempBoatRotationState_ForRoll_X = _boatCharacter.getBoatRotationState();
                }
                else
                {
                    _currentReachTargetInclinaison_X = false;
                    //je rotate
                    this.RotateToTargetX(_currentTargetInclinaison_X, _currentRollSpeed_X);
                }
            }

            #endregion VERIFICATION_X

            #region DO_THE_ROLL_X

            //DO THE ROLL
            if (_currentReachTargetInclinaison_X == true)
            {
                //LE SOUCIS EST QUE JE REPASSE PAS EN LARBOARD

                //define in which way start the  animation
                if (_currentIsRotating_Forward == false && _currentIsRotating_Backward == false)
                {
                    _currentIsRotating_Forward = true;
                    _currentIsRotating_Backward = false;
                }
                //set the the tempTargetAngle
                if (_currentIsRotating_Forward == true)
                {
                    _tempTargetAngleForRoll_X = _currentTargetInclinaison_X + _currentAngleMaxInclinaison_X;
                    //Debug.Log("DEFINE TEMP FORWARD --> _tempTargetAngleForRoll_X : " + _tempTargetAngleForRoll_X + " _currentAngleRotation.x" + _currentAngleRotation.x);
                }
                else if (_currentIsRotating_Backward == true)
                {
                    _tempTargetAngleForRoll_X = _currentTargetInclinaison_X - _currentAngleMaxInclinaison_X;
                    //Debug.Log("DEFINE TEMP BACKWARD --> _tempTargetAngleForRoll_X : " + _tempTargetAngleForRoll_X + " _currentAngleRotation.x" + _currentAngleRotation.x);
                }
                if (_tempTargetAngleForRoll_X < 0)
                {
                    _tempTargetAngleForRoll_X = MathTools.invert(_tempTargetAngleForRoll_X);
                    _tempTargetAngleForRoll_X = 360 - _tempTargetAngleForRoll_X;
                    //Debug.Log("DEFINE TEMP INVERT --> _tempTargetAngleForRoll_X : " + _tempTargetAngleForRoll_X + " _currentAngleRotation.x" + _currentAngleRotation.x);

                }


                //si l'angle target est a gauche et que mon angle est a droite
                if (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_X) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == false)
                {
                    //je met mon angle a gauche
                    this.RotateForwardX(_currentRollSpeed_X);
                }
                //si l'angle target est a droite et que mon agnle est a gauche
                else if (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_X) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == true)
                {
                    //je met mon angle a droite
                    this.RotateBackwardX(_currentRollSpeed_X);
                }
                //si l'angle target est mon angle sont du même coté
                else if ((MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_X) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == true)
                || (MathTools.AngleIsLeftOrRight0(_tempTargetAngleForRoll_X) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == false))
                {
                    float tempConvertForPlus = _tempTargetAngleForRoll_X + _currentRollSpeed_X;
                    float tempConvertForMoins = _tempTargetAngleForRoll_X - _currentRollSpeed_X;
                    //JE VEUT QUE DU POSITIF!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    if (tempConvertForPlus < 0)
                    {
                        tempConvertForPlus = MathTools.invert(tempConvertForPlus);
                        tempConvertForPlus = 360 - tempConvertForPlus;
                        //Debug.Log("RotateToTargetX --> invert & convert to 360 --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.x" + _currentAngleRotation.x);
                    }
                    if (tempConvertForMoins < 0)
                    {
                        tempConvertForMoins = MathTools.invert(tempConvertForMoins);
                        tempConvertForMoins = 360 - tempConvertForMoins;
                        //Debug.Log("RotateToTargetX --> invert & convert to 360 --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.x" + _currentAngleRotation.x);
                    }

                    //EN LARBOARD
                    if (_currentIsRotating_Forward)
                    {
                        //je rotate
                        if (_currentAngleRotation.x < _tempTargetAngleForRoll_X)
                        {
                            //Debug.Log("ROTATE FORWARD");
                            //je rotate vers l'avant
                            this.RotateForwardX(_currentRollSpeed_X);
                        }
                        //si j'ai atteint la target
                        else if (_currentAngleRotation.x >= _tempTargetAngleForRoll_X)
                        {
                            //Debug.Log("ATTEINT TARGET FORWARD");
                            _currentIsRotating_Forward= false;
                            _currentIsRotating_Backward= true;
                        }
                    }
                    //EN STARBOARD
                    else if (_currentIsRotating_Backward == true)
                    {
                        //je rotate
                        if (_currentAngleRotation.x > _tempTargetAngleForRoll_X)
                        {
                            //Debug.Log("ROTATE BACKWARD");
                            //je rotate vers la droite
                            this.RotateBackwardX(_currentRollSpeed_X);
                        }
                        //si j'ai atteint la target
                        else if (_currentAngleRotation.x <= _tempTargetAngleForRoll_X)
                        {
                            //Debug.Log("ATTEINT TARGET BACKWARD");
                            _currentIsRotating_Forward = true;
                            _currentIsRotating_Backward = false;
                        }
                    }
                }
            }
            #endregion DO_THE_ROLL_X
        }

        #endregion ROLL

        #region ROTATES

        /// <summary>
        /// Rotate to the target
        /// - the "pAngleTargetZ" can only be between 0 & 360 (a negative variable be transform into a poitive variable & convert into : 360 - the variable in positive)
        /// 
        /// </summary>
        /// <param name="pAngleTargetZ"></param>
        /// <returns></returns>
        private void RotateToTargetZ(float pAngleTarget, float pSpeedRotation)
        {
            //Je définie dans quelle partie est la target
            //et en fonction je fais mes calculs

            if (pAngleTarget < 0)
            {
                pAngleTarget = MathTools.invert(pAngleTarget);
                pAngleTarget = 360 - pAngleTarget;
                //Debug.Log("RotateToTargetZ --> invert & convert to 360 --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.z" + _currentAngleRotation.z);

            }
            //Debug.Log("RotateToTargetZ --> pAngleTarget " + pAngleTarget + " _currentAngleRotation.z" + _currentAngleRotation.z);


            //Target a gauche et current a gauche
            if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == true)
            {
                //Debug.Log("RotateToTargetZ --> Target a gauche et current a gauche");
                //incrémente si inférieur
                if (_currentAngleRotation.z < pAngleTarget)
                {
                    this.RotateLarboardZ(pSpeedRotation);
                }
                //Décrémente si supérieur
                if (_currentAngleRotation.z > pAngleTarget)
                {
                    this.RotateStarboardZ(pSpeedRotation);
                }
            }
            //Target a droite et current a droite
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == false)
            {
                //Debug.Log("RotateToTargetZ --> Target a droite et current a droite");
                //incrémente si inférieur
                if (_currentAngleRotation.z < pAngleTarget)
                {
                    this.RotateLarboardZ(pSpeedRotation);
                }
                //Décrémente si supérieur
                if (_currentAngleRotation.z > pAngleTarget)
                {
                    this.RotateStarboardZ(pSpeedRotation);
                }
            }
            //Target a gauche et current a droite
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == false)
            {
                //Debug.Log("RotateToTargetZ --> Target a gauche et current a droite");
                //increase 
                this.RotateLarboardZ(pSpeedRotation);
            }
            //Target a droite et current a gauche
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.z) == true)
            {
                //Debug.Log("RotateToTargetZ --> Target a droite et current a gauche");
                //decrease
                this.RotateStarboardZ(pSpeedRotation);
            }

        }

        /// <summary>
        /// increase the rotation on the z axis
        /// </summary>
        /// <param name="pSpeedRotate"></param>
        private void RotateLarboardZ(float pSpeedRotate)
        {
            //Note perso le prblème est que la rotation ne s'effectue pas seulement en z

            Vector3 rotationIncrementation = new Vector3(0, 0, +pSpeedRotate);
            this.transform.Rotate(rotationIncrementation);
            //Debug.Log("AnimationBoat --> RotateLarboardZ : " + _currentAngleRotation);
        }

        /// <summary>
        /// Decrease the rotation on the z axis
        /// </summary>
        /// <param name="pSpeedRotate"></param>
        private void RotateStarboardZ(float pSpeedRotate)
        {
            Vector3 rotationIncrementation = new Vector3(0, 0, -pSpeedRotate);
            this.transform.Rotate(rotationIncrementation);
            //Debug.Log("AnimationBoat --> RotateStarboardZ : " + _currentAngleRotation);
        }

        /// <summary>
        /// Rotate to the target
        /// - the "pAngleTargetX" can only be between 0 & 360 (a negative variable be transform into a poitive variable & convert into : 360 - the variable in positive)
        /// 
        /// </summary>
        /// <param name="pAngleTargetX"></param>
        /// <returns></returns>
        private void RotateToTargetX(float pAngleTarget, float pSpeedRotation)
        {
            //Je définie dans quelle partie est la target
            //et en fonction je fais mes calculs
            if (pAngleTarget < 0)
            {
                pAngleTarget = MathTools.invert(pAngleTarget);
                pAngleTarget = 360 - pAngleTarget;
            }

            //Target a gauche et current a gauche
            if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == true)
            {
                //incrémente si inférieur
                if (_currentAngleRotation.x < pAngleTarget)
                {
                    this.RotateForwardX(pSpeedRotation);
                }
                //Décrémente si supérieur
                if (_currentAngleRotation.x > pAngleTarget)
                {
                    this.RotateBackwardX(pSpeedRotation);
                }
            }
            //Target a droite et current a droite
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == false)
            {
                //Debug.Log("RotateToTargetZ --> Target a droite et current a droite");
                //incrémente si inférieur
                if (_currentAngleRotation.x < pAngleTarget)
                {
                    this.RotateForwardX(pSpeedRotation);
                }
                //Décrémente si supérieur
                if (_currentAngleRotation.x > pAngleTarget)
                {
                    this.RotateBackwardX(pSpeedRotation);
                }
            }
            //Target a gauche et current a droite
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == true && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == false)
            {
                //increase 
                this.RotateForwardX(pSpeedRotation);
            }
            //Target a droite et current a gauche
            else if (MathTools.AngleIsLeftOrRight0(pAngleTarget) == false && MathTools.AngleIsLeftOrRight0(_currentAngleRotation.x) == true)
            {
                //decrease
                this.RotateBackwardX(pSpeedRotation);
            }

        }

        /// <summary>
        /// increase the rotation on the x axis
        /// </summary>
        /// <param name="pSpeedRotate"></param>
        private void RotateForwardX(float pSpeedRotate)
        {
            Vector3 rotationIncrementation = new Vector3(+pSpeedRotate, 0, 0);
            this.transform.Rotate(rotationIncrementation);
            //Debug.Log("AnimationBoat --> RotateForwardX : " + _currentAngleRotation);
        }

        /// <summary>
        /// Decrease the rotation on the x axis
        /// </summary>
        /// <param name="pSpeedRotate"></param>
        private void RotateBackwardX(float pSpeedRotate)
        {
            Vector3 rotationIncrementation = new Vector3(-pSpeedRotate, 0, 0);
            this.transform.Rotate(rotationIncrementation);
            //Debug.Log("AnimationBoat --> RotateBackwardX : " + _currentAngleRotation);
        }

        #endregion ROTATES

        //private void OnDrawGizmos()
        //{
        //    //draw sphere at the target position

        //    //forward point
        //    Gizmos.color = Color.yellow;

        //    //calculate the direction by the wanted angle target
        //    Vector3 _PointTargetGizmo = this.transform.forward * _currentTargetInclinaison_Z * 10; 
            
        //    Gizmos.DrawSphere(this.transform.position + _PointTargetGizmo, 2f);
        //    //Debug.DrawLine(this.transform.position, this.transform.position + _direction_MovementDirection * 10);


        //}


        
    }
}
