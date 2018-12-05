using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{

    public Transform wheel;
    public GameObject player;
    public float distance;
    public float height;
    public float rotationDamping = 3f;
    public float heightDamping = 2f;
    private float desiredAngle = 0;


    // LateUpdate is called once per frame after Update() has been called
    void LateUpdate()
    {
        if (player.GetComponent<PlayerWheel>().isAttacking == false)
        {
            float currentAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;
            //Determine where we want to be.
            desiredAngle = wheel.eulerAngles.y;
            float desiredHeight = wheel.position.y + height;
            //Now move towards our goals.
            currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, rotationDamping * Time.deltaTime);
            currentHeight = Mathf.Lerp(currentHeight, desiredHeight, heightDamping * Time.deltaTime);
            Quaternion currentRotation = Quaternion.Euler(0, currentAngle, 0);
            //Set our new positions.
            Vector3 finalPosition = wheel.position - (currentRotation * Vector3.forward * distance);
            finalPosition.y = currentHeight;
            transform.position = finalPosition;
            transform.LookAt(wheel);
        }
    }
}
