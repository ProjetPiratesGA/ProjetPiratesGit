using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace ProjetPirate.UI.Menu
{
    public class LevelLoadingScript : MonoBehaviour
    {
        [Tooltip("On envoie en paramètre l'image afin de la changer")]
        public Image _loadingBar;
        float _timerBeforeStartLoading = 0;
        float _startLoadingAt = 2;
        bool canStartLoading = true;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(MenuManager._iDLevelToLoad);
        }

        // Update is called once per frame
        void Update()
        {
            _timerBeforeStartLoading += Time.deltaTime;
            if(_timerBeforeStartLoading > _startLoadingAt)
            {
                if(canStartLoading)
                {
                    StartCoroutine(LoadingLevel());
                }
            }
        }

        IEnumerator LoadingLevel()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(MenuManager._iDLevelToLoad);
            Debug.Log("Loading LvL " + MenuManager._iDLevelToLoad);
            while (!operation.isDone)
            {
                _loadingBar.fillAmount = operation.progress;
                yield return null;
            }

        }
    }
}
