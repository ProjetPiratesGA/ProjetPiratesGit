using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.AmbientSound
{
    public class MusicAmbienceManager : MonoBehaviour
    {

        public AudioSource Ambiance_Music;
        public AudioSource Ambiance_Ship;
        public AudioSource Ambiance_Ocean;

        // Use this for initialization
        void Start()
        {

            PlaySoundAmbient();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PlaySoundAmbient()
        {
            AudioManager.PlayLoop(Ambiance_Music, "Ambiance_Music");
            AudioManager.PlayLoop(Ambiance_Ship, "Ambiance_Ship");
            AudioManager.PlayLoop(Ambiance_Ocean, "Ambiance_Ocean");
        }

        void StopSoundAmbient(string _sound)
        {
            switch(_sound)
            {
                case "Music":
                    AudioManager.Stop(Ambiance_Music);
                    break;

                case "Ship":
                    AudioManager.Stop(Ambiance_Ship);
                    break;

                case "Ocean":
                    AudioManager.Stop(Ambiance_Ocean);
                    break;

                case "All":
                    AudioManager.Stop(Ambiance_Music);
                    AudioManager.Stop(Ambiance_Ship);
                    AudioManager.Stop(Ambiance_Ocean);
                    break;
            }
        }
    }
}
