using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float Enemyhealth;
    public float Enemyspeed;
    public float Enemydamage;


    public float MinDis;
    public float MaxDis;

    public Transform Player;
	// Use this for initialization
	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {

        Enemyhealth--;

        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) >= MinDis)
        {

            transform.position += transform.forward * Enemyspeed * Time.deltaTime;



           if (Vector3.Distance(transform.position, Player.position) <= MaxDis)
            {
                transform.position = transform.position;
            }

        }








        if (Enemyhealth <= 0)
        {
            Destroy(gameObject);
        }
		
	}
}
