using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmKinematicsController : MonoBehaviour
{

    public Limb[] arm; // Array of instances of the limb struct (one element for each joint + link)

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
            Vector3 offset = limblink.previousJoint.rotation * new Vector3(0, 0, -limblink.link_length); // Move down along the arm to position joints
            limblink.joint.position = limblink.previousJoint.position + offset; // Update position based on the position of the previous joint and the link length
            limblink.link.position = (limblink.joint.position + limblink.previousJoint.position) / 2f; // PLace link model at midpoint between each joint
            limblink.link.rotation = limblink.previousJoint.rotation; // Align link along the joint axes
        }
    }

    void ScaleLinks(Limb[] limb)
    {
        Vector3 linkLength = transform.localScale;

        foreach (Limb limblink in limb)
        {
            linkLength.z = limblink.link_length * 5;
            limblink.link.localScale = linkLength;
            // limblink.link.localScale = new Vector3(limblink.link_length, 0, 0);
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
