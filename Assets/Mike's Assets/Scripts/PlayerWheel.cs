using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheel : CharacterScript {

    private Rigidbody body;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public Transform wheelTransformBL;
    public Transform wheelTransformBR;
    public Transform wheelTransformFL;
    public Transform wheelTransformFR;

    public GameObject SpearL;
    public GameObject ShieldR;

    public GameObject SwordL;
    public GameObject SwordR;

    //public GameObject SpearR;


        
    public float currentSpeed = 0;
    public float topSpeed = 150;
    public float maxReverseSpeed = -50;
    public float maxTurnAngle = 10;
    public float maxTorque = 10;
    public Vector3 centerOfMassAdjustment = new Vector3(0f, -0.9f, 0f);




    
    // Use this for initialization
    void Start () {
        //lower center of mass for roll-over resistance
        body = GetComponent<Rigidbody>();
        body.centerOfMass += centerOfMassAdjustment;
        SwordSwing(SwordL, SwordR);
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the wheels based on RPM
        float rotationThisFrame = 180 * Time.deltaTime;
        wheelTransformFR.Rotate(wheelBR.rpm / rotationThisFrame, 0, 0);
        SwordSwing(SwordL, SwordR);
        //UpdateWheelPositions();
    }

    void FixedUpdate()
    {
        //calculate max speed in KM/H (optimized calc)
        currentSpeed = wheelBL.radius * wheelBL.rpm * Mathf.PI * 0.12f;
        if (currentSpeed < topSpeed && currentSpeed > maxReverseSpeed)
        {
            //rear wheel drive.
            wheelBL.motorTorque = Input.GetAxis("Vertical") * maxTorque;
            wheelBR.motorTorque = Input.GetAxis("Vertical") * maxTorque;
        }
        else
        {
            //can't go faster, already at top speed that engine produces.
            wheelBL.motorTorque = 0;
            wheelBR.motorTorque = 0;
        }

        //front wheel steering
        wheelFL.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
        wheelFR.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;

    }

    void UpdateWheelPositions()
    {
        //move wheels based on their suspension.
        WheelHit contact = new WheelHit();
        if (wheelFL.GetGroundHit(out contact))
        {
            Vector3 temp = wheelFL.transform.position;
            wheelTransformFL.position = temp;
        }
        if (wheelFR.GetGroundHit(out contact))
        {
            Vector3 temp = wheelFR.transform.position;
            wheelTransformFR.position = temp;
        }
        if (wheelBL.GetGroundHit(out contact))
        {
            Vector3 temp = wheelBL.transform.position;
            wheelTransformBL.position = temp;
        }
        if (wheelBR.GetGroundHit(out contact))
        {
            Vector3 temp = wheelBR.transform.position;
            wheelTransformBR.position = temp;
        }
    }

}
