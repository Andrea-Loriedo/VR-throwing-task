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
        // modeInstructionsPlaceholder.transform.parent = spawnPoint;
        // transform.parent = modeInstructionsPlaceholder.transform; 
        transform.localPosition = spawnPoint.localPosition + distanceInstructions.transform.localPosition;
    }

    public void ShowInstructions(String distance, String targetMode)
    {
        if(distance == "Close" && targetMode == "Still")
        {
            distanceInstructions.SetActive(false);
            targetModeInstructions.SetActive(false);
        }
        else if(distance == "Close" && targetMode == "Move")
        {
            RelocateInstructionsPanel(spawnPointClose);
            distanceInstructions.SetActive(false);
            targetModeInstructions.SetActive(true);
        }
        else if(distance == "Far" && targetMode == "Still")
        {
            RelocateInstructionsPanel(spawnPointFar);
            distanceInstructions.SetActive(true);
            targetModeInstructions.SetActive(false);
        }
        else if(distance == "Far" && targetMode == "Move")
        {
            RelocateInstructionsPanel(spawnPointClose);
            distanceInstructions.SetActive(false);
            targetModeInstructions.SetActive(true);
        }
    }
}
