using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartGrabbing : MonoBehaviour
    {
        public FloorCollision floor;
        public SteamVR_Action_Boolean grabAction;
        public Hand hand;
        public Transform attachmentPoint;
        public Transform hoverPoint;
        public GameObject prefabToGrab;
        public Transform spawnPoint;


        Collider dartCollider;
        Rigidbody dartRB;
        Follower currentFollower;

        // Update is called once per frame
        void Update()
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

            // check existing dart
            if (currentFollower != null)
            {
                Destroy(currentFollower.gameObject);
            }


            // Quaternion rotationOffset = Quaternion.Euler(0, 180, 0);
            if(prefabToGrab != null)
            prefabToGrab.SetActive(true);
            GameObject dart = Instantiate(prefabToGrab); // Create new instance of the dart prefab
            if(dart!= null)
            dartCollider = dart.GetComponent<CapsuleCollider>();
            dartCollider.enabled = false;

            currentFollower = dart.GetComponent<Follower>();
            currentFollower.AttachTo(attachmentPoint);

           
            // hand.AttachObject(dart, GrabTypes.Pinch);
            //hand.AttachObject(dart, GrabTypes.Pinch, Hand.AttachmentFlags.ParentToHand, attachmentPoint.transform);
            // dart.transform.position = spawnPoint.position + new Vector3(0, 0.2f, 0);
            // dart.transform.rotation = spawnPoint.rotation * rotationOffset;
        }


        void ReleaseDart()
        {
            currentFollower.Detach();
            currentFollower.GetComponent<Collider>().enabled = true;
            currentFollower.GetComponent<RotateAlongVelocity>().enabled = true;
            currentFollower = null;
        }


    }
}