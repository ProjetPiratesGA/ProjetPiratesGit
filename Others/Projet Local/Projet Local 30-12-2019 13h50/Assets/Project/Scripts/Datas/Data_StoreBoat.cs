using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_StoreBoat
    {

        Data_Boat boat;
        /* range where you are fight back by the ennemy when you first attack him */
        float attackRange = 0;
        /* range where you the ennemy flee you when you first attack him */
        float fleeRange = 0;
        /* range where you the ennemy keep chasing you when you fight him */
        float fieldOfVision = 0;
        /* origin spawn of the enemy */
        myVector3 originPosition;
        /* the radius of the circle where the enemy can move, its center is the origin position */
        float wanderRange = 0;


        public Data_Boat Boat { get { return boat; } set { boat = value; } }
        public myVector3 OriginPosition { get { return originPosition; } set { originPosition = value; } }
        public float AttackRange { get { return attackRange; } set { attackRange = value; } }
        public float FleeRange { get { return fleeRange; } set { fleeRange = value; } }
        public float FieldOfVision { get { return fieldOfVision; } set { fieldOfVision = value; } }
        public float WanderRange { get { return wanderRange; } set { wanderRange = value; } }
    }
}
