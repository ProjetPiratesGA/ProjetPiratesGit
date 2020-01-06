using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Project.Network;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

using ProjetPirate.Data;
using ProjetPirate.Boat;
using ProjetPirate.UI.HUD;

public class Player : Controller {
    
    // A virer test de pos respawn lier a l'ile
    private Ile myIle;

    [SerializeField]
    GameObject _boatPrefab = null;

    GameObject boatInstance;

    private Data_Player data;

    private bool _asBoatSpawned = false;

    public string _username;

    [SyncVar]
    public bool _isEnteringGame;

    [SyncVar]
    public bool _isConnected;

    //DEBUG SEB
    private float _debugLogDisplayTimer = 0;

    float timeSinceLastSetData;
    float timeLastSetData;

    private HUD_Script _myhUD = null;

    #region IA

    [SerializeField] [Range(1, 3)] private int _shipLevel = 1;
    [SerializeField] private List<GameObject> _structuresPerLevel;

    [SerializeField] public int _maxXp = 10000; // max XP
    [SerializeField] public int _maxPlank = 10000; // max XP
    [SerializeField] public int _maxMoney = 10000; // max XP

    [SerializeField] public int _xpLostByDeath = 10;
    [SerializeField] public float _goldRatiolostByDeath = 0.1f;
    [SerializeField] Transform _respawnPoint;

    int idBoatOnServer;
    #endregion

    public bool asBoatSpawned
    {
        get { return _asBoatSpawned; }
    }



    public Data_Player _data
    {
        get { return data; }
        set { data = value; }
    }

    public void InitPlayer()
    {
        if (!isServer)
        {
            Debug.Log("Init Player");
        }
        data = new Data_Player();
        DontDestroyOnLoad(this);
    }

    public void SetDataBoat(BoatCharacter pBoat)
    {
        _data.Boat = pBoat.Data;
    }

    public override void OnStartClient()
    {
        if(isServer)
        Debug.Log("TargetRpc Call form server ");
        else
        Debug.Log("TargetRpc Call form client ");

        InitPlayer();
    }

