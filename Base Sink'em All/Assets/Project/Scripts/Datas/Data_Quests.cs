using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Quests
    {
        private int type = -1;
        private int id = -1;
        private int itemCount = 0;
        private int itemCountNeeded = -1;
        private bool isAccepted = false;
        private string textQuest = null;
        private string itemNecessary = null;
        private Vector2 vReward = new Vector2(0, 0);

        public int Type { get { return type; } set { type = value; } }
        public int ID { get { return id; } set { id = value; } }
        public string ItemNecessary { get { return itemNecessary; } set { itemNecessary = value; } }
        public int ItemCount { get { return itemCount; } set { itemCount = value; } }
        public int ItemCountNeeded { get { return itemCountNeeded; } set { itemCountNeeded = value; } }
        public bool IsAccepted { get { return isAccepted; } set { isAccepted = value; } }
        public string TextQuest { get { return textQuest; } set { textQuest = value; } }

        public Vector2 Reward
        {
            get { return vReward; }
        }

        public Data_Quests(int _typeOfQuest, int _objectifCount, Vector2 _reward, string _dialogue)
        {
            type = _typeOfQuest;
            id = _typeOfQuest;
            itemCountNeeded = _objectifCount;
            vReward = _reward;
            textQuest = _dialogue;
        }

    }
} 