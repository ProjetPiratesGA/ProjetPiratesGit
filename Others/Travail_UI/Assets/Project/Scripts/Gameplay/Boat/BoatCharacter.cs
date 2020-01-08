using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjetPirate.Boat
{
    /// <summary>
    /// Base class of the boat posess al the main data
    /// </summary>
    public class BoatCharacter : MonoBehaviour
    {
        [Header("LIFE")]
        [SerializeField]
        private int _maxLife = 10;
        private int _currentLife;
        [Header("XP/REPUTATION")]
        [SerializeField] public int _maxXp = 10000; // max XP
        public int _currentXp;
        [SerializeField] public int _maxPlank = 10000; // max XP
        [SerializeField] public int _currentPlank;
        [SerializeField] int _xpLostByDeath = 10;

        [SerializeField] private List<Cannon> _larboardCannons;
        [SerializeField] private List<Cannon> _starboardCannons;

        [SerializeField] private float _shootCooldown;

        private bool _larboardCannonInCooldown = false;
        private bool _starboardCannonInCooldown = false;
        private float _currentLarboardShootCooldownTime = 0;
        private float _currentStarboardShootCooldownTime = 0;

        // Use this for initialization
        void Start()
        {
            //set the currentLife of the boat when it appear
            _currentLife = _maxLife;
            _currentXp = 1000;
        }

        // Update is called once per frame
        void Update()
        {

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

        //Shoot at Larboard (Babord)
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


        //Shoot at Starboard (Tribord)
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

        public void Damage(int _damage)
        {
            _currentLife -= _damage;
            if (_currentLife <= 0)
            {
               // Death();
            }
        }

        public virtual void Death()
        {
            _currentLife = _maxLife;
            _currentXp -= _xpLostByDeath;
            if (_currentXp < 0)
            {
                _currentXp = 0;
            }
            this.transform.position = new Vector3(0, 0, -105);
            this.GetComponent<BoxCollider>().enabled = false;
            ProjetPirate.IA.Ship_Controller[] enemies = FindObjectsOfType<ProjetPirate.IA.Ship_Controller>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].Target == this.gameObject)
                {
                    enemies[i].RemoveAlert();
                }
            }
            this.GetComponent<BoxCollider>().enabled = true;

        }

        public virtual void GainXP(int pEarnedXP)
        {
            _currentXp += pEarnedXP;
            if (_currentXp > _maxXp)
            {
                _currentXp = _maxXp;
            }
        }

        public virtual void GainPlank(int pGainedPlank)
        {
            _currentPlank += pGainedPlank;
            if (_currentPlank > _maxPlank)
            {
                _currentPlank = _maxPlank;
            }
        }

        #region ACCESSORS

        public float getShootCoolDown()
        {
            return _shootCooldown;
        }


        public int getMaxLife()
        {
            return _maxLife;
        }

        public float getCurrentLife()
        {
            return _currentLife;
        }

        public int getMaxXp()
        {
            return _maxXp;
        }

        public int getCurrentXp()
        {
            return _currentXp;
        }

        #endregion ACCESSORS
    }
}
