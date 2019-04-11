using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UXF;

namespace Valve.VR.InteractionSystem.Sample
{
    public class BullseyeController : MonoBehaviour
    {
        [HideInInspector]
        public enum ring {White, Black, Blue, Red, Yellow, Miss, Null};
        
        [HideInInspector]
        public ring hitZone;

        [HideInInspector]
        public int totalScore;
        public Transform boundA;
        public Transform boundB;
        public FloorCollision collision;

        // UXF
        public Session session;
        public ExperimentManager experiment;
        public TrialResults results;
        
        bool targetHit;
        int score;
        
        GameObject dart;
        Collider dartColl;
        Vector3 initialPosition;
        //Quaternion dartRotation;
        Vector3 dartScale;
    
        AudioSource audioData;
        Coroutine forwardsRoutine;
        Coroutine backwardsRoutine;

        // Start is called before the first frame update
        void Start()
        {
            audioData = GetComponent<AudioSource>();
            targetHit = false;
            hitZone = ring.Null;
            initialPosition = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            if(TargetHitByDart())
            {
                DetectHit();
            }
            else if(collision.MissDetected())
            {
                hitZone = ring.Miss;
                results.targetZone = hitZone.ToString(); 
                results.totalScore = GetScore();
                experiment.EndCurrentTrial();
                collision.HitStateReset(false); // Reset target miss to false
            }
        }

        void OnTriggerEnter(Collider dartCollider)
        {
            dart = dartCollider.gameObject;
            dartColl = dartCollider;
            dartScale = dart.transform.localScale;
            targetHit = true;
            Debug.LogFormat("Hit!");
            SnapToBoard(dartCollider);
            dartColl.enabled = false;
            audioData.Play();
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

        void DetectHit()
        {
            float distanceFromCentre = Vector2.Distance(dart.transform.position, transform.position); // Calculate 2D distance between dart and centre of the bullseye

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

            Debug.LogFormat("Hit {0}", hitZone + "zone");
            totalScore = ComputeScore();
            // Save results
            results.targetZone = hitZone.ToString(); 
            results.totalScore = GetScore(); 
            // End trial
            experiment.EndCurrentTrial();
            hitZone = ring.Null; // Reset target zone hit as 'Null'
            targetHit = false; // Reset target hit to false
        }

        public int GetScore()
        {
            return totalScore;
        }

        public void ResetScore()
        {
            score = 0;
            totalScore = 0;
        }

        void SnapToBoard(Collider dartCollider)
        {
            float dartOffset = (dart.transform.localScale.z * 2 + 0.06f); // Offset from the centre of the dart
            Destroy(dart.GetComponent<Rigidbody>());
            var dartPlaceholder = new GameObject(); // Empty game object to ensure dart scale and rotation are preserved when bullseye is set as parent
            dartPlaceholder.transform.parent = transform; // Set empty placeholder as child of the bullseye
            dart.transform.parent = dartPlaceholder.transform; // Set the dart as child of the empty placeholder to make it follow the bullseye trajectory when moving
            dart.transform.position = new Vector3(dart.transform.position.x, dart.transform.position.y, dart.transform.position.z - dartOffset); // Stick dart to the point it hit
        }

        public bool TargetHitByDart()
        {
            return targetHit;
        }

        void ResetPosition()
        {
            transform.localPosition = initialPosition;
        }

        public IEnumerator MoveTarget()
        {
            while(true) // Loop forever
            {   // One instance of Swing() coroutine for each swing
                yield return forwardsRoutine = StartCoroutine(Swing(boundA.position, boundB.position, 3.0f)); // Move from left bound to right bound (duration 3s)
                yield return backwardsRoutine = StartCoroutine(Swing(boundB.position, boundA.position, 3.0f)); // Wait until transition complete, then move back towards left bound
                yield return null; // Continue running next frame, prevents application from crashing when in an infinite loop
            }
        }

        public void StopTarget()
        {
            if(forwardsRoutine != null && backwardsRoutine != null)
            {
                StopAllCoroutines();
            }
            ResetPosition(); // Reposition bullseye in the centre of the workspace
        }

        IEnumerator Swing(Vector3 pointA, Vector3 pointB, float time)
        {
            Vector3 currPos = transform.localPosition;
            float i = 0.0f;
            float speed = 1/time;

            while(i < 1.0f)
            {
                i += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(pointA, pointB, i); // Move linearly from point A to point B at a rate of i
                currPos = transform.localPosition; // Update current position
                yield return null;
            }
        }

        public TrialResults GetBullseyeResults()
        {
            return results;
        }

        public struct TrialResults
        {
            public string targetZone;
            public int totalScore;
        }
    }
}
