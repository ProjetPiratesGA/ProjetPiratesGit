using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HarpoonArrow : MonoBehaviour {

    private Rigidbody _rigidbody;

    private float _force = 50;
    private int _damage = 50;
    private Vector3 _targetPosition;
    private Vector3 _travelVector;
    private bool _achievePosition = false;
    // Use this for initialization

    [SerializeField]
    private float _TimeToDelete = 2f;
    private float _timerforDelete;
    public ProjetPirate.Boat.BoatCharacter _owner;
    // Use this for initialization
    void Start () {

        _rigidbody = this.GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update () {

        _timerforDelete += Time.deltaTime;
        if (_timerforDelete > _TimeToDelete)
        {

            this.Delete();
        }
    }
    void FixedUpdate()
    {
        _travelVector = _targetPosition - this.transform.position;
        if (_travelVector.magnitude < 1)
        {
            _achievePosition = true;
        }

        this.ComputePosition();
    }
    private void ComputePosition()
    {
        if (_achievePosition == true)
        {
            _rigidbody.useGravity = true;
            _rigidbody.MovePosition(this.transform.position + this.transform.forward * _force * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.MovePosition(this.transform.position + _travelVector.normalized * (_force * Time.fixedDeltaTime));
        }
    }
    private void Delete()
    {
        Destroy(this.gameObject);
    }
    public void setTargetPosition(Vector3 pPositionTarget)
    {
        _targetPosition = pPositionTarget;
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
            Destroy(this.gameObject);
        }
    }
}
