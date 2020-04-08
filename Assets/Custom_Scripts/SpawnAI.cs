using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class SpawnAI : MonoBehaviour
{
    public GameObject newMonster;
    private NavMeshAgent nmon; 
    private bool f_pressed; 

    // Start is called before the first frame update
    void Start()
    {
        newMonster.SetActive(false); 
        nmon = newMonster.GetComponent<NavMeshAgent>();

        nmon.speed = 0;
        nmon.angularSpeed = 0;
        nmon.acceleration = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        f_pressed = Input.GetKeyDown("f");
    }

    //When player interact with item 
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Found box");

            if (f_pressed)
            {
                //give the other object
                newMonster.SetActive(true);

                nmon.speed = 1;
                nmon.angularSpeed = 120;
                nmon.acceleration = 8; 
                Destroy(this.gameObject);

            }
        }

    }


}
