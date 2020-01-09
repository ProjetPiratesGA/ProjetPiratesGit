using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHarpoon : MonoBehaviour {
    
    [SerializeField]
    private Transform _spawnCannonHarpoon;
    [SerializeField]
    private GameObject _prefabHarpoonArrow;
    [SerializeField]
    private ProjetPirate.Boat.BoatCharacter _owner;

    private List<GameObject> _listCannonHarpoonArrows;


	// Use this for initialization
	void Start () {

        if (_spawnCannonHarpoon == null)
        {
            Debug.Log("_spawnCannonHarpoon est null");
        }
        _listCannonHarpoonArrows = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void SetOwner(ProjetPirate.Boat.BoatCharacter pOwner)
    {
        _owner = pOwner;
    }
    public void _FireCannonHarpoon()
    {
        if (_prefabHarpoonArrow != null)
        {
            //instatiate & setup the cannon ball
            GameObject newCannonHarpoonArrow = Instantiate(_prefabHarpoonArrow, _spawnCannonHarpoon.position, _spawnCannonHarpoon.rotation);

            newCannonHarpoonArrow.GetComponent<HarpoonArrow>().setTargetPosition(_spawnCannonHarpoon.position + _spawnCannonHarpoon.forward * 2);
            newCannonHarpoonArrow.GetComponent<HarpoonArrow>()._owner = _owner;
            _listCannonHarpoonArrows.Add(newCannonHarpoonArrow);
        }
        else
        {
           Debug.Log("_cannonHarpoonArrow est null");
        }
    }
}
