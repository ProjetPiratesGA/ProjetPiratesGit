using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectifTransform : MonoBehaviour {

    public Transform objectifTransform;

	// Use this for initialization
	void Start () {
        objectifTransform = this.transform;

    }

    // Update is called once per frame
    void Update () {
        objectifTransform = this.transform;

    }
}
