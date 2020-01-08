using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    [SerializeField]
    float _speed = 200f;
	
	// Update is called once per frame
	void Update ()
    {
        float vertical = Input.GetAxis("Vertical");


        Vector3 newPos = Vector3.zero;
        newPos += this.transform.position + (vertical * Camera.main.transform.forward * Time.deltaTime * _speed);
        this.transform.position = newPos;

        if(this.transform.position.y <=2f)
        {
            Vector3 resetPos = this.transform.position;
            resetPos.y = 2f;
            this.transform.position = resetPos;
        }

    }
}
