
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace ProjetPirate.Data
{
    /// <summary>
    /// ici les fonctions qui vont etre utilisé pour sauvegrader et charger les données
    /// </summary>
    public static class SaveSystem
    {
        private static string ApplicationPath = System.Environment.CurrentDirectory + "/Assets";
        private static string strDatasFolder = "/Datas";
       
        /* Clients */
        private static string strPathClientData = "/ClientsDatas";
        private static string strPathClientList = "/ClientsList";
        private static string strClientListName = "/ListClient.data";

        /* Player */
        private static string strPathPlayersDatas = "/PlayersDatas";

        /* sum up */
        private static string strCompletePathToClientList = ApplicationPath + strDatasFolder + strPathClientList + strClientListName;
        private static string strCompletePathToClientDatas = ApplicationPath + strDatasFolder + strPathClientData;

        private static string strCompletePathToPlayerDatas = ApplicationPath + strDatasFolder + strPathClientData;

            //D:\Projet Pirate\Prog\Others\Data_PirateProject\Assets\Datas\ClientsDatas
        /// <summary>
        /// Cette fonction permet de sauvegrader certaines données d'un joueur
        /// definies dans PlayerData.           
        /// </summary>
        /// <param name="pPlayer"></param>
        public static void SavePlayer(Data_Boat data)
        {
            //On commence par créer un BinaryFormatter qui va servir a serialiser nos donnée
            /// Doc: https://docs.microsoft.com/fr-fr/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view=netframework-4.8
            BinaryFormatter formatter = new BinaryFormatter();

            //Ensuite on saisie la destination du fichier de sauvegarde
            string path = Application.dataPath + "/Datas/player.data";
            Debug.Log("path: " + path);

            //Le FileStream va nous permettre dans ce cas de créer un fichier
            FileStream stream = new FileStream(path, FileMode.Create);

            //On serialise les données de la variable "data" vers le fichier "Stram"
            formatter.Serialize(stream, data);

            stream.Close();
        }

        /// <summary>
        /// Cette fonction va nous permettre de charger notre fichier de personnage depuis
        /// -> Application.persistentDataPath + "/player.data"
        /// et de le retourner
        /// </summary>
        /// <returns></returns>
        public static Data_Boat LoadPlayer()
        {
            //on saisie la localisation du fichier a charger
            string path = Application.dataPath + "/Datas/player.data";
            ///Si le fichier existe bien alors
            if (File.Exists(path))
            {
                //On déclare un BinaryFormatter qui va servir a deserialiser nos donnée
                BinaryFormatter formatter = new BinaryFormatter();
                //Le FileStream va nous permettre dans ce cas d'ouvrir un fichier ou sont
                //stocker les donner a retourner
                FileStream stream = new FileStream(path, FileMode.Open);

                //Nous déclarons un PlayerData a qui l'ont assigne le FileStream deserialiser par le BinaryFormatter            
                Data_Boat data = formatter.Deserialize(stream) as Data_Boat;

                //Toujours refermer le fichier!
                stream.Close();

                //on retourne le PlayerData ou l'ont va extraire les donner pour les affecter a un personnage
                return data;
            }
            ///si le fichier n'existe pas on retourne NULL
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        /// <summary>
        /// /* Only call this on the server side, it will save the  id of the clientm its username and its password */
        /// </summary>
        /// <param name="p_client"></param>
        public static void SaveListClient(List<ClientData> _registeredPlayer)
        {
            BinaryFormatter br = new BinaryFormatter();

            //string path = Application.DataPath + strDatasFolder + strPathServerData + "/ListClient.data";
            //Debug.Log("Client List path " + strCompletePathToClientList);
            FileStream stream = new FileStream(strCompletePathToClientList, FileMode.Create);

            br.Serialize(stream, _registeredPlayer);

            stream.Close();
        }

        /* a tester */
        public static List<ClientData> LoadClientList()
        {
            if (File.Exists(strCompletePathToClientList))
            {
                Debug.Log("file exist");

                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(strCompletePathToClientList, FileMode.Open);
                List<ClientData> savedClientList = bf.Deserialize(stream) as List<ClientData>;
                for (int i = 0; i < savedClientList.Count; i++)
                {
                    WriteClientData(savedClientList[i]);
                }
                stream.Close();
                return savedClientList;
            }
            else
            {
                Debug.Log("Save file not found in " + strCompletePathToClientList);
                return new List<ClientData>();
            }
        }

        /* this function will test if we can register the new client trying to register, we return false if the id and the username are already used,
        or if the username is empty */
        public static bool RegisterPlayer(ClientData _playerToRegister, List<ClientData> _dataServer)
        {
            ClientData currentClientToAdd = new ClientData(_playerToRegister);
            List<ClientData> copyOfTheList = _dataServer;

            for (int i = 0; i < copyOfTheList.Count; i++)
            {
                /* check if the username has not already been chosen */ 
                if (copyOfTheList[i].Username == currentClientToAdd.Username)
                {
                    Debug.LogWarning("i : " + i + "username : " + copyOfTheList[i].Username + " already exist, you must enter another one" + " you entered : " + currentClientToAdd.Username);
                    return false;
                }
                /* check if the id isnt already used */
                if (copyOfTheList[i].ID == currentClientToAdd.ID)
                {
                    Debug.LogWarning("id : " + currentClientToAdd.ID + " already exists");
                    return false;
                }
#region Username Verifiaction 1 for now
                if (string.IsNullOrEmpty(currentClientToAdd.Username))
                {
                    Debug.LogWarning("Invalid username : " + currentClientToAdd.Username + " try another one");
                    return false;
                }
#endregion
            }

            /* we can now add PEACEFULLY the player to the list */
            copyOfTheList.Add(currentClientToAdd);
            Debug.Log("player successfully registered");
            WriteClientData(currentClientToAdd);
            SaveListClient(copyOfTheList);
            currentClientToAdd = null;
            return true;
        }

        /* This function will create a file named after the id of the client, and will contain ClientDatas Information */
        public static void WriteClientData(ClientData _playerToRegister)
        {
            if (_playerToRegister != null)
            {
                string path = strCompletePathToClientDatas + "/0_" + _playerToRegister.ID + ".data";
                FileStream stream = new FileStream(path, FileMode.Create);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, _playerToRegister);
                Debug.Log("wrote on the file" + strCompletePathToClientDatas);
                stream.Close();
            }
        }

        public static void WritePlayerData(ClientData _playerToRegister)
        {
            string path = strCompletePathToPlayerDatas + "1_" + _playerToRegister.ID;
            
            FileStream stream = new FileStream(path, FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();


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
}