using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{   
    public class DartControl : MonoBehaviour
    {
        public FloorCollision floor;
        public SteamVR_Action_Boolean grabAction;
        public Hand hand;
        public Transform controllerAttachmentPoint;
        public Transform gloveAttachmentPoint;
        public GameObject prefabToGrab;
        public AudioSource audioFX;
        public VRGloveController glove;

        // UXF
        public Session session;
        public ExperimentManager experiment;

        Collider dartCollider;
        Rigidbody dartRB;
        Follower currentFollower;

        int dartsLeft;

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (grabAction == null) // If grab action undefined
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No grab action assigned");
                return;
            }
            if(glove.GloveEnabled())
            {
                grabAction.AddOnChangeListener(OnGrabActionChange,SteamVR_Input_Sources.RightHand); // Use right hand if glove enabled
            }
            else
            {
                grabAction.AddOnChangeListener(OnGrabActionChange, hand.handType);
            }
        }

        private void OnDisable()
        {
            if (grabAction != null && glove.GloveEnabled()) // If grab action defined
            {
                grabAction.RemoveOnChangeListener(OnGrabActionChange, SteamVR_Input_Sources.RightHand); // Use right hand if glove enabled
            }
            else if(grabAction != null && !glove.GloveEnabled())
            {
                grabAction.RemoveOnChangeListener(OnGrabActionChange, hand.handType);
            }
        }

        private void OnGrabActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                DestroyDart(); // Destroy any leftover dart
                if(session.hasInitialised && (session.nextTrial.numberInBlock == 1 || session.currentTrial.status == UXF.TrialStatus.Done))
                {
                    experiment.StartNextTrial();
                    GrabDart();
                }
            }
            else
            {
                ReleaseDart();
            }
        }

        void GrabDart()
        {
            prefabToGrab.SetActive(true); // Enable dart object
            GameObject dart = Instantiate(prefabToGrab); // Create new instance of the dart prefab
            if(dart!= null)
            dartCollider = dart.GetComponent<Collider>();
            dartCollider.enabled = false; // Disable dart collider
            currentFollower = dart.GetComponent<Follower>();
            if(glove.GloveEnabled())
            {
                currentFollower.AttachTo(gloveAttachmentPoint); // Attach dart to glove
            }
            else
            {
                currentFollower.AttachTo(controllerAttachmentPoint); // Attach dart to hand
            }
        }

        void ReleaseDart()
        {
            if(currentFollower != null)
            {
                currentFollower.Detach();
                currentFollower.GetComponent<Collider>().enabled = true;
                currentFollower.GetComponent<RotateAlongVelocity>().enabled = true;
                currentFollower = null;
                audioFX.Play();
                dartsLeft = session.settings.GetInt("trials_per_block") - session.currentTrial.numberInBlock; 
            }
        }

        public void DestroyDart()
        {
            // check existing dart
            if(currentFollower != null)
            {
                Destroy(currentFollower.gameObject); // Destroy previous dart if two darts are instantiated simultaneously
            }
        }

        public int GetDartsLeft()
        {
            return dartsLeft;
        }

        public void SetDartsLeft(int number)
        {
            dartsLeft = number;
        }

        public void EndBehaviour(Trial endedTrial)
        {
            if (endedTrial == session.lastTrial)
            {
                session.End();
            }
            else
            {
                
            }
        }
    }
}