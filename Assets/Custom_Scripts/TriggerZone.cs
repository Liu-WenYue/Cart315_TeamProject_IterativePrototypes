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

            //col.SendMessage((isDamaging) ? "TakeDamage" : "HealthDamage", Time.deltaTime * damage);
        }
    }
}
