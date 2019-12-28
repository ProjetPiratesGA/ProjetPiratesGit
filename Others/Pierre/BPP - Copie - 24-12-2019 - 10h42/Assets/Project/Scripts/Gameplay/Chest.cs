using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    [SerializeField] private int _containedMoney = 0;
    private int _containedPlank = 0;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void SpawnChest(GameObject pChestPrefab, Vector3 pSpawnPosition, int pContainedMoney = 0, int pContainedPlank = 0)
    {
        Chest chest = Instantiate(pChestPrefab).GetComponent<Chest>();
        chest.transform.position = pSpawnPosition + new Vector3(0, 0.712f, 0);
        chest._containedMoney = pContainedMoney;
        chest._containedPlank = pContainedPlank;
    }


    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Player")
        {
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Controller.GetComponent<Player>().GainMoney(_containedMoney);
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().GoldFx(this.transform.position, _containedMoney);
            Destroy(this.gameObject);
        }
    }
}
