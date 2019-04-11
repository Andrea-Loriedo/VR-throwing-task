using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class VRGloveController : MonoBehaviour
    {
        [HideInInspector]
        public bool gloveEnabled;

        public GameObject glove;
        public ExperimentManager experiment;

        public void SetGloveStatus()
        {
            EnableGlove(experiment.GetParticipantDetails()); // Get the participant details struct
            if(gloveEnabled == true)
            {
                glove.SetActive(true);
            }
        }

        public bool GloveEnabled()
        {
            return gloveEnabled;
        }

        public void EnableGlove(ExperimentManager.ParticipantDetails participantDetails)
        {
            gloveEnabled = participantDetails.gloveOn;
        }
    }
}
