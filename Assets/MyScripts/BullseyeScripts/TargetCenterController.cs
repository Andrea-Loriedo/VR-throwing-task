using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCenterController : MonoBehaviour {

	[HideInInspector]
	public bool hitInCenter;

	// Use this for initialization
	void Start () {
		hitInCenter = false;
	}
	
	void OnTriggerEnter()
	{
		hitInCenter = true;
		Debug.LogFormat("Hit in center!");
	}

	public void Set(bool value)
	{
		hitInCenter = value;
	}
}
