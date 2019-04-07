using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class RotateAlongVelocity : MonoBehaviour
    {
        // Script to make the dart rotate along the velocity vector

        Rigidbody rb;
        BullseyeController bullseye;
        Quaternion rotationOffset;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rotationOffset = Quaternion.Euler(0, 180, 0);
        }

        // FixedUpdate has the frequency of the physics system
        void FixedUpdate()
        {   

            try
            {
                if(rb.velocity != null) // If the dart is flying
                transform.rotation = Quaternion.LookRotation(-rb.velocity); // Constrain the dart to facing the dartboard

            }
            catch (System.FormatException e)
            {
                
            }
        }
    }
}
