using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Store
    {
        private Data_Amelioration ameliorationBoat = new Data_Amelioration();
        private Data_Amelioration ameliorationCanon = new Data_Amelioration();

        public Data_Amelioration AmeliorationBoat { get {return ameliorationBoat; } set { ameliorationBoat = value; } }
        public Data_Amelioration AmeliorationCanon { get {return ameliorationCanon; } set { ameliorationCanon = value; } }
    }
}