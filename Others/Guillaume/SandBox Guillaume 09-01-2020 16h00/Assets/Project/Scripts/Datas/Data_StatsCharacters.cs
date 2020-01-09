using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_StatsCharacters
    {
        private float life = 0;
        private float speed = 0;

        public float Life { get { return life; } set { life = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
    }
}