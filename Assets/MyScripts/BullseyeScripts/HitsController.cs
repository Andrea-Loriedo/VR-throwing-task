using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsController : MonoBehaviour {

	public TimingBullsEyeController bullsEyeController;
	Collider wallCollider;

	void Start()
	{
		wallCollider = GetComponent<BoxCollider>();
		wallCollider.enabled = false;
	}

	void OnTriggerEnter()
	{
		bullsEyeController.hitSomething = true;
		wallCollider.enabled = false;
	}
}
