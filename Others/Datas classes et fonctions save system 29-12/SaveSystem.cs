
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine.Assertions;

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
        /* Server */
        private static string strPathServer = "/ListClient.data";

        /* Player */
        private static string strPathPlayersDatas = "/PlayersDatas";
        /* Boat */
        private static string strPathBoatsDatas = "/BoatsDatas";
        /* sum up */
        private static string strCompletePathToClientList = ApplicationPath + strDatasFolder + strPathClientList + strClientListName;
        private static string strCompletePathToClientDatas = ApplicationPath + strDatasFolder + strPathClientData;
        private static string strCompletePathToPlayerDatas = ApplicationPath + strDatasFolder + strPathPlayersDatas;
        private static string strCompletePathToBoatDatas = ApplicationPath + strDatasFolder + strPathBoatsDatas;
        private static string strCompletePathToServerDatas = ApplicationPath + strDatasFolder + strPathServer;

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
                Debug.LogError("Load player returning data boat " + path);

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
        /// /* Only call this on the server side, it will save the  clients' list */
        /// </summary>
        /// <param name="p_client"></param>
        public static void SaveListClient()
        {
            BinaryFormatter br = new BinaryFormatter();

            //string path = Application.DataPath + strDatasFolder + strPathServerData + "/ListClient.data";
            //Debug.Log("Client List path " + strCompletePathToClientList);
            FileStream stream = new FileStream(strCompletePathToClientList, FileMode.Create);

            br.Serialize(stream, Data_server.ClientRegistered);

            stream.Close();
        }
        /* a tester */
        public static List<ClientData> LoadClientList()
        {
            Debug.Log("LoadClient list path : " + strCompletePathToClientList);
            string pathToCheck = Path.Combine(Application.dataPath, strDatasFolder);
            if (!Directory.Exists(Path.Combine(Application.dataPath, strDatasFolder)))
            {
                Directory.CreateDirectory(strDatasFolder);
            }
            if (!Directory.Exists(pathToCheck))
            {
                Directory.CreateDirectory(pathToCheck);
            }

            if (!Directory.Exists(pathToCheck + strPathClientList))
            {
                Directory.CreateDirectory(pathToCheck + strPathClientList);
                Debug.Log("Created file Path : " + pathToCheck + strPathClientList);
            }
            else
            {
                Debug.Log("Path : " + Path.Combine(pathToCheck, strPathClientList));
            }
            if (!Directory.Exists(pathToCheck + strPathClientData))
            {
                Directory.CreateDirectory(pathToCheck + strPathClientData);
                Debug.Log("Created folder Path : " + pathToCheck + strPathClientData);

            }
            else
            {
                Debug.Log("Path : " + pathToCheck + strPathClientData);
            }
            if (!Directory.Exists(pathToCheck + strPathPlayersDatas))
            {
                Directory.CreateDirectory(pathToCheck + strPathPlayersDatas);
                Debug.Log("Create folder Path : " + pathToCheck + strPathPlayersDatas);
            }
            else
            {
                Debug.Log("Path : " + pathToCheck + strPathPlayersDatas);
            }
            if (!Directory.Exists(pathToCheck + strPathBoatsDatas))
            {
                Directory.CreateDirectory(pathToCheck + strPathBoatsDatas);
                Debug.Log("Create folder Path : " + pathToCheck + strPathBoatsDatas);

            }
            else
            {
                Debug.Log("Path : " + pathToCheck + strPathBoatsDatas);
            }


            if (File.Exists(strCompletePathToClientList))
            {
                Debug.Log("file exist");

                BinaryFormatter bf = new BinaryFormatter();
                FileStream stream = new FileStream(strCompletePathToClientList, FileMode.Open);
                List<ClientData> savedClientList = bf.Deserialize(stream) as List<ClientData>;
                for (int i = 0; i < savedClientList.Count; i++)
                {
                    Debug.Log("current ID : " + savedClientList[i].ID);
                    Debug.Break();
                    LoadClientData(savedClientList[i].ID);
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
        public static bool RegisterPlayer(ClientData _playerToRegister)
        {
            ClientData currentClientToAdd = new ClientData(_playerToRegister);
            List<ClientData> copyOfTheList = Data_server.ClientRegistered;
            currentClientToAdd.ID = Data_server.CountIDUnique + 1;

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
            Data_server.CountIDUnique++;
            copyOfTheList.Add(currentClientToAdd);
            Debug.Log("player successfully registered");
            WriteAllClientData(currentClientToAdd);
            SaveListClient();
            currentClientToAdd = null;
            return true;
        }

        /* This function will create a file named after the id of the client, and will contain ClientDatas Information */
        public static void WriteAllClientData(ClientData _playerToRegister)
        {
            if (_playerToRegister != null)
            {
                string path = strCompletePathToClientDatas + "/0_" + _playerToRegister.ID + ".data";
                //Debug.Log("All client data path : " + path);
                FileStream stream = new FileStream(path, FileMode.Create);

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, _playerToRegister);

                WritePlayerData(_playerToRegister);
                WriteBoatData(_playerToRegister);
                stream.Close();
            }
        }
        /* write and save PlayersDatas */
        public static void WritePlayerData(ClientData _playerToRegister)
        {
            string path = strCompletePathToPlayerDatas + "/1_" + _playerToRegister.ID + ".data";
            //Debug.Log("Player data path : " + path);

            FileStream stream = new FileStream(path, FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();
            //Debug.Log("Player datas before serializaing");
            if (_playerToRegister.Player != null)
            {
                bf.Serialize(stream, _playerToRegister.Player);
                Debug.Log("Player datas written");
            }
            else
            {
                Debug.Log("pLAYER IS NULL");
            }
            stream.Close();
        }
        /* write and save BoatsDatas */
        public static void WriteBoatData(ClientData _playerToRegister)
        {
            string path = strCompletePathToBoatDatas + "/2_" + _playerToRegister.ID + ".data";
            //Debug.Log("Boat data path : " + path);

            FileStream stream = new FileStream(path, FileMode.Create);

            BinaryFormatter bf = new BinaryFormatter();
            if (_playerToRegister.Player != null)
            {
                if (_playerToRegister.Player.Boat != null)
                {
                    bf.Serialize(stream, _playerToRegister.Player.Boat);
                    Debug.Log("Boat datas written");
                }
            }
            stream.Close();
        }

        public static Data_server LoadServer()
        {
            //on saisie la localisation du fichier a charger
            string path = Application.dataPath + "/Datas/Server/server.data";
            ///Si le fichier existe bien alors
            if (File.Exists(path))
            {
                //On déclare un BinaryFormatter qui va servir a deserialiser nos donnée
                BinaryFormatter formatter = new BinaryFormatter();
                //Le FileStream va nous permettre dans ce cas d'ouvrir un fichier ou sont
                //stocker les donner a retourner
                FileStream stream = new FileStream(path, FileMode.Open);

                //Nous déclarons un PlayerData a qui l'ont assigne le FileStream deserialiser par le BinaryFormatter            
                Data_server data = formatter.Deserialize(stream) as Data_server;

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

        public static ClientData LoadClientData(int id)
        {
            string strClientPath = strCompletePathToClientDatas + "/0_" + id + ".data";
            string strPlayerPath = strCompletePathToPlayerDatas + "/1_" + id + ".data";
            string strBoatPath = strCompletePathToBoatDatas + "/2_" + id + ".data";

            FileStream stream_client = new FileStream(strClientPath, FileMode.Open);
            FileStream stream_player = new FileStream(strPlayerPath, FileMode.Open);
            FileStream stream_boat = new FileStream(strBoatPath, FileMode.Open);

            //Debug.Log("LoadClientData Client Datas path : " + strClientPath);
            //Debug.Log("LoadClientData Player Datas path : " + strPlayerPath);
            //Debug.Log("LoadClientData Boat Datas path : " + strBoatPath);

            BinaryFormatter bf = new BinaryFormatter();

            ClientData loadedClient = null;
            Data_Player loadedPlayer = null;
            Data_Boat loadedBoat = null;

            if (File.Exists(strClientPath))
            {
                loadedClient = bf.Deserialize(stream_client) as ClientData;
                Debug.Log("client : " + loadedClient.ID + loadedClient.Username + loadedClient.Password);
                //Debug.Log("2 client deserialize");
            }

            BinaryFormatter bf_1 = new BinaryFormatter();
            if (File.Exists(strPlayerPath))
            {
                loadedPlayer = bf_1.Deserialize(stream_player) as Data_Player;
                Debug.Log("Loading player : " + loadedPlayer.Player_ID + " " + loadedPlayer.dRessource.Golds + " " + loadedPlayer.dRessource.Reputation + " " + loadedPlayer.dRessource.WoodBoard);
                //Debug.Log("2 player deserialize");
            }
            if (File.Exists(strBoatPath))
            {
                loadedBoat = bf.Deserialize(stream_boat) as Data_Boat;
                Debug.Log("2 boat deserialize");
            }
            return new ClientData(loadedClient, loadedPlayer, loadedBoat);
        }

    }
}