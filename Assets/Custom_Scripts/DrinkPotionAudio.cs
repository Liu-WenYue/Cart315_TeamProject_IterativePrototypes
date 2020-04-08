using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPotionAudio : MonoBehaviour
{
    public AudioClip DrinkSound;
    private float volume = 1; 
    AudioSource drank;
    public bool alreadyDrank = false;

    public Potion drink;
    bool drinkPickedUp; 
    // Start is called before the first frame update
    void Start()
    {
        drank = GetComponent<AudioSource>();    
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        if(drink.potion_picked)
        {
            drinkPickedUp = true; 
        }
        */ 
        
    }

    public void DrinkingAudio()
    {
        if (!alreadyDrank)
        {
            Debug.Log("Drinking potion");
            alreadyDrank = false;
            drank.PlayOneShot(DrinkSound);
        }
        
    }

}
