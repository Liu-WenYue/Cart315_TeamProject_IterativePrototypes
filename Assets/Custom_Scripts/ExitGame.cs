using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitGame()
    {
        Debug.Log("Exiting Game"); 
        Application.Quit(); 
    }
}
