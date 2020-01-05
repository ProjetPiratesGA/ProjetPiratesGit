using Project.Network;
using ProjetPirate.Boat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class respawnPlayer : NetworkBehaviour
{

    //BarLife _playerBarLife = null;

    BoatController _playerController = null;


    void Start()
    {
        //_playerBarLife = this.gameObject.GetComponent<BarLife>();
        _playerController = this.gameObject.GetComponent<BoatController>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            return;
        }

        ////if player is dead
        //if (_playerBarLife.currentLife <= 0 || Input.GetKeyDown(KeyCode.KeypadEnter))
        //{
        //    //Move main camera for this object children so it don't get destroyed when we destroy dead boat instance 
        //    Camera.main.transform.SetParent(null);
        //    //Remove the boat instance from the server list
        //    CmdRemoveFromServerList();
        //    //Spawn a new boat
        //    _playerController.player.CmdSpawnBoat();
        //    //Destroy old boat 
        //    CmdDestroy();

        //}
    }

    /// <summary>
    /// Ask to server to destroy current boat instance
    /// </summary>
    [Command]
    public void CmdDestroy()
    {
        Destroy(this.gameObject);
    }


    /// <summary>
    /// Removes the dead boat instance before spwaning a new one
    /// </summary>
    [Command]
    public void CmdRemoveFromServerList()
    {
        NetworkManager.singleton.gameObject.GetComponent<ServerNetworkManager>().boatList.Remove(this.gameObject.GetComponent<BoatController>());
    }
}
