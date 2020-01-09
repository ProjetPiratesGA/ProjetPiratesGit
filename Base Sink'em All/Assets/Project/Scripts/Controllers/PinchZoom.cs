using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjetPirate.Controllers
{
    public class PinchZoom : MonoBehaviour
    {
        [TextArea]
        public string Instruction = "Ce script doit être attaché à la main caméra";

        public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
        public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
        [Space]
        [Range(0.1f, 189.9f)]
        [Tooltip("Cast minimum du zoom --> le zoom est effectué entre min et max")]
        public float minValueCamPerspective = 30f;
        [Range(0.1f, 189.9f)]
        [Tooltip("Cast maximum du zoom --> le zoom est effectué entre min et max")]
        public float maxValueCamPerspective = 90f;

        public float distanceMaxBeetwenTwoPoint = 200;

        bool zoomCanBeDone = false;


        void Start()
        {
            //Gestion si le min est supérieur au max
            if(minValueCamPerspective > maxValueCamPerspective)
            {
                float tmpvar = minValueCamPerspective;
                minValueCamPerspective = maxValueCamPerspective;
                maxValueCamPerspective = minValueCamPerspective;
            }
        }

        void Update()
        {
            // Si il ya deux input sur l'écran
            if (Input.touchCount == 2)
            {
                Debug.Log(zoomCanBeDone);
                // on récupère les deux input
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                if (zoomCanBeDone)
                {              
                        // On récupère la position de chaque touches à la frame précédente.
                        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                        // On récupère la magnitude de la distance entre les touches entre chaque frame.
                        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        // Différence entre les distances entre chaques frames.
                        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


                        // Si la caméra est orthographique
                        if (Camera.main.orthographic)
                        {
                            // On change la taille de la camera par rapport à la distances entre les inputs.
                            Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                            // Faire en sorte que la taille ne passe jamais en dessous de 0.
                            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
                        }
                        else
                        {
                            // Sinon on change le fieldOfView par rapport à la distance entre les deux inputs.
                            Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                            // Faire en sortie que le FieldOfView reste entre 0 et 180.
                            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minValueCamPerspective, maxValueCamPerspective);
                        }
                    
                }
                else
                {

                    if (!EventSystem.current.currentSelectedGameObject)
                    {
                        zoomCanBeDone = true;
                    }

                }
            }
            else
            {
                zoomCanBeDone = false;
            }
        }
    }
}
