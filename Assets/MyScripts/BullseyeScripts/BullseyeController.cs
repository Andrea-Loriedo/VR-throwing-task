using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullseyeController : MonoBehaviour
{
    enum ring {White, Black, Blue, Red, Yellow, Miss};
    bool targetHit;
    int score;
    [HideInInspector]
    public int totalScore;

    public GameObject dart;
    ring bullseyeZone;

    // Start is called before the first frame update
    void Start()
    {
        targetHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectHit(targetHit);
        if(targetHit)
        {
            totalScore = ComputeScore();
        }
        StickDart(targetHit);
    }

    void OnTriggerEnter(Collider dartCollider)
    {
        targetHit = true;
        Debug.LogFormat("Hit!");

        float dartOffset = (dart.transform.localScale.z / 2f);
        if (dartCollider.attachedRigidbody){
            dartCollider.attachedRigidbody.useGravity = false;
        }
        dart.transform.position = new Vector3(dart.transform.position.x, dart.transform.position.y, dart.transform.position.z - dartOffset);
    }

    int ComputeScore()
    {
        switch(bullseyeZone)
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
        float distanceFromCentre = Vector3.Distance(dart.transform.position, this.transform.position);

        if(hit == true)
        {
            if(distanceFromCentre > 0 && distanceFromCentre < 0.1)
            {
                bullseyeZone = ring.Yellow;
            } 
            else if(distanceFromCentre > 0.1 && distanceFromCentre < 0.2)
            {
                bullseyeZone = ring.Red;
            }
            else if(distanceFromCentre > 0.2 && distanceFromCentre < 0.3)
            {
                bullseyeZone = ring.Blue;
            }
            else if(distanceFromCentre > 0.3 && distanceFromCentre < 0.4)
            {
                bullseyeZone = ring.Black;
            }
            else if(distanceFromCentre > 0.4 && distanceFromCentre < 0.5)
            {
                bullseyeZone = ring.White;
            }
            else
            {
                bullseyeZone = ring.Miss;
            }

            hit = false;
        }
    }
    void StickDart(bool hit)
    {

    }
}
