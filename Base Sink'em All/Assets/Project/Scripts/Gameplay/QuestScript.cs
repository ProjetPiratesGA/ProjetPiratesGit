using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Gameplay
{
    public class QuestScript : MonoBehaviour
    {

        private string strDialogue = "";
        private int iObjectif = 0;
        // x = gold y = reputation
        private Vector2 vReward = new Vector2(0, 0);
        private int typeOfQuest = 0;

        public bool isQuestAvailable = true;


        
        bool questHasBeenCreated = false;
        // in seconds
        float fCooldownRespawmQuest = 30;
        float fCurrentTime = 0;
        public string Dialogue
        {
            get { return strDialogue; }
        }
        public int Objectif
        {
            get { return iObjectif; }
        }
        public Vector2 Reward
        {
            get { return vReward; }
        }
        public int TypeOfQuest
        {
            get { return typeOfQuest; }
        }

        public Data.Data_Quests GenerateQuest(float _reputation)
        {
            //reset var
            strDialogue = "";
            iObjectif = 0;
            vReward = new Vector2(0, 0);
            typeOfQuest = 0;

            //Set The quest type
            bool questCanBeCrate = false;
            int typeQuest = 0;


            while (!questCanBeCrate)
            {
                typeQuest = Random.Range(1, 7); //random between 1 and 6
                Debug.Log("typeQuest = " + typeQuest);
                questCanBeCrate = true;

                if (typeQuest == 2 && _reputation < 5000)
                    questCanBeCrate = false;

                if (typeQuest == 4 && _reputation < 25000)
                    questCanBeCrate = false;

                if (typeQuest == 5 && _reputation > 25000)
                    questCanBeCrate = false;
            }


            //set the quest Objectif
            switch (typeQuest)
            {
                case 1:
                    if (_reputation < 5000)
                        iObjectif = 2;

                    if (5000 <= _reputation && _reputation < 10000)
                        iObjectif = 5;

                    if (10000 <= _reputation && _reputation < 25000)
                        iObjectif = 10;

                    if (_reputation >= 25000)
                        iObjectif = 20;

                    break;
                case 2:
                    if (5000 <= _reputation && _reputation < 10000)
                        iObjectif = 2;

                    if (10000 <= _reputation && _reputation < 25000)
                        iObjectif = 5;

                    if (_reputation >= 25000)
                        iObjectif = 10;
                    break;
                case 3:
                    if (_reputation < 5000)
                        iObjectif = 4;

                    if (5000 <= _reputation && _reputation < 10000)
                        iObjectif = 8;

                    if (10000 <= _reputation && _reputation < 25000)
                        iObjectif = 20;

                    if (_reputation >= 25000)
                        iObjectif = 50;
                    break;
                case 4:
                    iObjectif = 1;
                    break;
                case 5:
                    if (_reputation < 5000)
                        iObjectif = 100;

                    if (5000 <= _reputation && _reputation < 10000)
                        iObjectif = 300;

                    if (10000 <= _reputation && _reputation < 25000)
                        iObjectif = 500;
                    break;
                case 6:
                    if (_reputation < 5000)
                        iObjectif = 1000;

                    if (5000 <= _reputation && _reputation < 10000)
                        iObjectif = 4000;

                    if (10000 <= _reputation && _reputation < 25000)
                        iObjectif = 10000;

                    if (_reputation >= 25000)
                        iObjectif = 20000;
                    break;
                default:
                    Debug.LogError("TypeQuestInvalid");
                    break;
            }

            //set the quest Dialogue
            switch (typeQuest)
            {
                case 1:
                    strDialogue = "Pille " + iObjectif + " bateaux marchands.";
                    break;
                case 2:
                    strDialogue = "Attaque " + iObjectif + " bateaux millitaires.";
                    break;
                case 3:
                    strDialogue = "Elimine " + iObjectif + " requins.";
                    break;
                case 4:
                    strDialogue = "Elimine le Kraken.";
                    break;
                case 5:
                    strDialogue = "Recupere " + iObjectif + " planches.";
                    break;
                case 6:
                    strDialogue = "Vole " + iObjectif + " pieces d'or.";
                    break;
                default:
                    Debug.LogError("TypeQuestInvalid");
                    break;
            }




            //set the quest Reward
            switch (typeQuest)
            {
                case 1:
                    vReward.x = iObjectif * 100;
                    vReward.y = iObjectif * 200;
                    break;
                case 2:
                    vReward.x = iObjectif * 200;
                    vReward.y = iObjectif * 400;
                    break;
                case 3:
                    vReward.x = iObjectif * 20;
                    vReward.y = iObjectif * 50;
                    break;
                case 4:
                    vReward.x = 10000;
                    vReward.y = 10000;
                    break;
                case 5:
                    vReward.x = iObjectif * 5;
                    vReward.y = iObjectif * 5;
                    break;
                case 6:
                    vReward.x = 0;
                    vReward.y = iObjectif;
                    break;
                default:
                    Debug.LogError("TypeQuestInvalid");
                    break;
            }

            //Quest Creation
            return new Data.Data_Quests(typeQuest, iObjectif, vReward, strDialogue);

        }

        public void QuestIsAccepted()
        {
            Debug.LogError("Quest Is Accepted (we re in the Quest Script");
            isQuestAvailable = false;

        }
        
        

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!isQuestAvailable)
            {
                fCurrentTime += Time.deltaTime;
                if(fCurrentTime > fCooldownRespawmQuest)
                {
                    fCurrentTime = 0;
                    isQuestAvailable = true;
                }
            }


        }
    }
}
