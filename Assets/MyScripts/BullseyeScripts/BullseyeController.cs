using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Valve.VR.InteractionSystem.Sample
{
    public class BullseyeController : MonoBehaviour
    {
        enum ring {White, Black, Blue, Red, Yellow, Miss, Null};
        bool targetHit;
        int score;
        [HideInInspector]
        public int totalScore;

        ring hitZone;
        GameObject dart;
        Collider dartColl;

        // Start is called before the first frame update
        void Start()
        {
            targetHit = false;
            hitZone = ring.Null;
        }

        // Update is called once per frame
        void Update()
        {
            DetectHit(targetHit);
        }

        void OnTriggerEnter(Collider dartCollider)
        {
            dart = GameObject.Find("RightHand").GetComponent<DartGrabbing>().dart;
            dartColl = GameObject.Find("Dart").GetComponent<CapsuleCollider>();
            targetHit = true;
            Debug.LogFormat("Hit!");
            SnapToBoard(dartCollider);
            dartColl.enabled = false;
            // targetHit = false;
        }

        public int ComputeScore()
        {
            switch(hitZone)
            {
                case ring.White:
                score += 5;
                break;
                case ring.Black:
                score += 10;
                break;
                case ring.Blue:
                score += 15;
                break;
                case ring.Red:
                score += 20;
                break;
                case ring.Yellow:
                score += 25;
                break;
                default:
                break;
            }

            return score;
        }

        void DetectHit(bool hit)
        {
            if(hit == true)
            {
                float distanceFromCentre = Vector3.Distance(dart.transform.position, transform.position); // Calculate distance between dart and centre of the bullseye

                if(distanceFromCentre > 0 && distanceFromCentre < 0.1)
                {
                    hitZone = ring.Yellow;
                } 
                else if(distanceFromCentre > 0.1 && distanceFromCentre < 0.2)
                {
                    hitZone = ring.Red;
                }
                else if(distanceFromCentre > 0.2 && distanceFromCentre < 0.3)
                {
                    hitZone = ring.Blue;
                }
                else if(distanceFromCentre > 0.3 && distanceFromCentre < 0.4)
                {
                    hitZone = ring.Black;
                }
                else if(distanceFromCentre > 0.4 && distanceFromCentre < 0.5)
                {
                    hitZone = ring.White;
                }
                else
                {
                    hitZone = ring.Miss;
                }

                Debug.LogFormat("" + hitZone);
                totalScore = ComputeScore();
                hitZone = ring.Null;
                targetHit = false;
            }
        }

        public int GetScore()
        {
            return totalScore;
        }

        void SnapToBoard(Collider dartCollider)
        {
            Quaternion rotationOffset = Quaternion.Euler(0, 180, 0); // Rotate dart 180 degrees when it lands
            float dartOffset = ((dart.transform.localScale.z) / 2f); // Offset from the centre of the dart

            if (dartCollider.attachedRigidbody){
                dartCollider.attachedRigidbody.useGravity = false; // Prevent dart from falling
                dartCollider.attachedRigidbody.isKinematic = true; // Prevent dart from floating away
            }
            dart.transform.position = new Vector3(dart.transform.position.x, dart.transform.position.y, dart.transform.position.z - dartOffset); // Stick dart to the point it hit
            //dart.transform.rotation = dart.transform.rotation * rotationOffset; // Adjust orientation
        }

        public bool TargetHitByDart()
        {
            return targetHit;
        }
    }
}
