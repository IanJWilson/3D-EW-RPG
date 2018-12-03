using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour
{
    public float currentSpeed = 0;
    public float topSpeed = 150;
    public float maxReverseSpeed = -50;
    public float maxTurnAngle = 10;
    public float maxTorque = 10;
    public float decelerationTorque = 30;
    public Vector3 centerOfMassAdjustment = new Vector3(0f, -0.9f, 0f);
    public float spoilerRatio = 0.1f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelBL;
    public WheelCollider wheelBR;
    public Transform wheelTransformFL;
    public Transform wheelTransformFR;
    public Transform wheelTransformBL;
    public Transform wheelTransformBR;
    private Rigidbody body;

    public float maxBrakeTorque = 100;
    public bool applyHandbrake = false;
    public float handbrakeForwardSlip = 0.04f;
    public float handbrakeSidewaysSlip = 0.08f;


    void Start()
    {
        //lower center of mass for roll-over resistance
        body = GetComponent<Rigidbody>();
        body.centerOfMass += centerOfMassAdjustment;
    }

    // FixedUpdate is called once per physics frame
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

        //Spoilers add down pressure based on the car’s speed. (Upside-down lift)
        Vector3 localVelocity = transform.InverseTransformDirection(body.velocity);
        body.AddForce(-transform.up * (localVelocity.z * spoilerRatio), ForceMode.Impulse);

        //front wheel steering
        wheelFL.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;
        wheelFR.steerAngle = Input.GetAxis("Horizontal") * maxTurnAngle;

        //apply deceleration when not pressing the gas or when breaking in either direction.
        if ((Input.GetAxis("Vertical") <= -0.5f && localVelocity.z > 0) || (Input.GetAxis("Vertical") >= 0.5f && localVelocity.z < 0))
        {
            wheelBL.brakeTorque = decelerationTorque + maxTorque;
            wheelBR.brakeTorque = decelerationTorque + maxTorque;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            wheelBL.brakeTorque = decelerationTorque;
            wheelBR.brakeTorque = decelerationTorque;
        }
        else
        {
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }

        //Handbrake controls
        if (Input.GetButton("Jump"))
        {
            applyHandbrake = true;
            wheelFL.brakeTorque = maxBrakeTorque;
            wheelFR.brakeTorque = maxBrakeTorque;
            //Wheels are locked, so power slide!
            if (GetComponent<Rigidbody>().velocity.magnitude > 1)
            {
                SetSlipValues(handbrakeForwardSlip, handbrakeSidewaysSlip);
            }
            else //skid to a stop, regular friction enabled.
            {
                SetSlipValues(1f, 1f);
            }

        }
        else
        {
            applyHandbrake = false;
            wheelFL.brakeTorque = 0;
            wheelFR.brakeTorque = 0;
            SetSlipValues(1f, 1f);

        }


        if (!applyHandbrake && ((Input.GetAxis("Vertical") <= -0.5f && localVelocity.z > 0) || (Input.GetAxis("Vertical") >= 0.5f && localVelocity.z < 0)))
        {
            wheelBL.brakeTorque = decelerationTorque + maxTorque;
            wheelBR.brakeTorque = decelerationTorque + maxTorque;
        }
        else if (!applyHandbrake && Input.GetAxis("Vertical") == 0)
        {
            wheelBL.brakeTorque = decelerationTorque;
            wheelBR.brakeTorque = decelerationTorque;
        }
        else
        {
            wheelBL.brakeTorque = 0;
            wheelBR.brakeTorque = 0;
        }

        //Handbrake slip



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

    void Update()
    {
        //rotate the wheels based on RPM
        float rotationThisFrame = 360 * Time.deltaTime;
        wheelTransformFL.Rotate(-wheelBR.rpm / rotationThisFrame, 0, 0);
        wheelTransformFR.Rotate(-wheelBR.rpm / rotationThisFrame, 0, 0);
        wheelTransformBL.Rotate(-wheelBR.rpm / rotationThisFrame, 0, 0);
        wheelTransformBR.Rotate(-wheelBR.rpm / rotationThisFrame, 0, 0);

        UpdateWheelPositions();
    }

    void SetSlipValues(float forward, float sideways)
    {
        //Change the stiffness values of wheel friction curve and then reapply it.
        WheelFrictionCurve tempStruct = wheelBR.forwardFriction;
        tempStruct.stiffness = forward;
        wheelBR.forwardFriction = tempStruct;

        tempStruct = wheelBR.sidewaysFriction;
        tempStruct.stiffness = sideways;
        wheelBR.sidewaysFriction = tempStruct;

        tempStruct = wheelBL.forwardFriction;
        tempStruct.stiffness = forward;
        wheelBL.forwardFriction = tempStruct;

        tempStruct = wheelBL.sidewaysFriction;
        tempStruct.stiffness = sideways;
        wheelBL.sidewaysFriction = tempStruct;
    }


}
