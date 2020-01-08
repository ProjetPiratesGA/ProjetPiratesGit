using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    [SerializeField]
    float _speed = 120f;
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");

        this.transform.Rotate(Vector3.up, horizontal * Time.deltaTime * _speed);

    }
}
