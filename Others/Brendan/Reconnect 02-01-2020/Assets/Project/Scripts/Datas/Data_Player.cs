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

        public Data_Player(int _clientID = -1)
        {
            player_ID = _clientID;
            resources.Golds = 5000;
            resources.Reputation = 1500;
            resources.WoodBoard = 300;
            Debug.Log("pLAYER CONSTRUCTOR : " + player_ID + " " + resources.Golds + " " + resources.Reputation + " " + resources.WoodBoard);
            Boat = new Data_Boat(_clientID);
        }

        private Data_Resources resources = new Data_Resources();
        Data_Boat _boat = null;

        public Data_Resources dRessource { get { return resources; } set { resources = value; } }
        public Data_Boat Boat { get { return _boat; } set { _boat = value; } }
    }
}
