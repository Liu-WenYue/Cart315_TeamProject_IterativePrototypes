using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class EnemyAI : MonoBehaviour
{
    private float hitpoint = 100f;
    private float maxHitpoint = 100f;
    public Image enemySpeed;

    public NavMeshAgent monster; 
    public GameObject destination;

    public float time = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        monster = this.GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //continously find them
        monster.SetDestination(destination.transform.position);
    }

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
