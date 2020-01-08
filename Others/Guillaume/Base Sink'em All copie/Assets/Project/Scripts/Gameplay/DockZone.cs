using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockZone : MonoBehaviour
{

    [SerializeField] private List<Dock> _docks;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            //Put all the associated enemies on alert if they aren't already
            //other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Dock = false;
        }
    }
}
