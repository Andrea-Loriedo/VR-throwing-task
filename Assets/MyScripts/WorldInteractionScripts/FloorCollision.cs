using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FloorCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("DartObject"))
        {
            Destroy(col.gameObject);
        }
    }

}
