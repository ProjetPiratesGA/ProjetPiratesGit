using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Boat
{
    public class InvisibleWallPoint : MonoBehaviour
    {

        private bool _invisibleWallIsOn = false;
        private BoxCollider _boxCollider;

        public BoxCollider _collider
        {
            get
            {
                return _boxCollider;
            }
        }

        // Use this for initialization
        void Start()
        {
            _boxCollider = this.GetComponent<BoxCollider>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Island")
            {
                _invisibleWallIsOn = true;
                Debug.Log("Island");
            }
            else if (other.gameObject.tag != "Detector" & other.gameObject.tag != "Player")
            {
                _invisibleWallIsOn = false;
                //Debug.Log(other.gameObject.tag);

            }
            //Debug.Log("False");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Island")
            {
                _invisibleWallIsOn = false;
                //Debug.Log("Island");
            }
            //Debug.Log("False");
        }



        public bool InvisibleWallIsOn()
        {
            return _invisibleWallIsOn;
        }
    }
}