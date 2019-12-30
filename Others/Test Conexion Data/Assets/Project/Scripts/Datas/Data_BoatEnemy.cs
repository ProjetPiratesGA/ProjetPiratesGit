using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_BoatEnemy : Data_Enemies
    {
        Data_Boat boat;
        private int id = -1;
        private int idTarget = -1;

        public Data_Boat Boat { get { return boat; } set { boat = value; } }
        public int ID { get { return id; } set { id = value; } }
        public int IdTarget { get { return idTarget; } set { idTarget = value; } }
    }
}
