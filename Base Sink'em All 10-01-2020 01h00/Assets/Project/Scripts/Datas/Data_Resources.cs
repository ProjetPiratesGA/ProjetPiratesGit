﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Resources
    {
        private int golds = 1000;
        private int woodBoard = 2;
        private float reputation = 100;
        private int currentBoatLevel = 1;//SEB 09


        public int BoatLevel { get { return currentBoatLevel; } set { currentBoatLevel = value; } }//SEB 09
        public int Golds { get { return golds; } set { golds = value; } }
        public int WoodBoard { get { return woodBoard; } set { woodBoard = value; } }
        public float Reputation { get { return reputation; } set { reputation = value; } }

    }
}