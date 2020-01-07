using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjetPirate.IA
{
    public class Character : NetworkBehaviour
    {
        protected ProjetPirate.Data.Data_StatsCharacters _data = new Data.Data_StatsCharacters();
        private Player _player;
        public Player player { get { return _player; } set { _player = value; } }
        [Header("Character Data")]
        [SerializeField] protected int _maxLifePoint;
        //protected int _currentLifePoint;
        [SerializeField] protected int _attackDamage;
        [SerializeField] protected float _maxMovingSpeed;
        protected float _currentMovingSpeed;
        [SerializeField] protected float _maxAngularSpeed;
        protected float _currentAngularSpeed;
        [SerializeField] protected int _goldDropped;
        [SerializeField] protected int _xpEarned;
        [SerializeField] protected Transform _directionLocator;
        protected Controller _controller;
        public Controller Controller
        {
            get
            {
                return _controller;
            }
        }

        public int MaxLifePoint
        {
            get { return _maxLifePoint; }
        }

        public int CurrentLifePoint
        {
            get { return player._data.Boat.Stats.Life; }
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
            player._data.Boat.Stats.Life = _maxLifePoint;
            _directionLocator = Instantiate(new GameObject()).transform;
            _directionLocator.SetParent(this.transform);
            _directionLocator.localPosition = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FullLife()
        {
            player._data.Boat.Stats.Life = _maxLifePoint;
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
            /*if (_controller.GetComponent<Ship_Controller>() != null)
            {
                _controller.GetComponent<Ship_Controller>().ResetTime();
            }
            if (_controller.GetComponent<Shark_Controller>() != null)
            {
                _controller.GetComponent<Shark_Controller>().ResetTime();
            }*/
            player._data.Boat.Stats.Life -= _damage;
            if (player._data.Boat.Stats.Life <= 0)
            {
                Death();
                return _xpEarned;
            }
            return 0;
        }

        public virtual int Damage(int _damage, Transform pDamageLocation)
        {
            //if (_controller.GetComponent<Ship_Controller>() != null)
            //{
            //    _controller.GetComponent<Ship_Controller>().ResetTime();
            //}
            //if (_controller.GetComponent<Shark_Controller>() != null)
            //{
            //    _controller.GetComponent<Shark_Controller>().ResetTime();
            //}
            player._data.Boat.Stats.Life -= _damage;
            this.CmdSetLife(player._data.Boat.Stats.Life);


            if (player._data.Boat.Stats.Life <= 0)
            {
                Death();
                return _xpEarned;
            }
            return 0;
        }

        [Command]
        public void CmdSetLife(int life)
        {
            player._data.Boat.Stats.Life = life;
        }


        public virtual void Death()
        {
            Destroy(this.gameObject);
        }
    }
}