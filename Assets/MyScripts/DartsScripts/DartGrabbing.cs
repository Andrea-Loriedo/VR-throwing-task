using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class DartGrabbing : MonoBehaviour
    {
        public FloorCollision floor;
        public DartControl control;
        public SteamVR_Action_Boolean grabAction;
        public Hand hand;
        public Transform attachmentPoint;
        public Transform hoverPoint;
        public GameObject prefabToGrab;
        public Transform spawnPoint;

        public GameObject dart;

        Collider dartCollider;
        Rigidbody dartRB;

        // Update is called once per frame
        void Update()
        {
            if(dart != null)
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

        private void DestroyIfMiss(GameObject dart)
        {
            if(floor.GetHit() == true)
            {
                Destroy(dart); // Destroy the dart objects that have missed the bullseye
                Debug.LogFormat("Dart destroyed!");
            }
        }

        void GrabDart()
        {
            Quaternion rotationOffset = Quaternion.Euler(0, 180, 0);;
            control.Respawned(true);
            floor.SetHit(false);
            prefabToGrab.SetActive(true);
            dart = GameObject.Instantiate<GameObject>(prefabToGrab); // Create new instance of the dart prefab
            dartCollider = GameObject.Find("Dart").GetComponent<CapsuleCollider>();
            dartCollider.enabled = true;
            // dart.transform.position = attachmentPoint.position;
            // dart.transform.rotation = attachmentPoint.rotation;
            dart.transform.position = spawnPoint.position + new Vector3(0, 0.2f, 0);
            dart.transform.rotation = spawnPoint.rotation * rotationOffset;
        }

    }
}