using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerZone : MonoBehaviour
{
    public bool isDamaging;
    public float damage = 20f;

    public AudioClip biteSound;
    AudioSource bite;

    public Collider playercollider;
    Collider AI; 
    //public bool keep_playing;

    void Start()
    {
        bite = GetComponent<AudioSource>();
        bite.clip = biteSound;

        AI = GetComponent<Collider>(); 
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<HealthBar>().TakeDamage(damage * Time.deltaTime);

            //bite.loop = true; 
            bite.Play(); 
            //StartCoroutine(PlayBiteSound()); 
            //col.SendMessage((isDamaging) ? "TakeDamage" : "HealthDamage", Time.deltaTime * damage);
        }
        else //stop playing the clip
        {
            bite.Stop(); 
        }
    }

    IEnumerator PlayBiteSound()
    {
        bite.Play();
        yield return new WaitForSeconds(bite.clip.length);
        
    }
}
