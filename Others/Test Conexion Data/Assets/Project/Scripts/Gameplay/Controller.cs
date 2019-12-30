using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public virtual void Damage()
    {

    }

    public virtual void Death() {
    }
    public virtual void Disappear() {
    }
}
