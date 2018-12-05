using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float waitTime;
    public float startWaitTime;

    public GameObject Shurikan;

    void Start()
    {
        waitTime = startWaitTime;
    }


    void Update()
    {
        if (waitTime <= 0)
        {
            Instantiate(Shurikan, transform.position, Quaternion.identity);
            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

    }
}
