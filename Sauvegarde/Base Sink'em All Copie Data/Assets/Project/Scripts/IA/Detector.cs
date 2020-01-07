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
        [SerializeField] private bool _affectedBySafeZone = true;

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
            if (_affectedBySafeZone)
            {
                if (other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
                {
                    if (!other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe)
                    {
                        for (int i = 0; i < _enemies.Count; i++)
                        {
                            _enemies[i].TryAlert(other.gameObject);
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].TryAlert(other.gameObject);
                }
            }
            //Put all the associated enemies on alert if they aren't already


        }

        private void OnTriggerStay(Collider other)
        {
            //Check if object in collision is a player
            if (_affectedBySafeZone)
            {
                if (other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
                {
                    if (!other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe)
                    {
                        for (int i = 0; i < _enemies.Count; i++)
                        {
                            _enemies[i].TryAlert(other.gameObject);
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].TryAlert(other.gameObject);
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