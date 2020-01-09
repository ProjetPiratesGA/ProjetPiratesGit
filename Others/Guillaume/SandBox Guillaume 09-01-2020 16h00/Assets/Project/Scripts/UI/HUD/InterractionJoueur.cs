using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Boat;

namespace ProjetPirate.UI.HUD
{
    public class InterractionJoueur : MonoBehaviour
    {

        private GameObject _player;

        public GameObject lifeBar;
        public Text _XpValueText;
        public Text _playerName;

        [Header("Var Test au cas où certains accesseur sont indisponibles")]
        public int hp = 50;
        public int maxhp = 50;


        private float maxLifeBarSize;
        // Use this for initialization
        void Start()
        {
            maxLifeBarSize = lifeBar.GetComponent<RectTransform>().sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (_player != null)
            {
               this.transform.position = Camera.main.WorldToScreenPoint(_player.transform.position);
                UpdateXpOtherPlayer();
                UpdateLifeBar();
                UpdatePlayerName();
            }
            
        }

        public void SetPlayerToFollow(GameObject _playerToFolow)
        {
            _player = _playerToFolow;
        }

        void UpdateXpOtherPlayer()
        {
            //Remplacer par un accesseur aux gold du joueur
            _XpValueText.text = "test";
            //_XpValueText.text = _player.getGlod();
        }

        public void UpdateLifeBar()
        {
            if (_player != null)
            {
                //Remplacer par un accesseur aux HP du joueur
                //if (_player.GetComponent<BoatCharacter>().getCurrentLife() >= 0 && _player.GetComponent<BoatCharacter>().getCurrentLife() <= _player.GetComponent<BoatCharacter>().getMaxLife())
                //    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / _player.GetComponent<BoatCharacter>().getMaxLife()) * _player.GetComponent<BoatCharacter>().getCurrentLife(), lifeBar.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
            {
                if (hp >= 0 && hp <= maxhp)
                    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / maxhp) * hp, lifeBar.GetComponent<RectTransform>().sizeDelta.y);

            }
        }

        void UpdatePlayerName()
        {
            //Remplacer par un accesseur au pseudo du joeueur
            _playerName.text = _player.gameObject.ToString();
        }
    }
}
