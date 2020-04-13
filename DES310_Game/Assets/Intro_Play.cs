using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_Play : MonoBehaviour {

    private string movie = Resources.Load<Video Player("Final_Audio");

    void Start () 
    {
        StartCoroutine(streamVideo(movie));
    }

    private IEnumerator streamVideo(string video)
    {
        Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        yield return new WaitForEndOfFrame ();
        SceneManager.LoadScene (0);
    }
}