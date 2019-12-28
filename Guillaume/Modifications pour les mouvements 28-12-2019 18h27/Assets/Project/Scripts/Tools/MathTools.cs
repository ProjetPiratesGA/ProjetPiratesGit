using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Tools
{
    public class MathTools : MonoBehaviour
    {
        public static float invert(float pVariableToInvert)
        {
            return -pVariableToInvert;
        }
        public static int invert(int pVaribaleToInvert)
        {
            return -pVaribaleToInvert;
        }

        /// <summary>
        /// function create beacuse the angle use are between 0 & 360
        /// (0 to 180 --> Left, 180 to 360 --> right)
        /// return true if the angle is left or false if the angle is right
        /// </summary>
        /// <param name="pAngle"></param>
        /// <returns></returns>
        public static bool AngleIsLeftOrRight0(float pAngle)
        {
            if(pAngle >=0 && pAngle <= 360)
            {
                if (pAngle >= 0 && pAngle <= 180)
                {
                    return true;
                }
                else if (pAngle <= 360 && pAngle > 180)
                {
                    return false;
                }
            }
            else
            {
                Debug.LogError("MathTools --> AngleIsLeftOrRight0 / the sending variable cannot be use");
                return false;
            }
            Debug.LogError("MathTools --> AngleIsLeftOrRight0 / je veut pas mettre de return la mais je sais pas omment fair ça me gave");

            return false;
        }

        /// <summary>
        /// the trigonometric Sense (sens trigonométique/ antihoraire)
        /// </summary>
        /// <param name="pAngle"></param>
        /// <param name="_speedRotate"></param>
        public static float RotateTrigonometricSense(float pAngle, float _speedRotate)
        {
            Debug.Log("RotateTrigonometricSense / BEFORE pAngle : " + pAngle);
            pAngle += _speedRotate;
            Debug.Log("RotateTrigonometricSense / AFTER pAngle : " + pAngle);
            return pAngle;
        }

        /// <summary>
        /// the Clockwise sense (horaire)
        /// </summary>
        /// <param name="pAngle"></param>
        /// <param name="_speedRotate"></param>
        public static float RotateClockwiseSense(float pAngle, float _speedRotate)
        {
            Debug.Log("RotateClockwiseSense / BEFORE pAngle : " + pAngle);
            pAngle -= _speedRotate;
            Debug.Log("RotateClockwiseSense / AFTER pAngle : " + pAngle);
            return pAngle;
        }
    }
}
