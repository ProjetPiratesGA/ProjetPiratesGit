using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSmokeDeath : MonoBehaviour {

    ParticleSystem ps;
    float _currentTime;
    float _maxTime = 6;
    float _maxRadius = 5;
    bool _isPlaying;

	// Use this for initialization
	void Start () {
        ps = this.GetComponent<ParticleSystem>();
        ps.Stop();
        ParticleSystem.MainModule main = ps.main;
        main.duration = 3;
        main.maxParticles = (int)(_maxTime * 2000);
    }
	
	// Update is called once per frame
	void Update () {
        if (_isPlaying)
        {
            _currentTime += Time.deltaTime;
            Vector3 vec = ps.transform.eulerAngles;
            vec.x = -90;
            ps.transform.eulerAngles = vec;
            ParticleSystem.MainModule main = ps.main;
            main.startSpeed = 30 + 15 * _currentTime / _maxTime;
            ParticleSystem.VelocityOverLifetimeModule velocity = ps.velocityOverLifetime;
            ParticleSystem.MinMaxCurve curve = new ParticleSystem.MinMaxCurve(-30 * _currentTime / _maxTime, 30 * _currentTime / _maxTime);
            velocity.x = curve;
            velocity.y = curve;
            velocity.z = curve;
            ParticleSystem.ShapeModule shape = ps.shape;
            shape.radius = _maxRadius * _currentTime / _maxTime;
            ParticleSystem.EmissionModule emission = ps.emission;
            emission.rateOverTime = _maxRadius * 2000 * _currentTime / _maxTime;
            if (_currentTime > _maxTime)
            {
                _currentTime = 0;
                _isPlaying = false;
            }
        }
        
    }

    public void Play()
    {
        ps.Play();
        _isPlaying = true;
    }
}
