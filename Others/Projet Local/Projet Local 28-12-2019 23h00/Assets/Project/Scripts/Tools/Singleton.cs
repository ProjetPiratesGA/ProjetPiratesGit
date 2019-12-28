using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjetPirate.Tools
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static  T _instance;

        private static object _lock = new object();
        public static T getInstance
        {
            get
            {
                if(applicationIsQuitting)
                {
                    return null;
                }

                lock (_lock)
                {
                    //cherche si un objet du type donné existe déja
                    if (_instance == null)
                    {

                        _instance = (T)FindObjectOfType(typeof(T));

                        //si un objet du type donné n'existe pas il en crée un
                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(MonoSingleton) " + typeof(T).ToString();

                            if (Application.isPlaying)
                                DontDestroyOnLoad(singleton);
                        }
                    }
                    return _instance;
                }

            }
        }
        

        private static bool applicationIsQuitting = false;

        public void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }
    }
}
