using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    public bool testBool;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        testBool = true;
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().Currenthealth--;
        }
    }


}
