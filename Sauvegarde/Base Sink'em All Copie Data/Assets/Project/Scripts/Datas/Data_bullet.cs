using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Data
{
    [System.Serializable]
    public class Data_bullet
    {
        public Data_bullet(int _damage, int _id )
        {
            damage = _damage;
            id = _id;
        }

        private int damage = 1;
        private int id = 1;

        public  int Damage { get { return damage; } set { damage = value; } }
        /* get the id of the bulklet, it is supposed to have the same as the canon that shot it */
        public  int ID { get { return id; } set { id = value; } }

    }

}