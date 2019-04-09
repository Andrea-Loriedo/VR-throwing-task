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
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.useGravity = false;
            ve.BeginEstimatingVelocity();
        }

        public void Detach()
        {
            this.target = null; 
            rb.useGravity = true;

            rb.velocity = ve.GetVelocityEstimate() * velRatio;
            rb.angularVelocity = ve.GetAngularVelocityEstimate();

            ve.FinishEstimatingVelocity();
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
    }
}
