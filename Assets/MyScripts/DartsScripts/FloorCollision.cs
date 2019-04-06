using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FloorCollision : MonoBehaviour
{
    [HideInInspector]
    public bool hitFloor;

    void Start()
    {
        hitFloor = false;
    }

    void OnTriggerEnter()
    {
        hitFloor = true;
    }

    public void SetHit(bool hit)
    {
        hitFloor = hit;
    }

    public bool GetHit()
    {
        return hitFloor;
    }
}
