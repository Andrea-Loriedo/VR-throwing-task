﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DartControl : MonoBehaviour
{
    public AudioSource audioData;
    Collider dartCollider;
    Rigidbody rb;
    bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        dartCollider = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnDartThrow()
    {
        audioData.Play();
        thrown = true;
    }

    public bool Thrown()
    {
        return thrown;
    }

    public void Respawned(bool value)
    {
        thrown = !value;
    }
}
