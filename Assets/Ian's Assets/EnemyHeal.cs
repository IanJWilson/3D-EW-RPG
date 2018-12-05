using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : Enemy {

   
    public float CrntHealth;

	// Use this for initialization
	void Start () {
        CrntHealth = StartingHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CrntHealth <= 1)
        {
            Heal();
        }

        else if (CrntHealth > 1)
        {
            Roam();
        }

    }
}
