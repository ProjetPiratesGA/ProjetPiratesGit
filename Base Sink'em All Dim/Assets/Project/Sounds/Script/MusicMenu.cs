using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour {

    public AudioSource _source;
	// Use this for initialization
	void Start () {
        AudioManager.PlayLoop(_source, "Menu_Music");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
