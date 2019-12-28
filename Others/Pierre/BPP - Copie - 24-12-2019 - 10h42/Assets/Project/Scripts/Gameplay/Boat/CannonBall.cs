using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{

    private Rigidbody _rigidbody;

    private float _force;
    private int _damage = 1;
    //private float _distMax = 10f;
    //private float _currentDist = 0f;
    private Vector3 _targetPosition;
    private Vector3 _travelVector;
    private bool _achievePosition = false;
    // Use this for initialization

    [SerializeField]
    private float _TimeToDelete = 2f;
    private float _timerforDelete;
    public ProjetPirate.Boat.BoatCharacter _owner;

    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        //don't use gravity at the beginning
        _rigidbody.useGravity = false;
        //a faire dans le script du canon
    }

    private void Update()
    {
        _timerforDelete += Time.deltaTime;
        if (_timerforDelete > _TimeToDelete)
        {
            
            this.Delete();
        }
    }


    void FixedUpdate()
    {
        //compute currentDist

        //define if the cannonBall is going forward or if it start to fall with the gravity
        //if (_currentDist > _distMax)
        _travelVector = _targetPosition - this.transform.position;
        if (_travelVector.magnitude < 1)
        {
            _achievePosition = true;
        }

        this.ComputePosition();
    }

    private void ComputePosition()
    {
        //CannonBall must fall
        if (_achievePosition == true)
        {
            _rigidbody.useGravity = true;
            _rigidbody.MovePosition(this.transform.position + this.transform.forward * _force * Time.fixedDeltaTime);
            //_rigidbody.MovePosition(this.transform.position + _travelVector * _force * Time.fixedDeltaTime);
        }
        //Move the cannonBall
        else
        {
            //_rigidbody.MovePosition(this.transform.position + this.transform.forward * _force * Time.fixedDeltaTime);
            _rigidbody.MovePosition(this.transform.position + _travelVector.normalized * (_force * Time.fixedDeltaTime));
        }


    }

    public void setForceCannonBall(float pForceCannonBall)
    {
        _force = pForceCannonBall;
    }

    public void setTargetPosition(Vector3 pPositionTarget)
    {
        _targetPosition = pPositionTarget;

    }

    //private void OnDrawGizmos()
    //{
    //    //draw cube at the target position
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(_targetPosition, 0.5f);
    //}

    private void Delete()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != _owner)
            {
                collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(_damage, this.transform);
                if (collision.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>() != null)
                {
                    collision.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>().AlertFromShoot(_owner.gameObject);
                }
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Enemy" & collision.gameObject != _owner)
        {
            if (collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Character>() != null)
            {
                _owner.Controller.GetComponent<Player>().GainXP(collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Character>().Damage(_damage));
                collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Controller>().AlertFromShoot(_owner.gameObject);
            }
            else if (collision.gameObject.GetComponent<ProjetPirate.IA.Ship_Character>() != null)
            {
                _owner.Controller.GetComponent<Player>().GainXP(collision.gameObject.GetComponent<ProjetPirate.IA.Ship_Character>().Damage(_damage));
                collision.gameObject.GetComponent<ProjetPirate.IA.Ship_Controller>().AlertFromShoot(_owner.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
