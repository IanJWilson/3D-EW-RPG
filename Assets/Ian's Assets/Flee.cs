using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : Enemy {

    public float fleeDistance;



    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, Player.position) < fleeDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -Enemyspeed * Time.deltaTime);
        }


    }
}
