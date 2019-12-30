using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjetPirate.Data
{

    [System.Serializable]
    public class Data_Tentacle
    {
        Data_Transform transform = new Data_Transform();
        Data_StatsCharacters stats = new Data_StatsCharacters();

        myVector3 originPosition;
        bool isUnderWater = false;

        private int id = -1;
        private int idTarget = -1;

        public bool IsUnderWater { get { return isUnderWater; } set { isUnderWater = value; } }
        public myVector3 OriginPosition { get { return originPosition; } set { originPosition = value; } }

        public Data_Transform Transform { get { return transform; } set { transform = value; } }
        public Data_StatsCharacters Stats { get { return stats; } set { stats = value; } }

        public int ID { get { return id; } set { id = value; } }
        public int IdTarget { get { return idTarget; } set { idTarget = value; } }
    }

}