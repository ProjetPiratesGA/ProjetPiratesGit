using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : NetworkBehaviour
{

    private Rigidbody _rigidbody;

    private float _force;
    private int _damage = 10;
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
    public Cannon _shooter;
    private bool _hasSplashed;

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
        if (this.transform.position.y <= 0 & !_hasSplashed)
        {
            Debug.Log("Splash");
            _shooter.PlaySplashFX(this.transform.position);
            _hasSplashed = true;
        }
    }

    private void ComputePosition()
    {
        //CannonBall must fall
        if (_achievePosition == true)
        {
            _rigidbody.useGravity = true;
            Debug.Log(this.name + " forward : " + this.transform.forward);
            //_rigidbody.MovePosition(this.transform.position + this.transform.forward * _force * Time.fixedDeltaTime);
            _rigidbody.MovePosition(this.transform.position + new Vector3(this.transform.forward.x, 0f, this.transform.forward.z) * _force * Time.fixedDeltaTime);
        }
        //Move the cannonBall
        else
        {
            //_rigidbody.MovePosition(this.transform.position + this.transform.forward * _force * Time.fixedDeltaTime);
            _rigidbody.MovePosition(this.transform.position + _travelVector.normalized * (_force * Time.fixedDeltaTime));
        }
        //Debug.DrawRay(this.transform.position, this.transform.forward * _force, Color.red);


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
        //Debug.Log("Hit someone");

        //if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        //{
        //    if (collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != _owner)
        //    {
        //        Debug.Log("Hit boat");

        //        collision.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(_damage, this.transform);
        //        //this.CmdDamage(collision.gameObject);
        //        /*if (collision.gameObject.GetComponent<projetpirate.ia.ship_controller>() != null)
        //        {
        //            collision.gameobject.getcomponent<projetpirate.ia.ship_controller>().alertfromshoot(_owner.gameobject);
        //        }*/
        //        Destroy(this.gameObject);
        //    }
        //}
        //if (collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Character>() != null)
        //{
        //    Debug.Log("Hit Shark");
        //    collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Character>().Damage(_damage, this.transform);
        //    Destroy(this.gameObject);
        //}
        ///* else if (collision.gameobject.tag == "enemy" & collision.gameobject != _owner)
        // {
        //     if (collision.gameobject.getcomponent<projetpirate.ia.shark_character>() != null)
        //     {
        //         _owner.controller.getcomponent<player>().gainxp(collision.gameobject.getcomponent<projetpirate.ia.shark_character>().damage(_damage));
        //         collision.gameobject.getcomponent<projetpirate.ia.shark_controller>().alertfromshoot(_owner.gameobject);
        //     }
        //     destroy(this.gameobject);
        // }*/

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
                /*_owner.Controller.GetComponent<Player>().GainXP*/
                collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Character>().Damage(_damage);
                collision.gameObject.GetComponent<ProjetPirate.IA.Shark_Controller>().AlertFromShoot(_owner.gameObject);
            }
            else if (collision.gameObject.GetComponent<Kraken_Head>() != null)
            {
                collision.gameObject.GetComponent<Kraken_Head>().ReceiveDamage(_damage);
            }
            //else if (collision.gameObject.GetComponent<Kraken_Tentacle>() != null)
            //{
            //    collision.gameObject.GetComponent<Kraken_Tentacle>().ReceiveDamage(_damage);
            //}
            Destroy(this.gameObject);
        }
    }

    [Command]
    private void CmdDamage(GameObject collision)
    {
        collision.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(_damage, this.transform);
    }
}
