using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class EnemyAI : MonoBehaviour
{
   //private float hitpoint = 100f;
   // private float maxHitpoint = 100f;
    //public Image enemySpeed;

    public NavMeshAgent monster; 
    public GameObject destination;

    public AudioClip biteSound; 
    AudioSource bite;
    private bool bite_already_playing = false;
    private bool is_touching = false;

    public AudioClip growlSound; 
    private bool growl_is_playing = false;

    private float distance; 

    public float time = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        monster = this.GetComponent<NavMeshAgent>();

        bite = GetComponent<AudioSource>(); 
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //continously find them
        monster.SetDestination(destination.transform.position);
    }

    private void Update()
    {
        distance = Vector3.Distance(destination.transform.position, this.transform.position);


        GrowlWithinDistance(); 

        if (is_touching)
        {
            if (!bite_already_playing)
            {
                PlayBiteSound();
            }
        }
        else if (!is_touching)
        {
            if (bite_already_playing)
            {
                StopBiteSound();
            }

            Debug.Log("Stopped touching - but growling"); 
            GrowlWithinDistance();
        }
    }

    public void GrowlWithinDistance()
    {
        if (distance < 10)
        {
            if (!growl_is_playing)
            {
                PlayGrowlSound();
            }

        }
        else if (distance >= 10)
        {
            if (growl_is_playing)
            {
                StopGrowlSound();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            is_touching = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            is_touching = false;
        }
    }

    private void PlayBiteSound()
    {
        bite.clip = biteSound;
        bite.Play();
        bite.loop = true;
        bite_already_playing = true;
    }

    private void StopBiteSound()
    {
        bite.clip = biteSound;
        bite.loop = false;
        bite.Stop();
        bite_already_playing = false;
        growl_is_playing = false; // in order for the growl to play 
    }

    private void PlayGrowlSound()
    {
        bite.clip = growlSound;
        bite.loop = true;
        bite.Play();
        growl_is_playing = true; 
    }


    private void StopGrowlSound()
    {
        bite.clip = growlSound;
        bite.loop = false;
        bite.Stop();
        growl_is_playing = false; 
        
    }
    //OLD CODE TO IGNORE
    /*
    private void UpdateSpeedBar()
    {
        float r = hitpoint / maxHitpoint;
        enemySpeed.rectTransform.localScale = new Vector3(r, 1, 1);
    }


    public void SpeedUpdate()
    {
        // code for increasing speed and decreasing speed

        UpdateSpeedBar();
    }
    */

    /*
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("STOPP"); 
        if(other.gameObject.CompareTag("Vegetable"))
        {
            Debug.Log("Detected vegetable");

            monster.speed = 0;
            monster.angularSpeed = 0;
            monster.acceleration = 0;


        }
    }
    */

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Detected something - AI");

    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //Debug.Log("Ai killed player");
    //        SceneManager.LoadScene("GameOver_Lose", LoadSceneMode.Single);
    //    }

    //    if (other.gameObject.CompareTag("Vegetable"))
    //    {
    //        //Debug.Log("Found vege - stopping AI");

    //        monster.isStopped = true; 
    //        float count = 50000000f;
    //        if (count >= 0)
    //        {
    //            count -= Time.deltaTime;
    //        }
    //        else if (count < 0)
    //        {
    //            monster.isStopped = false; 
    //        }

    //    }


    //}

    /* 
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Ai killed player");
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOver_Lose", LoadSceneMode.Single);
        }
    }
    */

}
