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
    AudioSource pickup;
    public bool alreadyPlayed = false; 

    public static int num_daikon = 0;

    public GameObject daikon_active;
    public GameObject daikon_used;


    
    // Start is called before the first frame update
    void Start()
    {
        pickup = GetComponent<AudioSource>(); 
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
        Debug.Log("Inside countdown");

        temp.speed = 0; 
        yield return new WaitForSeconds(5);

        Debug.Log("Counted 5 seconds");
        temp.speed = 4;

        Destroy(this.gameObject); 
    }

    private void playPickUpAudio()
    {
        if (!alreadyPlayed)
        {
            Debug.Log("Playing audio");
            //pickup.PlayOneShot(PickUpSound);
            AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            alreadyPlayed = true;
        }
    }


}
