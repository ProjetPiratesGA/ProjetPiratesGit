using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_Transform
    {
        private myVector3 position = new myVector3();
        private myVector3 rotation = new myVector3();
        private myVector3 scale = new myVector3();

        public myVector3 Rotation { get { return rotation; } set { rotation = value; } }
        public myVector3 Scale { get { return scale; } set { scale = value; } }
        public myVector3 Position { get { return position; } set { position = value; } }
    }
}