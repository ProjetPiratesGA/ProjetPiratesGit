using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken_Tentacle : MonoBehaviour {

    bool _isDamaged = false;
    bool _isAttacking = false;
    bool _finishedAttacking = false;
    int _damage;

    float _currentRotationTime = 0;
    float _totalRotationTime = 1;
    Vector3 _startRotation = new Vector3(0, 0, 0);
    Vector3 _endRotation = new Vector3(90, 0, 0);

    public bool IsDamaged
    {
        get { return _isDamaged; }
    }

    public int Damage
    {
        get { return _damage; }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
        }
        if (_isAttacking)
        {
            _currentRotationTime += Time.deltaTime / _totalRotationTime;
            this.transform.eulerAngles = Vector3.Lerp(_startRotation, _endRotation, _currentRotationTime);
            if (_currentRotationTime > 1)
            {
                _currentRotationTime = 0;
                _isAttacking = false;
                _finishedAttacking = true;
            }
        }
        else if (_finishedAttacking)
        {
            _currentRotationTime += Time.deltaTime / _totalRotationTime;
            this.transform.eulerAngles = Vector3.Lerp(_endRotation,  _startRotation, _currentRotationTime);
            if (_currentRotationTime > 1)
            {
                _currentRotationTime = 0;
                _finishedAttacking = false;
            }
        }
	}

    public void ReceiveDamage(int pDamage)
    {
        _isDamaged = true;
        _damage = pDamage;
    }

    public void DisableDamage()
    {
        _isDamaged = false;
        _damage = 0;
    }

    public void Attack()
    {
        _isAttacking = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        if (_isAttacking & other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
        Debug.Log("Damage");
            other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(1);
        }
    }
}
