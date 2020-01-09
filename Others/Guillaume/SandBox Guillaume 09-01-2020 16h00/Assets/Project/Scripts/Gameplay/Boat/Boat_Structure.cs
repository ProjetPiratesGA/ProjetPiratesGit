using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat_Structure : MonoBehaviour {

    [SerializeField] private BoxCollider _collider;
    [SerializeField] private List<Transform> _larboardCannonPositions;
    [SerializeField] private List<Transform> _starboardCannonPositions;
    [SerializeField] private int _defaultCannonNumberBySide;
    [SerializeField] private GameObject _droppedChest;

    public BoxCollider Collider
    {
        get { return _collider; }
    }

    public List<Transform> LarboardCannonPositions
    {
        get { return _larboardCannonPositions; }
    }

    public List<Transform> StarboardCannonPositions
    {
        get { return _starboardCannonPositions; }
    }

    public int DefaultCannonNumberBySide
    {
        get { return _defaultCannonNumberBySide; }
    }

    public GameObject DroppedChest
    {
        get { return _droppedChest; }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
