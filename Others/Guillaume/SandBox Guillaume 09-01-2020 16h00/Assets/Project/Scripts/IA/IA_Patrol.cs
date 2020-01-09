using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class IA_Patrol : MonoBehaviour
    {
        [Header("IA Patrol")]
        [SerializeField] protected IA_Controller _controller;
        // Use this for initialization
        void Start()
        {
            
        }

        protected void SetUpCharacter()
        {
            if (this.gameObject.GetComponent<Ship_Controller>() != null)
            {
                _controller = this.gameObject.GetComponent<Ship_Controller>();
            }
            else if (this.gameObject.GetComponent<Shark_Controller>() != null)
            {
                _controller = this.gameObject.GetComponent<Shark_Controller>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        virtual public Vector3 Patrol()
        {
            return Vector3.zero;
        }

        
    }
}