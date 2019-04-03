using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCollisionDetection : MonoBehaviour {

    public GameObject cylinder;
    public GameObject wall;
    GameObject bullsEye;
    public Collider wallCollider;   
 
    TimingBullsEyeController bullsEyeController;

    void Start()
    {
        bullsEye = cylinder.transform.parent.gameObject;
        bullsEyeController = bullsEye.GetComponent<TimingBullsEyeController>();
        // wallCollider = wall.GetComponent<BoxCollider>();
    }

	void OnTriggerEnter(Collider other)
	{
        if (GameObject.ReferenceEquals( other.gameObject, cylinder))
        {
            bullsEyeController.OutOfBounds();
            // wallCollider.enabled = false;
        }
	}
}
