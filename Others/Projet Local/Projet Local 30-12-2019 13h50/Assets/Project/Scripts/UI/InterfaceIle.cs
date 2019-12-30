using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Data;

namespace ProjetPirate.UI
{
    public class InterfaceIle : MonoBehaviour
    {
        public GameObject _questUI;
        public GameObject _noQuestUI;
        public Text _textQuest;

        Data_Dock _dataDock;

        bool thereIsAQuest = true;
        // Use this for initialization
        void Start()
        {
            _dataDock = new Data_Dock();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_dataDock != null)
            {
                _textQuest.text = _dataDock.Pnj.DialogueQuete;
            }
        }

        public void AcceptQuest()
        {
            Debug.LogError("Quest Accept");
            //Fonction Pour Accepter la quest
        }

        public void RefuseQuest()
        {
            this.gameObject.SetActive(false);
        }

        public void SetDataDock( Data_Dock _pdataDock)
        {
            _dataDock = _pdataDock;
        }
    }
}
