using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static public bool once_call;

    // Use this for initialization
    void Start () 
    {
        if (!once_call)
        {
            DontDestroyOnLoad(this);
            once_call = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
