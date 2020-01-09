using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Patrol_Static : IA_Patrol
    {
        [Header("Patrol Static")]
        [SerializeField] private Transform _waitingPoint;

        // Use this for initialization
        void Start()
        {
            //Check if a IA_Character class is associated to that Script and what type it is.
            base.SetUpCharacter();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override Vector3 Patrol()
        {
            return _waitingPoint.position;
        }
    }
}