using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

    public bool swordDraw = true;
    public bool movingForward = true;
    public bool movingBack = false;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public Quaternion Quaternion1;
    public Quaternion Quaternion2;

    public void SwordSwing(GameObject swordL, GameObject swordR)
    {
        if (swordDraw)
        {
            Quaternion1 = Quaternion.Euler(0, 90, 0);
            swordL.transform.rotation = Quaternion.RotateTowards(swordL.transform.rotation, Quaternion1, (300 * Time.deltaTime));
            Quaternion2 = Quaternion.Euler(0, -90, 0);
            swordR.transform.rotation = Quaternion.RotateTowards(swordR.transform.rotation, Quaternion2, (300 * Time.deltaTime));
            //swordL.transform.Rotate(90, 0, 0);
            //swordR.transform.Rotate(-90, 0, 0);
        } if (swordL.transform.rotation == Quaternion1 && swordR.transform.rotation == Quaternion2)
        {
            swordDraw = false;
        }
    }

    public void SpearThrust(GameObject SpearR, GameObject SpearT)
    {
        if ((SpearR.transform.position + (SpearR.transform.forward * 1f)) != (SpearT.transform.position + (SpearT.transform.forward * 1f)))
            SpearR.transform.position = Vector3.Lerp(SpearR.transform.position, (SpearR.transform.position + (SpearR.transform.forward * 1f)), .1f);
    }

    public void SpearReturn(GameObject SpearR, GameObject SpearT)
    {
        SpearR.transform.position = SpearT.transform.position;
    }

    public void ShieldBounce(Rigidbody Target)
    {
        Target.velocity = (Target.transform.forward * -3f);
        //Target.transform.InverseTransformDirection(Target.velocity);
    }


}

            //if (SpearR.transform.position == (SpearR.transform.position + (SpearR.transform.forward* 1f)))
            //{
            //    movingBack = true;
            //    movingForward = false;
            //}
            //else if (movingBack)
            //{
            //    SpearR.transform.position = Vector3.Lerp(SpearR.transform.position, (SpearR.transform.position + (SpearR.transform.forward* -1f)), .1f);
            //}