using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjetPirate.Tools;
using ProjetPirate.Data;
using System;

namespace ProjetPirate.Boat
{
    public class AnimationBoatForAnimator : MonoBehaviour
    {
        private Animator _animator;

        private BoatCharacter _boatCharacter;


        public BoatRotationState _boatRotationState;
        [Header("JUST TO SEE PRIVATE")]
        public bool _Play_No_Roll;
        public bool _Play_Roll_Idle_Z;
        public bool _Play_Roll_Larboard_Z;
        public bool _Play_Roll_Starboard_Z;
        public bool _Play_Center_To_Larboard_Z;
        public bool _Play_Center_To_Starboard_Z;
        public bool _Play_Larboard_To_Center_Z;
        public bool _Play_Starboard_To_Center_Z;


        // Use this for initialization
        void Start()
        {
            _animator = this.GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError(this.name + " _animator is null");
            }

            if (this.GetComponentInParent<BoatCharacter>() != null)
            {
                _boatCharacter = this.GetComponentInParent<BoatCharacter>();
            }
            else
            {
                Debug.LogError(this.name + " there is no BoatCharacter in parent");
            }

            //il faut l'initialiser a true dans le start sinon ça ne fonctionne pas
            _Play_Roll_Idle_Z = true; 
        }

        // Update is called once per frame
        void Update()
        {
            //just to see
            _boatRotationState = _boatCharacter.getBoatRotationState();

            #region RESET BOOLEANS


            //No_Roll
            if (this.Animation_IsPlaying_No_Roll())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "No_Roll");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_No_Roll");
                _animator.SetBool("Do_No_Roll", false);
            }

            //Roll_Idle_Z
            if (this.Animation_IsPlaying_Roll_Idle_Z())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Roll_Idle_Z");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Roll_Idle_Z");
                _animator.SetBool("Do_Roll_Idle_Z", false);
            }

            //Roll_Larboard_Z
            if (this.Animation_IsPlaying_Roll_Larboard_Z())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Roll_Larboard_Z");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Roll_Larboard_Z");
                _animator.SetBool("Do_Roll_Larboard_Z", false);
            }

            //Roll_Starboard_Z
            if (this.Animation_IsPlaying_Roll_Starboard_Z())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Roll_Starboard_Z");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Roll_Starboard_Z");
                _animator.SetBool("Do_Roll_Starboard_Z", false);
            }

            //Center_To_Larboard
            if (this.Animation_IsPlaying_Center_To_Larboard())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Center_To_Larboard");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Center_To_Larboard_Z");
                _animator.SetBool("Do_Center_To_Larboard_Z", false);
            }

            //Center_To_Starboard
            if (this.Animation_IsPlaying_Center_To_Starboard())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Center_To_Starboard");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Center_To_Starboard_Z");
                _animator.SetBool("Do_Center_To_Starboard_Z", false);
            }

            //Larboard_To_Center
            if (this.Animation_IsPlaying_Larboard_To_Center())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Larboard_To_Center");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Larboard_To_Center_Z");
                _animator.SetBool("Do_Larboard_To_Center_Z", false);
            }

            //Starboard_To_Center
            if (this.Animation_IsPlaying_Starboard_To_Center())
            {
                //Debug.Log(this.name + " CurrentAnimationIs : " + "Starboard_To_Center");
            }
            else
            {
                Debug.LogWarning(this.name + "set false : " + "Do_Starboard_To_Center_Z");
                _animator.SetBool("Do_Starboard_To_Center_Z", false);
            }


            #endregion RESET BOOLEANS


            //enum
            switch (_boatCharacter.getBoatRotationState())
            {
                case BoatRotationState.FORWARD:

                    //VERIFICATIONS
                    //Roll_Larboard_Z --> Larboard to center
                    if (this.Animation_IsPlaying_Roll_Larboard_Z())
                    {
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Roll_Larboard_Z = false;
                            _Play_Larboard_To_Center_Z = true;
                        }
                    }

                    //Larboard to center --> Roll_Idle_Z
                    if (this.Animation_IsPlaying_Larboard_To_Center())
                    {
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Larboard_To_Center_Z = false;
                            _Play_Roll_Idle_Z = true;
                        }
                    }

                    //DO ANIMATIONS
                    //Animation Roll Idle Z
                    if (_Play_Roll_Idle_Z == true)
                    {
                        this.Animation_Roll_Idle_Z();
                    }
                    //Larboard_To_Center
                    if (_Play_Larboard_To_Center_Z == true)
                    {
                        this.Animation_Larboard_To_Center_Z();
                    }
                    break;
                case BoatRotationState.BABORD:

                    //VERIFICATIONS
                    //Roll_Idle_Z --> Center_To_Larboard
                    if (this.Animation_IsPlaying_Roll_Idle_Z())
                    {
                        //je vérifie le normalize 
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Roll_Idle_Z = false;
                            _Play_Center_To_Larboard_Z = true;
                        }
                    }

                    //Center_To_Larboard --> Roll_Larboard_Z
                    if (this.Animation_IsPlaying_Center_To_Larboard())
                    {
                        //je vérifie le normalize 
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Center_To_Larboard_Z = false;
                            _Play_Roll_Larboard_Z = true;
                        }
                    }

                    //DO ANIMATIONS
                    if (_Play_Center_To_Larboard_Z)
                    {
                        this.Animation_Center_To_Larboard_Z();
                    }
                    if(_Play_Roll_Larboard_Z == true)
                    {
                        this.Animation_Roll_Larboard_Z();
                    }

                    break;
                case BoatRotationState.TRIBORD:

                    //VERIFICATIONS
                    //Roll_Idle_Z --> Center_To_Starboard
                    if (this.Animation_IsPlaying_Roll_Idle_Z())
                    {
                        //je vérifie le normalize 
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Roll_Idle_Z = false;
                            _Play_Center_To_Starboard_Z = true;
                        }
                    }

                    //Center_To_Starboard --> Roll_Starboard_Z
                    if (this.Animation_IsPlaying_Center_To_Starboard())
                    {
                        //je vérifie le normalize 
                        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                        {
                            _Play_Center_To_Starboard_Z = false;
                            _Play_Roll_Starboard_Z = true;
                        }
                    }

                    //DO ANIMATIONS
                    if (_Play_Center_To_Starboard_Z)
                    {
                        this.Animation_Center_To_Starboard_Z();
                    }
                    if (_Play_Roll_Starboard_Z == true)
                    {
                        this.Animation_Roll_Starboard_Z();
                    }

                    break;
                default:
                    break;
            }
        }

       

        #region ANIMATIONS

        private void Animation_Roll_Idle_Z()
        {
            if (!_animator.GetBool("Do_Roll_Idle_Z"))
            {
                //Debug.LogWarning(this.name + "set true : " + "Do_Roll_Idle_Z");
                _animator.SetBool("Do_Roll_Idle_Z", true);
            }
        }

        private void Animation_Center_To_Larboard_Z()
        {
            if (!_animator.GetBool("Do_Center_To_Larboard_Z"))
            {
                //Debug.LogWarning(this.name + "set true : " + "Do_Center_To_Larboard");
                _animator.SetBool("Do_Center_To_Larboard_Z", true);
            }
        }

        private void Animation_Center_To_Starboard_Z()
        {
            if(!_animator.GetBool("Do_Center_To_Starboard_Z"))
            {
                _animator.SetBool("Do_Center_To_Starboard_Z", true);
            }
        }



        private void Animation_Roll_Larboard_Z()
        {
            if(!_animator.GetBool("Do_Roll_Larboard_Z"))
            {
                _animator.SetBool("Do_Roll_Larboard_Z", true);
            }
        }

        private void Animation_Larboard_To_Center_Z()
        {
            //Larboard_To_Center
            if (!_animator.GetBool("Do_Larboard_To_Center_Z"))
            {
                Debug.LogWarning(this.name + "set true : " + "Do_Larboard_To_Center_Z");
                _animator.SetBool("Do_Larboard_To_Center_Z", true);
            }
        }


        private void Animation_Roll_Starboard_Z()
        {
            if (!_animator.GetBool("Do_Roll_Starboard_Z"))
            {
                //Debug.LogWarning(this.name + "set true : " + "Do_Roll_Starboard_Z");
                _animator.SetBool("Do_Roll_Starboard_Z", true);
            }
        }

        private void Animation_Starboard_To_Center_Z()
        {
            if (!_animator.GetBool("Do_Starboard_To_Center_Z"))
            {
                //Debug.LogWarning(this.name + "set true : " + "Do_Starboard_To_Center_Z");
                _animator.SetBool("Do_Starboard_To_Center_Z", true);
            }
        }

        #endregion ANIMATIONS

        //Get which animation is playing
        #region ANIMATION IS PLAYING

        //No_Roll
        bool Animation_IsPlaying_No_Roll()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("No_Roll"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Roll_Idle_Z
        bool Animation_IsPlaying_Roll_Idle_Z()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll_Idle_Z"))
            {
                Debug.Log("return true");
                //return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Roll_Larboard_Z
        bool Animation_IsPlaying_Roll_Larboard_Z()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll_Larboard_Z"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Roll_Starboard_Z
        bool Animation_IsPlaying_Roll_Starboard_Z()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Roll_Starboard_Z"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Center_To_Larboard
        bool Animation_IsPlaying_Center_To_Larboard()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Center_To_Larboard"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Center_To_Starboard
        bool Animation_IsPlaying_Center_To_Starboard()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Center_To_Starboard"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Larboard_To_Center
        bool Animation_IsPlaying_Larboard_To_Center()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Larboard_To_Center"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }

        //Starboard_To_Center
        bool Animation_IsPlaying_Starboard_To_Center()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Starboard_To_Center"))
            {
                Debug.Log("return true");
                return true;
            }
            else
            {
                Debug.Log("return false");
                return false;
            }
        }
        #endregion ANIMATION IS PLAYING
    }
}
