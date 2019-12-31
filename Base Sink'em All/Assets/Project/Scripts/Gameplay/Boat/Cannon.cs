using ProjetPirate.Boat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cannon : NetworkBehaviour
{
    private ProjetPirate.Data.Data_Canon _data = new ProjetPirate.Data.Data_Canon();
    [SerializeField]
    private Transform _spawnCannon;
    [SerializeField]
    private GameObject _prefabCannonBall;
    [SerializeField]
    private ProjetPirate.Boat.BoatCharacter _owner;

    private List<GameObject> _listCannonBall = new List<GameObject>();

    [SerializeField]
    private float _forceCannonBall;

    [SerializeField]
    private float _distShoot = 10f;

    [SerializeField] private ParticleSystem _smokeFX;

    public ProjetPirate.Data.Data_Canon Data
    {
        get { return _data; }
    }

    // Use this for initialization
    void Start()
    {
        if (_spawnCannon == null)
        {
            //Debug.LogError("_spawnCannon est null");

        }
        _listCannonBall = new List<GameObject>();
        if (_smokeFX != null)
        {
            _smokeFX.Stop();
        }
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOwner(ProjetPirate.Boat.BoatCharacter pOwner)
    {
        _owner = pOwner;
    }

    /// <summary>
    /// Fire a cannonball
    /// </summary>
    public void _FireCannon()
    {
        if (_prefabCannonBall != null)
        {
            //instatiate & setup the cannon ball
            GameObject newCannonBall = Instantiate(_prefabCannonBall, _spawnCannon.position, _spawnCannon.rotation);

            newCannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);
            newCannonBall.GetComponent<CannonBall>().setTargetPosition(_spawnCannon.position + _spawnCannon.forward * _distShoot);
            newCannonBall.GetComponent<CannonBall>()._owner = _owner;
            _listCannonBall.Add(newCannonBall);
            if (_smokeFX != null)
            {
                _smokeFX.Play();
            }
            //Debug.Log("_listCannonBall.Count : " + _listCannonBall.Count);
        }
        else
        {
            //Debug.LogError("_cannonBall est null");

        }
    }

    [Command]
    public void CmdFireCannon()
    {
        if (_prefabCannonBall != null)
        {
            //instatiate & setup the cannon ball
            GameObject newCannonBall = Instantiate(_prefabCannonBall, _spawnCannon.position, _spawnCannon.rotation);

            newCannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);
            newCannonBall.GetComponent<CannonBall>().setTargetPosition(_spawnCannon.position + _spawnCannon.forward * _distShoot);
            newCannonBall.GetComponent<CannonBall>()._owner = _owner;
            _listCannonBall.Add(newCannonBall);
            if (_smokeFX != null)
            {
                _smokeFX.Play();
            }
            NetworkServer.SpawnWithClientAuthority(newCannonBall, _owner.gameObject.GetComponent<BoatController>().player.connectionToClient);
            this.RpcFireCannon(newCannonBall);
            //Debug.Log("_listCannonBall.Count : " + _listCannonBall.Count);
        }
        else
        {
            //Debug.LogError("_cannonBall est null");

        }
    }

    [ClientRpc]
    public void RpcFireCannon(GameObject cannonBall)
    {
        cannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);
        cannonBall.GetComponent<CannonBall>().setTargetPosition(_spawnCannon.position + _spawnCannon.forward * _distShoot);
        cannonBall.GetComponent<CannonBall>()._owner = _owner;
        _listCannonBall.Add(cannonBall);
        if (_smokeFX != null)
        {
            _smokeFX.Play();
        }
    }


}
