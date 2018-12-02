﻿using System.Collections;
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
            Chase();
        }

        else if (Vector3.Distance(transform.position, Player.position) <= MaxDis)
        {
            transform.position = transform.position;
        }

       
        if (Currenthealth > 0 && Currenthealth <= 1)
        {
            transform.position += transform.forward * -Enemyspeed * Time.deltaTime;
        }


        // Death

        if (Currenthealth <= 0)
        {

            // Give the player EXP
            //GetComponent<Player>().PlayerEXP++;
            
            Destroy(gameObject);
        }
		
	}
    void Chase()
    {
        transform.position += transform.forward * Enemyspeed * Time.deltaTime;
    }

}
