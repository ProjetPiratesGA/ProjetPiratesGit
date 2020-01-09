using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMenu : MonoBehaviour {

    public AudioSource _source;

    bool isPlay = false; 
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        if (!isPlay)
        {

            AudioManager.PlayLoop(_source, "Menu_Music");

            
            isPlay = true;
        }

        if(_source.isPlaying)
        {
            Debug.Log("Son play");
        }
    }
}
