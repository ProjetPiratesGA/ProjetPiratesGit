using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Player
    {
        private int player_ID = -1;
        public int Player_ID
        {
            get { return player_ID; }
            set { player_ID = value; }
        }

        public Data_Player (int _clientID)
        {
            player_ID = _clientID;
        }

        private Data_Resources resources = new Data_Resources();
        Data_Boat _boat = null;

        public Data_Resources dRessource { get { return resources; } set { resources = value; } }
        public Data_Boat Boat { get { return _boat; } set { _boat = value; } }
    }
}
