using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float StartingHealth;
    public float Currenthealth;
    public float Enemyspeed;
    public float Enemydamage;

    public Slider EnemyHealth;


    public float MinDis;
    public float MaxDis;

    public Transform Player;
	// Use this for initialization
	void Start ()
    {
        Currenthealth = StartingHealth;

        Player = GameObject.FindGameObjectWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void Update () {

        EnemyHealth.value = Currenthealth;

        Currenthealth--;

        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDis)
        {

            transform.position += transform.forward * Enemyspeed * Time.deltaTime;



           if (Vector3.Distance(transform.position, Player.position) <= MaxDis)
            {
                transform.position = transform.position;
            }

        }




        // Death

        if (Currenthealth <= 0)
        {
            Destroy(gameObject);
        }
		
	}
}
