
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
        private static string ApplicationPath = Application.dataPath + "/Assets";
        //private static string ApplicationPath = System.IO.Path.GetDirectoryName(). + "/Assets";
        private static string strDatasFolder = "/Datas";

        /* Clients */
        private static string strPathClientData = "/ClientsDatas";
        private static string strPathClientList = "/ClientsList";
        private static string strClientListName = "/ListClient.data";
        /* Server */
        private static string strPathServer = "Server";

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
                Debug.Log("Load player returning data boat " + path);

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
                Debug.Log("Save file not found in" + path);
                return null;
            }
        }

        /// <summary>
        /// /* Only call this on the server side, it will save the  clients' list */
        /// </summary>
        /// <param name="p_client"></param>
        //public static void SaveListClient()
        //{
        //    BinaryFormatter br = new BinaryFormatter();

        //    //string path = Application.DataPath + strDatasFolder + strPathServerData + "/ListClient.data";
        //    //Debug.Log("Client List path " + strCompletePathToClientList);
        //    FileStream stream = new FileStream(strCompletePathToClientList, FileMode.Create);

        //    br.Serialize(stream, Data_server.ClientRegistered);

        //    stream.Close();
        //}
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
                    //LoadClientData(savedClientList[i].ID);
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
        public static bool RegisterPlayer(ClientData _playerToRegister, Data_server data_Server)
        {
            ClientData currentClientToAdd = new ClientData(_playerToRegister);
            List<ClientData> copyOfTheList = data_Server.ClientRegistered;
            currentClientToAdd.ID = data_Server.CountIDUnique + 1;

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
            data_Server.CountIDUnique++;
            data_Server.ClientRegistered.Add(currentClientToAdd);
            Debug.Log("player successfully registered");

            //WriteAllClientData(currentClientToAdd);
            //SaveListClient();
            SaveServer(data_Server);
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
            Debug.LogWarning("In load server");

            //on saisie la localisation du fichier a charger
            string path = MonoData_Tools.pathHDDServer + "server";
            if (!File.Exists(path))
            {
                (new FileInfo(path)).Directory.Create();
                Debug.LogWarning("Folder has been created at " + path);
            }
            else
                Debug.LogWarning("Folder is already exist at" + path);
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
                Debug.Log("Save file not found in" + path);
                return new Data_server();
            }
        }

        public static void SaveServer(Data_server pData)
        {
            BinaryFormatter br = new BinaryFormatter();

            string path = MonoData_Tools.pathHDDServer + "server";
            if (!File.Exists(path))
            {
                (new FileInfo(path)).Directory.Create();
            }
            //string path = Application.DataPath + strDatasFolder + strPathServerData + "/ListClient.data";
            //Debug.Log("Client List path " + path);
            FileStream stream = new FileStream(path, FileMode.Create);
            br.Serialize(stream, pData);

            stream.Close();
        }

        //public static ClientData LoadClientData(int id)
        //{
        //    string strClientPath = strCompletePathToClientDatas + "/0_" + id + ".data";
        //    string strPlayerPath = strCompletePathToPlayerDatas + "/1_" + id + ".data";
        //    string strBoatPath = strCompletePathToBoatDatas + "/2_" + id + ".data";

        //    FileStream stream_client = new FileStream(strClientPath, FileMode.Open);
        //    FileStream stream_player = new FileStream(strPlayerPath, FileMode.Open);
        //    FileStream stream_boat = new FileStream(strBoatPath, FileMode.Open);

        //    //Debug.Log("LoadClientData Client Datas path : " + strClientPath);
        //    //Debug.Log("LoadClientData Player Datas path : " + strPlayerPath);
        //    //Debug.Log("LoadClientData Boat Datas path : " + strBoatPath);

        //    BinaryFormatter bf = new BinaryFormatter();

        //    ClientData loadedClient = null;
        //    Data_Player loadedPlayer = null;
        //    Data_Boat loadedBoat = null;

        //    if (File.Exists(strClientPath))
        //    {
        //        loadedClient = bf.Deserialize(stream_client) as ClientData;
        //        Debug.Log("client : " + loadedClient.ID + loadedClient.Username + loadedClient.Password);
        //        //Debug.Log("2 client deserialize");
        //    }

        //    BinaryFormatter bf_1 = new BinaryFormatter();
        //    if (File.Exists(strPlayerPath))
        //    {
        //        loadedPlayer = bf_1.Deserialize(stream_player) as Data_Player;
        //        Debug.Log("Loading player : " + loadedPlayer.Player_ID + " " + loadedPlayer.dRessource.Golds + " " + loadedPlayer.dRessource.Reputation + " " + loadedPlayer.dRessource.WoodBoard);
        //        //Debug.Log("2 player deserialize");
        //    }
        //    if (File.Exists(strBoatPath))
        //    {
        //        loadedBoat = bf.Deserialize(stream_boat) as Data_Boat;
        //        Debug.Log("2 boat deserialize");
        //    }
        //    return new ClientData();
        //}


        public static void InitHDDFolder()
        {
            MonoData_Tools.initVar();
            #region CreateFolder
            ///Creat folder in AppData
            ///Questes
            if (!File.Exists(MonoData_Tools.pathHDDQuestes))
            {
                Debug.Log("In initFolder");
                (new FileInfo(MonoData_Tools.pathHDDQuestes)).Directory.Create();
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDQuestes);
            }
            else
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDQuestes + " as alrady exist");

            ///Dock
            if (!File.Exists(MonoData_Tools.pathHDDDock))
            {
                (new FileInfo(MonoData_Tools.pathHDDDock)).Directory.Create();
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDDock);
            }
            else
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDDock + " as alrady exist");

            ///Dialogues
            if (!File.Exists(MonoData_Tools.pathHDDDialogues))
            {
                (new FileInfo(MonoData_Tools.pathHDDDialogues)).Directory.Create();
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDDialogues);
            }
            else
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDDialogues + " as alrady exist");

            ///Pnj
            if (!File.Exists(MonoData_Tools.pathHDDPnj))
            {
                (new FileInfo(MonoData_Tools.pathHDDPnj)).Directory.Create();
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDPnj);
            }
            else
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDPnj + " as alrady exist");

            ///Objects
            if (!File.Exists(MonoData_Tools.pathHDDbject))
            {
                (new FileInfo(MonoData_Tools.pathHDDbject)).Directory.Create();
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDbject);
            }
            else
                Debug.Log("FileCreated at " + MonoData_Tools.pathHDDbject + " as alrady exist");
            #endregion CreateFolder
            #region CreateFiles
            StreamWriter streamWrite = null;
            #region Questes
            for (int indiceQuest = 0; indiceQuest < 5; indiceQuest++)
            {
                string RessourcesPath = "Datas/Questes/" + indiceQuest;
                TextAsset ressourcesText = Resources.Load<TextAsset>(RessourcesPath);
                if (ressourcesText != null)
                {
                    Debug.Log("Load resources " + RessourcesPath + " sucsessfull");
                }
                else
                {
                    Debug.Log("Failed to load resources " + RessourcesPath);
                }

                streamWrite = new StreamWriter(MonoData_Tools.pathHDDQuestes + indiceQuest);
                streamWrite.WriteLine(ressourcesText.text);
                streamWrite.Close();
            }
            #endregion Questes
            #region Pnjs
            List<string> tmpName = new List<string>();
            tmpName.Add("Adalyn North");
            tmpName.Add("Alvaro the Nightmare");
            tmpName.Add("Horton two Face");
            tmpName.Add("Jengo the Snake");
            tmpName.Add("Kamkin");
            tmpName.Add("Lord Grady");
            tmpName.Add("Razortooth");
            for (int indicePnj = 0; indicePnj < tmpName.Count; indicePnj++)
            {
                string RessourcesPath = "Datas/Pnj/" + tmpName[indicePnj];
                TextAsset ressourcesText = Resources.Load<TextAsset>(RessourcesPath);
                if (ressourcesText != null)
                {
                    Debug.Log("Load resources " + RessourcesPath + " sucsessfull");
                }
                else
                {
                    Debug.Log("Failed to load resources " + RessourcesPath);
                }

                streamWrite = new StreamWriter(MonoData_Tools.pathHDDPnj + tmpName[indicePnj]);
                streamWrite.WriteLine(ressourcesText.text);
                streamWrite.Close();
            }
            #endregion Pnjs
            #region Objects
            List<string> tmpNameObject = new List<string>();
            tmpNameObject.Add("Boat");
            tmpNameObject.Add("Bullet");
            tmpNameObject.Add("Canon");
            tmpNameObject.Add("Flag");
            tmpNameObject.Add("Gold");
            tmpNameObject.Add("Kraken");
            tmpNameObject.Add("MilitaryBoat");
            tmpNameObject.Add("Objects");
            tmpNameObject.Add("Snail");
            tmpNameObject.Add("StoreBoat");
            tmpNameObject.Add("WoodenBoard");
            
            for (int indicePnj = 0; indicePnj < tmpNameObject.Count; indicePnj++)
            {
                string RessourcesPath = "Datas/Objects/" + tmpNameObject[indicePnj];
                TextAsset ressourcesText = Resources.Load<TextAsset>(RessourcesPath);
                if (ressourcesText != null)
                {
                    Debug.Log("Load resources " + RessourcesPath + " sucsessfull");
                }
                else
                {
                    Debug.Log("Failed to load resources " + RessourcesPath);
                }

                streamWrite = new StreamWriter(MonoData_Tools.pathHDDbject + tmpNameObject[indicePnj]);
                streamWrite.WriteLine(ressourcesText.text);
                streamWrite.Close();
            }
            #endregion Objects
            #region Docks
            for (int indiceDock = 0; indiceDock < 1; indiceDock++)
            {
                string RessourcesPath = "Datas/Docks/" + indiceDock;
                TextAsset ressourcesText = Resources.Load<TextAsset>(RessourcesPath);
                if (ressourcesText != null)
                {
                    Debug.Log("Load resources " + RessourcesPath + " sucsessfull");
                }
                else
                {
                    Debug.Log("Failed to load resources " + RessourcesPath);
                }

                streamWrite = new StreamWriter(MonoData_Tools.pathHDDDock + indiceDock);
                streamWrite.WriteLine(ressourcesText.text);
                streamWrite.Close();
            }
            #endregion Docks
            #region Dialogues
            for (int indiceDialogue = 0; indiceDialogue < 17; indiceDialogue++)
            {
                string RessourcesPath = "Datas/Dialogues/" + indiceDialogue;
                TextAsset ressourcesText = Resources.Load<TextAsset>(RessourcesPath);
                if (ressourcesText != null)
                {
                    Debug.Log("Load resources " + RessourcesPath + " sucsessfull");
                }
                else
                {
                    Debug.Log("Failed to load resources " + RessourcesPath);
                }

                streamWrite = new StreamWriter(MonoData_Tools.pathHDDDialogues + indiceDialogue);
                streamWrite.WriteLine(ressourcesText.text);
                streamWrite.Close();
            }
            #endregion Dialogues
            #endregion CreateFiles
        }
    }
}