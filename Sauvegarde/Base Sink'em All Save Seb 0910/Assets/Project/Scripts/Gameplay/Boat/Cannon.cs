using ProjetPirate.Boat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private ProjetPirate.Data.Data_Canon _data = new ProjetPirate.Data.Data_Canon();
    [SerializeField]
    private Transform _spawnCannon;

        private float _saveCannonPosY;
    private float _saveCannonRotX;
    private float _saveCannonRotY;
    private float _saveCannonRotZ;
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
    [SerializeField] private ParticleSystem _splashFX;

    public ProjetPirate.Data.Data_Canon Data
    {
        get { return _data; }
    }

    // Use this for initialization
    void Start()
    {
        if (_spawnCannon == null)
        {
            //Debug.Log("_spawnCannon est null");

        }

        _saveCannonPosY = _spawnCannon.position.y;
        _saveCannonRotX = _spawnCannon.rotation.eulerAngles.x;
        _saveCannonRotY = _spawnCannon.rotation.eulerAngles.y;
        _saveCannonRotZ = _spawnCannon.rotation.eulerAngles.z;
        //Debug.Log(this.name + " _saveCannonPosY : " + _saveCannonPosY + " _saveCannonRotX : " + _saveCannonRotX + " _saveCannonRotZ : " + _saveCannonRotZ);


        _listCannonBall = new List<GameObject>();
        
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        if (_smokeFX != null)
        {
            _smokeFX.Stop();
        }
        if (_splashFX != null)
        {
            _splashFX.Stop();
        }
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
            GameObject newCannonBall = Instantiate(_prefabCannonBall, new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z), _spawnCannon.rotation);

            newCannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);

            //Vector3 targetPosition = _spawnCannon.position + _spawnCannon.forward * _distShoot;
            Vector3 targetPosition = new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z) + _spawnCannon.forward * _distShoot;
            targetPosition.y = _saveCannonPosY;
            newCannonBall.GetComponent<CannonBall>().setTargetPosition(targetPosition);

            newCannonBall.GetComponent<CannonBall>()._owner = _owner;
            _listCannonBall.Add(newCannonBall);
            if (_smokeFX != null)
            {
                _smokeFX.Play();
            }

            AudioManager.PlayRandom(this.gameObject.GetComponent<AudioSource>(), "Canon1", "Canon2", "Canon3", "Canon4");

            //Debug.Log("_listCannonBall.Count : " + _listCannonBall.Count);
        }
        else
        {
            //Debug.Log("_cannonBall est null");

        }
    }

    public void PlaySplashFX(Vector3 pPos)
    {
        if (_splashFX != null)
        {
            _splashFX.transform.position = pPos;
            _splashFX.Play();
        }
        
    }

    public void FireCannon()
    {
        if (_prefabCannonBall != null)
        {
            //instatiate & setup the cannon ball
            GameObject newCannonBall = Instantiate(_prefabCannonBall, new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z), _spawnCannon.rotation);

            newCannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);

            //Vector3 targetPosition = _spawnCannon.position + _spawnCannon.forward * _distShoot;
            Vector3 targetPosition = new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z) + _spawnCannon.forward * _distShoot;
            targetPosition.y = _saveCannonPosY;
            newCannonBall.GetComponent<CannonBall>().setTargetPosition(targetPosition);

            newCannonBall.GetComponent<CannonBall>()._owner = _owner;
            newCannonBall.GetComponent<CannonBall>()._shooter = this;

            _listCannonBall.Add(newCannonBall);

            if (_smokeFX != null)
            {
                _smokeFX.Play();
            }

            AudioManager.PlayRandom(this.gameObject.GetComponent<AudioSource>(), "Canon1", "Canon2", "Canon3", "Canon4");

            //NetworkServer.SpawnWithClientAuthority(newCannonBall, _owner.gameObject.GetComponent<BoatController>().player.connectionToClient);
            this.FireCannon(newCannonBall);
            //Debug.Log("_listCannonBall.Count : " + _listCannonBall.Count);
        }
        else
        {
            //Debug.Log("_cannonBall est null");

        }
    }

    public void FireCannon(GameObject cannonBall)
    {
        cannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);

        //Vector3 targetPosition = _spawnCannon.position + _spawnCannon.forward * _distShoot;
        Vector3 targetPosition = new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z) + _spawnCannon.forward * _distShoot;
        targetPosition.y = _saveCannonPosY;
        cannonBall.GetComponent<CannonBall>().setTargetPosition(targetPosition);

        cannonBall.GetComponent<CannonBall>()._owner = _owner;
        _listCannonBall.Add(cannonBall);
        if (_smokeFX != null)
        {
            _smokeFX.Play();
        }
    }


}
