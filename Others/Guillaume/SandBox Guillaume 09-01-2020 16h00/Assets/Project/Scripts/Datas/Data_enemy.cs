using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_enemy
    {
        int level = -1;
        Data_StatsCharacters stats = new Data_StatsCharacters();
        Data_Transform transform = new Data_Transform();
        Data_Resources resources = new  Data_Resources();
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
    }
}


