using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankOnSea : MonoBehaviour {

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

    public static void SpawnPlank(GameObject pPlankPrefab, Vector3 pSpawnPosition, int pSpawnedPlankNumber = 1)
    {
        for (int i = 0; i < pSpawnedPlankNumber; i++)
        {
            PlankOnSea plank = Instantiate(pPlankPrefab).GetComponent<PlankOnSea>();
            plank.transform.position = pSpawnPosition + new Vector3(0, 0.712f, 0);
            plank.SetDestination();
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
            other.GetComponentInParent<Player>().GainPlank(1);
            Destroy(this.gameObject);
        }
    }
}
