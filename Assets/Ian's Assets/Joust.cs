using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joust : Enemy
{

    public float RecoverTime;
    public bool testBool = false;
    
    public Vector3 target;

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
        target = Player.position;
        target.y = target.y + 1;
        transform.position = Vector3.MoveTowards(transform.position, target, 15);

        RecoverTime = 3;

        RecoverTime -= Time.deltaTime;

        if (RecoverTime > 0)
        {
            transform.position = transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            testBool = true;
            Charge();
        }
       
    }
}
