using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Network;

public class GameManager : MonoSingletonTest<GameManager> {

    [SerializeField]
    ServerNetworkManager _server;

    public ServerNetworkManager server
    {
        get { return server; }
    }

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
