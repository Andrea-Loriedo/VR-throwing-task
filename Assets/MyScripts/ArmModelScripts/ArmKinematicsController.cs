using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmKinematicsController : MonoBehaviour
{

    public Limb[] arm;

    // Update is called once per frame
    void Update()
    {   
        MoveLimb(arm);
    }


    void MoveLimb(Limb[] limb)
    {
        foreach (Limb limblink in limb)
        {
            Vector3 offset = limblink.previousJoint.rotation * new Vector3(limblink.link_length, 0, 0);
            limblink.joint.position = limblink.previousJoint.position + offset;
            limblink.link.position = (limblink.joint.position + limblink.previousJoint.position) / 2f;
            limblink.link.rotation = limblink.previousJoint.rotation;
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
