using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken_Head : MonoBehaviour {

    bool _isDamaged = false;
    int _damage;
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
		
	}

    public void ReceiveDamage(int pDamage)
    {
        _isDamaged = true;
        _damage = pDamage  * 2;
    }

    public void DisableDamage()
    {
        _isDamaged = false;
        _damage = 0;
    }
}
