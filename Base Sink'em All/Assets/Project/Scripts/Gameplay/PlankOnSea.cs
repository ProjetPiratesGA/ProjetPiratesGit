using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlankOnSea : NetworkBehaviour {

    private Vector3 _destination;
    private Vector3 _startPosition;
    private bool _isMoving = false;
    private float _currentTime = 0;
    [SerializeField] private float _movingTime = 1;

    // Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
        if (_isMoving)
        {
            _currentTime += Time.deltaTime / _movingTime;
            this.transform.position = Vector3.Lerp(_startPosition, _destination, _currentTime);
            if (_currentTime >= 1)
            {
                this.transform.position = _destination;
                _isMoving = false;
                _currentTime = 0;
            }
        }
        
	}

    public void SetDestination()
    {
        _startPosition = this.transform.position;
        _destination = this.transform.position;
        while(Vector3.Distance(_destination, _startPosition) < 3)
        {
            _destination = _startPosition;
            _destination.x += Random.Range(-5, 5);
            _destination.z += Random.Range(-5, 5);
        }
        Vector3 rotation = this.transform.eulerAngles;
        rotation.y = Random.Range(0, 360);
        this.transform.eulerAngles = rotation;
        _isMoving = true;
    }

	void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().GainPlank(1);
            Destroy(this.gameObject);
        }
    }
}
