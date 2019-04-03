using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAlongVelocity : MonoBehaviour
{

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
