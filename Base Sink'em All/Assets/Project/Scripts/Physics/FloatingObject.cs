using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class FloatingObject : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [Header("WaterVariables")]
    //[SerializeField]
    //private GameObject _sea; // la mer
    [SerializeField]
    private float Height = 0f;
    //[SerializeField]
    //private float _waterLevel = 0;
    [SerializeField]
    private float _floatThreshold = 1.0f;
    [SerializeField]
    private float _waterDensity = 0.125f;
    [SerializeField]
    private float _downForce = 4.0f;

    private float _forceFactor;
    private Vector3 _floatForce;

    private void Start()
    {
        //_rigidbody = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// I use FixedUpdate for calculations 
    /// </summary>
    private void FixedUpdate()
    {
        //this.MoveOnTheSea();
    }

    /// <summary>
    /// add a force to the boat according to the Treshold of the sea
    /// </summary>
    //private void MoveOnTheSea()
    //{
    //    //_forceFactor = 1.0f - ((transform.position.y - _waterLevel) / _floatThreshold);
    //    _forceFactor = 1.0f - ((transform.position.y - Height) / _floatThreshold);

    //    if (_forceFactor > 0f)
    //    {
    //        _floatForce = -Physics.gravity * _rigidbody.mass * (_forceFactor - _rigidbody.velocity.y * _waterDensity);
    //        _floatForce += new Vector3(0.0f, -_downForce * _rigidbody.mass, 0.0f);

    //        _rigidbody.AddForceAtPosition(_floatForce, transform.position);
    //    }
    //}

    
}
