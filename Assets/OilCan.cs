using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilCan : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Player>().PlayerHealth++;
           
            
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Currenthealth++;
        }
        Destroy(gameObject);
    }
}
