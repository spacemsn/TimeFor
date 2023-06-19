using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using Unity.VisualScripting;

public class BoostrapEntryPoint : MonoBehaviour
{
    [SerializeField] TMP_Text Text;
    [SerializeField] float loadingDuration;
    public VideoPlayer videoPlayer;
    public bool isPlay;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;

        if(isPlay)
        {
            videoPlayer.Play();
        }
    }

    private void LateUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && isPlay)
        {
            videoPlayer.loopPointReached -= EndReached;
            SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
            videoPlayer.Stop();
        }
        else if(Input.anyKeyDown && !isPlay && Text != null)
        {
            isPlay = true;
            videoPlayer.Play();
            Text.gameObject.SetActive(false);
        }
    }

    void EndReached(VideoPlayer vp)
    {
        SceneLoad.SwitchIndexScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
