using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAudioSound : MonoBehaviour
{
    public AudioClip DropSound;
    public float volume;
    AudioSource dropped;
    public bool alreadyPlayed = false;


    public bool vegePickedUp = false;
    public Vegetable daikon;

    // Start is called before the first frame update
    void Start()
    {
        dropped = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (daikon.daikon_ispicked)
        {
            vegePickedUp = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (vegePickedUp)
        {
            if (!alreadyPlayed)
            {
                Debug.Log("Playing audio");
                dropped.PlayOneShot(DropSound);
                alreadyPlayed = true;
            }
        }
    }

}
