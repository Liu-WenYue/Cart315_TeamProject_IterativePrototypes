using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System; 

public class Vegetable : MonoBehaviour
{
    public bool f_pressed = false; 
    private bool e_pressed;

    public bool daikon_ispicked; 
    private bool touchedVege; 

    public float speed = 1000f; 
    //public GameObject vege; 
    public Transform p; //player 

    private NavMeshAgent temp;

    public float time = 5f;

    //Audio

    public AudioClip PickUpSound;
    public float volume; 
    AudioSource Source;
    public bool alreadyPlayed = false;

    public AudioClip wrongSound; 
    public bool wrongPlayed = false; 

    public static int num_daikon = 0;

    public GameObject daikon_active;
    public GameObject daikon_used;

    public AudioSource EatSource; 
    //public AudioClip eatingSound;
    private bool eating_is_playing = false;

    //public AudioSource[] vege_array = new AudioSource[2]; 


    // Start is called before the first frame update
    void Start()
    {
        Source = GetComponent<AudioSource>(); 
        //vege_array = this.GetComponents<AudioSource>(); 

        //Source = vege_array[0]; //for bite sounds and wrong sounds
        //EatSource = vege_array[1]; //sound for eating daikon 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        f_pressed = Input.GetKeyDown("f"); //returns true if pressed 
     
        /*
        e_pressed = Input.GetKeyDown("e"); 

        if (e_pressed)
        {
            Debug.Log("Pressed e");
            this.gameObject.SetActive(true);
            //transform.position = Vector3.MoveTowards(transform.position, p.position, speed * Time.deltaTime); 
            //Instantiate(vege, p.position, Quaternion.identity); 
        }
        */
    }

    /*
    void Update()
    {
        if (touchedVege)
        {
            Debug.Log("inside touchedVEGE true");

            float start = Time.deltaTime;

            while (time != 0)
            {
                float diff = Time.deltaTime - start;
                Debug.Log(diff); 
                if (diff >= 5f)
                {
                    temp.speed = 2;
                    time = 0;
                }
            }
        }
    }
    */ 

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found player by vegetable"); 
            if(f_pressed)
            {
                if(num_daikon == 0)
                {
                    playPickUpAudio();
                    daikon_ispicked = true;

                    
                    daikon_used.SetActive(false);
                    daikon_active.SetActive(true);
                    num_daikon++;
                    //this.gameObject.GetComponent<MeshRenderer>().enabled = false; 
                    Destroy(this.gameObject); 
                }
                else if(num_daikon == 1)
                {
                    //Wrong input sound?
                    playWrongAudio(); 
                    Debug.Log("Cannot pickup more than one daikon!"); 
                }
               
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI"))
        {
            Debug.Log("AI ennemy detected");
            temp = other.gameObject.GetComponent<NavMeshAgent>();

            StartCoroutine(countDown(temp));
            this.gameObject.GetComponent<MeshRenderer>().enabled = false; 
            //DateTime current = DateTime.Now;

            //touchedVege = true;
            //Debug.Log(touchedVege); 
        }
    }

    IEnumerator countDown(NavMeshAgent temp)
    {
        if(!eating_is_playing)
        {
            eating_is_playing = true;
            EatSource.loop = true; 
            EatSource.Play(); 
            Debug.Log("Playing eating audio"); 
            
        }

        Debug.Log("Inside countdown");

        temp.speed = 0; 
        yield return new WaitForSeconds(5);

        Debug.Log("Counted 5 seconds");
        temp.speed = 4;

        if(eating_is_playing)
        {
            EatSource.loop = false;
            EatSource.Stop(); 
            eating_is_playing = false; 
        }

        Destroy(this.gameObject); 
    }

    private void playPickUpAudio()
    {
        if (!alreadyPlayed)
        {
            Debug.Log("Playing audio");
            //pickup.PlayOneShot(PickUpSound);
            //Source.clip = PickUpSound;
            //Source.PlayOneShot(PickUpSound); 
            AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            alreadyPlayed = true;
        }
    }

    private void playWrongAudio()
    {

        Debug.Log("Playing wrong audio");

        Source.clip = wrongSound;
        Source.PlayOneShot(wrongSound); 
        
        //Source.clip = wrongSound;
        //Source.PlayOneShot(wrongSound);

        //wrongPlayed = true;
        //AudioSource.PlayClipAtPoint(wrong, transform.position); 

    }


}
