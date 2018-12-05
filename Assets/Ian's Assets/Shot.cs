using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public float speed;

    public Transform Player;
    private Vector3 target;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(Player.position.x, Player.position.y);
    }


    void Update()
    {                                                //put Player.position in place of target if you want the fireball to be homing
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerWheel>().playerHealth--;
            Destroy(gameObject);

        }
    }
}
