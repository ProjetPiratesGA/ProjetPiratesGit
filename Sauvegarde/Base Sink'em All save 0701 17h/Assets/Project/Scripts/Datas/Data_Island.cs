using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Island
    {
        int ameliorationPrice;
        Data_Transform transform = new Data_Transform();
        List<Data_Dock> docks = new List<Data_Dock>();
        Data_Pnj pnj = new Data_Pnj();

        public int AmeliorationPrice { get { return ameliorationPrice; } set { ameliorationPrice = value; } }
        public List<Data_Dock> Docks { get { return docks; } set { docks = value; } }
        public Data_Transform Transform { get { return transform; } set { transform = value; } }
        public Data_Pnj Pnj { get { return pnj; } set { pnj = value; } }

        public Data_Island()
        {
           // docks.Add(new Data_Dock()); 
        }



    }
}