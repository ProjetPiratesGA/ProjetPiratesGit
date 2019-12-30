using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Team
    {
        List<Data_Player> player = new List<Data_Player>();

        public List<Data_Player> Player { get { return player; } set { player = value; } }  
    }
}