using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public Button close;
    public VideoPlayer vid;
    public GameObject videoRender;
    public GameObject btn;

    void Start()
    {
        Button btn = close.GetComponent<Button>();
        btn.onClick.AddListener(CloseOnClick);
    }

    void CloseOnClick()
    {
        vid.enabled = false;
        videoRender.SetActive(false);
        btn.SetActive(false);
    }
}


