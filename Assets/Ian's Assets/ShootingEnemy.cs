using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy {

    public float FiringDis;
    public float FleeingDis;

	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector2.Distance(transform.position, Player.position) > FiringDis)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, Enemyspeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, Player.position) < FiringDis && Vector2.Distance(transform.position, Player.position) > FleeingDis)
        {
            transform.position = transform.position;
        }
        else if (Vector2.Distance(transform.position, Player.position) < FleeingDis)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -Enemyspeed * Time.deltaTime);
        }
	}
}
