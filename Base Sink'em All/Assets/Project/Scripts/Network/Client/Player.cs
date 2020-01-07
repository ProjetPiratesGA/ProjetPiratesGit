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
using ProjetPirate.IA;

public class Player : Controller
{

    // A virer test de pos respawn lier a l'ile
    private Ile myIle;

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

    bool _isDataReady = false;

    public bool isDataReady { get { return _isDataReady; } set { _isDataReady = value; } }

    //DEBUG SEB
    private float _debugLogDisplayTimer = 0;

    float timeSinceLastSetData;
    float timeLastSetData;

    private HUD_Script _myhUD = null;

    float _currentTime = 0f;
    float _timeToReloadData = 0.5f;

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

    public override void OnStartClient()
    {
        if (isServer)
            Debug.Log("TargetRpc Call form server ");
        else
            Debug.Log("TargetRpc Call form client ");

        InitPlayer();
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        //_currentTime += Time.deltaTime;
        //if(_currentTime >= _timeToReloadData)
        //{
        //    CmdUpdateDataGold(this.data.Ressource.Golds);
        //    CmdSendLife(this.data.Boat.Stats.Life);
        //    _currentTime = 0;
        //}


        if (!_asBoatSpawned && isLocalPlayer)
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
               // Debug.LogError("IN SPAWN BOAT");
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
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                this.data.Ressource.Golds += 10;
                CmdUpdateDataGold(this.data.Ressource.Golds);
            }

            _debugLogDisplayTimer += Time.deltaTime;

