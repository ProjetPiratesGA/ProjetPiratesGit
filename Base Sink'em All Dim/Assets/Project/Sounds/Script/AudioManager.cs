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

            pSource.PlayOneShot(pSource.clip);
        }
    }

    static public void PlayRandom(AudioSource pSource, string pAudioClipName1 = null, string pAudioClipName2 = null, string pAudioClipName3 = null, string pAudioClipName4 = null)
    {

        string pAudioClipName = null;

        int rand = Random.Range(1, 5);

        while (pAudioClipName == null)
        {
            switch (rand)
            {
                case 1:
                    pAudioClipName = pAudioClipName1;
                    break;

                case 2:
                    pAudioClipName = pAudioClipName2;
                    break;

                case 3:
                    pAudioClipName = pAudioClipName3;
                    break;

                case 4:
                    pAudioClipName = pAudioClipName4;
                    break;
            }

            if (pAudioClipName == null)
            {
                rand = Random.Range(1, 5);
            }
        }

        pSource.clip = _clipFromString[pAudioClipName];

        pSource.PlayOneShot(pSource.clip);
    }

    static public void PlayLoop(AudioSource pSource, string pAudioClipName)
    {
        if (_clipFromString[pAudioClipName] != null)
        {
            pSource.clip = _clipFromString[pAudioClipName];

            pSource.PlayOneShot(pSource.clip);
            pSource.PlayScheduled(AudioSettings.dspTime + pSource.clip.length);
        }
    }

    static public void Stop(AudioSource pSource)
    {
        pSource.Stop();
    }
}
