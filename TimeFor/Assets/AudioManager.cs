using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("јудио»сточник")]
    public AudioSource audioSource;

    [Header("—писок музыки")]
    public AudioClip backgroundMusic;
    public AudioClip mainMenuMusic;
    public AudioClip PauseMenuMusic;

    private void OnEnable()
    {
        SceneLoad.onSceneLoaded += backgroundMusicPlay;
    }

    private void OnDisable()
    {
        SceneLoad.onSceneLoaded -= backgroundMusicPlay;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void backgroundMusicPlay()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }


}
