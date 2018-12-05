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

    public GameObject SpearNShield;
    public GameObject SpearL;
    public GameObject SpearT;
    public GameObject ShieldR;

    public GameObject Swords;
    public GameObject SwordL;
    public GameObject SwordR;

    //public GameObject SpearR;

    public bool isAttacking;
    public bool spearEq;
    public bool swordEq;


    public float currentSpeed = 0;
    public float topSpeed = 150;
    public float maxReverseSpeed = -50;
    public float maxTurnAngle = 10;
    public float maxTorque = 10;
    public Vector3 centerOfMassAdjustment = new Vector3(0f, -0.9f, 0f);

    public float groundDist = 0f;



    // Use this for initialization
    void Start() {
        //lower center of mass for roll-over resistance
        body = GetComponent<Rigidbody>();
        body.centerOfMass += centerOfMassAdjustment;
        SwordSwing(SwordL, SwordR);
    }

    // Update is called once per frame
    void Update()
    {
        
        groundDist = GroundRayCast();
        //rotate the wheels based on RPM
        float rotationThisFrame = 360 * Time.deltaTime;
        wheelTransformFR.Rotate(wheelBR.rpm / rotationThisFrame, 0, 0);
        SwordSwing(SwordL, SwordR);

        if (GetComponentInChildren<WeaponScript>().shieldHit)
        {
            GetComponentInChildren<CharacterScript>().ShieldBounce(body);
            GetComponentInChildren<WeaponScript>().shieldHit = false;
        }

        if (Input.GetKeyDown("1"))
        {
            SpearNShield.SetActive(true);
            Swords.SetActive(false);
            spearEq = true;
            swordEq = false;
        }

        if (Input.GetKeyDown("2"))
        {
            SpearNShield.SetActive(false);
            Swords.SetActive(true);
            spearEq = false;
            swordEq = true;
        }

        if (swordEq) { 
            if (Input.GetButton("Fire1") && Mathf.Abs(GroundRayCast()) > 0f)
            {
                isAttacking = true;
                body.constraints = RigidbodyConstraints.FreezePosition;
                body.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                transform.RotateAround(transform.position, Vector3.up, 1080.0f * Time.deltaTime / 1f);
            }
        if (Input.GetButtonUp("Fire1") && Mathf.Abs(GroundRayCast()) > 0f)
            {
                isAttacking = false;
                body.constraints = RigidbodyConstraints.FreezePosition;
            body.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        }
    }
        if (spearEq){
            if (Input.GetButton("Fire1") && Mathf.Abs(GroundRayCast()) > 0f)
            {
                isAttacking = true;
                SpearThrust(SpearL,SpearT);
            }
            if (Input.GetButtonUp("Fire1") && Mathf.Abs(GroundRayCast()) > 0f)
            {
                isAttacking = false;
                SpearReturn(SpearL, SpearT);
            }
            }

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

    public float GroundRayCast()
    {
        RaycastHit hit;
        Vector3 wheelCenter = transform.position + (transform.forward * .5f);
        Debug.DrawRay(wheelCenter, transform.up * -.4f );
        //if we detect a car infront of us, slow down or even reverse based on distance.
        if ((Physics.Raycast(wheelCenter, transform.up, out hit, -.4f))==false)
        {
            return (((wheelCenter - hit.point).magnitude / .4f));
        }
        //otherwise no change
         return 0f;
    }

    //IEnumerator Rotate(float duration)
    //{
    //    float startRotation = transform.eulerAngles.y;
    //    float endRotation = startRotation + 360.0f;
    //    float t = 0.0f;
    //    while (t < duration)
    //    {
    //        t += Time.deltaTime;
    //        float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
    //        transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
    //        yield return null;
    //    }
    //}

}
