using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public bool f_pressed = false;

    public bool potion_picked = false; 

    public AudioClip PickUpSound;
    public float volume = 1; 
    AudioSource Source;
    public bool alreadyPlayed = false;

    public AudioClip wrongSound; 

    public MeshRenderer cork;
    public MeshRenderer liquid;

    public static int num_potion = 0;

    public GameObject potion_active;
    public GameObject potion_used;

    private Collider potion_c; 
    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>();
        potion_c = GetComponent<Collider>(); 
    }

    // Update is called once per frame
    void Update()
    {
        f_pressed = Input.GetKeyDown("f");
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Detected Potion"); 
        if(other.gameObject.CompareTag("Player"))
        {
            if(f_pressed)
            {
                if(num_potion == 0)
                {
                    // Debug.Log("Player pressed F"); 
                    //Play pickup audio
                    playPickUpAudio();

                    potion_picked = true;

                    //Make potion disappear - mesh renderer
                    this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    cork.enabled = false;
                    liquid.enabled = false;

                    potion_used.SetActive(false); 
                    potion_active.SetActive(true);

                    potion_c.enabled = false; 
                    num_potion++;
                }
                else if(num_potion == 1)
                {
                    playWrongAudio();
                    Debug.Log("Cannot pickup more than one potion"); 
                }
               
            }
        }
    }


    private void playPickUpAudio()
    {
        if (!alreadyPlayed)
        {
            //Debug.Log("Playing audio");
            //pickup.PlayOneShot(PickUpSound);
            AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            alreadyPlayed = true;
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
