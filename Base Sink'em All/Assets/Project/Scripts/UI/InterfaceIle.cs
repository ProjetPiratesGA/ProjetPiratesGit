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
        public GameObject _messagePasAssezHL;


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
        int costCanon = 200;
        int costGoldCanonProue = 500;
        int costXpCanonProue = 500;

        bool canUpgradeCheck = false;

        bool AddCanLarboard = true;

        // Use this for initialization
        void Start()
        {
            // faire la gestion en fonction de si on a une quete ou non
            _player = GameObject.FindGameObjectWithTag("Player");
            _dataDock = new Data_Dock();

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

            UpdateCurrentValue();
        }

        private void OnEnable()
        {
            if (_player.GetComponentInChildren<BoatCharacter>() != null)
            {
                if (_player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().isQuestAvailable)
                {
                    thereIsAQuest = true;
                    quest = _player.GetComponentInChildren<BoatCharacter>()._dock.gameObject.GetComponent<QuestScript>().GetComponent<QuestScript>().GenerateQuest(6500);
                }
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

            if (quest != null)
            {
                _textQuest.text = quest.TextQuest;
            }

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
                    _messagePasAssezHL.SetActive(false);
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
                    _goldValueInfoPlayerText.text = _player.GetComponentInParent<Player>()._data.Ressource.Golds.ToString();
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
                    _XpValueInfoPlayerText.text = _player.GetComponentInParent<Player>()._data.Ressource.Reputation.ToString();
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

            UpdateGoldValue();
            UpdateXpValue();
        }

        void UpdateCurrentValue()
        {
            currentGold = _player.GetComponentInParent<Player>()._data.Ressource.Golds;
            currentXp = _player.GetComponentInParent<Player>()._data.Ressource.Reputation;
            currentLvL = _player.GetComponentInParent<Player>().ShipLevel;
            int countCanon = _player.GetComponentInParent<Player>()._data.Boat.CurrentCanonLeft + _player.GetComponentInParent<Player>()._data.Boat.CurrentCanonRight;
            currentNumberCanon = countCanon;

            int countCanonProue = 0;
            if (_player.GetComponentInChildren<BoatCharacter>()._prowCannonHarpoon.gameObject.activeSelf)
            {
                countCanonProue = 1;
            }
            currentCanonProue = countCanonProue;

            _player.GetComponentInParent<Player>().CmdUpdateDataGold(_player.GetComponentInParent<Player>()._data.Ressource.Golds);
        }

        public void BuyCanon()
        {
            Debug.Log("Achat Canon");
            Debug.Log(currentNumberCanon);

            if (currentGold > costCanon)
            {
                if (currentNumberCanon < ((maxCanon / maxLvL) * (currentLvL)))
                {
                    Debug.Log("argent avant avoir enelve" + _player.GetComponentInParent<Player>()._data.Ressource.Golds);
                    _player.GetComponentInParent<Player>().LoseMoney(costCanon);
                    Debug.Log("argent apressss avoir enelve" + _player.GetComponentInParent<Player>()._data.Ressource.Golds);

                    //fonction ajout de canon

                    if (AddCanLarboard)
                    {
                        if ((_player.GetComponentInParent<Player>()._data.Boat.CurrentCanonLeft < _player.GetComponentInChildren<BoatCharacter>()._maxCannonsPerSide))
                        {
                            _player.GetComponentInParent<Player>()._data.Boat.CurrentCanonLeft++;
                        }

                        _player.GetComponentInParent<Player>().CmdSendCurrentCanonLeft(_player.GetComponentInParent<Player>()._data.Boat.CurrentCanonLeft);

                        _player.GetComponentInChildren<BoatCharacter>().CmdUpdateActiveCanons();
                        AddCanLarboard = false;
                    }
                    else
                    {
                        if ((_player.GetComponentInParent<Player>()._data.Boat.CurrentCanonRight < _player.GetComponentInChildren<BoatCharacter>()._maxCannonsPerSide))
                        {
                            _player.GetComponentInParent<Player>()._data.Boat.CurrentCanonRight++;
                        }

                        _player.GetComponentInParent<Player>().CmdSendCurrentCanonRight(_player.GetComponentInParent<Player>()._data.Boat.CurrentCanonRight);

                        _player.GetComponentInChildren<BoatCharacter>().CmdUpdateActiveCanons();
                        AddCanLarboard = true;
                    }
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
                    // Set le current Gold et Xp au joueur
                    _player.GetComponentInParent<Player>().LoseMoney(costGoldCanonProue);
                    _player.GetComponentInParent<Player>().LoseXP(costXpCanonProue);

                    //fonction ajout de canon proue
                    _player.GetComponentInChildren<BoatCharacter>()._prowCannonHarpoon.gameObject.SetActive(true);
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
                if (currentLvL < 2)
                {
                    _messagePasAssezHL.SetActive(true);
                }
                else
                {
                    _messageErreurRessources.SetActive(true);
                }
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
                    // Set le current Gold et Xp au joueur
                    _player.GetComponentInParent<Player>().LoseMoney(costUpgradeLvLGold);
                    _player.GetComponentInParent<Player>().LoseXP(costUpgradeLvLXp);

                    //fonctionChangement lvl bateau  this._data.Ressource.BoatLevel += 1;
                    _player.GetComponentInParent<Player>().CmdSendBoatLevel(_player.GetComponentInParent<Player>()._data.Ressource.BoatLevel);
                    _player.GetComponentInParent<Player>().CmdSpawnBoat();

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
            if (currentGold > costUpgradeLvLGold && currentXp > costUpgradeLvLXp)
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
