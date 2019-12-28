using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGoldFX : MonoBehaviour {

    [SerializeField] ParticleSystem _goldFx;
    Vector3 startPos;
    Vector3 endPos;
    float currentTime;
	// Use this for initialization
	void Start () {
        _goldFx.Stop();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) & !_goldFx.isPlaying)
        {
            _goldFx.Play();
            startPos = this.transform.position;
            endPos = this.transform.position + this.transform.up * 5;
        }
        if (_goldFx.isPlaying)
        {
            currentTime += Time.deltaTime;
            _goldFx.transform.position = Vector3.Lerp(startPos, endPos, currentTime);
        }
        else
        {
            currentTime = 0;
        }
	}
}
