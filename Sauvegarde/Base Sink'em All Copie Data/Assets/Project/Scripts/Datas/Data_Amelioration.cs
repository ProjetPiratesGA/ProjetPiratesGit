using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Amelioration
    {
        private int item = -1;
        private int level = 2;
        private int price = -1;
        private float reputationNecessary = -1;

        public int Item { get { return item; } set { item = value; } }
        public int Price { get { return price; } set { price = value; } }
        public int Level { get { return level; } set { level = value; } }
        public float ReputationNecessary { get { return reputationNecessary; } set { reputationNecessary = value; } }
    }
}