using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    public bool isDamaging;
    public float damage = 20f;


    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<HealthBar>().TakeDamage(damage * Time.deltaTime);
            //StartCoroutine(PlayBiteSound()); 
            //col.SendMessage((isDamaging) ? "TakeDamage" : "HealthDamage", Time.deltaTime * damage);
        }
       
    }



    /*
    IEnumerator PlayBiteSound()
    {
        bite.Play();
        yield return new WaitForSeconds(bite.clip.length);
        
    }
    */ 

}
