using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFile
{
    public string key;
    public AudioClip value;
}

public class AudioManager : MonoBehaviour
{

    [SerializeField] private List<AudioFile> _clipsToLoad;

    static private Dictionary<string, AudioClip> _clipFromString = new Dictionary<string, AudioClip>();


    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < _clipsToLoad.Count; i++)
        {
            Debug.Log(i);
            if (!_clipFromString.ContainsKey(_clipsToLoad[i].key))
            {
                _clipFromString.Add(_clipsToLoad[i].key, _clipsToLoad[i].value);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    static public void Play(AudioSource pSource, string pAudioClipName)
    {
        if (_clipFromString[pAudioClipName] != null)
        {
            pSource.clip = _clipFromString[pAudioClipName];
            pSource.Play();
        }
    }
}
