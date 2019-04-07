using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DartControl : MonoBehaviour
{
    private AudioSource audioData;
    Collider dartCollider;
    Rigidbody rb;
    bool thrown;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        dartCollider = GameObject.Find("Dart").GetComponent<CapsuleCollider>();
        rb = GameObject.Find("Dart").GetComponent<Rigidbody>();
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
