﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ActionSetManager : MonoBehaviour
    {
        public ZoneDetector zone;
        string game;

        public SteamVR_ActionSet dartsActionSet = SteamVR_Input.GetActionSet("dartsplaying");
        public SteamVR_Input_Sources forSources = SteamVR_Input_Sources.Any;

        bool inZone;

        [HideInInspector]
        public bool disableAllOtherActionSets = false;

        // Start is called before the first frame update
        void Start()
        {
            inZone = true;
        }

        // Update is called once per frame
        void Update()
        {
            CheckPLayerLocation();
            SelectActionSet();
        }

        void SelectActionSet()
        {
            if(inZone == true)
            {
                //Debug.Log(string.Format("[SteamVR] Activating {0} action set.", actionSet.fullPath));
                dartsActionSet.Activate(forSources, 0, disableAllOtherActionSets);
            }
            else
            {
                dartsActionSet.Deactivate(forSources); // Disable darts action set
            }
        }

        void CheckPLayerLocation()
        {
            if(zone.GetZone() == "Darts" && dartsActionSet != null)
            {
                inZone = true;
            }
            else
            {
                inZone = false;
            }
        }
    }
}