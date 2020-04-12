using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DropAudioSound : MonoBehaviour
{
    public AudioClip DropSound;
    public float volume;
    AudioSource dropped;
    public bool alreadyPlayed = false;


    public bool vegePickedUp = false;
    public FirstPersonController player; 

    //public Vegetable daikon;

    // Start is called before the first frame update
    void Start()
    {
        dropped = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(player.daikon != null) //check that it exists 
        {
            vegePickedUp = player.daikon_already_used;
        }
        */ 
       
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
