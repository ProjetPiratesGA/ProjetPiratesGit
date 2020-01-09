using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjetPirate.Data;
using ProjetPirate.Boat;
using ProjetPirate.Gameplay;

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
        public GameObject _messageInfoCanUpgrade;    


        [Header("TEXT")]
        public Text _goldValueInfoPlayerText;
        public Text _XpValueInfoPlayerText;
        public Text _textQuest;
        public Text _goldValueBuyCanonText;
        public Text _goldValueBuyProueText;
        public Text _XpValueBuyProueText;
        public Text _goldValueUpgradeText;
        public Text _XpValueUpgradeText;


        GameObject _player;

        Data_Dock _dataDock;

        Data_Quests quest;

        bool thereIsAQuest = false;


        bool blockInput = false;
        float timerBlockInput = 0;

        //Gestion Marchand 

        //currentValue
        int currentGold = 15000;
        float currentXp = 1000;
        int currentLvL = 1;
        int currentNumberCanon = 2;
        int currentCanonProue = 0;
        int currentBoatCost;

        //Max Value
        int maxCanon = 12;
        int maxLvL = 3;
        int maxCanonProue = 1;

        //Cost Upgrade
        int costUpgradeLvLGold;
        int costUpgradeLvLXp;
        int costCanon;
        int costGoldCanonProue;
        int costXpCanonProue;

        bool canUpgradeCheck = false;


        // Use this for initialization
        void Start()
        {
            // faire la gestion en fonction de si on a une quete ou non
            _player = GameObject.FindGameObjectWithTag("Player");
            _dataDock = new Data_Dock();

            if(_player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().isQuestAvailable)
            {
                thereIsAQuest = true;
                quest = _player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().GetComponent<QuestScript>().GenerateQuest(6500);
            }

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

            UpdateCurrentValue();
        }

        private void OnEnable()
        {
            if (_player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().isQuestAvailable)
            {
                thereIsAQuest = true;
                quest = _player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().GetComponent<QuestScript>().GenerateQuest(6500);
            }
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
            canUpgradeCheck = false;

            UpdateCurrentValue();
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
                _textQuest.text = quest.TextQuest;
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
                    _messageInfoCanUpgrade.SetActive(false);
                    this.GetComponent<GraphicRaycaster>().enabled = true;
                    blockInput = false;
                }
            }

            UpdateShopTextValue();

            //if(currentLvL > 1 || accesseur bateau possede canon proue == false)
            //{
            //_buttonBuyCanonProue.GetComponent<Button>().interactable = true;
            //}
            //else
            //{
            //_buttonBuyCanonProue.GetComponent<Button>().interactable = false;
            //}

            if (_marchandUI.activeSelf)
            {
                if (!canUpgradeCheck)
                {
                    CheckIfCanUpgradeBoat();
                }
            }
        }

        public void AcceptQuest()
        {
            Debug.Log("Quest Accept");
            //Fonction Pour Accepter la quest
            _player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().GetComponent<QuestScript>().QuestIsAccepted();
            _player.GetComponentInParent<Player>().data_quest = quest;
            _player.GetComponentInParent<Player>().data_quest.IsAccepted = true;
            _player.GetComponentInParent<Player>().haveAQuest = true;
            Debug.Log(_player.GetComponentInParent<Player>().data_quest.ID);
            _questUI.SetActive(false);
            _marchandUI.SetActive(true);
            thereIsAQuest = false;
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
                    _goldValueInfoPlayerText.text = currentGold.ToString();
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
                    _XpValueInfoPlayerText.text = currentXp.ToString();
                }
                else
                {
                    _XpValueInfoPlayerText.text = "Joueur NULL";

                }
            }
        }

        public void QuitCanvasIle()
        {
            Debug.Log("QuitCanvas");
            _questUI.SetActive(true);
            _marchandUI.SetActive(false);
            this.gameObject.SetActive(false);
            _player.GetComponentInChildren<BoatCharacter>().StartDocking();
        }

        void UpdateShopTextValue()
        {
            //Update The value
            // _goldValueBuyCanonText;
            // _goldValueBuyProueText;
            //_XpValueBuyProueText;

            if (currentLvL == 1)
            {
                costUpgradeLvLGold = 10000;
                costUpgradeLvLXp = 500;
                _goldValueUpgradeText.text = costUpgradeLvLGold.ToString();
                _XpValueUpgradeText.text = costUpgradeLvLXp.ToString();

            }

            if (currentLvL == 2)
            {
                costUpgradeLvLGold = 50000;
                costUpgradeLvLXp = 2000;
                _goldValueUpgradeText.text = costUpgradeLvLGold.ToString();
                _XpValueUpgradeText.text = costUpgradeLvLXp.ToString();

            }

        }

        void UpdateCurrentValue()
        {
            //currentGold =_playerScript._data.Ressource.Golds;
            //currentXp = _playerScript._data.Ressource.Reputation;
            //currentLvL = accesseurLvLPlayer;
            //currentNumberCanon = accesseurCanonPlayer;
            //currentCanonProue = accesseurCanonProuePlayer;
        }

        public void BuyCanon()
        {
            Debug.Log("Achat Canon");
            Debug.Log(currentNumberCanon);

            if (currentGold > costCanon)
            {
                if (currentNumberCanon < ((maxCanon / maxLvL) * (currentLvL)))
                {

                    //Enlever le cout au current
                    currentGold -= costCanon;

                    //fonction ajout de canon
                    currentNumberCanon += 1;



                    UpdateCurrentValue();
                }
                else
                {
                    _messageErreurNbreCanonMax.SetActive(true);
                    this.GetComponent<GraphicRaycaster>().enabled = false;
                    timerBlockInput = 0;
                    blockInput = true;
                }
            }
            else
            {
                _messageErreurRessources.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
        }

        public void BuyCanonProue()
        {
            Debug.Log("Achat Canon Proue");
            Debug.Log(currentCanonProue);

            if (currentGold > costGoldCanonProue && currentXp > costXpCanonProue)
            {
                if (currentCanonProue < maxCanonProue && currentLvL > 1)
                {

                    //Enlever le cout au current
                    currentGold -= costGoldCanonProue;
                    currentXp -= costXpCanonProue;

                    // Set le current Gold et Xp au joueur


                    //fonction ajout de canon proue
                    currentCanonProue += 1;
                    UpdateCurrentValue();
                }
                else
                {
                    _messageErreurNbreCanonProueMax.SetActive(true);
                    this.GetComponent<GraphicRaycaster>().enabled = false;
                    timerBlockInput = 0;
                    blockInput = true;
                }
            }
            else
            {
                    _messageErreurRessources.SetActive(true);
                    this.GetComponent<GraphicRaycaster>().enabled = false;
                    timerBlockInput = 0;
                    blockInput = true;
                }
            }

        public void UpgradeBoat()
        {
            Debug.Log("UpgradeBoat");
            Debug.Log(currentLvL);

            if (currentGold > costUpgradeLvLGold && currentXp > costUpgradeLvLXp)
            {
                if (currentLvL < maxLvL)
                {
                    //Enlever le cout au current
                    currentGold -= costUpgradeLvLGold;
                    currentXp -= costUpgradeLvLXp;

                    //Set le current Gold et Xp au joueur

                    //Changement de LvL du bateau
                    currentLvL += 1;
                    //fonctionChangement lvl bateau


                    //    fonction upgradeboat
                    UpdateCurrentValue();
                }
                else
                {
                    _messageErreurLvLMax.SetActive(true);
                    this.GetComponent<GraphicRaycaster>().enabled = false;
                    timerBlockInput = 0;
                    blockInput = true;
                }
            }
            else
            {
                _messageErreurRessources.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
        }

        void CheckIfCanUpgradeBoat()
        {
            if(currentGold > costUpgradeLvLGold && currentXp > costUpgradeLvLXp)
            {
                Debug.Log("On peut améliorer le beateau");
                canUpgradeCheck = true;
                _messageInfoCanUpgrade.SetActive(true);
                this.GetComponent<GraphicRaycaster>().enabled = false;
                timerBlockInput = 0;
                blockInput = true;
            }
            else
            {
                canUpgradeCheck = true;
            }
        }

        public void SetPlayer(GameObject pPlayer)
        {
            _player = pPlayer;
        }
    }
}
