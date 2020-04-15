using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public bool f_pressed;

    public bool shield_picked;

    public MeshRenderer major;
    public MeshRenderer p1;
    public MeshRenderer p2;
    public MeshRenderer p3;
    public MeshRenderer p4;
    public MeshRenderer p5;
    public MeshRenderer p6;
    public MeshRenderer p7;
    public MeshRenderer p8;
    public MeshRenderer p9;
    public MeshRenderer p10;

    public AudioClip PickUpSound;
   // public float volume;
    AudioSource Source;
    public bool alreadyPlayed = false;
    public AudioClip wrongSound;


    public GameObject shield_active;
    public GameObject shield_used;


    public static int num_shield = 0;

    private Collider shield_c; 


    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        shield_c = GetComponent<Collider>(); 

    }

    // Update is called once per frame
    void Update()
    {
        f_pressed = Input.GetKeyDown("f");
        //mouseclick = Input.GetMouseButtonDown(1); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(f_pressed) //f_pressed
            {
                if(num_shield == 0)
                {
                    shield_picked = true;

                    shield_used.SetActive(false); 
                    shield_active.SetActive(true);

                    playPickUpAudio();
                    Debug.Log("Player found shield");

                    //this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    major.enabled = false;
                    p1.enabled = false;
                    p2.enabled = false;
                    p3.enabled = false;
                    p4.enabled = false;
                    p5.enabled = false;
                    p6.enabled = false;
                    p7.enabled = false;
                    p8.enabled = false;
                    p9.enabled = false;
                    p10.enabled = false;

                    shield_c.enabled = false; //disable this specific collider 
                    num_shield++; 

                }
                else if(num_shield == 1)
                {
                    playWrongAudio();
                    Debug.Log("Cannot pickup more than one shield!");
                    
                }
                  
                
            }
        }
    }

    private void playPickUpAudio()
    {
        if (!alreadyPlayed)
        {
            alreadyPlayed = true;
            Debug.Log("Playing audio");
            //pickup.PlayOneShot(PickUpSound);
            AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
           
        }
    }

    private void playWrongAudio()
    {

        Debug.Log("Playing wrong audio");
        Source.clip = wrongSound;
        Source.PlayOneShot(wrongSound);

        //wrongPlayed = true;
        //AudioSource.PlayClipAtPoint(wrong, transform.position); 


    }
}
