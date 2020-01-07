using System.Collections.Generic;
using UnityEngine;


namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Boat
    {
        private int boat_ID = -1;

        public int Boat_ID
        {
            get { return boat_ID; }
            set { boat_ID = value; }
        }

        public Data_Boat(int _clientID = -1)
        {
            boat_ID = _clientID;
        }

        private int maxCanonPerSide = 2;
        private int currentCanonLeft = 1;
        private int currentCanonRight = 1;

        public int MaxCanonPerSide { get { return maxCanonPerSide; } set { maxCanonPerSide = value; } }
        public int CurrentCanonLeft { get { return currentCanonLeft; } set { currentCanonLeft = value; } }
        public int CurrentCanonRight { get { return currentCanonRight; } set { currentCanonRight = value; } }

        Data_Transform transform = new Data_Transform();
        Data_StatsCharacters stats = new Data_StatsCharacters();
        Data_Sail sail = new Data_Sail();
        List<Data_Canon> canon = new List<Data_Canon>();

        private myVector3 colorBoat = new myVector3();

        public Data_Transform Transform { get { return transform; } set { transform = value; } }
        public Data_StatsCharacters Stats { get { return stats; } set { stats = value; } }
        public Data_Sail Sail { get { return sail; } set { sail = value; } }
        public List<Data_Canon> Canon { get { return canon; } set { canon = value; } }

        public myVector3 dColorBoat { get { return colorBoat; } set { colorBoat = value; } }

        private myVector3 _initPositionVector = new myVector3(0, 0, 0);


        public Data_Boat(GameObject boat)
        {
            _initPositionVector.x = boat.transform.position.x;
            _initPositionVector.y = boat.transform.position.y;
            _initPositionVector.z = boat.transform.position.z;

            transform.Position = _initPositionVector;
        }

        public void InitTransform(GameObject boat)
        {
            _initPositionVector.x = boat.transform.position.x;
            _initPositionVector.y = boat.transform.position.y;
            _initPositionVector.z = boat.transform.position.z;

            transform.Position = _initPositionVector;
        }

        public void LoadTransform(GameObject boat)
        {
            boat.transform.position = new Vector3(transform.Position.x, transform.Position.y, transform.Position.z);
        }

        public void UpdateTransform(GameObject boat)
        {
            _initPositionVector.x = boat.transform.position.x;
            _initPositionVector.y = boat.transform.position.y;
            _initPositionVector.z = boat.transform.position.z;

            transform.Position = _initPositionVector;
        }
    }
}