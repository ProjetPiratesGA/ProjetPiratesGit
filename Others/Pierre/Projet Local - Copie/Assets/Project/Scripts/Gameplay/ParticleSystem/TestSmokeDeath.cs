using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSmokeDeath : MonoBehaviour {

    ParticleSystem ps;
    float _currentTime;
    float _maxTime = 6;
    float _maxRadius = 10;
	// Use this for initialization
	void Start () {
        ps = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        _currentTime += Time.deltaTime;
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.radius = _maxRadius * _currentTime / _maxTime;
	}
}
