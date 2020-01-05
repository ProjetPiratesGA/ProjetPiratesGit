using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Canon
    {
        private int id = 0;
        private myVector3 color = new myVector3();

        public int ID { get { return id; } set { id = value; } }
        public myVector3 Color { get { return color; } set { color = value; } }
    }
}