using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

public class ExperimentManager : MonoBehaviour
{
    // UXF
    public Session session;
    public ExperimentSettings settings;  
    public DartsInstructionsManager instructions;

    public void StartNextTrial()
    {
        session.nextTrial.Begin();
        Debug.LogFormat("Started trial {0}", session.currentTrialNum);
        settings.distance = session.currentTrial.settings["distance"].ToString();
        settings.targetMode = session.currentTrial.settings["target_mode"].ToString();
        instructions.ShowInstructions(settings.distance, settings.targetMode);
    }

    public void EndCurrentTrial()
    {
        session.currentTrial.End();
        Debug.LogFormat("Ended trial {0}", session.currentTrialNum);
    }

    public ExperimentSettings GetExperimetSettings()
    {
        return settings;
    }

    public struct ExperimentSettings
    {
        public string distance;
        public string targetMode;
    }
}
