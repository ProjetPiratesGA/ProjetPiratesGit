using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace ProjetPirate.Boat
{

    //Guillaume 30-12-2019 11h37 --> Automatiquement le BoatController ajoute un BoatCharecter car les fonctions de déplacements sont dans BoatCharacter
    [RequireComponent(typeof(BoatCharacter))]
    public class BoatController : NetworkBehaviour
    {
        //Guillaume 30-12-2019 11h15 --> il ne me faut pas le gameObject mais le script/composant : PlayerController
        //private GameObject _joystick;
        private ProjetPirate.Controllers.PlayerController _playerController;
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

        //utiliser pour définir si le player bouge ou pas
        private float _verticalInput;
        private float _horizontalInput;


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
            //Guillaume 30-12-2019 11h15 --> il ne me faut pas le gameObject mais le script/composant : PlayerController
            //_joystick = GameObject.Find("FondJoystick");
            //_playerController = GameObject.Find("FondJoystick").GetComponent<ProjetPirate.Controllers.PlayerController>();
            _playerController = FindObjectOfType<ProjetPirate.Controllers.PlayerController>();
            if(_playerController == null)
            {
                Debug.LogError(this.name + " --> InitLocalBoat / _playerController est null");
            }
            _boatCharacter = this.GetComponent<BoatCharacter>();
            if(_boatCharacter == null)
            {
                Debug.LogError(this.name + " --> InitLocalBoat / _boatCharacter est null");
            }


            _buttonShootUp = GameObject.Find("ButtonShootUp");
            _buttonShootLeft = GameObject.Find("ButtonShootLeft");
            _buttonShootRight = GameObject.Find("ButtonShootRight");


            this.gameObject.GetComponent<BoatCharacter>().CmdSetUpBoat(_player.gameObject);

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

            //cette fonction doit être appelée constamment dans l'update
            this.PerformMovement();

            if (_verticalInput != 0 || _horizontalInput != 0)
            {
                _boatCharacter.setControllerIsMoving(true);
            }
            else
            {
                _boatCharacter.setControllerIsMoving(false);
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

        private void PerformMovement()
        {
            //TEST AVEC ZQSD
            //_verticalInput = Input.GetAxis("Vertical");
            //_horizontalInput = Input.GetAxis("Horizontal");
            //Debug.Log("AXIS VERTICAL : " + Input.GetAxis("Vertical") + " AXIS HORIZONTAL : " + Input.GetAxis("Horizontal"));
            //_boatCharacter.PerformMovement(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

            _verticalInput = _playerController.GetJoystickInput().y;
            _horizontalInput = _playerController.GetJoystickInput().x;
            Debug.Log("_verticalInput : " + _playerController.GetJoystickInput().y + " _horizontalInput : " + _playerController.GetJoystickInput().x);
            _boatCharacter.PerformMovement(_playerController.GetJoystickInput().y, _playerController.GetJoystickInput().x);
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
