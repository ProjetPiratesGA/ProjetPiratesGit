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

        /// <summary>
        /// On envoie en paramètre le player concerné afin de récupérer ses informations
        /// </summary>
        /// <param name="_playerToKnow"></param>
        public void SetPlayerToKnow(GameObject _playerToKnow)
        {
            _player = _playerToKnow;
        }

        void UpdateXpOtherPlayer()
        {
            if (_XpValueText != null)
            {
                if (_player != null)
                {
                    _XpValueText.text = _player.GetComponent<Player>()._data.Ressource.Reputation.ToString();
                }
                else
                {
                    _XpValueText.text = "Joueur NULL";

                }
            }

        }

        public void UpdateLifeBar()
        {
            if (_player != null)
            {
                BoatCharacter boatChar = _player.GetComponentInChildren<BoatCharacter>();
                if (boatChar == null)
                {
                    Debug.LogError("Boatchar is null");
                    Debug.Break();
                }
                //if (boatChar.getCurrentLife() == null)
                //{
                //    Debug.LogError("Boatchar is null");
                //    Debug.Break();
                //}
                //if (boatChar.getMaxLife() == null)
                //{
                //    Debug.LogError("Boatchar is null");
                //    Debug.Break();
                //}
                if (boatChar.getCurrentLife() >= 0 && boatChar.getCurrentLife() <= boatChar.getMaxLife())
                {
                    //Debug.LogError("Life : " + boatChar.getCurrentLife());
                    //Debug.LogError("before : " + lifeBar.GetComponent<RectTransform>().sizeDelta);
                    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / boatChar.getMaxLife()) * boatChar.getCurrentLife(), lifeBar.GetComponent<RectTransform>().sizeDelta.y);
                    //Debug.LogError("after : " + lifeBar.GetComponent<RectTransform>().sizeDelta);
                    //Debug.Break();
                }
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
            _playerName.text = _player.GetComponent<Player>()._username;
        }
    }
}
