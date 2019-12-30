using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletCanon : NetworkBehaviour {

    float _timer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.forward * 4f * Time.deltaTime;

        _timer += Time.deltaTime;

        if (_timer > 2.0f)
        {
            if(!isServer)
            {
                CmdDestroyBullet();
            }
            Destroy(this.gameObject);

        }
    }
    
    [Command]
    public void CmdDestroyBullet()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //var hit = collision.gameObject;
        //collision.gameObject.GetComponentInParent<BarLife>().setDamage(10);

        if (!isServer)
        {
            CmdDestroyBullet();
        }

        Destroy(this.gameObject);
    }
}