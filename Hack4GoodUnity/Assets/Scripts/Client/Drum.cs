using UnityEngine;
using System.Collections;

public class Drum : MonoBehaviour {

	float accelerometerUpdateInterval = 1.0f / 60.0f;
	// The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample (and vice versa).
	float lowPassKernelWidthInSeconds = 1.0f;
	// This next parameter is initialized to 2.0 per Apple's recommendation, or at least according to Brady! ;)
	public float shakeDetectionThreshold = 1.5f;

	private float lowPassFilterFactor;
	private Vector3 lowPassValue;
	private Vector3 acceleration;
	private Vector3 deltaAcceleration;

	ClientManager clientManager;

	void Start()
	{
		clientManager = GameObject.Find("ClientManager");

		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		lowPassValue = Vector3.zero;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}

	void Update()
	{
		acceleration = Input.acceleration;
		lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
		deltaAcceleration = acceleration - lowPassValue;
		if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
		{
			clientManager.PlayNoteVelocity(0.0f, deltaAcceleration.sqrMagnitude);
		}
	}
}
