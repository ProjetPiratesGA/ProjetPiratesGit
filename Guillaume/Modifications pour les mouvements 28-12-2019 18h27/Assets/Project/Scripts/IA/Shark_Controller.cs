using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    [RequireComponent(typeof(Shark_Character))]
    public class Shark_Controller : IA_Controller
    {
        private float Fow = 3f;
        //private Vector3 TargetPosition = Vector3.zero;
        //private Transform TargetTransform;
        //private GameObject Target;

        private float Timer = 8f;
        private float LastTimeReset = 0;


        [Header("Shark Controller")]
        [SerializeField] private float MaxDistance = 30;
        [SerializeField] private float MinDistance = 30;
        [SerializeField] private Vector3 FallingTarget;

        // Mettre les états dans le character et âppeler toutes les fonction
        // Dans le characters avec les memes conditions
        // puis retirer les conditions du Controller
        [SerializeField] protected Shark_Character _character;

        public Shark_Character Character
        {
            get { return _character; }
        }

        void Start()
        {
            _character = this.GetComponent<Shark_Character>();
                //Target = Player_Singleton.instance.Player;
            //TargetTransform = Player_Singleton.instance.Player.transform;
            //TargetPosition = Target.transform.position;
        }

        void Update()
        {
            if (!_isOnAlert)
            {
                if (this.GetComponent<Patrol_Static>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Static>().Patrol());
                }
                if (this.GetComponent<Patrol_Wander>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Wander>().Patrol());
                }
                if (this.GetComponent<Patrol_Checkpoint>() != null)
                {
                    GoToDestination(this.GetComponent<Patrol_Checkpoint>().Patrol());
                }
                _character.GetComponent<Shark_Character>()._inBattle = false;
            }
            else
            {
                _character.GetComponent<Shark_Character>()._inBattle = true;
                if (Vector3.Distance(transform.position, _target.transform.position) < MaxDistance && Vector3.Distance(transform.position, _target.transform.position) > MinDistance || GetComponent<Shark_Character>()._isApplyed == true)
                {
                    if (Vector3.Angle(transform.position, transform.forward) != Vector3.Angle(transform.position, _target.transform.position))
                    {
                        if (Time.time - LastTimeReset >= Timer)
                        {
                            if (GetComponent<Shark_Character>()._isApplyed == false)
                            {
                                GetComponent<Shark_Character>()._isApplyed = true;
                                GetComponent<Shark_Character>()._isCharging = true;
                            }
                            if (GetComponent<Shark_Character>()._isCharging == true)
                            {
                                GetComponent<Shark_Character>().Jumping(_target.transform.position);

                            }
                            else if (GetComponent<Shark_Character>()._isFalling == true)
                            {
                                if (GetComponent<Shark_Character>().Falling())
                                {
                                    LastTimeReset = Time.time;
                                    GetComponent<Shark_Character>()._isCharging = false;
                                    GetComponent<Shark_Character>()._isFalling = false;
                                    GetComponent<Shark_Character>()._isApplyed = false;
                                }
                            }
                        }
                        else
                        {
                            GetComponent<Shark_Character>().TurnAroundTargetPosition(_target.transform.position);
                        }
                    }
                    //transform.position += transform.forward / 2.5f;
                }
                else if (Vector3.Distance(transform.position, _target.transform.position) >= MaxDistance)
                {
                    Vector3 direction = (_target.transform.position - transform.position).normalized;
                    Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = LookRotation;
                }
                else if (Vector3.Distance(transform.position, _target.transform.position) <= MinDistance)
                {
                    Vector3 direction = (_target.transform.position - transform.position).normalized;
                    Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = LookRotation;
                    transform.Rotate(new Vector3(0, 180, 0));
                }
                GetComponent<Shark_Character>().MoveForward();
            }
        }

        public void GoToDestination(Vector3 _destination)
        {
            // Make the DirectionLocator faces the destination
            _character.DirectionLocator.LookAt(_destination);
            Vector3 rotation = _character.DirectionLocator.eulerAngles;
            rotation.x = Mathf.Abs(rotation.x);
            rotation.y = Mathf.Abs(rotation.y);
            rotation.z = Mathf.Abs(rotation.z);
            _character.DirectionLocator.eulerAngles = rotation;

            //Check the distance to the destination
            if (Vector3.Distance(this.transform.position, _destination) > _character.MaxMovingSpeed * Time.deltaTime)
            {
                //If the destination is too far, move forward
                _character.MoveForward();
            }
            else
            {
                //If the destination is close enough, the character take the destination's position
                this.transform.position = _destination;
            }

            //Check in which direction is the destination
            if (Mathf.Abs(_character.DirectionLocator.eulerAngles.y - _character.transform.eulerAngles.y) <= _character.MaxAngularSpeed * Time.deltaTime)
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

        ///// <summary>
        ///// Check in an aray if one is in range and get his position
        ///// </summary>
        ///// <param name="_targets[]">position array to check in</param>
        //public void CheckTargetInRange(Vector3[] _targets)
        //{
        //    for (int a = 0; a < _targets.Length; a++)
        //    {
        //        if (Vector3.Distance(transform.position, _targets[a]) <= 3f)
        //        {
        //            TargetTransform.position = _targets[a];
        //        }
        //    }
        //}

        ///// <summary>
        ///// Check if the target is in range and keep his position in memory
        ///// </summary>
        ///// <param name="_targets">Target array to check in</param>
        //public void CheckTargetInRange(Vector3 _targets)
        //{
        //    if (Vector3.Distance(transform.position, _targets) <= 3f)
        //    {
        //        TargetTransform.position = _targets;
        //    }
        //}
    }
}
