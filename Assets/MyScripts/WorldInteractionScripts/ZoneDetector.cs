using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ZoneDetector : MonoBehaviour
    {
        private bool playingDarts;
        public GameObject dartsZone;
        public GameObject midZone;
        Hand hand;

        void Update()
        {
            DetectZone();
        }

        private void OnEnable()
        {
            if (hand == null)
            {
                hand = this.GetComponent<Hand>();
            }
        }

        void DetectZone()
        {
            float distDarts = Vector3.Distance(hand.transform.position, dartsZone.transform.position);
            float distMid = Vector3.Distance(hand.transform.position, midZone.transform.position);

            if(distDarts > 0 && distDarts < 2)
            {
                playingDarts = true;
            }
            else if(distMid > 0 && distMid < 2)
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