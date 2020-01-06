using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public enum Behaviour
    {
        Aggressive,
        Punisher,
        Careful
    }
    public class IA_Controller : MonoBehaviour
    {
        [Header("IA Controller")]
        [SerializeField] protected Behaviour _behaviour;
        [SerializeField] protected int _punisherMinXP = 10;
        [SerializeField] protected GameObject _target;
        [SerializeField] protected IA_Character _character;


        public GameObject Target
        {
            get { return _target; }
        }

        protected bool _isOnAlert = false;

        public IA_Character Character
        {
            get { return _character; }
        }

        public bool IsOnAlert
        {
            get { return _isOnAlert; }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Alert(GameObject pTarget)
        {
            switch (_behaviour)
            {
                case Behaviour.Aggressive:
                    _isOnAlert = true;
                    _target = pTarget;
                    break;
                case Behaviour.Punisher:
                    if (pTarget.GetComponent<ProjetPirate.Boat.BoatCharacter>().getCurrentXp() >= _punisherMinXP)
                    {
                        _isOnAlert = true;
                        _target = pTarget;
                    }
                    break;
                case Behaviour.Careful:
                    _isOnAlert = true;
                    _target = pTarget;
                    break;
                default:
                    break;
            }
            
        }

        public void AlertFromShoot(GameObject pTarget)
        {
            switch (_behaviour)
            {
                case Behaviour.Aggressive:
                    _isOnAlert = true;
                    _target = pTarget;
                    break;
                case Behaviour.Punisher:
                    _isOnAlert = true;
                    _target = pTarget;
                    break;
                case Behaviour.Careful:
                    _isOnAlert = true;
                    _target = pTarget;
                    break;
                default:
                    break;
            }
        }
        
        public void RemoveAlert()
        {
            _isOnAlert = false;
            _target = null;
        }
    }
}