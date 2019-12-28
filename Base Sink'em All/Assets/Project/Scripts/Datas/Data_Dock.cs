using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Dock
    {
        Data_Transform transform = new Data_Transform();
        Data_Pnj pnj;
        bool isEmpty = false;
        string pnjName = null;


        public Data_Transform Transform { get { return transform; } set { transform = value; } }
        public bool IsEmpty { get { return isEmpty; } set { isEmpty = value; } }
        public string PnjName { get { return pnjName; } set { pnjName = value; } }

        public Data_Dock()
        {
            pnjName = "Adalyn North";
            pnj = Data_Tools.FindPnj(pnjName);
        }
    }
}