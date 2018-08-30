using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NDAPIWrapperSpace;

public class GloveoneFingerCollision : MonoBehaviour
{
	public Location GloveHand;
	public GloveFinger GloveFinger;

	private GloveoneController _gloveoneController;

	void Start()
	{
		_gloveoneController = FindObjectOfType<GloveoneController>();
	}

	void FixedUpdate()
	{
		transform.localPosition = Vector3.zero;
	}

	void OnCollisionEnter()
	{
		_gloveoneController.PulseFinger(GloveHand, GloveFinger);
	}
}
