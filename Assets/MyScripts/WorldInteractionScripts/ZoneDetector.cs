using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDetector : MonoBehaviour
{
    private bool playingDarts;

    void OnTriggerEnter(Collider zone)
    {
        if(zone.CompareTag("Darts") || zone.CompareTag("Middle"))
        {
            playingDarts = true;
            Debug.LogFormat("Playing darts");
        }
        else
        {
            playingDarts = false;
            Debug.LogFormat("Not playing darts");
        }
    }

    public string GetZone()
    {
        if(playingDarts == true)
        {
            return "Darts";
        }
        else
        {
            return null;
        }
    }
}
