using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MainExitGame : MonoBehaviour
{
    //public FirstPersonController player; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainGameExit()
    {
        //player.getMouse().lockCursor = false; 
        
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
