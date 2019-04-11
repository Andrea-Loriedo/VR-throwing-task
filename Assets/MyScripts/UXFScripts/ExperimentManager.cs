using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class ExperimentManager : MonoBehaviour
    {
        // UXF
        public Session session;
        public BlockSettings settings;  
        public DartsInstructionsManager instructions;
        public DartsOptions options;

        public void StartNextTrial()
        {
            session.nextTrial.Begin();
            Debug.LogFormat("Started trial {0}", session.currentTrialNum);
            settings.distance = session.currentTrial.settings["distance"].ToString();
            settings.targetMode = session.currentTrial.settings["target_mode"].ToString();
            instructions.ShowInstructions(settings);
            options.ApplyBlockSettings(settings);
        }

        public void EndCurrentTrial()
        {
            session.currentTrial.End();
            Debug.LogFormat("Ended trial {0}", session.currentTrialNum);
        }

        public BlockSettings GetBlockSettings()
        {
            return settings;
        }

        public struct BlockSettings
        {
            public string distance;
            public string targetMode;
        }
    }
}
