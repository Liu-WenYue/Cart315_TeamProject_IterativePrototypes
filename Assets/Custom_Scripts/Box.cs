using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject inside; 
    private bool f_pressed; 
    // Start is called before the first frame update
    void Start()
    {
        //mute all the inner objects
        inside.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        f_pressed = Input.GetKeyDown("f"); 
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found box");

            if (f_pressed)
            {
                //give the other object
                inside.SetActive(true);
                Destroy(this.gameObject); 

            }
        }

    }
    

    /*
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found box");

            if (f_pressed)
            {
                //give the other object
                inside.SetActive(true);
                Destroy(this.gameObject);

            }
        }
    }
    */ 

    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found box");

            if (f_pressed)
            {
                //give the other object
                inside.SetActive(true);
                Destroy(this.gameObject);

            }
        }
    }
    */
}
