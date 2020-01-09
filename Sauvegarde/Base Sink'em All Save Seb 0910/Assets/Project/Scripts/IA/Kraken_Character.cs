using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Kraken_Character : Character {

        [SerializeField] Kraken_Head _head;
        [SerializeField] List<Kraken_Tentacle> _tentacles;

        private float _currentHeadTime = 0;
        [SerializeField] private float _totalHeadTime;
        [SerializeField] private float _downPosition;
        [SerializeField] private float _upPosition;

        // Use this for initialization
        void Start() {
            _data.Life = _maxLifePoint;
        }

        // Update is called once per frame
        void Update() {
            if (_head.IsDamaged)
            {
                ReceiveDamage(_head.Damage);
                _head.DisableDamage();
            }
            for (int i = 0; i < _tentacles.Count; i++)
            {
                if (_tentacles[i].IsDamaged)
                {
                    ReceiveDamage(_tentacles[i].Damage);
                    _tentacles[i].DisableDamage();
                }
            }
        }

        public void MoveHead(bool pGoingUp)
        {
            if (pGoingUp)
            {
                _currentHeadTime += Time.deltaTime / _totalHeadTime;
            }
            else
            {
                _currentHeadTime -= Time.deltaTime / _totalHeadTime;
            }

            Vector3 pos = _head.transform.localPosition;
            pos.y = Mathf.Lerp(_downPosition, _upPosition, _currentHeadTime);
            if (_currentHeadTime > 1)
            {
                _currentHeadTime = 1;
                pos.y = _upPosition;
            }
            if (_currentHeadTime < 0)
            {
                _currentHeadTime = 0;
                pos.y = _downPosition;
            }
            _head.transform.localPosition = pos;

        }

        void ReceiveDamage(int pDamage)
        {
            _data.Life -= pDamage;
            Debug.Log(_data.Life);
            if (_data.Life < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}