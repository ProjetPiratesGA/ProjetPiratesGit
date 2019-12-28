using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace ProjetPirate.Boat
{
    public class BoatController : NetworkBehaviour
    {
        private GameObject _joystick;
        private GameObject _buttonShootUp;
        private GameObject _buttonShootLeft;
        private GameObject _buttonShootRight;


        private Player _player = null;



        public Player player
        {
            get { return _player; }
            set { _player = value; }

        }

        // Use this for initialization
        void Start()
        {
            if (hasAuthority)
            {
                InitLocalBoat();
            }
            DontDestroyOnLoad(this.gameObject);
        }

        public void InitLocalBoat()
        {
            _joystick = GameObject.Find("FondJoystick");
            _buttonShootUp = GameObject.Find("ButtonShootUp");
            _buttonShootLeft = GameObject.Find("ButtonShootLeft");
            _buttonShootRight = GameObject.Find("ButtonShootRight");

            //Camera.main.transform.position = posCam.position;
            //Camera.main.transform.rotation = posCam.rotation;
            Camera.main.transform.SetParent(this.transform);
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasAuthority)
            {
                return;
            }

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            transform.position += vertical * this.transform.forward * 10f * Time.deltaTime;
            transform.Rotate(0, horizontal, 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdShoot();
            }
        }

        [Command]
        private void CmdShoot()
        {
            //for(int i = 0; i < listposShoot.Count; i++)
            //{
            //    GameObject obj = Instantiate(prefabBulletCanon, listposShoot[i].position, listposShoot[i].rotation);
            //    obj.transform.SetParent(parentBullet);
            //    NetworkServer.Spawn(obj);  
            //}
        }
    }
}
