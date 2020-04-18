using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Start_Anim : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject videoRender;
    public GameObject close;


    void Start() {
        vid.loopPointReached += CheckOver;
        vid.frame = 0;
    }


    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("Video Is Over");

        close.SetActive(true);

    }
}


