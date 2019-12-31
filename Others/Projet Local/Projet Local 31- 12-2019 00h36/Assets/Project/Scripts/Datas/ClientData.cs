﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProjetPirate.Data
{
    [Serializable]
    public class ClientData
    {
        private string strUsername = "";
        private int strPassword = 0;
        private int id = -1;
        private Data_Player data_Player = new Data_Player(-1);
        private bool accountIsUsed;

        public string Username { get { return strUsername; } set { strUsername = value; } }
        public int Password { get { return strPassword; } set { strPassword = value; } }
        public int ID { get { return id; } set { id = value; } }

        public bool AccountIsUsed { get { return accountIsUsed; } set { accountIsUsed = value; } }


        public Data_Player Player
        {
            get { return data_Player; }
        }

        public ClientData(string _username, int cryptedPassword)
        {
            strUsername = _username;
            strPassword = cryptedPassword;
            id = Data_server.CountIDUnique;
            Data_server.CountIDUnique++;
        }
        public ClientData(ClientData _copy)
        {
            strUsername = _copy.strUsername;
            strPassword = _copy.strPassword;
            id = _copy.id;
        }
        public ClientData(ClientData _copy, Data_Player p_dataPlayer, Data_Boat p_data_Boat)
        {
            strUsername = _copy.strUsername;
            strPassword = _copy.strPassword;
            id = _copy.id;
            data_Player = p_dataPlayer;
            data_Player.Boat = p_data_Boat;
        }
    }
}