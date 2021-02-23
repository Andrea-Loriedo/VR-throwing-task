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
        [HideInInspector]
        public bool armEnabled;

        public GameObject glove;
        public GameObject arm; 
        public ExperimentManager experiment;

        uint index = 0;

        void Start()
        {
            // var error = ETrackedPropertyError.TrackedProp_Success;
            // for (uint i = 0; i < 16; i++)
            // {
            //     var result = new System.Text.StringBuilder((int)64);
            //     OpenVR.System.GetStringTrackedDeviceProperty(i, ETrackedDeviceProperty.Prop_RenderModelName_String, result, 64, ref error);
            //     if (result.ToString().Contains("Tracker"))
            //     {
            //         index = i;
            //         break;
            //     }
            // }

            // glove.GetComponent<SteamVR_TrackedObject>().index = (SteamVR_TrackedObject.EIndex)index;
        }

        public void SetGloveStatus()
        {
            EnableGlove(experiment.GetParticipantDetails()); // Get the participant details struct
            if(gloveEnabled == true)
            {
                glove.SetActive(true);
            }
            else
            {
                glove.SetActive(false);
            }
        }
        
        public void SetArmStatus()
        {
            EnableArm(experiment.GetParticipantDetails()); // Get the participant details struct
            if(armEnabled == true)
            {
                arm.SetActive(true);
            }
            else
            {
                arm.SetActive(false);
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

        public void EnableArm(ExperimentManager.ParticipantDetails participantDetails)
        {
            armEnabled = participantDetails.armOn;
        }
    }
}
