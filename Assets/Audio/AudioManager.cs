using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

//This script is to centralize audio management.
{
    public static AudioManager instance;
    public AudioSource soundEffectSource;
    public AudioClip nameOfAudio;
    public AudioClip backgroundMusic;
    public AudioClip marioDeath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //"PlayAudioClipName" is a method to to play "nameOfAudio".
    public void PlayAudioClipName()
    {
        soundEffectSource.PlayOneShot(nameOfAudio);
        //"nameOfAudio" is a reference to the actual audio clip to be used. Assign any audio clip to this field in the Inspector.
    }

    public void PlayBGM()
    {
        soundEffectSource.PlayOneShot(backgroundMusic);
    }

    public void PlayMarioDeath()
    {
        soundEffectSource.PlayOneShot(marioDeath);
    }





    //Assign the sound effects to be played to the appropriate fields in the AudioManager component attached to the AudioManager empty Game Object in  the Editor.

    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}


