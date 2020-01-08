using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using ProjetPirate.Boat;

namespace ProjetPirate.UI.HUD
{
    public class HUD_Script : MonoBehaviour
    {
        [Header("TEXT")]
        public Text _goldValueText;
        public Text _XpValueText;
        public Text _NumberWoodenBoardValue;
        public Text _playerName;
        public Text _IntituleQuete;
        public Text _suivieText;

        [Header("Canvas")]
        public GameObject _menuInGameCanvas;
        public GameObject _interractionPlayer;
        public GameObject _interractionOtherPlayer;
        public GameObject _interactionIle;

        [Header("GameObject")]
        public GameObject goldSprite;
        public GameObject xpSprite;
        public GameObject woodenBoard;
        public GameObject lifeBar;
        public GameObject _buttonQuitterGroupe;
        public GameObject _buttonHarpon;
        public GameObject _confirmQuitGroup;
        public GameObject _playerInformations;
        public GameObject _otherPlayerInformations;
        public GameObject _interfaceQuest;

        [Header("Var Test au cas où certains accesseur sont indisponibles")]
        public int hp = 50;
        public int maxhp = 50;

        private GameObject _player;//On récupère le player afin que le controller possede ses info 
        private GameObject _otherPlayer;
        private float maxLifeBarSize;
        //Gestion des interractions avec les joueurs
        bool interractCanBeDraw = false;
        bool interractOtherPlayerCanBeDraw = false;
        private GameObject _playerToFollow;

        bool informationCanBeDraw = false;

        private Player _playerScript = null;/// SEB
        bool _isPlayerNameSet = false; //SEB
        // Use this for initialization
        void Start()
        {
            //_player = GameObject.FindGameObjectWithTag("Player");
            //_NumberWoodenBoardValue.text = ;

            //UpdateGoldValue();
           // UpdateXpValue();
            if (_interractionPlayer != null)
            {
                _interractionPlayer.SetActive(false);
            }

            _playerInformations.SetActive(false);
            _otherPlayerInformations.SetActive(false);
            _confirmQuitGroup.SetActive(false);
            _interfaceQuest.SetActive(false);

            maxLifeBarSize = lifeBar.GetComponent<RectTransform>().sizeDelta.x;

        }

        //TEST SEB
        public void SetPlayerReference(GameObject player)
        {
            
            _player = player;
            _playerScript = _player.GetComponent<Player>();
            _interactionIle.GetComponent<InterfaceIle>().SetPlayer(_player);
        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }
            if (!_isPlayerNameSet)
            {
                _playerName.text = _playerScript._username;
                _isPlayerNameSet = true;
            }

            //Check du button Harpon afin de le desactriver si on n'en possède pas
            //if(player possede harpon)
            //{
            //    if (!_buttonHarpon.activeSelf)
            //    {
            //        _buttonHarpon.SetActive(true);
            //    }
            //}
            //else
            //{
            //    if(_buttonHarpon.activeSelf)
            //    {
            //        _buttonHarpon.SetActive(false);
            //    }
            //}


            UpdateGoldValue();
            UpdateXpValue();
            UpdatePlankValue();

            if (_interractionPlayer != null)
            {
                CheckClickOnBoat();
            }

            if (_player.GetComponentInChildren<BoatCharacter>())
            {
                UpdateLifeBar();
            }

            if(_player.GetComponentInParent<Player>().haveAQuest)
            {
                if (!_interfaceQuest.activeSelf)
                {
                    _IntituleQuete.text = _player.GetComponentInParent<Player>().data_quest.TextQuest;
                    _interfaceQuest.SetActive(true);
                }

                _suivieText.text = _player.GetComponentInParent<Player>().data_quest.ItemCount + " / " + _player.GetComponentInParent<Player>().data_quest.ItemCountNeeded;
            }
            else
            {
                if (_interfaceQuest.activeSelf)
                {
                    _interfaceQuest.SetActive(false);
                }

            }

            //CheckIfPlayerDocked();
        }

        void UpdateGoldValue()
        {
            if (_goldValueText != null)
            {
                if (_player != null)
                {
                    _goldValueText.text = _playerScript._data.Ressource.Golds.ToString();
                }
                else
                {
                    _goldValueText.text = "Joueur NULL";
                }
            }
        }

