using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartsInstructionsManager : MonoBehaviour
    {
        public GameObject distanceInstructions;
        public GameObject targetModeInstructions;
        public Transform spawnPointClose;
        public Transform spawnPointFar;

        // UXF
        public Session session;
        public ExperimentManager experiment;

        GameObject modeInstructionsPlaceholder; // Empty game object to ensure dart scale and rotation are preserved


        // Start is called before the first frame update
        void Start()
        {
            targetModeInstructions.SetActive(false);
            distanceInstructions.SetActive(false);
            RelocateInstructionsPanel(spawnPointClose);
        }

        void RelocateInstructionsPanel(Transform spawnPoint)
        {
            transform.localPosition = spawnPoint.localPosition + new Vector3 (-0.6f, 1.4f, 0.7f);
        }

        public void ShowInstructions(ExperimentManager.BlockSettings settings)
        {
            if(settings.distance == "Close" && settings.targetMode == "Still")
            {
                distanceInstructions.SetActive(false);
                targetModeInstructions.SetActive(false);
            }
            else if(settings.distance == "Close" && settings.targetMode == "Move")
            {
                RelocateInstructionsPanel(spawnPointClose);
                distanceInstructions.SetActive(false);
                targetModeInstructions.SetActive(true);
            }
            else if(settings.distance == "Far" && settings.targetMode == "Still")
            {
                RelocateInstructionsPanel(spawnPointFar);
                distanceInstructions.SetActive(true);
                targetModeInstructions.SetActive(false);
            }
            else if(settings.distance == "Far" && settings.targetMode == "Move")
            {
                RelocateInstructionsPanel(spawnPointFar);
                distanceInstructions.SetActive(false);
                targetModeInstructions.SetActive(true);
            }
        }
    }
}
