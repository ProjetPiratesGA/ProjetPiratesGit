using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Detector : MonoBehaviour
    {
        [Header("Detector")]
        [SerializeField] private List<IA_Controller> _enemies;
        [SerializeField] private bool _removeAlertWhenOutOfView = true;

        // Use this for initialization
        void Start()
        {
            this.gameObject.layer = 2;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            //Check if object in collision is a player
            if (other.gameObject.tag == "Player")
            {
                //Put all the associated enemies on alert if they aren't already
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (!_enemies[i].IsOnAlert)
                    {
                        _enemies[i].Alert(other.gameObject);
                    }
                }
            }
        }
        
        private void OnTriggerStay(Collider other)
        {
            //Check if object in collision is a player
            if (other.gameObject.tag == "Player")
            {
                //Put all the associated enemies on alert if they aren't already
                for (int i = 0; i < _enemies.Count; i++)
                {
                    if (!_enemies[i].IsOnAlert)
                    {
                        _enemies[i].Alert(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Check if object in collision is a player
            if (other.gameObject.tag == "Player" & _removeAlertWhenOutOfView)
            {
                //Remove the state of alert form all the associated enemies
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].RemoveAlert();
                }
            }
        }
    }
}