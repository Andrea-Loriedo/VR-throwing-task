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


        public GameObject dart;

        // Update is called once per frame
        void Update()
        {
            DestroyIfMiss(dart);
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
        }

        private void GrabDart()
        {
            dart = GameObject.Instantiate<GameObject>(prefabToGrab); // Create new instance of the dart prefab
            dart.transform.position = attachmentPoint.position;
            dart.transform.rotation = attachmentPoint.rotation;
        }

        private void DestroyIfMiss(GameObject dart)
        {
            if(floor.GetHit() == true)
            {
                Destroy(dart);
                floor.SetHit(false);
            }
        }
    }
}
