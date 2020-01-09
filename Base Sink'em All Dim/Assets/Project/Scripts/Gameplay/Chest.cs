using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Boat;
using UnityEngine.Networking;

public class Chest : NetworkBehaviour {

    [SerializeField] private int _containedMoney = 0;
    private int _containedPlank = 0;

    bool chestDestroy = false;
    GameObject destroyer;

    public int containedMoney
    {
        get { return _containedMoney;  }
        set { _containedMoney = value; }
    }
    public int containedPlank
    {
        get { return _containedPlank; }
        set { _containedPlank = value; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(chestDestroy == true && this.gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            destroyer.GetComponent<BoatController>().player.CmdDestroyChest(this.gameObject);
        }
	}

    [TargetRpc]
    public void TargetSpawnChest(NetworkConnection _conn, GameObject chest, Vector3 _pos, int _pContainedMoney, int _pContainedPlank)
    {
        chest.transform.position = _pos;
        chest.GetComponent<Chest>().containedMoney = _pContainedMoney;
        chest.GetComponent<Chest>().containedPlank = _pContainedPlank;
    }

    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<BoatCharacter>()._isDying == false)
            {
                //Debug.LogError("Collid chest");
                AudioManager.Play(this.gameObject.GetComponent<AudioSource>(), "OpenChest");
               
                other.GetComponent<BoatCharacter>().player.GetComponent<Player>().GainMoney(_containedMoney);
                other.GetComponent<BoatCharacter>().GoldFx(this.transform.position, _containedMoney);
                this.GetComponent<MeshRenderer>().enabled = false;
                destroyer = other.gameObject;
                chestDestroy = true;
            }
        }
    }
}