using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour {

    [SerializeField] public List<Transform> _dockCheckpoints;
    public bool _isAvailable = true;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < _dockCheckpoints.Count; i++)
        {
            Vector3 pos = _dockCheckpoints[i].position;
            pos.y = 0;
            _dockCheckpoints[i].position = pos;
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
