using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Follower : MonoBehaviour
    {
        public float velRatio = 1f;
        public Transform target;
        Rigidbody rb;
        VelocityEstimator ve;
        KinematicResults results;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            ve = GetComponent<VelocityEstimator>();
        }

        public void AttachTo(Transform target)
        {
            this.target = target;
            transform.position = target.position;
            transform.rotation = target.rotation;
            rb.velocity = Vector3.zero; // Follow target velocity
            rb.angularVelocity = Vector3.zero; // Follow target angular velocity
            rb.useGravity = false;
            ve.BeginEstimatingVelocity();
        }

        public void Detach()
        {
            this.target = null; 
            if(rb != null)
            {            
                rb.useGravity = true;
                rb.velocity = ve.GetVelocityEstimate() * velRatio;
                rb.angularVelocity = ve.GetAngularVelocityEstimate();       
                ve.FinishEstimatingVelocity();
                LogResults(ve.GetVelocityEstimate() * velRatio, ve.GetAngularVelocityEstimate());
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {   
            if (target != null)
            {
                rb.MovePosition(target.position);
                rb.MoveRotation(target.rotation);
            }
        }

        void LogResults(Vector3 velocity, Vector3 angularVelocity)
        {
            results.velocityAtReleaseX = velocity.x;
            results.velocityAtReleaseX = velocity.y;
            results.velocityAtReleaseX = velocity.z;
            results.angVelocityAtReleaseX = angularVelocity.x;
            results.angVelocityAtReleaseX = angularVelocity.y;
            results.angVelocityAtReleaseX = angularVelocity.z;

        }

        public KinematicResults GetKinematicResults()
        {
            return results;
        }

        public struct KinematicResults
        {
            public float velocityAtReleaseX;
            public float velocityAtReleaseY;
            public float velocityAtReleaseZ;
            public float angVelocityAtReleaseX;
            public float angVelocityAtReleaseY;
            public float angVelocityAtReleaseZ;
        }
    }
}
