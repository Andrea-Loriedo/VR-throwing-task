using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UXF;

public class DartsInstructionsManager : MonoBehaviour
{
    public GameObject distanceInstructions;
    public GameObject targetModeInstructions;
    public Transform spawnPointClose;
    public Transform spawnPointFar;
    public Session session;
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
        if(modeInstructionsPlaceholder == null){
            modeInstructionsPlaceholder = new GameObject();
        } 
        modeInstructionsPlaceholder.transform.parent = spawnPoint;
        targetModeInstructions.transform.parent = modeInstructionsPlaceholder.transform; 
        targetModeInstructions.transform.localPosition += spawnPoint.localPosition;
    }

    public void ShowInstructions()
    {
        foreach (Trial trial in session.trials)
        { 
            string distance = trial.settings.GetString("distance");
            string targetMode = trial.settings.GetString("target_mode");

            if(distance == "Close" && targetMode == "Still")
            {
                distanceInstructions.SetActive(false);
                targetModeInstructions.SetActive(false);
            }
            else if(distance == "Close" && targetMode == "Move")
            {
                distanceInstructions.SetActive(false);
                RelocateInstructionsPanel(spawnPointClose);
                targetModeInstructions.SetActive(true);
            }
            else if(distance == "Far" && targetMode == "Still")
            {
                distanceInstructions.SetActive(true);
                targetModeInstructions.SetActive(false);
            }
            else if(distance == "Far" && targetMode == "Move")
            {
                distanceInstructions.SetActive(false);
                RelocateInstructionsPanel(spawnPointFar);
                targetModeInstructions.SetActive(false);
            }
        }
    }
}
