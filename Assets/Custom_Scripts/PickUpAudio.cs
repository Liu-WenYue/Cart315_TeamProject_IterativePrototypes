using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAudio : MonoBehaviour
{

    public AudioClip PickUpSound;
    public float volume;
    AudioSource pickup;
    public bool alreadyPlayed = false;

    Vegetable daikon;
    bool f_pressed; 
    //public GameObject player; 

    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<AudioSource>();
        f_pressed = daikon.f_pressed; 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            
            

        }   
    }
}
