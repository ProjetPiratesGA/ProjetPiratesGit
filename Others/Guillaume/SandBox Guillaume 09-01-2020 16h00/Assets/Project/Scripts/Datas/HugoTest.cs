using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Data;

public class HugoTest : MonoBehaviour
{
    ClientData fakeClient = null;
    public string username;
    public int password;
    public int  ID;

    // Use this for initialization
    void Start()
    {
        fakeClient = new ClientData(username, password);
        Data_server.InitData_server();
    }

    // Update is called once per frame
    void Update()
    {
        fakeClient.ID = ID;
        fakeClient.Username = username;
        fakeClient.Password = password;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveSystem.RegisterPlayer(fakeClient, Data_server.ClientRegistered);
        }

    }
}
