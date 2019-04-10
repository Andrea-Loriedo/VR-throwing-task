using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UXF;

public class SessionGenerator : MonoBehaviour
{     
    public void GenerateExperiment(Session experimentSession)
    {
        // Retrieve the n_blockX_trials setting from the session settings
        int n_block1Trials = experimentSession.settings.GetInt("n_block1_trials");
        int n_block2Trials = experimentSession.settings.GetInt("n_block2_trials");
        int n_block3Trials = experimentSession.settings.GetInt("n_block3_trials");
        int n_block4Trials = experimentSession.settings.GetInt("n_block4_trials");
        // Create block 1
        Block Block1 = experimentSession.CreateBlock(n_block1Trials); // Block 1 (stationary target, close)
        Block Block2 = experimentSession.CreateBlock(n_block1Trials); // Block 2 (moving target, close)
        Block Block3 = experimentSession.CreateBlock(n_block1Trials); // Block 3 (stationary target, far)
        Block Block4 = experimentSession.CreateBlock(n_block1Trials); // Block 4 (moving target, far)
    }
}