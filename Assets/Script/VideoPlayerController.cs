using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    bool ISPlaying, isStarted, isEnded;
    public GameObject playImg, pauseImg;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        ISPlaying = false;
        isStarted = false;
        isEnded = false;
    }

    private void Update()
    {
        if ((videoPlayer.frame) > 0 && (videoPlayer.isPlaying == false))
        {
            playImg.SetActive(true);
            pauseImg.SetActive(false);
            ISPlaying = false;
            videoPlayer.Pause();
        }
    }

    public void OnMouseDown()
    {
        if(ISPlaying)
        {
            
            playImg.SetActive(true);
            pauseImg.SetActive(false);
            ISPlaying = false;
            videoPlayer.Pause();

        }
        else
        {
            playImg.SetActive(false);
            pauseImg.SetActive(true);
            ISPlaying = true;
            videoPlayer.Play();
        }
        
    }
}