        void UpdateXpValue()
        {
            if (_XpValueText != null)
            {
                if (_player != null)
                {
                    _XpValueText.text = _playerScript._data.Ressource.Reputation.ToString();
                }
                else
                {
                    _XpValueText.text = "Joueur NULL";

                }
            }
        }

        void UpdatePlankValue()
        {
            if (_NumberWoodenBoardValue != null)
            {
                if (_player != null)
                {
                    _NumberWoodenBoardValue.text = _playerScript._data.Ressource.WoodBoard.ToString();
                }
                else
                {
                    _NumberWoodenBoardValue.text = "Joueur NULL";
                }
            }
        }



        public void ActivateMenuInGame()
        {
            if (!_menuInGameCanvas.activeSelf)
            {
                this.GetComponent<GraphicRaycaster>().enabled = false;
                _menuInGameCanvas.SetActive(true);

            }
            else
            {
                this.GetComponent<GraphicRaycaster>().enabled = true;
                _menuInGameCanvas.SetActive(false);

            }
        }

        public void ReturnMenu()
        {
            //Debug.LogError("Return Menu");
            //Mettre le retour au menu avec la sortie du serveur
            // SceneManager.LoadScene(1);
        }

        public void CheckClickOnBoat()
        {

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray;
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.LogError("Je clique sur : "+hit.collider.gameObject);
                    //On check si on est sur le player

                    if (hit.collider.gameObject.tag == "Player")
                    {
                        Debug.LogError("J'ai cliqué sur un joueur");
                        interractCanBeDraw = true;
                        ActivateInterractionPlayer();
                        
                    }
                    else if(hit.collider.gameObject.tag == "Enemy")//Changer avec le tag des autres joueurs
                    {
                        _otherPlayer = hit.collider.gameObject;
                        interractOtherPlayerCanBeDraw = true;
                        ActivateInterractionOtherPlayer();
                    }
                    else if (hit.collider.gameObject.tag != "Player" && !EventSystem.current.IsPointerOverGameObject() && hit.collider.gameObject.tag != "Enemy")
                    {
                        Debug.LogError("On sort de l interaction player");
                        interractOtherPlayerCanBeDraw = false;
                        interractCanBeDraw = false;
                        ActivateInterractionOtherPlayer();
                        ActivateInterractionPlayer();                     


                    }
                }
                else
                {
                    Debug.LogError("On ne touche rien");
                }
            }
#elif UNITY_ANDROID
              if (Input.touchCount == 1)
            {
                 Ray ray;
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                 RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.LogError(hit.collider.gameObject);
                    //On check si on est sur le player

                    if (hit.collider.gameObject.tag == "Player")
                    {
                        interractCanBeDraw = true;
                        ActivateInterractionPlayer();
                        
                    }
                    else if(hit.collider.gameObject.tag == "Enemy")//Changer avec le tag des autres joueurs
                    {
                        _otherPlayer = hit.collider.gameObject;
                        interractOtherPlayerCanBeDraw = true;
                        ActivateInterractionOtherPlayer();
                    }
                    else if (hit.collider.gameObject.tag != "Player" &&  hit.collider.gameObject.tag != "Enemy")
                    {
                        if(!EventSystem.current.currentSelectedGameObject)
                         {
                            Debug.LogError("On sort de l interaction player");
                            interractOtherPlayerCanBeDraw = false;
                            interractCanBeDraw = false;
                            ActivateInterractionOtherPlayer();
                            ActivateInterractionPlayer();
                         }
                    }
                }
                else
                {
                    Debug.LogError("On ne touche rien");
                }
            }
