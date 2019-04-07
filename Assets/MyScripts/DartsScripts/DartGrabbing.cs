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

        Collider dartCollider;

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
                StartCoroutine(GrabDart());
            }
        }

        private void DestroyIfMiss(GameObject dart)
        {
            if(floor.GetHit() == true)
            {
                Destroy(dart); // Destroy the dart objects that have missed the bullseye
            }
        }

        IEnumerator GrabDart()
        {
            floor.SetHit(false);
            dart = GameObject.Instantiate<GameObject>(prefabToGrab); // Create new instance of the dart prefab
            dartCollider = GameObject.Find("Dart").GetComponent<CapsuleCollider>();
            dartCollider.enabled = true;
            dart.transform.position = attachmentPoint.position;
            dart.transform.rotation = attachmentPoint.rotation;
            yield return null;
        }
    }
}
