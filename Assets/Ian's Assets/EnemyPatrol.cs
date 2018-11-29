using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : Enemy
{

    public Transform[] PatrolPos;
    private int randomPos;

    private float waitTime;
    public float startWaitTime;

    void Start()
    {
        waitTime = startWaitTime;
        randomPos = Random.Range(0, PatrolPos.Length);

    }


    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, PatrolPos[randomPos].position, Enemyspeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, PatrolPos[randomPos].position) < 0.2f)
        {

            if (waitTime <= 0)
            {
                randomPos = Random.Range(0, PatrolPos.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }



        }

    }
}
