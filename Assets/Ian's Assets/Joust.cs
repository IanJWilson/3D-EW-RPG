using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joust : Enemy
{

   
    public bool IsCharging;

    public float RecovTime;

    public Vector3 target;

    void Start ()
    {
        Currenthealth = StartingHealth;
        
    }
	
	
	void Update () {
     


        if (RecovTime > 0)
        {
            RecovTime -= Time.deltaTime;
            transform.position = transform.position;
        }
        else if (!IsCharging)
        {
            base.Roam();
        }
    }

    public void Charge()
    {
        IsCharging = true;

        if(IsCharging == true)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            target = Player.position;
            target.y = target.y + 1;


            transform.position = Vector3.Lerp(transform.position, target, 1);


            if (Vector3.Distance(transform.position, target) <= 1)
            {
                RecovTime = 5;
                IsCharging = false;
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && RecovTime <= 0)
        {
          
            Charge();

            
        }
       
    }
}
//float h = Input.GetAxis("Horizontal") * Enemyspeed * Time.deltaTime;
//float v = Input.GetAxis("Vertical") * Enemyspeed * Time.deltaTime;

//transform.Translate(new Vector3(h, 0, v));