using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterTransform : MonoBehaviour {

    public Transform transformCenter;
	// Use this for initialization
	void Start () {

        transformCenter = this.transform;

    }

    // Update is called once per frame
    void Update () {
        transformCenter = this.transform;

    }
}
