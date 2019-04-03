using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartPointController : MonoBehaviour {

	public GameObject startPoint;
	public GameObject wall;
	public GameObject glove;
	public UnityEvent OnStart;
	public Collider wallCollider;

	Transform sphere;

	Vector3 originalSize;
	Vector3 maxSize;

	float speed = 0.05f;
	bool tooSoon;

	void Awake ()
	{
		sphere = startPoint.GetComponent<Transform>();
		wallCollider = wall.GetComponent<BoxCollider>();

		originalSize = sphere.localScale;
		maxSize = sphere.localScale += new Vector3(0.2f, 0.2f, 0.2f);
	}

	// Use this for initialization
	void Start () {
		sphere.localScale = originalSize;
	}

	void OnTriggerEnter()
	{
		Debug.LogFormat("Glove on start point");
		Inflate(maxSize);
		wallCollider.enabled = true;
	}

	void OnTriggerExit()
	{
		Debug.LogFormat("Left start point");
		Deflate(originalSize);
	}

	void Inflate(Vector3 max)
	{
		while(sphere.localScale.x < max.x)
		{
			sphere.localScale += new Vector3(speed, speed, speed);
		}
		OnStart.Invoke();
	}

	void Deflate(Vector3 original)
	{
		while(sphere.localScale.x > original.x)
		{
			sphere.localScale -= new Vector3(speed, speed, speed);
		}
	}
}
