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

        private BoatCharacter _boatCharacter = null;
        private Transform _cameraPivot = null;
        private Transform _cameraPosition = null;

        [SerializeField] private float _zoom = 2;
        [SerializeField] float _minZoom = 1f;
        [SerializeField] float _maxZoom = 4f;


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

            _boatCharacter = this.GetComponent<BoatCharacter>();
            _cameraPivot = CameraPivot.Instance.transform;
            _cameraPosition = CameraPosition.Instance.transform;
            Vector3 pos = _cameraPosition.localPosition;
            pos *= _zoom;
            Camera.main.transform.localPosition = pos;
        }

        // Update is called once per frame
        void Update()
        {
            if (!hasAuthority)
            {
                return;
            }
            Zoom(0);

            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                Zoom(0.01f);
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                Zoom(-0.01f);
            }

            _cameraPivot.position = this.transform.position;

        }

        public void Zoom(float pZoom)
        {
            _zoom += pZoom;
            if (_zoom < _minZoom)
            {
                _zoom = _minZoom;
            }
            if (_zoom > _maxZoom)
            {
                _zoom = _maxZoom;
            }
            Vector3 pos = _cameraPosition.localPosition;
            pos *= _zoom;
            Camera.main.transform.localPosition = pos;
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
