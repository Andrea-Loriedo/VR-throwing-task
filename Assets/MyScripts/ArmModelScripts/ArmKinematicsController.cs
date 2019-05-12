using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ArmKinematicsController : MonoBehaviour
    {

        public Limb[] arm; // Array of instances of the limb struct (one element for each joint + link)
        public float[] userLinkLength;
        public Transform handJoint;

        // UXF
        public Session session;
        public ExperimentManager experiment;
        public ArmResults results;

        // Update is called once per frame

        void Start()
        {
            //retrieveLinkLengths(experiment.GetParticipantDetails());
        }

        void retrieveLinkLengths(ExperimentManager.ParticipantDetails participantDetails)
        {
            userLinkLength = new float[3];

            userLinkLength[0] = participantDetails.wristLength;
            userLinkLength[1] = participantDetails.lowerArmLength;
            userLinkLength[2] = participantDetails.upperArmLength;
        }

        void Update()
        {   
            ScaleLinks(arm);
            MoveLimb(arm);
            calculateJointAngles();
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

        void calculateJointAngles()
        {
            float handPitch = 360 - handJoint.rotation.eulerAngles.x;
            float wristPitch = 360 - arm[0].joint.rotation.eulerAngles.x;
            float elbowPitch = 360 - arm[1].joint.rotation.eulerAngles.x;

            if(handPitch >180)
            handPitch -= 360;
            if(wristPitch > 180)
            wristPitch -= 360;
            if(elbowPitch > 180)
            elbowPitch -= 360;

            results.wristAngle = wristPitch - handPitch;
            results.elbowAngle = elbowPitch - wristPitch;
            results.shoulderAngle = elbowPitch;
            // Debug.LogFormat("Wrist: {0}, Elbow: {1}, Shoulder: {2}", results.wristAngle, results.elbowAngle, results.shoulderAngle);
            Debug.LogFormat("W: {0}, E: {1}, S: {2}", handPitch, wristPitch, elbowPitch);
        }

        // void ScaleLinks(Limb[] limb)
        // {
        //     Vector3 linkLength = transform.localScale;
        //     for(int i = 0; i < 3; i++)
        //     {          
        //         linkLength.z = userLinkLength[i] * 5f;
        //         arm[i].link.localScale = linkLength;
        //         // limblink.link.localScale = new Vector3(limblink.link_length, 0, 0);
        //     }
        // }

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

        public ArmResults GetArmResults()
        {
            return results;
        }

        [System.Serializable]
        public struct Limb
        {
            public string internalName;
            public Transform previousJoint;
            public Transform joint;
            public Transform link;
            //[HideInInspector]
            public float link_length;
        }

        public struct ArmResults
        {
            public float wristAngle;
            public float elbowAngle;
            public float shoulderAngle;
        }

    }
}
