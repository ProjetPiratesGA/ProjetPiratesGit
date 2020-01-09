using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public static class ServerData
    {
        [System.Serializable]
        public struct LogIn
        {
            public string strUsername;/*{ get; set; }*/
            public int cryptedPassword; /*{ get; set; }*/
        }

        /* strings username and password are going to be send by the client who are trying eother to connect itself or to register  */
        static private LogIn _log = new LogIn() { cryptedPassword = -1, strUsername = "" };
        static public LogIn Log
        {
            get
            {
                return _log;
            }
        }

        [System.Serializable]
        public class IntWithLogInDictionnary 
        {
            public Dictionary<LogIn, int> dictionnaryIntWithLogIn = new Dictionary<LogIn, int>();
        }

        /* will register a player's ID with a logIn structure for now it's a int but it will be a gameobject or something */
        private static IntWithLogInDictionnary registeredPlayers = new IntWithLogInDictionnary();
        public static IntWithLogInDictionnary RegisteredPlayers
        {
            get
            {
                return registeredPlayers;
            }
            set
            {
                registeredPlayers = value;
            }
        }

        //public static bool LogInExist(string p_username, int p_password)
        //{
        //    _log.strUsername = p_username;
        //    _log.cryptedPassword = p_password;

        //    if (registeredPlayers.ContainsKey(_log))
        //    {
        //        Debug.Log("ServerData : Player has been found ! ");
        //        return true;
        //    }
        //    else
        //    {
        //        Debug.Log("Username or password incorrect ");
        //        return false;
        //    }
        //} // Return true when we find something in the map with the log we just received or return false we don't find anything

        //public static void Register(string p_username, int p_password)
        //{
        //    _log.strUsername = p_username;
        //    _log.cryptedPassword = p_password;

        //    if (registeredPlayers.ContainsKey(_log))
        //    {
        //        Debug.LogWarning("Username Already Chosen, try Another One! ");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("You have been rightly registered !");
        //        registeredPlayers.Add(_log, -1);
        //    }
        //}//check if the username isnt already chosen, and if so, we register the new player
    }


}