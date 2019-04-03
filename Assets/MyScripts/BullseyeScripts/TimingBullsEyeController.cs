using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UXF;

public class TimingBullsEyeController : MonoBehaviour {

	public GameObject cylinder;
	public GameObject glove;
	public Transform window;
	public Transform horizontalOrigin;
	public Transform verticalOrigin;
	public Transform bound_h;
	public Transform bound_v;
	public SpriteRenderer wall;

	float speed = 0.75f;

	ParticleSystem particles;
	Collider targetCenter;
	TargetCenterController centerCollision;
	TargetHitController hitTheTarget;

	//UXF
    public Session session;

	public StartPointController start;

	Vector3 destination_h;
	Vector3 destination_v;

	int hitCount;
	bool hit;
	bool inRange;
	bool centered;
	bool outOfBounds;
	bool windowMissed;
	string mode;
	
	[HideInInspector]
	public bool hitSomething;

	/// <summary>
	// +============================================================+======================================+
	// |                     Possible scenarios                     |               Feedback               |
	// +============================================================+======================================+
	// | Hit center of the target while centered in window (case 1) | Particles (ripple) + haptics + sound |
	// +------------------------------------------------------------+--------------------------------------+
	// | Hit outer target while centered in window (case 2)         | Particles (normal) + sound (normal)  |
	// +------------------------------------------------------------+--------------------------------------+
	// | Hit centre of target but not centered in window (case 3)   | Particles (ripple) + sound           |
	// +------------------------------------------------------------+--------------------------------------+
	// | Hit outer target and not centered in window (case 4)       | Particles (normal) + sound (normal)  |
	// +------------------------------------------------------------+--------------------------------------+
	// | Hit the wall (case 5)                                      | Wall turns red + buzz sound          |
	// +------------------------------------------------------------+--------------------------------------+
	/// </summary>

	void Awake()
	{
		particles = GetComponent<ParticleSystem>();
		targetCenter = GetComponentInChildren<BoxCollider>();
		centerCollision = GetComponentInChildren<TargetCenterController>();
		hitTheTarget = GetComponentInChildren<TargetHitController>();
	}

	// Use this for initialization
	void Start ()
	{
		hitCount = 0;
		inRange = false;
		centered = false;
		outOfBounds = false;
		hitSomething = false;
		hitTheTarget.Set(false);
		destination_h = bound_h.localPosition;
		destination_v = bound_v.localPosition;
		TurnOff();
	}

	void Update () 
	{
		CheckIfInRange();
		CheckIfCentered();
		hit = hitTheTarget.Get();

		if(inRange && hit)
		{
			TargetHitThroughWindow();
			hitCount++;
		}
		else
		{
			HitTheWall();
		}
	}

	public void Run()
	{
		Debug.LogFormat("Mode = {0}", mode);
		if(string.Equals(mode, "horizontal"))
		{
			StartCoroutine(MoveHorizontal(destination_h));
			Debug.LogFormat("Running horizontal coroutine");
		}
		else if(string.Equals(mode, "vertical"))
		{
			StartCoroutine(MoveHorizontal(destination_v));
			Debug.LogFormat("Running horizontal coroutine");
		}
	}

	void TargetHitThroughWindow()
	{	
		float delayTime = 0;
		delayTime = particles.main.startLifetime.constantMax;
		var sh = particles.shape;

		if(hitSomething)
		{
			if(centered) // hit while centered in window
				if(centerCollision.hitInCenter) // hit inner target
				{
					sh.shapeType = ParticleSystemShapeType.Circle;
					particles.Play();
					Debug.LogFormat("Case 1");
				}
				else // hit outer target
				{
					sh.shapeType = ParticleSystemShapeType.Cone;
					particles.Play();
					Debug.LogFormat("Case 2");
				}
			else // hit while off the center of the window but within window range
				if(centerCollision.hitInCenter) // hit inner target
				{
					sh.shapeType = ParticleSystemShapeType.Circle;
					particles.Play();
					Debug.LogFormat("Case 3");
				}
				else // hit outer target
				{
					sh.shapeType = ParticleSystemShapeType.Cone;
					particles.Play();
					Debug.LogFormat("Case 4");
				}
			hitSomething = false;
			session.currentTrial.End();
			TurnOff();
			particles.Play();
			// StopAllCoroutines(); ///////// Causing errors
			windowMissed = false;
			StartCoroutine(DelayedReset(delayTime));
			centerCollision.Set(false);
		}
	}

