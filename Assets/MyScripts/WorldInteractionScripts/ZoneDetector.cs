using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ZoneDetector : MonoBehaviour
    {
        private bool playingDarts;
        public GameObject dartsZoneClose;
        public GameObject dartsZoneFar;

        int zoneRange;

        void Start()
        {
            zoneRange = 3;
        }

        void Update()
        {
            DetectZone();
        }

        void DetectZone()
        {
            float distDarts = Vector2.Distance(this.transform.position, dartsZoneClose.transform.position);
            float distMid = Vector2.Distance(this.transform.position, dartsZoneFar.transform.position);

            if(distDarts >= 0 && distDarts < zoneRange)
            {
                playingDarts = true;
            }
            else if(distMid >= 0 && distMid < zoneRange)
            {
                playingDarts = true;
            }
            else
            {
                playingDarts = false;
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
}