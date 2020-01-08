using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjetPirate.UI
{
    public class RespawnUI : MonoBehaviour
    {


        [Header("Text")]
        public Text _numberCanonValue;
        public Text _numberCanonProueValue;
        public Text _numberLvLValue;
        public Text _boatValourValue;
        public Text _playerGoldValue;

        //Valeurs à set lors de la réaparition, elles correspondent aux composants du bateau lors de sa mort
        int boatLvLBeforeDeath = 3;
        int numberCanonBeforeDeath = 12;
        int numberCanonProueBeforeDeath = 1;
        int bateauCostBeforeDeath;

        //currentValue
        int currentLvL;
        int currentNumberCanon;
        int currentCanonProue;
        int currentBoatCost;


        //cost
        int canonCost = 100;
        int canonProueCost = 500;
        int lvlCost = 2000;

        //Min Value
        int minNumberCanon = 2;
        int minNumberCanonProue = 0;
        int minLvl = 1;

        //Max Value
        int maxCanon = 12;
        int maxLvL = 3;


        //Player
        private GameObject _player;

        // Use this for initialization


        private void OnEnable()
        {
            //On Récupère les value grace aux accesseur du boat Player
            int hasHarpoon = 1;
            if (_player.GetComponentInChildren<Boat.BoatCharacter>()._prowCannonHarpoon == null)
            {
                hasHarpoon = 0;
            }
            SetBoatValueBeforeDeath(_player.GetComponentInChildren<Boat.BoatCharacter>().larboardCannons.Count + _player.GetComponentInChildren<Boat.BoatCharacter>().starboardCannons.Count, hasHarpoon, _player.GetComponent<Player>().ShipLevel);


            UpdateGoldPlayerValue();
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void SetPlayer(GameObject pPlayer)
        {
            _player = pPlayer;
            int hasHarpoon = 1;
            if (pPlayer.GetComponentInChildren<Boat.BoatCharacter>()._prowCannonHarpoon == null)
            {
                hasHarpoon = 0;
            }
            SetBoatValueBeforeDeath(_player.GetComponentInChildren<Boat.BoatCharacter>().larboardCannons.Count + _player.GetComponentInChildren<Boat.BoatCharacter>().starboardCannons.Count, hasHarpoon, _player.GetComponent<Player>().ShipLevel);
            UpdateGoldPlayerValue();
        }

        void SetBoatValueBeforeDeath(int _numberCanon, int _numberCanonProue, int _lvl)
        {
            numberCanonBeforeDeath = _numberCanon;
            numberCanonProueBeforeDeath = _numberCanonProue;
            boatLvLBeforeDeath = _lvl;

            //Set CurrentValue
            currentNumberCanon = numberCanonBeforeDeath;
            currentCanonProue = numberCanonProueBeforeDeath;
            currentLvL = boatLvLBeforeDeath;

            //Set Boat Cost
            CalculatingBoatValue();

        }

        void CalculatingBoatValue()
        {
            currentBoatCost = (currentNumberCanon * canonCost) + (currentCanonProue * canonProueCost) + (currentLvL * lvlCost);

            if (currentNumberCanon == minNumberCanon && currentCanonProue == minNumberCanonProue && currentLvL == minLvl)
                currentBoatCost = 0;

            SetTextValue();
        }

        void SetTextValue()
        {
            _numberCanonValue.text = currentNumberCanon.ToString();
            _numberCanonProueValue.text = currentCanonProue.ToString();
            _numberLvLValue.text = currentLvL.ToString();
            _boatValourValue.text = currentBoatCost.ToString() + " Gold";
        }

        public void AddCanon()
        {
            if(currentNumberCanon < numberCanonBeforeDeath && (currentNumberCanon < ((maxCanon / maxLvL) * (currentLvL))))
            {
                //on ajoute un canon
                currentNumberCanon += 1;
                //On actualise Le Coût
                CalculatingBoatValue();
            }
        }

        public void RemoveCanon()
        {
            if(currentNumberCanon > minNumberCanon && (currentNumberCanon > ((maxCanon / maxLvL) * (currentLvL - 1))))
            {
                //on retire un canon
                currentNumberCanon -= 1;
                //On actualise Le Coût
                CalculatingBoatValue();
            }
        }

        public void AddCanonProue()
        {
            if(currentCanonProue < numberCanonProueBeforeDeath && currentLvL > minLvl)
            {
                //on ajoute un canon Proue
                currentCanonProue += 1;
                //On actualise Le Coût
                CalculatingBoatValue();
            }
        }

        public void RemoveCanonProue()
        {
            if(currentCanonProue > minNumberCanonProue)
            {
                //on retire un canon Proue
                currentCanonProue -= 1;
                //On actualise Le Coût
                CalculatingBoatValue();
            }
        } 
        
        public void AddLvL()
        {
            if(currentLvL < boatLvLBeforeDeath)
            {
                //on ajoute un LvL
                currentLvL += 1;

                //On ajuste les plafond de valeurs (il ya un nbre max de canon par lvl)
                if (currentLvL == 2)
                {
                    if (currentNumberCanon < 4)
                    {
                        currentNumberCanon = 4;
                    }
                }

                if (currentLvL == 3)
                {
                    if (currentNumberCanon < 6)
                    {
                        currentNumberCanon = 6;
                    }
                }

                //On actualise Le Coût
                CalculatingBoatValue();
            }
        }

        public void RemoveLvL()
        {
            if(currentLvL > minLvl)
            {
                //on retire un LvL
                currentLvL -= 1;
                
                //On ajuste les plafond de valeurs (il ya un nbre max de canon par lvl)
                if(currentLvL == 2)
                {
                    if(currentNumberCanon > ((maxCanon / maxLvL) * (currentLvL)))
                    {
                        currentNumberCanon = 8;
                    }
                }

                if(currentLvL == 1)
                {
                    if (currentNumberCanon > ((maxCanon / maxLvL) * (currentLvL)))
                    {
                        currentNumberCanon = 4;
                    }

                    if(currentCanonProue == 1)
                    {
                        currentCanonProue = 0;
                    }
                }

                //On actualise Le Coût
                CalculatingBoatValue();
            }
        }

        public void ValidateBoatPurchase()
        {
            //On appel les fonction pour set les compasant du bateau grâce aux valeurs de current
            if (currentBoatCost < _player.GetComponent<Player>()._data.Ressource.Golds)
            {
                _player.GetComponentInChildren<Boat.BoatCharacter>()._respawningAnimationIsPlaying = true;
            }


            //On désactive l'interface
            this.gameObject.SetActive(false);
            _player.GetComponentInChildren<Boat.BoatCharacter>()._respawnUI = false;

        }

        void UpdateGoldPlayerValue()
        {
            _playerGoldValue.text = _player.GetComponent<Player>()._data.Ressource.Golds.ToString();
        }
    }
}
