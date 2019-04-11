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
        public ParticipantDetails ppDetails;

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

        public ParticipantDetails GetParticipantDetails()
        {
            return ppDetails;
        }

        public BlockSettings GetBlockSettings()
        {
            return settings;
        }

        public void RetrieveParticipantDetails()
        {
            ppDetails.gloveOn = (bool) session.participantDetails["glove_on"];
        }

        // Structs for session parameters
        public struct BlockSettings
        {
            public string distance;
            public string targetMode;
        }

        public struct ParticipantDetails
        {
            public bool gloveOn;
        }
    }
}
