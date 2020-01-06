using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class testServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<NetworkManager>().StartServer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
