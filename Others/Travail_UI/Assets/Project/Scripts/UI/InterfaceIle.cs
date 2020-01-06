using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Data;
using ProjetPirate.Boat;

namespace ProjetPirate.UI
{
    public class InterfaceIle : MonoBehaviour
    {
        public GameObject _questUI;
        public GameObject _marchandUI;
        public GameObject _buttonBuyCanonProue;
        public GameObject _messageErreurRessources;    
        public GameObject _messageErreurNbreCanonMax;    
        public GameObject _messageErreurNbreCanonProueMax;    
        public GameObject _messageErreurLvLMax;    

        [Header("TEXT")]
        public Text _goldValueInfoPlayerText;
        public Text _XpValueInfoPlayerText;
        public Text _textQuest;
        public Text _goldValueBuyCanonText;
        public Text _goldValueBuyProueText;
        public Text _XpValueBuyProueText;
        public Text _goldValueUpgradeText;
        public Text _XpValueUpgradeText;


        private GameObject _player;

        Data_Dock _dataDock;

        bool thereIsAQuest = true;


        bool blockInput = false;
        float timerBlockInput = 0;

        //Gestion Marchand 

        //currentValue
        int currentLvL;
        int currentNumberCanon;
        int currentCanonProue;
        int currentBoatCost;

        //Max Value
        int maxCanon = 12;
        int maxLvL = 3;
        int maxCanonProue = 1;




        // Use this for initialization
        void Start()
        {
            // faire la gestion en fonction de si on a une quete ou non
            _player = GameObject.FindGameObjectWithTag("Player");
            _dataDock = new Data_Dock();

            //Set thereIsAQuest grâce au data Dock            
            if (thereIsAQuest)
            {
                _questUI.SetActive(true);
                _marchandUI.SetActive(false);
            }
            else
            {
                _questUI.SetActive(false);
                _marchandUI.SetActive(true);
            }
        }

        private void OnEnable()
        {
            //Set thereIsAQuest grâce au data Dock 
            if (thereIsAQuest)
            {
                _questUI.SetActive(true);
                _marchandUI.SetActive(false);
            }
            else
            {
                _questUI.SetActive(false);
                _marchandUI.SetActive(true);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }

            if (_dataDock != null)
            {
                _textQuest.text = _dataDock.Pnj.DialogueQuete;
            }

            UpdateGoldValue();
            UpdateXpValue();


            if (blockInput)
            {
                timerBlockInput += Time.deltaTime;
                if (timerBlockInput >= 2)
                {
                    _messageErreurRessources.SetActive(false);
                    _messageErreurNbreCanonMax.SetActive(false);
                    _messageErreurNbreCanonProueMax.SetActive(false);
                    _messageErreurLvLMax.SetActive(false);
                    this.GetComponent<GraphicRaycaster>().enabled = true;
                    blockInput = false;
                }
            }




            //if(accesseur lvl bateau > 1 || accesseur bateau possede canon proue == false)
            //{
            //    _buttonBuyCanonProue.SetActive(true);
            //}
            //else
            //{
            //    _buttonBuyCanonProue.SetActive(false);
            //}

        }

        public void AcceptQuest()
        {
            Debug.LogError("Quest Accept");
            //Fonction Pour Accepter la quest
            _questUI.SetActive(false);
            _marchandUI.SetActive(true);
        }

        public void RefuseQuest()
        {
            _questUI.SetActive(false);
            _marchandUI.SetActive(true);
        }

        public void SetDataDock(Data_Dock _pdataDock)
        {
            _dataDock = _pdataDock;
        }

        void UpdateGoldValue()
        {
            if (_goldValueInfoPlayerText != null)
            {
                if (_player != null)
                {
                    // _goldValueText.text = accesseur gold player;
                }
                else
                {
                    _goldValueInfoPlayerText.text = "Joueur NULL";
                }
            }
        }

        void UpdateXpValue()
        {
            if (_XpValueInfoPlayerText != null)
            {
                if (_player != null)
                {
                    _XpValueInfoPlayerText.text = _player.GetComponent<BoatCharacter>()._currentXp.ToString();
                }
                else
                {
                    _XpValueInfoPlayerText.text = "Joueur NULL";

                }
            }
        }

        public void QuitCanvasIle()
        {
            Debug.LogError("QuitCanvas");
            _questUI.SetActive(true);
            _marchandUI.SetActive(false);
            this.gameObject.SetActive(false);
        }

        void UpdateShopTextValue()
        {
            //Update The value
            // _goldValueBuyCanonText;
            // _goldValueBuyProueText;
            //_XpValueBuyProueText;
            // _goldValueUpgradeText;
            //_XpValueUpgradeText;
        }

        public void BuyCanon()
        {
            Debug.Log("Achat Canon");

            //if(accesseur gold player > accesseur cout canon)
            //{
            if (currentNumberCanon < ((maxCanon / maxLvL) * (currentLvL)))
            {

                //    accesseur gold player -= accesseur canon 
                //    fonction ajout de canon
            }
            else
            {
                //_messageErreurNbreCanonMax.SetActive(true);
                //    this.GetComponent<GraphicRaycaster>().enabled = false;
                //    timerBlockInput = 0;
                //    blockInput = true;
            }
            ////}
            //else
            //{
            //    _messageErreurRessources.SetActive(true);
            //    this.GetComponent<GraphicRaycaster>().enabled = false;
            //    timerBlockInput = 0;
            //    blockInput = true;
            //}
        }

        public void BuyCanonProue()
        {
            Debug.Log("Achat Canon Proue");
            //if(accesseur gold player > accesseur cout canon && accesseur xp player > accesseur Xp Canon)
            //{
            if (currentCanonProue < maxCanonProue && currentLvL > 1)
            {

                //    accesseur gold player -= accesseur canon 
                //    fonction ajout de canon proue
            }
            else
            {
                //_messageErreurNbreCanonProueMax.SetActive(true);
                //    this.GetComponent<GraphicRaycaster>().enabled = false;
                //    timerBlockInput = 0;
                //    blockInput = true;
            }
            ////}
            //else
            //{
            //    _messageErreurRessources.SetActive(true);
            //    this.GetComponent<GraphicRaycaster>().enabled = false;
            //    timerBlockInput = 0;
            //    blockInput = true;
            //}
        }

        public void UpgradeBoat()
        {
            Debug.Log("UpgradeBoat");

            //if(accesseur gold player > accesseur cout upgrade && accesseur xp player > accesseur Xp upgrade )
            //{
            if (currentLvL < maxLvL)
            {

                //    accesseur gold player -= accesseur upgrade 
                //    fonction upgradeboat
            }
            else
            {
                //    _messageErreurLvLMax.SetActive(true);
                //    this.GetComponent<GraphicRaycaster>().enabled = false;
                //    timerBlockInput = 0;
                //    blockInput = true;
            }
            //}           
            //else
            //{
            //    _messageErreurRessources.SetActive(true);
            //    this.GetComponent<GraphicRaycaster>().enabled = false;
            //    timerBlockInput = 0;
            //    blockInput = true;
            //}
        }
    }
}
