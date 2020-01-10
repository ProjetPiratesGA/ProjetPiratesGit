using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace ProjetPirate.UI.Menu
{

    public class videoPlayerScript : MonoBehaviour
    {

        public RawImage rawImage;
        public VideoPlayer videoPlayer;
        public AudioSource audioSource;

        public GameObject _menuManager;
        bool videoPlay = false;
        float currentTime = 0;
        Color colorImage;
        // Use this for initialization
        void Start()
        {
            StartCoroutine(PlayVideo());
        }

        private void Update()
        {
            if (videoPlay)
            {
                if (!videoPlayer.isPlaying)
                    _menuManager.GetComponent<MenuManager>().FinishIntroLogoGa();
            }
        }

        IEnumerator PlayVideo()
        {
            videoPlayer.Prepare();
            WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
            while(!videoPlayer.isPrepared)
            {
                yield return waitForSeconds;
                break;
            }
            rawImage.texture = videoPlayer.texture;
            videoPlayer.Play();
            audioSource.Play();
            Debug.Log("Video Start");
            videoPlay = true;
        }
    }
}
