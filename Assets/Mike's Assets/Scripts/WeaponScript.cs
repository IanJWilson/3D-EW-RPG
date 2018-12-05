using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    public bool isAttacking;
    public bool shieldHit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isAttacking = true;
            other.GetComponent<Enemy>().Currenthealth--;
        }
        else if (other.CompareTag("Shield"))
        {
            isAttacking = true;
            shieldHit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isAttacking = false;
        }
        else if (other.CompareTag("Shield"))
        {
            isAttacking = false;
        }

    }
    }
