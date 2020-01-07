﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Boat;

public class Chest : MonoBehaviour {

    [SerializeField] private int _containedMoney = 0;
    private int _containedPlank = 0;

    public int containedMoney
    {
        get { return _containedMoney;  }
        set { _containedMoney = value; }
    }
    public int containedPlank
    {
        get { return _containedPlank; }
        set { _containedPlank = value; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   
    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<BoatCharacter>()._isDying == false)
            {
                //Debug.LogError("Collid chest");
                other.GetComponent<BoatCharacter>().player.GetComponent<Player>().GainMoney(_containedMoney);
                other.GetComponent<BoatCharacter>().GoldFx(this.transform.position, _containedMoney);
                other.GetComponent<BoatController>().player.CmdDestroyChest(this.gameObject);
            }
        }
    }
}
