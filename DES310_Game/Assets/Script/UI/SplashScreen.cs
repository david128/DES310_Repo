using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += LoadMainMenu;
    }

    // Update is called once per frame
    void LoadMainMenu(VideoPlayer vp)
    {
        SceneManager.LoadSceneAsync(1);
    }
}
