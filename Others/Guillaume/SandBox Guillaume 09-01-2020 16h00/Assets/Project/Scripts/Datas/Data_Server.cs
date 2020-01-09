using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ProjetPirate.Data
{
    [System.Serializable]
    public static class Data_server
    {
        private static int countIDUnique = 0;
        private static List<ClientData> clientRegistered = new List<ClientData>();
        public static List<ClientData> ClientRegistered
        {
            get { return clientRegistered; }
        }

        public static void InitData_server()
        {
            clientRegistered = SaveSystem.LoadClientList();
        }

        public static int CountIDUnique { get { return countIDUnique; } set { countIDUnique = value; } }
    }
}
