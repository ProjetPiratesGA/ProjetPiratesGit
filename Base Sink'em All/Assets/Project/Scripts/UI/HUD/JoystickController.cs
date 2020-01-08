using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjetPirate.Controllers
{

    public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //Get The joystick --> this script is on the background of the joystick
        public GameObject _joystick;

        //If the joycon is on the deadZone, it returns 0 
        public float _joystickDeadZone = 0.2f;

        //Position of the joycon related to the background
        Vector2 _positionJoystick;

        //Bool to know if we hold the joycon or not
        bool isDragging = false;

        Vector2 _screenPoint;

        //Radius of the joycon's background (this)
        float _radius;     

        private void Start()
        {
            _radius = this.GetComponent<RectTransform>().sizeDelta.x / 2;
        }

        private void Update()
        {
            if (isDragging)
            {

#if UNITY_ANDROID
                _screenPoint = Input.GetTouch(0).position;
#else
                _screenPoint = Input.mousePosition;
#endif
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), _screenPoint, null, out _positionJoystick))
                {
                    if (_positionJoystick.magnitude > _radius)
                    {
                        _positionJoystick /= _positionJoystick.magnitude;
                        _positionJoystick *= _radius;
                    }

                    _joystick.GetComponent<RectTransform>().anchoredPosition = _positionJoystick;
                }
            }
        }

        /// <summary>
        /// If we "click" on the screen, we check the joycon's position on the screen
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(), eventData.position, null, out _positionJoystick))
            {
                _joystick.GetComponent<RectTransform>().anchoredPosition = _positionJoystick;
                isDragging = true;
            }
        }

        /// <summary>
        /// Reset the position when we stop dragging the joycon
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            _positionJoystick = Vector2.zero;
            isDragging = false;
        }


        /// <summary>
        /// Return the normalize value of the joystick position and apply the deadZone
        /// </summary>
        /// <returns></returns>
        public Vector2 GetNormalizeJoystickPosition()
        {
            Vector2 tmpVector = Vector2.zero;

            tmpVector.x = _positionJoystick.x / _radius;
            tmpVector.y = _positionJoystick.y / _radius;
            //Debug.Log("TMP Vector Normalized = " + tmpVector);
            if ((tmpVector.x > -_joystickDeadZone) && (tmpVector.x < _joystickDeadZone) && ((tmpVector.y > -_joystickDeadZone) && (tmpVector.y < _joystickDeadZone)))
            {
                tmpVector.x = 0;
            }

            if ((tmpVector.y > -_joystickDeadZone) && (tmpVector.y < _joystickDeadZone) && ((tmpVector.x > -_joystickDeadZone) && (tmpVector.x < _joystickDeadZone)))
            {
                tmpVector.y = 0;
            }
            return tmpVector;
        }      

    }
}
