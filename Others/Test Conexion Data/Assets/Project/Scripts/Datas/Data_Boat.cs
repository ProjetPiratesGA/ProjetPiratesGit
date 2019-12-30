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
            this.canon = new List<Data_Canon>();
            this.colorBoat = new myVector3(Color.red);
            Debug.Log("Boat constructor : " + this.canon.Count);
        }

        Data_Transform transform = new Data_Transform();
        Data_StatsCharacters stats = new Data_StatsCharacters();
        Data_Sail sail = new Data_Sail();
        List<Data_Canon> canon = new List<Data_Canon>();

        private myVector3 colorBoat = new myVector3();

        public Data_Transform dTransform { get { return transform; } set { transform = value; } }
        public Data_StatsCharacters dStats { get { return stats; } set { stats = value; } }
        public Data_Sail Sail { get { return sail; } set { sail = value; } }
        public List<Data_Canon> dCanon { get { return canon; } set { canon = value; } }

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

        public void ReloadTransform(GameObject boat)
        {
            boat.transform.position = new Vector3(transform.Position.x, transform.Position.y, transform.Position.z);
        }

        public void ReverseReloadTransform(GameObject boat)
        {
            _initPositionVector.x = boat.transform.position.x;
            _initPositionVector.y = boat.transform.position.y;
            _initPositionVector.z = boat.transform.position.z;

            transform.Position = _initPositionVector;
        }
    }
}