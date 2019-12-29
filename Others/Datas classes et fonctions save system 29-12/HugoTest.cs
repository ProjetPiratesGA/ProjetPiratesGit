using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Data;

public class HugoTest : MonoBehaviour
{
    ClientData fakeClient = null;
    public string username;
    public int password;

    // Use this for initialization
    void Start()
    {
        ClientData clientToCopy = new ClientData(username, password);

        fakeClient = new ClientData(clientToCopy);
        Data_server.InitData_server();
    }

    // Update is called once per frame
    void Update()
    {
        fakeClient.Username = username;
        fakeClient.Password = password;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveSystem.RegisterPlayer(fakeClient);
        }

    }
}