    private void Update()
    {
    
        timeSinceLastSetData = Time.time - timeLastSetData;
        if (isLocalPlayer)
        {
            if (timeSinceLastSetData >= 1)
            {
                timeLastSetData = Time.time;
                //Debug.Log("Launch Set DATA COMMAND");
                //_isSetData = false;
                CmdLoadDataEveryTime();
            }
        }

        if (!_asBoatSpawned && isLocalPlayer)
        {
            if(SceneManager.GetActiveScene().name == "Game")
            {
                //Debug.LogError("IN SPAWN BOAT");
                _asBoatSpawned = true;
                CmdSpawnBoat();
                //TEST SEB
                _myhUD = FindObjectOfType<HUD_Script>();
                _myhUD.SetPlayerReference(this.gameObject);
                //END TEST
                myIle = FindObjectOfType<Ile>();               
            }
        }

        if (_isConnected == true && isLocalPlayer)
        {
            _isConnected = false;
            CmdLoadDataLogin();

        }
        if (_isEnteringGame == true && isLocalPlayer)
        {

            _isEnteringGame = false;
            CmdLoadDataEnterOnGame();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("Client | Identifiant : " + _data._identifiant + "  Password : " + _data._password);
        }

        ///DEBUG SEB 0401
        if(isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                _data.dRessource.Golds += 10;
                CmdUpdateDataGold();
            }

            _debugLogDisplayTimer += Time.deltaTime;

            if(_debugLogDisplayTimer > 2.0f)
            {
                _debugLogDisplayTimer = 0;
                CmdSendDebug(_data.dRessource.Golds);
            }
        }
        ///FIN DEBUG
    }


    ///DEBUG SEB 0401
    [Command]
    public void CmdUpdateDataGold()
    {
        _data.dRessource.Golds += 10;
    }


    [Command]
    public void CmdSendDebug(int goldValue)
    {
        //Debug.LogError("Gold : " + goldValue);
    }
    ///FIN DEBUG
    

    // Player Data
    /// <summary>
    /// Refresh player data with rpc
    /// </summary>
    /// <param name="_playerData"></param>
    [ClientRpc]
    public void RpcRefreshPlayersDatas(byte[] _playerData)
    {
        RefreshPlayersDatas(_playerData);
    }

    /// <summary>
    /// fonction to refresh player data
    /// </summary>
    /// <param name="_playerData"></param>
    public void RefreshPlayersDatas(byte[] _playerData)
    {
        _data = unformateByte(_playerData);
    }

    public Data_Player unformateByte(byte[] _byte)
    {
        var mStream = new MemoryStream();
        var binFormatter = new BinaryFormatter();

        // Where 'objectBytes' is your byte array.
        mStream.Write(_byte, 0, _byte.Length);
        mStream.Position = 0;

        var myObject = binFormatter.Deserialize(mStream) as Data_Player;

        return myObject;
    }

    public string unformateByteString(byte[] _byte)
    {
        var mStream = new MemoryStream();
        var binFormatter = new BinaryFormatter();

        // Where 'objectBytes' is your byte array.
        mStream.Write(_byte, 0, _byte.Length);
        mStream.Position = 0;

        var myObject = binFormatter.Deserialize(mStream) as string;

        return myObject;
    }

    // Boat
    /// <summary>
    /// Ask the server to spawn an new boat instance with authority
    /// </summary>
    [Command]
    public void CmdSpawnBoat()
    {
        //Debug.LogError("IN SERVER SPAWN BOAT");

        //Get spawn point from network manager
        Transform spawnTransform = NetworkManager.singleton.GetStartPosition();
        //instanciate a new boat
        boatInstance = Instantiate(_boatPrefab, spawnTransform.position, spawnTransform.rotation);
        //Set the new instance as chil of player (server side only)
        boatInstance.transform.SetParent(this.gameObject.transform);
        //Set the reference to the player for the new boat (server side only)
        boatInstance.GetComponent<BoatController>().player = this;
        //Set new tag
        boatInstance.tag = "myBoat";
        //Get the boat list form the network manager
        List<BoatController> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().boatList;

        //Add the new boat instance to the list
        tempList.Add(boatInstance.GetComponent<BoatController>());

        data.Boat = boatInstance.GetComponent<BoatCharacter>().Data;

        //spawn this new boat with player autorithy
        NetworkServer.SpawnWithClientAuthority(boatInstance, this.connectionToClient);

        //SetDataBoat(boatInstance.GetComponent<BoatCharacter>());

        //Set reference to the player from client side
        TargetSetPlayerReference(this.connectionToClient, this.gameObject, boatInstance);
    }

    /// <summary>
    /// Set reference to the player for new spawned boat (client side)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="playerReference"></param>
    /// <param name="obj"></param>
    [TargetRpc]
    public void TargetSetPlayerReference(NetworkConnection target, GameObject playerReference, GameObject obj)
    {
        obj.GetComponent<BoatController>().player = playerReference.GetComponent<Player>();
        //SetDataBoat(obj.GetComponent<BoatCharacter>());

    }



    [Command]
    public void CmdLoadDataLogin()
    {
        byte[] dataUsernameBuffer = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>()._usernameBuffer;

        string _userBuffer = unformateByteString(dataUsernameBuffer);
        _username = unformateByteString(dataUsernameBuffer);

        Debug.Log("On Server Command Load Data for Login; try to set username | Data Username : " + _userBuffer);
        TargetLoadDataLogin(this.connectionToClient, dataUsernameBuffer);

    }

    [TargetRpc]
    public void TargetLoadDataLogin(NetworkConnection target, byte[] _usernameData)
    {

        Debug.Log("Set Username Data");

        _username = unformateByteString(_usernameData);

        Debug.Log("FINAL USERNAME SET : " + _username + " | Buffer : " + unformateByteString(_usernameData));

    }

    [Command]
    public void CmdLoadDataEnterOnGame()
    {
        Debug.Log("On Server Command Load Data for Enter the Game; try to set Data Resources");
        Data_Player dataBuffer = unformateByte(NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEnterOnGame);

        if (dataBuffer != null)
        {
            //Load Data
            Debug.Log("Set data Entering on Game");

            data = dataBuffer;

        }
        else
        {

            Debug.Log("Data Entering on Game is NULL");
        }

        TargetLoadDataEnterOnGame(this.connectionToClient, NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEnterOnGame);
    }

    [TargetRpc]
    public void TargetLoadDataEnterOnGame(NetworkConnection target, byte[] _playerData)
    {

        Data_Player dataBuffer = unformateByte(_playerData);
        if (dataBuffer != null)
        {
            //Load Data
            Debug.Log("Set data Entering on Game");

            data = dataBuffer;

        }
        else
        {

            Debug.Log("Data Entering on Game is NULL");
        }
    }

    [Command]
    public void CmdLoadDataEveryTime()
    {
        Debug.Log("On Server Command Load Data for Every Time; try to set Data Resources");
        Data_Player dataBuffer = unformateByte(NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEveryTime);

        if (dataBuffer != null)
        {
            //Load Data
            Debug.Log("Set data Entering on Game");

            data = dataBuffer;

        }
        else
        {

            Debug.Log("Data Entering on Game is NULL");
        }

        TargetLoadDataEveryTime(this.connectionToClient, NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEveryTime);
    }

    [TargetRpc]
    public void TargetLoadDataEveryTime(NetworkConnection target, byte[] _playerData)
    {

        Data_Player dataBuffer = unformateByte(_playerData);
        if (dataBuffer != null)
        {
            //Load Data
            //Debug.Log("Set data Every Time");

            data = dataBuffer;

        }
        else
        {

            Debug.Log("Data Every Time is NULL");
        }
    }

    public virtual void GainXP(int pEarnedXP)
    {
        data.dRessource.Reputation += pEarnedXP;
        if (data.dRessource.Reputation > _maxXp)
        {
            data.dRessource.Reputation = _maxXp;
        }
    }

    public virtual void GainPlank(int pGainedPlank)
    {
        data.dRessource.WoodBoard += pGainedPlank;
        if (data.dRessource.WoodBoard > _maxPlank)
        {
            data.dRessource.WoodBoard = _maxPlank;
        }
    }

    public virtual void GainMoney(int pGainedMoney)
    {
        data.dRessource.Golds += pGainedMoney;
        if (data.dRessource.Golds > _maxMoney)
        {
            data.dRessource.Golds = _maxMoney;
        }
    }

    public virtual void LoseXP(int pLostXP)
    {
        data.dRessource.Reputation -= pLostXP;
        if (data.dRessource.Reputation < 0)
        {
            data.dRessource.Reputation = 0;
        }
    }

    public virtual void LosePlank(int pLostPlank)
    {
        data.dRessource.WoodBoard += pLostPlank;
        if (data.dRessource.WoodBoard < 0)
        {
            data.dRessource.WoodBoard = 0;
        }
    }

    public virtual void LoseMoney(int pLostMoney)
    {
        data.dRessource.Golds += pLostMoney;
        if (data.dRessource.Golds < 0)
        {
            data.dRessource.Golds = 0;
        }
    }

    public override void Death()
    {
        LoseXP(_xpLostByDeath);
        int lostMoney = (int)(data.dRessource.Golds * _goldRatiolostByDeath);

        LoseMoney(lostMoney);
        LosePlank(data.dRessource.WoodBoard);
    }

    [Command]
    public void CmdCallDisappear()
    {
        List<BoatController> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().boatList;

        for(int i = 0; i < tempList.Count; i++)
        {
            if(this.connectionToClient == tempList[i].connectionToClient)
            {
                GameObject boatInstance = tempList[i].gameObject;
            }
        }

        TargetDisppear(this.connectionToClient, boatInstance);
    }

    [TargetRpc]
    public void TargetDisppear(NetworkConnection target, GameObject _myBoat)
    {
        _myBoat.transform.position = myIle._posRespawnBoat.position;
        _myBoat.transform.rotation = myIle._posRespawnBoat.rotation;
    }

    public override void Disappear()
    {        
        CmdCallDisappear();
    }
}
