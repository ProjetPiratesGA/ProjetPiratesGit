using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankOnSea : MonoBehaviour {

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
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().GainPlank(1);
            Destroy(this.gameObject);
        }
    }
}
