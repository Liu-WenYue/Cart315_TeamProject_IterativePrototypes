using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Start_Anim : MonoBehaviour
{
    public VideoPlayer vid;
    public GameObject videoRender;


    void Start() { vid.loopPointReached += CheckOver; }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("Video Is Over");
        vid.enabled = false;
        videoRender.SetActive(false);
    }
}


