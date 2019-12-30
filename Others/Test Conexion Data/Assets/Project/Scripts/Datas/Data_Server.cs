using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_server
    {
        private int countIDUnique = 0;
        private List<ClientData> clientRegistered = new List<ClientData>();
        private List<Data_Enemies> enemiesList = new List<Data_Enemies>();

        public List<ClientData> ClientRegistered
        {
            get { return clientRegistered; }
        }

        public List<Data_Enemies> EnemiesList { get { return enemiesList; } set { enemiesList = value; } }

        public void InitData_server()
        {
            clientRegistered = SaveSystem.LoadClientList();
        }

        public int CountIDUnique { get { return countIDUnique; } set { countIDUnique = value; } }
    }
}
