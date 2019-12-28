using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.UI.HUD
{
    public class InterractionJoueur : MonoBehaviour
    {

        private GameObject _player;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_player != null)
            {
                this.transform.position = _player.transform.position;
            }
        }

        public void SetPlayerToFollow(GameObject _playerToFolow)
        {
            _player = _playerToFolow;
        }
    }
}
