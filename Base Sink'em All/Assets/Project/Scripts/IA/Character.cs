using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Character : MonoBehaviour
    {
        [Header("Character Data")]
        [SerializeField] protected int _maxLifePoint;
        protected int _currentLifePoint;
        [SerializeField] protected int _attackDamage;
        [SerializeField] protected float _maxMovingSpeed;
        protected float _currentMovingSpeed;
        [SerializeField] protected float _maxAngularSpeed;
        protected float _currentAngularSpeed;
        [SerializeField] protected int _goldDropped;
        [SerializeField] protected int _xpEarned;
        [SerializeField] protected Transform _directionLocator;

        public int MaxLifePoint
        {
            get { return _maxLifePoint; }
        }

        public int CurrentLifePoint
        {
            get { return _currentLifePoint; }
        }

        public int AttackDamage
        {
            get { return _attackDamage; }
        }

        public float MaxMovingSpeed
        {
            get { return _maxMovingSpeed; }
        }

        public float CurrentMovingSpeed
        {
            get { return _currentMovingSpeed; }
        }

        public float MaxAngularSpeed
        {
            get { return _maxAngularSpeed; }
        }

        public float CurrentAngularSpeed
        {
            get { return _currentAngularSpeed; }
        }

        public int Gold
        {
            get { return _goldDropped; }
        }

        public int XP
        {
            get { return _xpEarned; }
        }

        public Transform DirectionLocator
        {
            get { return _directionLocator; }
        }

        // Use this for initialization
        void Start()
        {
            _currentLifePoint = _maxLifePoint;
            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual void MoveForward()
        {
           
        }

        //Turn left based on _angularSpeed
        public virtual void TurnLarboard()
        {
            
        }

        //Turn right based on _angularSpeed
        public virtual void TurnStarboard()
        {
            
        }

        public virtual int Damage(int _damage)
        {
            _currentLifePoint -= _damage;
            if (_currentLifePoint <= 0)
            {
                Death();
                return _xpEarned;
            }
            return 0;
        }

        public virtual void Death()
            {
                Destroy(this.gameObject);
            }
    }
}