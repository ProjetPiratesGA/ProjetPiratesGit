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
    private float _timerforDelete = 0;
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
        Debug.Log("Hit someone");

        if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != _owner)
            {
                Debug.Log("Hit boat");

                collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(_damage, this.transform);
                /*if (collision.gameObject.GetComponent<projetpirate.ia.ship_controller>() != null)
                {
                    collision.gameobject.getcomponent<projetpirate.ia.ship_controller>().alertfromshoot(_owner.gameobject);
                }*/
                Destroy(this.gameObject);
            }
        }
       /* else if (collision.gameobject.tag == "enemy" & collision.gameobject != _owner)
        {
            if (collision.gameobject.getcomponent<projetpirate.ia.shark_character>() != null)
            {
                _owner.controller.getcomponent<player>().gainxp(collision.gameobject.getcomponent<projetpirate.ia.shark_character>().damage(_damage));
                collision.gameobject.getcomponent<projetpirate.ia.shark_controller>().alertfromshoot(_owner.gameobject);
            }
            destroy(this.gameobject);
        }*/
    }
}
