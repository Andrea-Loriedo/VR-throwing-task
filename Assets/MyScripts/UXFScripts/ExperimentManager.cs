﻿using System.Collections;
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
        public BullseyeController bullseye;
        public Follower dart;
        bool sessionHasEnded;
        bool newBlockStart;

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
            RecordResults(bullseye.GetBullseyeResults(), dart.GetKinematicResults());
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
            ppDetails.age = (int) session.participantDetails["participant_age"];
            ppDetails.gender = (string) session.participantDetails["participant_gender"];
            ppDetails.gloveOn = (bool) session.participantDetails["glove_on"];
        }

        public void RecordResults(BullseyeController.TrialResults bullseyeResults, Follower.KinematicResults kinematicResults)
        {
            // Behavioural results
            session.currentTrial.result["target_zone_hit"] = bullseyeResults.targetZone;
            session.currentTrial.result["total_score"] = bullseyeResults.totalScore;

            // Kinematic results
            session.currentTrial.result["vel_at_release_x"] = kinematicResults.velocityAtReleaseX;
            session.currentTrial.result["vel_at_release_y"] = kinematicResults.velocityAtReleaseX;
            session.currentTrial.result["vel_at_release_z"] = kinematicResults.velocityAtReleaseX;
            session.currentTrial.result["ang_vel_at_release_x"] = kinematicResults.angVelocityAtReleaseX;
            session.currentTrial.result["ang_vel_at_release_y"] = kinematicResults.angVelocityAtReleaseX;
            session.currentTrial.result["ang_vel_at_release_z"] = kinematicResults.angVelocityAtReleaseX;
        }

        public void MarkBlockBegin()
        {
            if(session.currentTrial.numberInBlock == 1)
            {
                newBlockStart = true;
                bullseye.ResetScore(); // Reset the score after each block
            }
            else
            {
                newBlockStart = false;
            }

        }

        public void SetBlockBegin(bool value)
        {
            newBlockStart = value;
        }

        public bool NewBlockBegin()
        {
            return newBlockStart;
        }

        public void MarkSessionEnd()
        {
            sessionHasEnded = true;
        }

        public bool SessionHasEnded()
        {
            return sessionHasEnded;
        }

        // Structs for session parameters
        public struct BlockSettings
        {
            public string distance;
            public string targetMode;
        }

        public struct ParticipantDetails
        {
            public int age;
            public string gender;
            public bool gloveOn;
        }
    }
}
