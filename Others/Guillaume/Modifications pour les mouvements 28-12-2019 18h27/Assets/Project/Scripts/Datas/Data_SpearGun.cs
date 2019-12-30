using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_SpearGun
    {
        Data_Transform transform = new Data_Transform();
        public Data_Transform dTransform { get { return transform; } set { transform = value; } }
    }
}
