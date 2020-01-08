using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<NetworkManager>().StartServer();
	}
}