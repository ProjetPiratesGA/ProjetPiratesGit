using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockZone : MonoBehaviour
{

    [SerializeField] private List<Dock> _docks;

    // Use this for initialization
    void Start()
    {
        this.gameObject.layer = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._dock != null)
            {
                if (!other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._dock._isAvailable)
                {
                    for (int i = 0; i < _docks.Count; i++)
                    {
                        if (_docks[i]._isAvailable)
                        {
                            other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._dock = _docks[i];
                            other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._canDock = true;

                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _docks.Count; i++)
                {
                    if (_docks[i]._isAvailable)
                    {
                        other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._dock = _docks[i];
                        other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._canDock = true;
                        break;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._dock = null;
        other.GetComponent<ProjetPirate.Boat.BoatCharacter>()._canDock = false;
    }
}
