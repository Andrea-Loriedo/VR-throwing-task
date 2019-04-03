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

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(-rb.velocity);
    }
}
