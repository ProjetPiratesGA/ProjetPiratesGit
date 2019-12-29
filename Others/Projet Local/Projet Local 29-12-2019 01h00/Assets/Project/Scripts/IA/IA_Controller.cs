using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public enum Behaviour
    {
        Aggressive,
        Careful,
        Fearful,
        Peaceful
    }

    public enum AggroRequiredType
    {
        XP,
        BoatLevel_More,
        BoatLevel_Less,
        None,
    }

    [System.Serializable]
    public struct RangeCategory
    {
        public float RangeRatio;
        public AggroRequiredType Type;
        public int Quantity;
        public Behaviour AssociatedBehaviour;
    }
    public class IA_Controller : Controller
    {
        [Header("IA Controller")]
        [SerializeField] protected Behaviour _behaviour;
        [SerializeField] protected int _punisherMinXP = 10;
        [SerializeField] protected GameObject _target;

        [SerializeField] protected float _defaultRange;
        [SerializeField] protected List<RangeCategory> _onDetectionRangeCategories;
        [SerializeField] protected List<RangeCategory> _onAttackRangeCategories;


        protected Character _character;
        [SerializeField] protected float _remainingTimeBeforeResettingHealth;
        protected float _timeToResetHealth = 30;

        public GameObject Target
        {
            get { return _target; }
        }

        protected bool _isOnAlert = false;


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

        public void CheckResetHealth()
        {
            _remainingTimeBeforeResettingHealth -= Time.deltaTime;
            if (_remainingTimeBeforeResettingHealth <= 0)
            {
                _character.FullLife();
                _remainingTimeBeforeResettingHealth = _timeToResetHealth;
                Debug.Log("Reset");
            }
        }

        public void ResetTime()
        {
            _remainingTimeBeforeResettingHealth = _timeToResetHealth;
        }

        public bool CheckRangeCategoryOnDetection(ProjetPirate.Boat.BoatCharacter pPotentialTarget)
        {
            float distance = Vector3.Distance(this.transform.position, pPotentialTarget.transform.position);
            for (int i = 0; i < _onDetectionRangeCategories.Count; i++)
            {
                if (_onDetectionRangeCategories[i].RangeRatio * _defaultRange >= distance | _onDetectionRangeCategories[i].RangeRatio == 0)
                {
                    switch (_onDetectionRangeCategories[i].Type)
                    {
                        case AggroRequiredType.XP:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._currentXp >= _onDetectionRangeCategories[i].Quantity)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.BoatLevel_Less:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                    if (_onDetectionRangeCategories[i].Quantity <= 1)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_2:
                                    if (_onDetectionRangeCategories[i].Quantity <= 2)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_3:
                                    if (_onDetectionRangeCategories[i].Quantity <= 3)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.BoatLevel_More:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                    if (_onDetectionRangeCategories[i].Quantity >= 1)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_2:
                                    if (_onDetectionRangeCategories[i].Quantity >= 2)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_3:
                                    if (_onDetectionRangeCategories[i].Quantity >= 3)
                                    {
                                        Debug.Log(i);
                                        _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.None:
                                        Debug.Log(i);
                            _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                            return true;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        public bool CheckRangeCategoryOnAttack(ProjetPirate.Boat.BoatCharacter pPotentialTarget)
        {
            Debug.Log("1");
            float distance = Vector3.Distance(this.transform.position, pPotentialTarget.transform.position);
            for (int i = 0; i < _onAttackRangeCategories.Count; i++)
            {
            Debug.Log("2");
                if (_onAttackRangeCategories[i].RangeRatio * _defaultRange >= distance | _onAttackRangeCategories[i].RangeRatio == 0)
                {
            Debug.Log("3");
                    switch (_onAttackRangeCategories[i].Type)
                    {
                        case AggroRequiredType.XP:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._currentXp >= _onAttackRangeCategories[i].Quantity)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.BoatLevel_Less:
            Debug.Log("4");
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                    if (_onAttackRangeCategories[i].Quantity <= 1)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_2:
            Debug.Log("5");
                                    if (_onAttackRangeCategories[i].Quantity <= 2)
                                    {
            Debug.Log("6");
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_3:
                                    if (_onAttackRangeCategories[i].Quantity <= 3)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.BoatLevel_More:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                    if (_onAttackRangeCategories[i].Quantity >= 1)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_2:
                                    if (_onAttackRangeCategories[i].Quantity >= 2)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_3:
                                    if (_onAttackRangeCategories[i].Quantity >= 3)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.None:
                            _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                            return true;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        public void Alert(GameObject pTarget)
        {
            if (pTarget.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
            {
                if (CheckRangeCategoryOnDetection(pTarget.GetComponent<ProjetPirate.Boat.BoatCharacter>()))
                {
                    _isOnAlert = true;
                    _target = pTarget;
                }
            }
            else
            {
                _isOnAlert = true;
                _target = pTarget;
            }

        }

        public void AlertFromShoot(GameObject pTarget)
        {
            if (pTarget.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
            {
                if (CheckRangeCategoryOnAttack(pTarget.GetComponent<ProjetPirate.Boat.BoatCharacter>()))
                {
                    _isOnAlert = true;
                    _target = pTarget;
                }
            }
            else
            {
                _isOnAlert = true;
                _target = pTarget;
            }
        }

        public override void Damage()
        {
            if (_behaviour == Behaviour.Careful)
            {
                _behaviour = Behaviour.Aggressive;
            }
        }

        public void CheckIfTargetIsSafe()
        {
            if (_target != null)
            {
                if (_target.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe)
                {
                    RemoveAlert();
                }
            }
        }

        public void RemoveAlert()
        {
            _isOnAlert = false;
            _target = null;
        }
    }
}