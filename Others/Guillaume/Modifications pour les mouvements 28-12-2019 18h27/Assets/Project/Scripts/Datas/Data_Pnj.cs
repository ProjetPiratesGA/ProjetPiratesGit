using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Pnj
    {
        private bool haveQuest = false;
        private int questId = -1;
        Data_Quests quest;
        Data_Store store = new Data_Store();
        string dialogue = null;
        string dialogueQuete = null;

        public bool HaveQuest { get { return haveQuest; } set { haveQuest = value; } }
        public int QuestId { get { return questId; } set { questId = value; } }
        public string Dialogue { get { return dialogue; } set { dialogue = value; } }
        public string DialogueQuete { get { return dialogueQuete; } set { dialogueQuete = value; } }
        public Data_Quests Quete { get { return quest; } set { quest = value; } }

    }
}