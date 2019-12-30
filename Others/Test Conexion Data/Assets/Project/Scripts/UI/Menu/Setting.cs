using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.UI.Menu
{
    public class Setting : MonoBehaviour
    {

        public GameObject sound;
        public GameObject graphisme;
        public GameObject compte;


        // Use this for initialization
        void Start()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);
        }

        private void OnEnable()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ChangeToSound()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);
        }
        public void ChangeToGraphism()
        {
            graphisme.SetActive(true);
            sound.SetActive(false);
            compte.SetActive(false);
        }
        public void ChangeToAccount()
        {
            graphisme.SetActive(false);
            sound.SetActive(false);
            compte.SetActive(true);
        }
    }
}