            if (_debugLogDisplayTimer > 2.0f)
            {
                _debugLogDisplayTimer = 0;
                CmdSendDebug(_data.Ressource.Golds);
            }
        }
        ///FIN DEBUG
    }


    ///DEBUG SEB 0401
    [Command]
    public void CmdUpdateDataGold(int gold)
    {
        _data.Ressource.Golds = gold;
        RpcUpdateDataGold(gold);
    }
    [ClientRpc]
    public void RpcUpdateDataGold(int gold)
    {
        _data.Ressource.Golds = gold;
    }


    #region Command And Fonction cmd et rpc pour envoyer les datas BOAT
    [ClientRpc]
    public void RpcSendLife(int p_life)
    {
        this._data.Boat.Stats.Life = p_life;
    }
    //[ClientRpc]
    //public void RpcSendDanageReceived(Dictionary<int, int> p_damageReceived)
    //{
    //    this._data.Boat.Stats.DamageReceived = p_damageReceived;
    //}
    [ClientRpc]
    public void RpcSendMaxCanonPerSide(int p_maxCanonPerSide)
    {
        this._data.Boat.MaxCanonPerSide = p_maxCanonPerSide;
    }
    [ClientRpc]
    public void RpcSendCurrentCanonLeft(int p_currentCanonLeft)
    {
        this._data.Boat.CurrentCanonLeft = p_currentCanonLeft;
    }
    [ClientRpc]
    public void RpcSendCurrentCanonRight(int p_currentCanonRight)
    {
        this._data.Boat.CurrentCanonRight = p_currentCanonRight;
    }

    [Command]
    public void CmdSendLife(int p_life)
    {
        this._data.Boat.Stats.Life = p_life;
        RpcSendLife(p_life);
    }
    //[Command]
    //public void CmdSendDamageReceived()
    //{
    //    RpcSendDanageReceived(this._data.Boat.Stats.DamageReceived);
    //}
    [Command]
    public void CmdSendMaxCanonPerSide()
    {
        RpcSendMaxCanonPerSide(this._data.Boat.MaxCanonPerSide);
    }
    [Command]
    public void CmdSendCurrentCanonLeft(int number)
    {
        this._data.Boat.CurrentCanonLeft = number;
        RpcSendCurrentCanonLeft(number);
    }
    [Command]
    public void CmdSendCurrentCanonRight(int number)
    {
        this._data.Boat.CurrentCanonRight = number;
        RpcSendCurrentCanonRight(number);
    }
    #endregion
    [Command]
    public void CmdSendDebug(int goldValue)
    {
        //Debug.LogError("Gold : " + goldValue);
    }

    [TargetRpc]
    public void TargetSetDataOk(NetworkConnection target)
    {
        isDataReady = true;
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
        boatInstance.GetComponent<Character>().player = this;
        //Set new tag
        boatInstance.tag = "myBoat";
        //Get the boat list form the network manager
        List<BoatController> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().boatList;

        //Add the new boat instance to the list
        tempList.Add(boatInstance.GetComponent<BoatController>());

        //spawn this new boat with player autorithy
        NetworkServer.SpawnWithClientAuthority(boatInstance, this.connectionToClient);

        //SetDataBoat(boatInstance.GetComponent<BoatCharacter>());

        //Set reference to the player from client side
        RpcSetPlayerReference(boatInstance);
    }

    /// <summary>
    /// Set reference to the player for new spawned boat (client side)
    /// </summary>
    /// <param name="target"></param>
    /// <param name="playerReference"></param>
    /// <param name="obj"></param>
    [TargetRpc]
    public void TargetSetPlayerReference(NetworkConnection Target, GameObject obj)
    {
        if (obj.GetComponentInChildren<BoatController>() == null)
        {
            Debug.LogError("BOAT CONTROLLER NUll");
            Debug.Break();
        }
        if (obj.GetComponentInChildren<Character>() == null)
        {
            Debug.LogError("CHARACTER NUll");
            Debug.Break();
        }

        obj.GetComponentInChildren<BoatController>().player = this.GetComponent<Player>();
        obj.GetComponentInChildren<Character>().player = this.GetComponent<Player>();
        //SetDataBoat(obj.GetComponent<BoatCharacter>());

        if (obj.GetComponentInChildren<Character>().player == null)
        {
            Debug.LogWarning("PLAYER SET NULL");
            Debug.Break();
        }
        else
            Debug.Log("PLAYER SET OK");
    }

    [ClientRpc]
    public void RpcSetPlayerReference(GameObject obj)
    {
        obj.GetComponent<BoatController>().player = this.GetComponent<Player>();
        obj.GetComponent<Character>().player = this.GetComponent<Player>();
        if (obj.GetComponent<Character>().player == null)
        {
            Debug.LogWarning("PLAYER SET NULL");
            Debug.Break();
        }
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
        //Debug.Log("On Server Command Load Data for Enter the Game; try to set Data Resources");
        //Data_Player dataBuffer = unformateByte(NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEnterOnGame);

        //if (dataBuffer != null)
        //{
        //    //Load Data
        //    Debug.Log("Set data Entering on Game");

        //    data = dataBuffer;

        //}
        //else
        //{

        //    Debug.Log("Data Entering on Game is NULL");
        //}

        //TargetLoadDataEnterOnGame(this.connectionToClient, NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEnterOnGame);
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
        //Debug.Log("On Server Command Load Data for Every Time; try to set Data Resources");
        //Data_Player dataBuffer = unformateByte(NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEveryTime);

        //if (dataBuffer != null)
        //{
        //    //Load Data
        //    Debug.Log("Set data Entering on Game");

        //    data = dataBuffer;

        //}
        //else
        //{

        //    Debug.Log("Data Entering on Game is NULL");
        //}

        //TargetLoadDataEveryTime(this.connectionToClient, NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().byteDataUpdatePlayerEveryTime);
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
        data.Ressource.Reputation += pEarnedXP;
        if (data.Ressource.Reputation > _maxXp)
        {
            data.Ressource.Reputation = _maxXp;
        }
    }

    public virtual void GainPlank(int pGainedPlank)
    {
        data.Ressource.WoodBoard += pGainedPlank;
        if (data.Ressource.WoodBoard > _maxPlank)
        {
            data.Ressource.WoodBoard = _maxPlank;
        }
    }

    public virtual void GainMoney(int pGainedMoney)
    {
        data.Ressource.Golds += pGainedMoney;
        if (data.Ressource.Golds > _maxMoney)
        {
            data.Ressource.Golds = _maxMoney;
        }
    }

    public virtual void LoseXP(int pLostXP)
    {
        data.Ressource.Reputation -= pLostXP;
        if (data.Ressource.Reputation < 0)
        {
            data.Ressource.Reputation = 0;
        }
    }

    public virtual void LosePlank(int pLostPlank)
    {
        data.Ressource.WoodBoard += pLostPlank;
        if (data.Ressource.WoodBoard < 0)
        {
            data.Ressource.WoodBoard = 0;
        }
    }

    public virtual void LoseMoney(int pLostMoney)
    {
        data.Ressource.Golds += pLostMoney;
        if (data.Ressource.Golds < 0)
        {
            data.Ressource.Golds = 0;
        }
    }

    public override void Death()
    {
        LoseXP(_xpLostByDeath);
        int lostMoney = (int)(data.Ressource.Golds * _goldRatiolostByDeath);

        CmdSpawnChest(lostMoney, data.Ressource.WoodBoard);

        LoseMoney(lostMoney);
        LosePlank(data.Ressource.WoodBoard);
    }

    [ClientRpc]
    public void RpcSetStartData(int life, int gold, int cannonLeft, int cannonRight, int maxCannons, float speed)
    {
        this.data.Ressource.Golds = gold;
        this.data.Boat.Stats.Life = life;
        this.data.Boat.CurrentCanonLeft = cannonLeft;
        this.data.Boat.CurrentCanonRight = cannonRight;
        this.data.Boat.MaxCanonPerSide = maxCannons;
        this.data.Boat.Stats.Speed = speed;

    }
    [TargetRpc]
    public void TargetSetStartData(NetworkConnection target, int life, int gold, int cannonLeft, int cannonRight, int maxCannons, float speed)
    {
        this.data.Ressource.Golds = gold;
        this.data.Boat.Stats.Life = life;
        this.data.Boat.CurrentCanonLeft = cannonLeft;
        this.data.Boat.CurrentCanonRight = cannonRight;
        this.data.Boat.MaxCanonPerSide = maxCannons;
        this.data.Boat.Stats.Speed = speed;

    }

    [Command]
    public void CmdDestroyChest(GameObject _chest)
    {
        Debug.LogError("Destroy Cmd Player chest");
        List<Chest> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().ChestList;

        tempList.Remove(_chest.GetComponent<Chest>());
        Destroy(_chest);
    }

    [Command]
    private void CmdSpawnChest(int pContainedMoney, int pContainedPlank)
    {
        Chest chest = Instantiate(boatInstance.GetComponent<BoatCharacter>().DroppedChest).GetComponent<Chest>();
        chest.transform.position = boatInstance.transform.position + new Vector3(0, 0.712f, 0);
        chest.containedMoney = pContainedMoney;
        chest.containedPlank = pContainedPlank;

        List<Chest> tempList = NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().ChestList;

        tempList.Add(chest);

        NetworkServer.Spawn(chest.gameObject);
    }

    public override void Disappear()
    {
        if (boatInstance == null)
        {
            boatInstance = this.GetComponentInChildren<BoatCharacter>().gameObject;
        }

        boatInstance.transform.position = myIle._posRespawnBoat.position;
        boatInstance.transform.rotation = myIle._posRespawnBoat.rotation;

        boatInstance.GetComponent<BoatCharacter>()._isDying = false;
    }
}
