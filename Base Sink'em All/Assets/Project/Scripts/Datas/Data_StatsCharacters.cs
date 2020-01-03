using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_StatsCharacters
    {
        private int life = 100;
        private float speed = 0;
        Dictionary<int, int> damageReceived = new Dictionary<int, int>();

        public int Life { get { return life; } set { life = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public Dictionary<int, int> DamageReceived { get { return damageReceived; } set { damageReceived = value; } }
    }
}