using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FloorCollision : MonoBehaviour
{
    private bool bullseyeMissed;

    void Start()
    {
        bullseyeMissed = false;
    }

    public bool MissDetected()
    {
        return bullseyeMissed;
    }

    public void HitStateReset(bool state)
    {
        bullseyeMissed = state;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("DartObject"))
        {
            Destroy(col.gameObject);
            bullseyeMissed = true;
        }
    }

}
