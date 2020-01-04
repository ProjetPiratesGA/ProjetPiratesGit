using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Military
    {
        Data_Boat boat;
        /* range where you are fight back by the ennemy when you first attack him */
        float attackRange = 0;

        float minRange = 0;
        float maxRange = 0;

        /* range where you the ennemy keep chasing you when you fight him */
        float fieldOfVision = 0;
        /* origin spawn of the enemy */
        myVector3 originPosition;



        public Data_Boat Boat { get { return boat; } set { boat = value; } }
        public myVector3 OriginPosition { get { return originPosition; } set { originPosition = value; } }
        public float AttackRange { get { return attackRange; } set { attackRange = value; } }
        public float MinRange { get { return minRange; } set { minRange = value; } }
        public float FieldOfVision { get { return fieldOfVision; } set { fieldOfVision = value; } }
        public float MaxRange { get { return maxRange; } set { maxRange = value; } }
    }
}
