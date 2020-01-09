using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    public class Data_Prow
    {
        myVector3 color = new myVector3();
        Data_SpearGun spearGun = null;

        public Data_SpearGun SpearGun { get { return spearGun; } set { spearGun = value; } }
        public myVector3 Color { get { return color; } set { color = value; } }
    }
}