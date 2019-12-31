using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using ProjetPirate.Boat;

namespace ProjetPirate.UI.HUD
{
    public class HUD_Script : MonoBehaviour
    {
        [Header("TEXT")]
        public Text _goldValueText;
        public Text _XpValueText;
        public Text _NumberWoodenBoardValue;
        public Text _playerName;

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

        // Use this for initialization
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            //_NumberWoodenBoardValue.text = ;

            UpdateGoldValue();
            UpdateXpValue();
            if (_interractionPlayer != null)
            {
                _interractionPlayer.SetActive(false);
            }

            _playerInformations.SetActive(false);
            _otherPlayerInformations.SetActive(false);
            _confirmQuitGroup.SetActive(false);

            maxLifeBarSize = lifeBar.GetComponent<RectTransform>().sizeDelta.x;

        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
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

            UpdateLifeBar();

            CheckIfPlayerDocked();
        }

        void UpdateGoldValue()
        {
            if (_goldValueText != null)
            {
                if (_player != null)
                {
                    // _goldValueText.text = accesseur gold player;
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
                //if (_player != null)
                //{
                //    _XpValueText.text = _player.GetComponent<BoatCharacter>()._currentXp.ToString();
                //}
                //else
                //{
                //    _XpValueText.text = "Joueur NULL";

                //}
            }
        }

        void UpdatePlankValue()
        {
            if (_NumberWoodenBoardValue != null)
            {
                //if (_player != null)
                //{
                //    _NumberWoodenBoardValue.text = _player.GetComponent<BoatCharacter>()._currentPlank.ToString();
                //}
                //else
                //{
                //    _NumberWoodenBoardValue.text = "Joueur NULL";
                //}
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

        public void UtilisateWoodenStick()
        {
            //Fonction pour rendre des pv au joueurs
            //Actualisation du nombre de planches du joueurs
            //_NumberWoodenBoardValue.text = ;
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
                    Debug.Log(hit.collider.gameObject);
                    //On check si on est sur le player

                    if (hit.collider.gameObject == _player)
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
                    else if (hit.collider.gameObject != _player && !EventSystem.current.IsPointerOverGameObject() && hit.collider.gameObject.tag != "Enemy")
                    {

                        interractOtherPlayerCanBeDraw = false;
                        interractCanBeDraw = false;
                        ActivateInterractionOtherPlayer();
                        ActivateInterractionPlayer();
                    }
                }
                else
                {
                    Debug.Log("On ne touche rien");
                }
            }
#elif UNITY_ANDROID
                 Ray ray;
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                 RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.gameObject);
                    //On check si on est sur le player

                    if (hit.collider.gameObject == _player)
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
                    else if (hit.collider.gameObject != _player && !EventSystem.current.IsPointerOverGameObject() && hit.collider.gameObject.tag != "Enemy")
                    {

                        interractOtherPlayerCanBeDraw = false;
                        interractCanBeDraw = false;
                        ActivateInterractionOtherPlayer();
                        ActivateInterractionPlayer();
                    }
                }
                else
                {
                    Debug.Log("On ne touche rien");
                }
#endif

        }

        public void UpdateLifeBar()
        {
            //if (_player != null)
            //{
            //    if (_player.GetComponent<BoatCharacter>().getCurrentLife() >= 0 && _player.GetComponent<BoatCharacter>().getCurrentLife() <= _player.GetComponent<BoatCharacter>().getMaxLife())
            //        lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / _player.GetComponent<BoatCharacter>().getMaxLife()) * _player.GetComponent<BoatCharacter>().getCurrentLife(), lifeBar.GetComponent<RectTransform>().sizeDelta.y);
            //}
            //else
            //{
            //    if (hp >= 0 && hp <= maxhp)
            //        lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize / maxhp) * hp, lifeBar.GetComponent<RectTransform>().sizeDelta.y);

            //}
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
                Debug.LogError("Vous Avez cliqué sur un autre joueur");
               
                _interractionOtherPlayer.SetActive(true);
                _interractionOtherPlayer.GetComponent<InterractionJoueur>().SetPlayerToFollow(_otherPlayer);
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
    }
}
