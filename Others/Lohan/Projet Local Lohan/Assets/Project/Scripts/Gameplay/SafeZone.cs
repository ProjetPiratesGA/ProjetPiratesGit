using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.layer = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //Check if object in collision is a player
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null && other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe == false)
        {
            //Put all the associated enemies on alert if they aren't already
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe = true;
            MeshRenderer[] children = other.GetComponentsInChildren<MeshRenderer>();
            Color newColor;
            foreach (MeshRenderer child in children)
            {
                newColor = child.material.color;
                newColor.a = 0.7f;
                child.material.color = newColor;
                child.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                child.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                child.material.SetInt("_ZWrite", 0);
                child.material.DisableKeyword("_ALPHATEST_ON");
                child.material.DisableKeyword("_ALPHABLEND_ON");
                child.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                child.material.renderQueue = 3000;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check if object in collision is a player
        if (other.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
        {
            //Put all the associated enemies on alert if they aren't already
            other.GetComponent<ProjetPirate.Boat.BoatCharacter>().Safe = false;

            MeshRenderer[] children = other.GetComponentsInChildren<MeshRenderer>();
            Color newColor;
            foreach (MeshRenderer child in children)
            {
                newColor = child.material.color;
                newColor.a = 0.7f;
                child.material.color = newColor;
                child.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                child.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                child.material.SetInt("_ZWrite", 1);
                child.material.DisableKeyword("_ALPHATEST_ON");
                child.material.DisableKeyword("_ALPHABLEND_ON");
                child.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                child.material.renderQueue = -1;
            }
        }
    }
}
