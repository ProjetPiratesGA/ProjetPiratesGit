using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjetPirate.IA
{
    public class Kraken_Tentacle : IA_Controller
{

        bool _isDamaged = false;
        bool _isAttacking = false;
        bool _finishedAttacking = false;
        int _damage;

        float _currentRotationTime = 0;
        float _totalRotationTime = 1;
        Vector3 _startRotation = new Vector3(0, 0, 0);
        Vector3 _endRotation = new Vector3(90, 0, 0);
        Vector3 TargetPosition;

        [SerializeField] private float Fow = 10f;

        private Rigidbody MyRigidbody;

        private float LastAnimStart = 0;

        public bool IsDamaged
        {
            get { return _isDamaged; }
        }

        public new int Damage
        {
            get { return _damage; }
        }
        //Use this for initialization

       void Start()
        {
            TargetPosition = transform.forward;
            MyRigidbody = this.GetComponent<Rigidbody>();
            LastAnimStart = -3.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Attack();
            }
            if (_isAttacking)
            {
                //_currentRotationTime += Time.deltaTime / _totalRotationTime;
                //this.transform.eulerAngles = Vector3.Lerp(_startRotation, _endRotation, _currentRotationTime);
                //if (_currentRotationTime > 1)
                //{
                //    _currentRotationTime = 0;
                //    _isAttacking = false;
                //    _finishedAttacking = true;
                //}

                if (transform.localEulerAngles.x < 87)
                {
                    Quaternion quat = Quaternion.AngleAxis(90, transform.right);

                    float StockedAngle;
                    Vector3 StockedAxis;
                    quat.ToAngleAxis(out StockedAngle, out StockedAxis);
                    MyRigidbody.angularVelocity = StockedAxis * StockedAngle * Mathf.Deg2Rad;
                }
                else
                {
                    _finishedAttacking = true;
                    _isAttacking = false;
                    MyRigidbody.angularVelocity = Vector3.zero;
                }

            }
            else if (_finishedAttacking)
            {
                if (GetComponentInParent<Animator>().GetBool("isattacking") == true)
                    this.GetComponentInParent<Animator>().SetBool("isattacking", false);
                //_currentRotationTime += Time.deltaTime / _totalRotationTime;
                //this.transform.eulerAngles = Vector3.Lerp(_endRotation, _startRotation, _currentRotationTime);
                //if (_currentRotationTime > 1)
                //{
                //    _currentRotationTime = 0;
                //    _finishedAttacking = false;
                //}

                if (transform.localEulerAngles.x > 3)
                {
                    Quaternion quat = Quaternion.AngleAxis(-90, transform.right);

                    float StockedAngle;
                    Vector3 StockedAxis;
                    quat.ToAngleAxis(out StockedAngle, out StockedAxis);
                    MyRigidbody.angularVelocity = StockedAxis * StockedAngle * Mathf.Deg2Rad;
                }
                else
                    MyRigidbody.angularVelocity = Vector3.zero;
            }
            else
            {
                FaceTheTarget();
            }
        }

        public void ReceiveDamage(int pDamage)
        {
            _isDamaged = true;
            _damage = pDamage;
        }

        public void DisableDamage()
        {
            _isDamaged = false;
            _damage = 0;
        }

        public void Attack()
        {
            transform.localPosition = new Vector3(0, 20, 0);
            if (GetComponentInParent<Animator>().GetBool("isattacking") == false && Time.time - LastAnimStart >= 3.5)
            {
                this.GetComponentInParent<Animator>().SetBool("isattacking", true);
                LastAnimStart = Time.time;
            }

            _isAttacking = true;
        }

        public void FaceTheTarget()
        {
            Vector3 direction = (TargetPosition - transform.position).normalized;
            Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = LookRotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision");
            if (_isAttacking & other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>() != null)
            {
                Debug.Log("Damage");
                other.gameObject.GetComponent<ProjetPirate.Boat.BoatCharacter>().Damage(1);
            }
        }
    }
}