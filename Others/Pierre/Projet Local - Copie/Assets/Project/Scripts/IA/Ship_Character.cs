using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public enum ShipType
    {
        Military,
        Merchant
    }

    public class Ship_Character : Character
    {
        [Header("Ship Data")]
        [SerializeField] private ShipType _shipType;
        [SerializeField] private float _currentSpeed = 0;
        [SerializeField] private float _acceleration = 1;
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private int _numberPlankDroppedByDeath = 1;
        [SerializeField] GameObject _droppedPlank;
        [SerializeField] GameObject _droppedChest;

        private ProjetPirate.Data.Data_Military _data = new ProjetPirate.Data.Data_Military();

        public ProjetPirate.Data.Data_Military Data
        {
            get { return _data; }
        }


        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;

        private bool _isMovingForward = false;
        private float _stoppingDistance;

        public float CurrentSpeed
        {
            get { return _data.Boat.dStats.Speed; }
        }

        public float Acceleration
        {
            get { return _acceleration; }
        }

        public float StoppingDistance
        {
            get { return _stoppingDistance; }
        }

        // Use this for initialization
        void Start()
        {
            _currentLifePoint = _maxLifePoint;
            //_data.Boat.dStats.MaxLife = _maxLifePoint;
            //_data.Boat.dStats.Life = _maxLifePoint;
            //_data.Boat.dStats.Damage = _attackDamage;
        }

        // Update is called once per frame
        void Update()
        {
            _isMovingForward = false;

            _currentLarboardShootCooldownTime += Time.deltaTime;
            _currentStarboardShootCooldownTime += Time.deltaTime;
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

            //_data.Boat.ReverseReloadTransform(this.gameObject);
        }

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * _data.Boat.dStats.Speed * Time.deltaTime;
            pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        public void Accelerate()
        {
            _data.Boat.dStats.Speed += _acceleration * Time.deltaTime;
            if (_data.Boat.dStats.Speed > _maxMovingSpeed)
            {
                _data.Boat.dStats.Speed = _maxMovingSpeed;
            }
            _stoppingDistance = ((_data.Boat.dStats.Speed / 10) * (_data.Boat.dStats.Speed / 10)) * 50 / _acceleration;
        }

        public void Decelerate()
        {
            _data.Boat.dStats.Speed -= _acceleration * Time.deltaTime;
            if (_data.Boat.dStats.Speed < 0)
            {
                _data.Boat.dStats.Speed = 0;
            }
            _stoppingDistance = ((_data.Boat.dStats.Speed / 10) * (_data.Boat.dStats.Speed / 10)) * 50 / _acceleration;
        }

        //Turn left based on _angularSpeed
        public override void TurnLarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, -_maxAngularSpeed * Time.deltaTime, 0);
            }
        }

        //Turn right based on _angularSpeed
        public override void TurnStarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, _maxAngularSpeed * Time.deltaTime, 0);
            }
        }

        //Shoot at Larboard
        public void ShootLarboard()
        {
            
            if (!_larboardCannonInCooldown)
            {
                for (int i = 0; i < _larboardCannons.Count; i++)
                {
                    _larboardCannons[i]._FireCannon();
                }
                _larboardCannonInCooldown = true;
            }
            
            
        }

        //Shoot at Starboard
        public void ShootStarboard()
        {
            if (!_starboardCannonInCooldown)
            {
                for (int i = 0; i < _starboardCannons.Count; i++)
                {
                    _starboardCannons[i]._FireCannon();
                }
                _starboardCannonInCooldown = true;
            }
        }

        public override int Damage(int _damage)
        {
            _data.Boat.dStats.Life -= _damage;
            if (_data.Boat.dStats.Life <= 0)
            {
                Death();
                return _xpEarned;
            }
            return 0;
        }

        public override void Death()
        {
            PlankOnSea.SpawnPlank(_droppedPlank, this.transform.position, _numberPlankDroppedByDeath);
            Chest.SpawnChest(_droppedChest, this.transform.position, _goldDropped);
            Destroy(this.gameObject);
        }

        public void Stop()
        {
            _data.Boat.dStats.Speed = 0;
        }
    }
}