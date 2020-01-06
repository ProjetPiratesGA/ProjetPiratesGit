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
    public class Ship_Character : IA_Character
    {
        [Header("Ship Data")]
        [SerializeField] private ShipType _shipType;
        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;
        [SerializeField] private float _shootCooldown;
        [SerializeField] GameObject _droppedPlank;

        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;

        private bool _isMovingForward = false;

        // Use this for initialization
        void Start()
        {
            _currentLifePoint = _maxLifePoint;

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
        }

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            pos += this.transform.forward * _movingSpeed * Time.deltaTime;
            pos.y = 0;
            this.transform.position = pos;
            _isMovingForward = true;
        }

        //Turn left based on _angularSpeed
        public override void TurnLarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, -_angularSpeed * Time.deltaTime, 0);
            }
        }

        //Turn right based on _angularSpeed
        public override void TurnStarboard()
        {
            if (_isMovingForward)
            {
                this.transform.Rotate(0, _angularSpeed * Time.deltaTime, 0);
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

        public override void Death()
        {
            Instantiate(_droppedPlank).transform.position = this.transform.position + new Vector3(0, 0.712f, 0);
            Destroy(this.gameObject);
        }
    }
}