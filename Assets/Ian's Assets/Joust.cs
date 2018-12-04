using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joust : Enemy
{

    public float RecoverTime;
    public bool testBool = false;
    
    private Vector2 target;

    void Start ()
    {
        Currenthealth = StartingHealth;
    }
	
	
	void Update () {
       // base.Roam();
	}

    void Charge()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(Player.position.x, Player.position.z);

        transform.position = Vector2.MoveTowards(transform.position, target, Enemyspeed * Time.deltaTime);

        RecoverTime = 3;

        RecoverTime -= Time.deltaTime;

        if (RecoverTime > 0)
        {
            transform.position = transform.position;
        }
    }

    void OnTriggerEnter(SphereCollider other)
    {
        testBool = true;
        if (other.CompareTag("Player"))
        {
            Charge();
            



        }
       
    }
}
