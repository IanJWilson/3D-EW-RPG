using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilCan : MonoBehaviour {
    public GameObject canSelf;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().PlayerHealth++;
            Destroy(gameObject);

        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyHeal>().CrntHealth++;
            Destroy(canSelf);
        }
       
    }
}
