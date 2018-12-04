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

    public bool IsFighting;
    public float RoamTime;

    public Transform Player;
    public Transform OilCan;


    void Start()
    {
        Currenthealth = StartingHealth;
    }
   




    void Update()
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

    public void Roam()
    {
        if (RoamTime > 0)
        {
            transform.Translate(Vector3.forward * Enemyspeed);
            RoamTime -= Time.deltaTime;
        }
        else
        {
            RoamTime = Random.Range(3f, 16f);
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);

        }
        

    }

    void Heal()
    {
       OilCan = GameObject.FindGameObjectWithTag("OilCan").transform;
       transform.position = Vector2.MoveTowards(transform.position, OilCan.position, Enemyspeed * Time.deltaTime);


    }

}
