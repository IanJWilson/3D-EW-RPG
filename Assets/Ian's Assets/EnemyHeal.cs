using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : Enemy {

   
    public float CrntHealth;

	// Use this for initialization
	void Start () {
        CrntHealth = base.StartingHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Currenthealth <= 1)
        {
            Heal();
        }

        else if (Currenthealth > 1)
        {
            Roam();
        }

    }
}
