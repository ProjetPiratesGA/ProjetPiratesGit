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
        XP_More,
        XP_Less,
        BoatLevel_More,
        BoatLevel_Less,
        Life_More,
        Life_Less,
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
        [SerializeField] protected List<AggroRequiredType> _targettingPriority;


        protected Character _character;
        protected Character_Shark _characterShark;

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
                if (_character != null)
                    _character.FullLife();
                if (_characterShark != null)
                    _characterShark.FullLife();

                _remainingTimeBeforeResettingHealth = _timeToResetHealth;
                Debug.Log("Reset");
            }
        }

        public void ResetTime()
        {
            _remainingTimeBeforeResettingHealth = _timeToResetHealth;
        }

        public void TryAlert(GameObject pObject)
        {
            if (!IsOnAlert)
            {
                Alert(pObject);
            }
            else if (CheckTargetPriority(pObject.GetComponent<Boat.BoatCharacter>()))
            {
                Alert(pObject);
            }
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
                        case AggroRequiredType.XP_More:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._data.Ressource.Reputation >= _onDetectionRangeCategories[i].Quantity)
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
                        case AggroRequiredType.XP_Less:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._data.Ressource.Reputation <= _onDetectionRangeCategories[i].Quantity)
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
                            _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                            return true;
                        case AggroRequiredType.Life_More:
                            if (pPotentialTarget.getCurrentLife() > _onDetectionRangeCategories[i].Quantity)
                            {
                                _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                            }
                            break;
                        case AggroRequiredType.Life_Less:
                            if (pPotentialTarget.getCurrentLife() < _onDetectionRangeCategories[i].Quantity)
                            {
                                _behaviour = _onDetectionRangeCategories[i].AssociatedBehaviour;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        public bool CheckRangeCategoryOnAttack(ProjetPirate.Boat.BoatCharacter pPotentialTarget)
        {
            float distance = Vector3.Distance(this.transform.position, pPotentialTarget.transform.position);
            for (int i = 0; i < _onAttackRangeCategories.Count; i++)
            {
                if (_onAttackRangeCategories[i].RangeRatio * _defaultRange >= distance | _onAttackRangeCategories[i].RangeRatio == 0)
                {
                    switch (_onAttackRangeCategories[i].Type)
                    {
                        case AggroRequiredType.XP_More:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._data.Ressource.Reputation >= _onAttackRangeCategories[i].Quantity)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case AggroRequiredType.XP_Less:
                            switch (pPotentialTarget.ShipType)
                            {
                                case Boat.ShipType.Player_Level_1:
                                case Boat.ShipType.Player_Level_2:
                                case Boat.ShipType.Player_Level_3:
                                    if (pPotentialTarget.Controller.GetComponent<Player>()._data.Ressource.Reputation <= _onDetectionRangeCategories[i].Quantity)
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
                                    if (_onAttackRangeCategories[i].Quantity <= 1)
                                    {
                                        _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                                        return true;
                                    }
                                    break;
                                case Boat.ShipType.Player_Level_2:
                                    if (_onAttackRangeCategories[i].Quantity <= 2)
                                    {
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
                        case AggroRequiredType.Life_More:
                            if (pPotentialTarget.getCurrentLife() > _onAttackRangeCategories[i].Quantity)
                            {
                                _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                            }
                            break;
                        case AggroRequiredType.Life_Less:
                            if (pPotentialTarget.getCurrentLife() < _onAttackRangeCategories[i].Quantity)
                            {
                                _behaviour = _onAttackRangeCategories[i].AssociatedBehaviour;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return false;
        }

        public bool CheckTargetPriority(ProjetPirate.Boat.BoatCharacter pCharacter)
        {
            bool isTakingPriority = true;
            for (int i = 0; i < _targettingPriority.Count; i++)
            {
                switch (_targettingPriority[i])
                {
                    case AggroRequiredType.XP_More:
                        if (pCharacter.getCurrentXp() < _target.GetComponent<Boat.BoatCharacter>().getCurrentXp())
                        {
                            isTakingPriority = false;
                        }
                        break;
                    case AggroRequiredType.XP_Less:
                        if (pCharacter.getCurrentXp() > _target.GetComponent<Boat.BoatCharacter>().getCurrentXp())
                        {
                            isTakingPriority = false;
                        }
                        break;
                    case AggroRequiredType.BoatLevel_More:
                        if ((int)pCharacter.ShipType < (int)_target.GetComponent<Boat.BoatCharacter>().ShipType)
                        {
                            isTakingPriority = false;
                        }
                        break;
                    case AggroRequiredType.BoatLevel_Less:
                        if ((int)pCharacter.ShipType > (int)_target.GetComponent<Boat.BoatCharacter>().ShipType)
                        {
                            isTakingPriority = false;
                        }
                        break;
                    case AggroRequiredType.None:
                        break;
                    case AggroRequiredType.Life_More:
                        if (pCharacter.getCurrentLife() < _target.GetComponent<Boat.BoatCharacter>().getCurrentLife())
                        {
                            isTakingPriority = false;
                        }
                        break;
                    case AggroRequiredType.Life_Less:
                        if (pCharacter.getCurrentLife() > _target.GetComponent<Boat.BoatCharacter>().getCurrentLife())
                        {
                            isTakingPriority = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            return isTakingPriority;
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