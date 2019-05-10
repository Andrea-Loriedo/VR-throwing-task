using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ArmKinematicsController : MonoBehaviour
{

    public Limb[] arm; // Array of instances of the limb struct (one element for each joint + link)
    public float[] userLinkLength;

    // UXF
    public Session session;

    // Update is called once per frame

    void Start()
    {
        retrieveLinkLengths();
    }

    void retrieveLinkLengths()
    {
        userLinkLength[0] = (float) session.participantDetails["wrist_length"];
        userLinkLength[1] = (float) session.participantDetails["lower_arm_length"];
        userLinkLength[2] = (float) session.participantDetails["upper_arm_length"];
    }

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
            foreach (float linkL in userLinkLength)
            {
                linkLength.z = linkL * 5f;
            }
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
