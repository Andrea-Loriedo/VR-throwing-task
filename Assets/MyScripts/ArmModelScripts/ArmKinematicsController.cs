using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmKinematicsController : MonoBehaviour
{

    public Limb[] arm;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        ScaleLinks(arm);
        MoveLimb(arm);
    }

    void MoveLimb(Limb[] limb)
    {
        foreach (Limb limblink in limb)
        {
            Vector3 offset = limblink.previousJoint.rotation * new Vector3(0, 0, -limblink.link_length); // Move down along the arm to locate joints
            limblink.joint.position = limblink.previousJoint.position + offset; // Update position
            limblink.link.position = (limblink.joint.position + limblink.previousJoint.position) / 2f; // PLace link model at midpoint between joints
            limblink.link.rotation = limblink.previousJoint.rotation; // Align link along the joint axes
        }
    }

    void ScaleLinks(Limb[] limb)
    {
        Vector3 linkLength = transform.localScale;

        foreach (Limb limblink in limb)
        {
            linkLength.x = limblink.link_length * 5;
            limblink.link.localScale = linkLength;
        }
    }


    [System.Serializable]
    public struct Limb
    {
        public string internalName;
        public Transform previousJoint;
        public Transform joint;
        public Transform link;
        public float link_length;
    }

}
