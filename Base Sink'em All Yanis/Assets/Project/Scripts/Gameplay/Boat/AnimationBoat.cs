using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjetPirate.Data;

namespace ProjetPirate.Boat
{
    public class AnimationBoat : MonoBehaviour
    {        

        /// <summary>
        /// envoyer le game Object ou est le script boatMovement
        /// </summary>
        private BoatCharacter _boatCharacter;

        [SerializeField]
        private Transform _transformParent; // must be set with the parent object
        private Transform _transformMesh;

        // Use this for initialization
        void Start()
        {
            //REFERENCES (ENFANT)
            if (this.GetComponentInParent<BoatCharacter>() != null)
            {
                _boatCharacter = this.GetComponentInParent<BoatCharacter>();
            }
            else
            {
                Debug.LogError("AnimationBoat --> Start / there is no BoatCharacter in parent");
            }
            if (_transformParent == null)
            {
                Debug.LogError("AnimationBoat --> Start / transformParent est null");
            }

            _transformMesh = this.GetComponent<Transform>();

        }

        // Update is called once per frame
        void Update()
        {
                //A COMMENTEZ EN PARENT
                Vector3 _rotationeulerToApply = new Vector3(
                    _transformMesh.eulerAngles.x,
                    _transformParent.eulerAngles.y,
                    _transformMesh.eulerAngles.z);

                _transformMesh.eulerAngles = _rotationeulerToApply;

                //_boatCharacter.Data_Boat.ChildTransform.Rotation = new myVector3(_rotationeulerToApply.x, _rotationeulerToApply.y, _rotationeulerToApply.z);
        }

    }
}
