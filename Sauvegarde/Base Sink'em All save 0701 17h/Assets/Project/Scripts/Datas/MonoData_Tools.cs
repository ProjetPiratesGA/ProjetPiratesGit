using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    public class MonoData_Tools : MonoSingletonTest<MonoData_Tools>
    {
        public static string pathHDDQuestes;
        public static string pathHDDPnj;
        public static string pathHDDDock;
        public static string pathHDDbject;
        public static string pathHDDDialogues;
        public static string pathHDDServer;

        // Use this for initialization      
        public void Start()
        {
            pathHDDQuestes = Application.persistentDataPath + "/Resources/Datas/Questes/";
            pathHDDPnj = Application.persistentDataPath + "/Resources/Datas/Pnj/";
            pathHDDDock = Application.persistentDataPath + "/Resources/Datas/Dock/";
            pathHDDbject = Application.persistentDataPath + "/Resources/Datas/Object/";
            pathHDDDialogues = Application.persistentDataPath + "/Resources/Datas/Dialogues/";
            pathHDDServer = Application.persistentDataPath + "/Resources/Datas/Server/";

            Debug.Log("Init Path finished");
        }

        public static void initVar()
        {
            pathHDDQuestes = Application.persistentDataPath + "/Resources/Datas/Questes/";
            pathHDDPnj = Application.persistentDataPath + "/Resources/Datas/Pnj/";
            pathHDDDock = Application.persistentDataPath + "/Resources/Datas/Dock/";
            pathHDDbject = Application.persistentDataPath + "/Resources/Datas/Object/";
            pathHDDDialogues = Application.persistentDataPath + "/Resources/Datas/Dialogues/";
            pathHDDServer = Application.persistentDataPath + "/Resources/Datas/Server/";
        }
    }

}