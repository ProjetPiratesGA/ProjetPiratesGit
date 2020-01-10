using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


namespace ProjetPirate.UI.Menu
{
    public class Setting : MonoBehaviour
    {

        [Header("Setting Component")]
        [Tooltip("On récupère le dropDown de la qualité graphique afin de récupérer la <<Value>>")]
        public GameObject dropDownQualitySetting;
        [Tooltip("On récupère le dropDown de l'antialiasing afin de récupérer la <<Value>>")]
        public GameObject dropDownAntialiasingSetting;

        [Header("Change Butoton")]
        public GameObject sound;
        public GameObject graphisme;
        public GameObject compte;

        [Header("AudioMixer")]
        public AudioMixer _MusicMixer;
        public AudioMixer _FXMixer;


        int _qualityValue;
        int _antialiasingValue;

        float _volumeMusic = 0;
        float _volumeFX = 0;

        bool _desactivateMusic = false;
        bool _desactivateFX = false;
        // Use this for initialization
        void Start()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);


            _MusicMixer.SetFloat("VolumeMusic", _volumeMusic);
            _FXMixer.SetFloat("VolumeFX", _volumeFX);

        }

        private void OnEnable()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (graphisme.activeSelf)
            {
                _qualityValue = dropDownQualitySetting.GetComponent<Dropdown>().value;
                SetQuality(_qualityValue);

                _antialiasingValue = dropDownAntialiasingSetting.GetComponent<Dropdown>().value;
                SetAntialiasingSetting(_antialiasingValue);
            }           
        }

        public void ChangeToSound()
        {
            graphisme.SetActive(false);
            sound.SetActive(true);
            compte.SetActive(false);
        }
        public void ChangeToGraphism()
        {
            graphisme.SetActive(true);
            sound.SetActive(false);
            compte.SetActive(false);
        }
        public void ChangeToAccount()
        {
            graphisme.SetActive(false);
            sound.SetActive(false);
            compte.SetActive(true);
        }

        void SetQuality(int _idQuality)
        {
            QualitySettings.SetQualityLevel(_idQuality);
        }

        public void SetSyncVertical()
        {
            if(QualitySettings.vSyncCount == 0)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }

        void SetAntialiasingSetting(int _set)
        {
            switch (_set)
            {
                case 1:
                    QualitySettings.antiAliasing = 0;
                    break;
                case 2:
                    QualitySettings.antiAliasing = 2;
                    break;
                case 3:
                    QualitySettings.antiAliasing = 4;
                    break;
                default:
                    break;
            }
        }

        public void SetVolumeMusic(float _volume)
        {
            Debug.Log("Volume valour = " + _volume);
            _volumeMusic = _volume;
            _MusicMixer.SetFloat("VolumeMusic", _volume);
        }


        public void SetVolumeFX(float _volume)
        {
            Debug.Log("Volume FX valour = " + _volume);
            _volumeFX = _volume;
            _FXMixer.SetFloat("VolumeFX", _volume);
        }

        public void DesactivateMusic(bool _desactivate)
        {
            if(_desactivate)
            {
                _MusicMixer.SetFloat("VolumeMusic", -80);
            }
            else
            {
                _MusicMixer.SetFloat("VolumeMusic", _volumeMusic);
            }
        }

        public void DesactivateFX(bool _desactivate)
        {
            if(_desactivate)
            {
                _FXMixer.SetFloat("VolumeFX", -80);
            }
            else
            {
                _FXMixer.SetFloat("VolumeFX", _volumeFX);
            }
        }
    }
}