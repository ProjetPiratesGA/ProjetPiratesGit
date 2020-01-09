using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    public class Data_flag
    {
        private myVector3 color = new myVector3();
        public myVector3 CoolorFlag { get { return color; }set { color = value; } }

        public void SetColor(Color _color)
        {
            color.x = _color.r;
            color.y = _color.g;
            color.z = _color.b;
        }
    }
}