using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static public bool once_call;

    // Use this for initialization
    void Start () 
    {
        DontDestroyOnLoad(this);
    }
}