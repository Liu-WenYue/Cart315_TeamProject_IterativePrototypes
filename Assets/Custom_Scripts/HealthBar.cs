using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image currentHealth;
    public Image LowHealth;
    public Text ratio;
    //public GameObject red;


    private float hitpoint = 100f;
    private float maxHitpoint = 100f;

    //OTHER METHOD - GET AUDIOSOURCE FROM AN EMPTY OBJECT 
    //public AudioSource empty; 
    //[SerializeField]private AudioClip lowSound;
    //private AudioSource health_audio; 
    
    //public AudioSource[] player_audio = new AudioSource[2]; 


    private void Start()
    {
        UpdateHealthBar();
        LowHealth.enabled = false;

        //health_audio = GetComponent<AudioSource>();
    }


    /*
    IEnumerator PlayLowHealthAudio()
    {
        
        //health_audio.loop = true;
        health_audio.Play();
        yield return new WaitForSeconds(health_audio.clip.length);
        health_audio.Stop();
    }
    */ 


    public void UpdateHealthBar()
    {
        float r = hitpoint / maxHitpoint;
        currentHealth.rectTransform.localScale = new Vector3(r, 1, 1);
        LowHealth.rectTransform.localScale = new Vector3(r, 1, 1);
        ratio.text = (r * 100).ToString("0") + '%';
    }

    public void TakeDamage(float damage)
    {
        if (hitpoint < 30)
        {
            LowHealth.enabled = true;

            Debug.Log("Playing audio");
           //health_audio.clip = lowSound;
            //health_audio.Play();
        }

        hitpoint -= damage;
        if (hitpoint < 0)
        {
            hitpoint = 0;
            Debug.Log("Dead!");
            SceneManager.LoadScene("GameOver_Lose", LoadSceneMode.Single);
        }

        UpdateHealthBar();

    }


    public void HealthDamage(float heal)
    {
        hitpoint += heal;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }

        UpdateHealthBar();
    }
    public float getMaxHitPoint()
    {
        return maxHitpoint; 

    }
    public float getHitpoint()
    {
        return hitpoint; 
    }

    public void setHitpoint(float nh)
    {
        hitpoint = nh;
        //UpdateHealthBar();
    }
}
