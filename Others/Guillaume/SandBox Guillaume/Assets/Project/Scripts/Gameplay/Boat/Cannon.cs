using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

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

    private List<GameObject> _listCannonBall;

    [SerializeField]
    private float _forceCannonBall;

    [SerializeField]
    private float _distShoot = 10f;

    [SerializeField] private ParticleSystem _smokeFX;

    // Use this for initialization
    void Start()
    {
        if (_spawnCannon == null)
        {
            Debug.LogError("_spawnCannon est null");

        }
        //Au start du bateau
        //sauvegarde de la position y et de la rotation x et z pour le spawn de cannonBall et la définition de la target
        
        _saveCannonPosY = _spawnCannon.position.y;
        _saveCannonRotX = _spawnCannon.rotation.eulerAngles.x;
        _saveCannonRotY = _spawnCannon.rotation.eulerAngles.y;
        _saveCannonRotZ = _spawnCannon.rotation.eulerAngles.z;
        Debug.Log(this.name + " _saveCannonPosY : " + _saveCannonPosY + " _saveCannonRotX : " + _saveCannonRotX + " _saveCannonRotZ : " + _saveCannonRotZ);

        _listCannonBall = new List<GameObject>();
        if (_smokeFX != null)
        {
            _smokeFX.Stop();
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
            GameObject newCannonBall = Instantiate(_prefabCannonBall, new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z) , _spawnCannon.rotation);

            newCannonBall.GetComponent<CannonBall>().setForceCannonBall(_forceCannonBall);

            //Vector3 targetPosition = _spawnCannon.position + _spawnCannon.forward * _distShoot;
            Vector3 targetPosition = new Vector3(_spawnCannon.position.x, _saveCannonPosY, _spawnCannon.position.z) + _spawnCannon.forward * _distShoot;
            targetPosition.y = _saveCannonPosY;
            newCannonBall.GetComponent<CannonBall>().setTargetPosition(targetPosition);
            Debug.Log(this.name + " INSTANTIATE CANNONBALL WITH / POSITION --> " + newCannonBall.transform.position + " --> ROTATION : " + newCannonBall.transform.rotation
                + " TARGET / POSITION : " + targetPosition);
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
}
