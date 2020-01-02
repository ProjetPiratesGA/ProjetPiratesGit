using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Resources
    {
        private int golds = -1;
        private int woodBoard = -1;
        private float reputation = -1;
        
        public int Golds { get { return golds; } set { golds = value; } }
        public int WoodBoard { get { return woodBoard; } set { woodBoard = value; } }
        public float Reputation { get { return reputation; } set { reputation = value; } }

    }
}