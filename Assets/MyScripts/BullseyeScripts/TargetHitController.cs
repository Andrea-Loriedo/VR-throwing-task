using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHitController : MonoBehaviour {
	
	[HideInInspector]
	public bool onTarget;

	// Use this for initialization
	void Start () {
		onTarget = false;
	}
	
	void OnTriggerEnter()
	{
		onTarget = true;
	}

	public void Set(bool value)
	{
		onTarget = value;
	}

	public bool Get()
	{
		return onTarget;
	}
}
