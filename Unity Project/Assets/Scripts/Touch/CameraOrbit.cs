using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{

		//camera variables
	public Transform cameraLookAtTarget = null;
	[Range(0,80)] public float height = 0.0f;
	public float rotationDamping = 1.0f;
	public float heightDamping = 0.5f;
	[Range(0,150)] public float distance = 30.0f;
	[Range(0,360)]public float startingY = 90;
	[Range(0,180)]public float yMovementLimit = 45;
	[Range(0,80)]public float xMinLimit = 45;
	[Range(0, 80)]public float xMaxLimit = 80;
	public float xSpeed = 75;
	public float ySpeed = 120;
	private float x = 0.0f;
	private float y = 0.0f;

	void Start()
	{
		var angles = gameObject.transform.eulerAngles;
		x = angles.x;
		y = angles.y;
	}
	float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
	
	void LateUpdate()
	{
		if (cameraLookAtTarget == null)
		{
			return;
		}
		
		if (Input.GetMouseButton (1))
		{
			x = ClampAngle (x - (Input.GetAxis ("Mouse Y") * xSpeed * 0.02f), xMinLimit, xMaxLimit);
			y = ClampAngle (y + (Input.GetAxis ("Mouse X") * ySpeed * 0.02f), startingY - yMovementLimit, startingY + yMovementLimit);
			
			transform.eulerAngles = new Vector3 (x, y, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
		}
		else
		{
			var rotationT = rotationDamping * Time.deltaTime;
			var currentXRotation = Mathf.LerpAngle (transform.eulerAngles.x, height, rotationT);
			var currentYRotation = Mathf.LerpAngle (transform.eulerAngles.y, startingY, rotationT);
			
			transform.eulerAngles = new Vector3 (x = currentXRotation, y = currentYRotation, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
			
			transform.LookAt (cameraLookAtTarget);
		}
	}

} //End class