	void HitTheWall()
	{
		if(hitSomething)
		{
			hitSomething = false;
			session.currentTrial.End();
			TurnOff();
			// StopAllCoroutines(); ///////// Causing errors
			StartCoroutine(ColorTransition(0.3f));
			hitTheTarget.Set(false);
			windowMissed = true;
			Debug.LogFormat("Glove hit the wall!");
		}
	}

	IEnumerator ColorTransition(float delay)
	{
		wall.color = Color.red;
		yield return new WaitForSeconds(delay);
		wall.color = Color.white;
	}

	IEnumerator DelayedReset(float delay)
	{
		yield return new WaitForSeconds(delay);
	}

	IEnumerator MoveHorizontal(Vector3 destination)
    {
        yield return new WaitForSeconds(2f);

		float speedValue = 0f;

		
		Spawn();
		Debug.LogFormat("Spawned");

		foreach (Trial trial in session.trials)
        { 
			Debug.LogFormat("Starting trial {0}", session.currentTrialNum);

           string speed = Convert.ToString(trial.settings["speed"]);

			switch(speed)
			{
				case "low":
					speedValue = 0.5f;
					break;
				case "medium":
					speedValue = 1f;
					break;
				case "high":
					speedValue = 2f;
					break;
			}

			hitSomething= false;

			Vector3 currPos = transform.localPosition;
			Vector3 diff = destination - currPos;
			
			trial.Begin();

			


            while ((trial.status != UXF.TrialStatus.Done) || (diff.magnitude > 0.001 && (!outOfBounds || !hitSomething)))
			{   				
				//Debug.Log("Target moving");
				transform.localPosition = Vector3.MoveTowards(currPos, destination, speedValue * Time.deltaTime);
				currPos = transform.localPosition;
            	diff = destination - currPos;
				yield return null; // continue running next frame
            }

		}
    }


	void CheckIfInRange()
	{
		if(transform.position.x >= ((window.position.x - (window.localScale.x))) && (transform.position.x <= (window.position.x + (window.localScale.x))))
		{
			inRange = true;
		}
		else
		{
			inRange = false;
		}
	}

	void CheckIfCentered()
	{
		if(transform.position.x >= (window.position.x - ((window.localScale.x/8.75f))) && (transform.position.x <= (window.position.x + ((window.localScale.x/8.75f)))))
		{
			centered = true;
		}
		else
		{
			centered = false;
		}
	}

	public void	OutOfBounds()
	{
		// If Horizontal
		outOfBounds = true;
		Debug.LogFormat("Out of bounds!");
		session.currentTrial.End();
		Debug.LogFormat("Ended trial {0}", session.currentTrialNum);
		TurnOff();
		// StopAllCoroutines(); ///////// Causing errors
	}

	void Spawn()
	{
		cylinder.SetActive(true);
	}

	public void ResetPosition(UXF.Trial trial)
	{
		
		string mode = trial.settings["mode"].ToString();

		if(string.Equals(mode, "horizontal"))
		{
			transform.localPosition = horizontalOrigin.localPosition;
			Debug.LogFormat("Position reset to {0}", transform.localPosition);
		}
		else if(string.Equals(mode, "vertical"))
		{
			transform.localPosition = verticalOrigin.localPosition;
			Debug.LogFormat("Position reset to {0}", transform.localPosition);
		}
	}

	void TurnOff()
	{
		cylinder.SetActive(false);
	}

	public void EndBehaviour(Trial endedTrial)
    {
        if (endedTrial == session.lastTrial)
        {
            session.End();
        }
        else
        {
			
        }
    }
}
