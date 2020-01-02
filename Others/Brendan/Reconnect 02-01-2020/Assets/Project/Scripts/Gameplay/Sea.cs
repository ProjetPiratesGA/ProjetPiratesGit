using ProjetPirate.Physic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Gameplay
{

    public class Sea : MonoBehaviour
    {


        [Header("SEA VARIABLES")]
        private Transform _seaTransform;
        [SerializeField]
        private float _seaRadius = 200f;
        [SerializeField]
        private float _attractForce = 10f;
        [SerializeField]
        private float _seaZoneLimit = 100f;

        static public List<AttractObject> _attractObjects = new List<AttractObject>();

        // Use this for initialization
        void Start()
        {
            _seaTransform = this.transform;
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < _attractObjects.Count; i++)
            {
                if (!_attractObjects[i]._isFalling)
                {
                    if (_attractObjects[i].AttractMethod(_seaTransform, _seaRadius, _attractForce, _seaZoneLimit))
                    {
                        if (_attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
                        {
                            Vector3 rotation = _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().transform.eulerAngles;
                            Vector3 position = _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().transform.position;
                            _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().Controller.transform.position = position;
                            _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().Controller.transform.LookAt(this.transform);

                            _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().transform.eulerAngles = rotation;
                            _attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().transform.position = position;
                            //_attractObjects[i].GetComponent<ProjetPirate.Boat.BoatCharacter>().Fall();
                        }
                    }
                }

            }
        }
    }
}

