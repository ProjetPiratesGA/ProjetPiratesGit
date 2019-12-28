using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ProjetPirate.Boat;

namespace ProjetPirate.UI.HUD
{
    public class HUD_Script : MonoBehaviour
    {
        [Header("TEXT")]
        public Text _goldValueText;
        public Text _XpValueText;
        public Text _NumberWoodenBoardValue;

        [Header("Canvas")]
        public GameObject _menuInGameCanvas;
        public GameObject _interractionPlayer;

        [Header("GameObject")]
        public GameObject goldSprite;
        public GameObject xpSprite;
        public GameObject woodenBoard;
        public GameObject lifeBar;


        [Header("Var Test à enlever et remplacer par des accesseur")]
        public int hp = 50;
        public int maxhp = 50;

        private GameObject _player;//On récupère le player afin que le controller possede ses info 

        private float maxLifeBarSize;
        //Gestion des interractions avec les joueurs
        bool interractCanBeDraw = false;
        private GameObject _playerToFollow;


        // Use this for initialization
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            //_NumberWoodenBoardValue.text = ;

            UpdateGoldValue();
            UpdateXpValue();
            if(_interractionPlayer != null)
            {
                _interractionPlayer.SetActive(false);
            }

            maxLifeBarSize = lifeBar.GetComponent<RectTransform>().sizeDelta.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
            }

            UpdateGoldValue();
            UpdateXpValue();
            UpdatePlankValue();

            if (_interractionPlayer != null)
            {
                CheckClickOnBoat();
            }

            UpdateLifeBar();
        }

        void UpdateGoldValue()
        {
            _goldValueText.text = _player.GetComponent<Player>()._currentMoney.ToString();
            // _goldValueText.text = accesseur gold player;
        }

        void UpdateXpValue()
        {
            //_XpValueText.text = "2000";
            _XpValueText.text = _player.GetComponent<Player>()._currentXp.ToString();
        }

        void UpdatePlankValue()
        {
            //_XpValueText.text = "2000";
            _NumberWoodenBoardValue.text = _player.GetComponent<Player>()._currentPlank.ToString();
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
            Ray ray;
#if UNITY_EDITOR
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#elif UNITY_ANDROID
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
#if UNITY_EDITOR
                    if (Input.GetMouseButtonDown(0))
                    {
                        _interractionPlayer.SetActive(true);
                        _playerToFollow = hit.collider.gameObject;
                        _interractionPlayer.GetComponent<InterractionJoueur>().SetPlayerToFollow(_playerToFollow);
                        interractCanBeDraw = true;

                    }
#elif UNITY_ANDROID
                    
                _interractionPlayer.SetActive(true);
                _playerToFollow = hit.collider.gameObject;
                _interractionPlayer.GetComponent<InterractionJoueur>().SetPlayerToFollow(_playerToFollow);
                interractCanBeDraw = true;
               
#endif
                }
                else if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.layer != 5)
                {

#if UNITY_EDITOR
                    if (Input.GetMouseButtonDown(0))
                    {
                        _interractionPlayer.SetActive(false);
                        interractCanBeDraw = false;

                    }
#elif UNITY_ANDROID
                _interractionPlayer.SetActive(false);
                interractCanBeDraw = false;
                
#endif
                }
            }



        }

        public void UpdateLifeBar()
        {
            if(_player.GetComponentInChildren<BoatCharacter>().getCurrentLife() >= 0 && _player.GetComponentInChildren<BoatCharacter>().getCurrentLife() <= _player.GetComponentInChildren<BoatCharacter>().getMaxLife())
            lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2((maxLifeBarSize/ _player.GetComponentInChildren<BoatCharacter>().getMaxLife()) * _player.GetComponentInChildren<BoatCharacter>().getCurrentLife(), lifeBar.GetComponent<RectTransform>().sizeDelta.y);
        }
    }
}