#endif

        }

        public void UpdateLifeBar()
        {
            if (_player != null)
            {
                BoatCharacter boatChar = _player.GetComponentInChildren<BoatCharacter>();
                if (boatChar == null)
                {
                    Debug.LogError("Boatchar is null");
                    Debug.Break();
                }
                //if (boatChar.getCurrentLife() == null)
                //{
                //    Debug.LogError("Boatchar is null");
                //    Debug.Break();
                //}
                //if (boatChar.getMaxLife() == null)
                //{
                //    Debug.LogError("Boatchar is null");
                //    Debug.Break();
                //}
                if (boatChar.getCurrentLife() >= 0 && boatChar.getCurrentLife() <= boatChar.getMaxLife())
                {
                    //Debug.LogError("Life : " + boatChar.getCurrentLife());
                    //Debug.LogError("before : " + lifeBar.GetComponent<RectTransform>().sizeDelta);
                    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / boatChar.getMaxLife()) * boatChar.getCurrentLife(), lifeBar.GetComponent<RectTransform>().sizeDelta.y);
                    //Debug.LogError("after : " + lifeBar.GetComponent<RectTransform>().sizeDelta);
                    //Debug.Break();
                }
            }
            else
            {
                if (hp >= 0 && hp <= maxhp)
                    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / maxhp) * hp, lifeBar.GetComponent<RectTransform>().sizeDelta.y);

            }
        }


        void ActivateInterractionPlayer()
        {
            if(!interractCanBeDraw)
            {
                _playerInformations.SetActive(false);
                _interractionPlayer.SetActive(false);
            }
            else
            {
                _playerInformations.SetActive(true);
                _interractionPlayer.SetActive(true);

                //Le boutton quitter le groupe doit être désactiver si on ne fait pas partie d'un groupe, il faut donc vérifier si on appartient à un groupe
                //if(_player appartient à un groupe) //On active l'interraction du button
                //{
                //    _buttonQuitterGroupe.GetComponent<Button>().interactable = true;
                //}
                //else //Sinon on la desactive
                //{
                //    _buttonQuitterGroupe.GetComponent<Button>().interactable = false;
                //}
            }
        }

        void ActivateInterractionOtherPlayer()
        {
            if (!interractOtherPlayerCanBeDraw)
            {
                _otherPlayerInformations.SetActive(false);
                _interractionOtherPlayer.SetActive(false);
            }
            else
            {
                //Debug.LogError("Vous Avez cliqué sur un autre joueur");
               
                _interractionOtherPlayer.SetActive(true);
                _interractionOtherPlayer.GetComponent<InterractionJoueur>().SetPlayerToKnow(_otherPlayer);
            }
        }

        public void QuitGroupAppear()
        {
            _confirmQuitGroup.SetActive(true);
        }

        public void YesQuitGroup()
        {
            //Mettre la fonction pour quitter le groupe
            Debug.LogError("You said you want to quit the group");
        }

        public void NoQuitGroup()
        {
            _confirmQuitGroup.SetActive(false);
        }

        public void ActivateOtherPlayerInformation()
        {
            _otherPlayerInformations.SetActive(true);
        }

        public void InviteToGroup()
        {
            //fonction pour inviter quelqu'un dans un groupe
        }

        public void FollowSomeOne()
        {
            //Fonction pour suivre quelqu'un
            //l'objet _otherPlayer designe le joueur qu'on sélectionne
        }

        /// <summary>
        /// Cette Fonction sert à vérifier si le joueur est sur un dock
        /// Si oui, elle active les canvas de l'interface de l ile et envoie sur quel ile le joueur est docké
        /// </summary>
        public void CheckIfPlayerDocked()
        {
            //Remplacer par l'accesseur afin de savoir si le joueur est dans un dock

            //if(Player.isDocked == true)
            //{
            //    if(!_interactionIle.activeSelf)
            //     {
            //      _interactionIle.SetActive(true);
            //On set le dock sur lequel le player est avec  _interactionIle.GetComponent<InterfaceIle>().SetDataDock(player.Dock);
            //      }
            //}
            //else
            //{
            //    if (_interactionIle.activeSelf)
            //        _interactionIle.SetActive(false);
            //}
        }

        /// <summary>
        /// Appelé sur le onClick d'un boutton
        /// le boutton envoie un int entre 0 et 3, il servira à parcourir le tableau du groupe afin de récupérer les informations de ce joueur
        /// </summary>
        /// <param name="whichMember"></param>
        public void ClickOnGroupIcon(int whichMember)
        {
            //_otherPlayer = parcours tableau groupe joueur avec l ID whichMember-- > tableauGroupe[whichMember];

            ActivateOtherPlayerInformation();
        }
    }
}
