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

public class Player : NetworkBehaviour {

    [SerializeField]
    GameObject _boatPrefab = null;

    private Data_Player data;

    private bool _asBoatSpawned = false;

    public int idClientBuffer = 0;

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
        
        data = new Data_Player(idClientBuffer);
        DontDestroyOnLoad(this);
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
        if(!_asBoatSpawned && isLocalPlayer)
        {
            if(SceneManager.GetActiveScene().name == "Game")
            {
                Debug.LogError("IN SPAWN BOAT");
                _asBoatSpawned = true;
                CmdSpawnBoat();
            }
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("Client | Identifiant : " + _data._identifiant + "  Password : " + _data._password);
        }
    }

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

    // Boat
    /// <summary>
    /// Ask the server to spawn an new boat instance with authority
    /// </summary>
    [Command]
    public void CmdSpawnBoat()
    {
        Debug.LogError("IN SERVER SPAWN BOAT");

        //Get spawn point from network manager
        Transform spawnTransform = NetworkManager.singleton.GetStartPosition();
        //instanciate a new boat
        GameObject boatInstance = Instantiate(_boatPrefab, spawnTransform.position, spawnTransform.rotation);
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
        //spawn this new boat with player autorithy
        NetworkServer.SpawnWithClientAuthority(boatInstance, this.connectionToClient);
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
    }

    public void CallCmdLoadData()
    {
        CmdLoadData();
    }

    [Command]
    public void CmdLoadData()
    {
        if (NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().tmpDataBuffer.Player != null)
        {
            Debug.Log("On Server Command Load Data");

        }
        Debug.Log("On Server Command Load Data");

        TargetLoadData(this.connectionToClient, NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().dataUpdatePlayer);
    }

    [TargetRpc]
    public void TargetLoadData(NetworkConnection _conn, byte[] _playerData)
    {
        Data_Player dataBuffer = unformateByte(_playerData);
        if (dataBuffer != null)
        {
            //Load Data
            Debug.Log("Set data");

            data = dataBuffer;

        }
        else
        {

            Debug.Log("TMPData is null");
        }
        

    }
}
