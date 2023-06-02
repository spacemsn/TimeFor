using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class BoostrapEntryPoint : MonoBehaviour
{ 
    [SerializeField] float loadingDuration;

    //private IEnumerator Start()
    //{
    //    loadingDuration = 3f;
    //    while (loadingDuration > 0)
    //    {
    //        loadingDuration -= Time.deltaTime;
    //        Debug.Log("Loading...");

    //        yield return null;
    //    }

    //    Debug.Log("All application service are initialied.");
    //    VideoPlayer.
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
