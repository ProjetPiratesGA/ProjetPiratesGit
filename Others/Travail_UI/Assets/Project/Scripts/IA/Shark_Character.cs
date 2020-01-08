﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace ProjetPirate.IA
{
    public class Shark_Character : IA_Character
    {
        private float Fow = 3f;
        //public Vector3 TargetPosition = Vector3.zero;
        //public Transform TargetTransform;
        //private GameObject Target;

        public bool _isCharging = false;
        public bool _isApplyed = false;
        public bool _isFalling = false;
        public bool _isMovingForward = false;

        private float BaseSpeed;
        public bool _inBattle = false;

        [Header("Shark Data")]
        [SerializeField] private float _battleSpeed;
        [SerializeField] private Vector3 FallingTarget = Vector3.zero;

        void Start()
        {
            //Target = Player_Singleton.instance.Player;
            //TargetTransform = Player_Singleton.instance.Player.transform;
            //TargetPosition = Target.transform.position;
            BaseSpeed = _battleSpeed;
            _currentLifePoint = _maxLifePoint;

        }

        // Move forward based on _movingSpeed
        public override void MoveForward()
        {
            Vector3 pos = this.transform.position;
            if (!_inBattle)
            {
                pos += this.transform.forward * _movingSpeed * Time.deltaTime;
            }
            else
            {
                pos += this.transform.forward * _battleSpeed * Time.deltaTime;
            }
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


        /// <summary>
        /// If threres a targer, make the shark face it.
        /// </summary>
        public void TurnAroundTargetPosition(Vector3 _Target)
        {
            Vector3 TargetStockedPos = new Vector3(_Target.x, 0, _Target.z);
            Vector3 direction = (TargetStockedPos - transform.position).normalized;
            Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = LookRotation;
            transform.Rotate(0, 89.5f, 0);
        }



        public void Jumping(Vector3 _Target)
        {
            if (_battleSpeed != 50)
            {
                _battleSpeed = 50;
            }
            Vector3 TargetStockedPos = new Vector3(_Target.x, 1.5f, _Target.z);
            Vector3 direction = (TargetStockedPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

            if (Vector3.Distance(transform.position, TargetStockedPos) >= 1f && _isFalling == false)
            {
                _isCharging = true;
            }
            else if (Vector3.Distance(transform.position, TargetStockedPos) < 1f)
            {
                _isFalling = true;
                _isCharging = false;
                FallingTarget = transform.position + transform.forward * 20;
                FallingTarget.y = 0;
            }
        }

        public bool Falling()
        {
            Vector3 TargetStockedPos = new Vector3(FallingTarget.x, FallingTarget.y, FallingTarget.z);
            Vector3 direction = (TargetStockedPos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

            if (Vector3.Distance(transform.position, TargetStockedPos) <= 0.5f)
            {
                _isCharging = false;
                _isFalling = false;
                _isApplyed = false;
                _battleSpeed = BaseSpeed;
                return true;
            }
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, Fow);
        }
    }
}