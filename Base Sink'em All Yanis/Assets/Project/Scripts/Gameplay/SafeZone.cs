using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {



	// Use this for initialization
	void Start () {
        this.gameObject.layer = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Check if object in collision is a player
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            //Put all the associated enemies on alert if they aren't already
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if object in collision is a player
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            //Put all the associated enemies on alert if they aren't already
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe = false;
        }
    }
}
