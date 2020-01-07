using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.IA
{
    public class Kraken_Controller : IA_Controller
    {
        public Kraken_Character Character
        {
            get { return (Kraken_Character)_character; }
        }
        // Use this for initialization
        void Start()
        {
            _character = this.GetComponent<Kraken_Character>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isOnAlert)
            {
                Character.MoveHead(false);
            }
            else
            {
                Character.MoveHead(true);
            }
        }
    }
}