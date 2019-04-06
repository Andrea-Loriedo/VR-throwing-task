using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAlongVelocity : MonoBehaviour
{
    // Script to make the dart rotate along the velocity vector

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate has the frequency of the physics system
    void FixedUpdate()
    {   
        // Constrain the dart to facing the dartboard
        transform.rotation = Quaternion.LookRotation(-rb.velocity);
    }
}
