using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Sail
    {
        myVector3 color = new myVector3();


        public myVector3 Color { get { return color; } set { color = value; } }
    }
}