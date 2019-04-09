using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{   
    public class DartControl : MonoBehaviour
    {
        public FloorCollision floor;
        public SteamVR_Action_Boolean grabAction;
        public Hand hand;
        public Transform attachmentPoint;
        public GameObject prefabToGrab;
        public AudioSource audioFX;

        Collider dartCollider;
        Rigidbody dartRB;
        Follower currentFollower;

        void Start() 
        {

        }

        private void OnEnable()
        {
            if (hand == null)
                hand = this.GetComponent<Hand>();

            if (grabAction == null) // If grab action undefined
            {
                Debug.LogError("<b>[SteamVR Interaction]</b> No grab action assigned");
                return;
            }
            grabAction.AddOnChangeListener(OnGrabActionChange, hand.handType);
        }

        private void OnDisable()
        {
            if (grabAction != null) // If grab action defined
            grabAction.RemoveOnChangeListener(OnGrabActionChange, hand.handType);
        }

        private void OnGrabActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
        {
            if (newValue)
            {
                GrabDart();
            }
            else
            {
                ReleaseDart();
            }
        }

        void GrabDart()
        {
            DestroyDart();
            // Quaternion rotationOffset = Quaternion.Euler(0, 180, 0);
            prefabToGrab.SetActive(true);
            GameObject dart = Instantiate(prefabToGrab); // Create new instance of the dart prefab
            if(dart!= null)
            dartCollider = dart.GetComponent<CapsuleCollider>();
            dartCollider.enabled = false;

            currentFollower = dart.GetComponent<Follower>();
            currentFollower.AttachTo(attachmentPoint);
        }

        void ReleaseDart()
        {
            currentFollower.Detach();
            currentFollower.GetComponent<Collider>().enabled = true;
            currentFollower.GetComponent<RotateAlongVelocity>().enabled = true;
            currentFollower = null;
            audioFX.Play();
        }

        public void DestroyDart()
        {
            // check existing dart
            if (currentFollower != null)
            {
                Destroy(currentFollower.gameObject); // Destroy previous dart if two darts are instantiated simultaneously
            }
        }
    }
}