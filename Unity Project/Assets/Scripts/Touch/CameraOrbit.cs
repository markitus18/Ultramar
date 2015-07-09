using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{

		//camera variables
	public Transform cameraLookAtTarget = null;
	public float height = 0.0f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float distance = 2.0f;
	public float yMinLimit = -20;
	public float yMaxLimit = 80;
	public float xMinLimit = -100;
	public float xMaxLimit = 100;
	public float xSpeed = 250;
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
		if (cameraLookAtTarget == null) {
			return;
		}
		
		if (Input.GetMouseButton (1)) {
			x = ClampAngle (x - (Input.GetAxis ("Mouse Y") * xSpeed * 0.02f), xMinLimit, xMaxLimit);
			y = ClampAngle (y + (Input.GetAxis ("Mouse X") * ySpeed * 0.02f), yMinLimit, yMaxLimit);
			
			transform.eulerAngles = new Vector3 (x, y, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
		} else {
			var wantedRotationAngle = 0;//cameraLookAtTarget.eulerAngles.y;
			
			var rotationT = rotationDamping * Time.deltaTime;
			var currentXRotation = Mathf.LerpAngle (transform.eulerAngles.x, height, rotationT);
			var currentYRotation = Mathf.LerpAngle (transform.eulerAngles.y, wantedRotationAngle, rotationT);
			
			transform.eulerAngles = new Vector3 (x = currentXRotation, y = currentYRotation, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
			
			transform.LookAt (cameraLookAtTarget);
		}
	}

		/*void Start()
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
		if (cameraLookAtTarget == null) {
			return;
		}
			
		if (Input.GetMouseButton (1)) {
			x = ClampAngle (x - (Input.GetAxis ("Mouse Y") * xSpeed * 0.02f), xMinLimit, xMaxLimit);
			y = ClampAngle (y + (Input.GetAxis ("Mouse X") * ySpeed * 0.02f), yMinLimit, yMaxLimit);
			distance = Mathf.Clamp (distance - (Input.GetAxis ("Mouse ScrollWheel") * 5), distanceMin, distanceMax);
				
			transform.eulerAngles = new Vector3 (x, y, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
		} else {
			var wantedRotationAngle = cameraLookAtTarget.eulerAngles.y;
			var wantedHeight = cameraLookAtTarget.position.y + height;
				
			var rotationT = rotationDamping * Time.deltaTime;
			var currentXRotation = Mathf.LerpAngle (transform.eulerAngles.x, 0.0f, rotationT);
			var currentYRotation = Mathf.LerpAngle (transform.eulerAngles.y, wantedRotationAngle, rotationT);
			var currentHeight = Mathf.Lerp (transform.position.y, wantedHeight, heightDamping * Time.deltaTime);
				
			transform.eulerAngles = new Vector3 (x = currentXRotation, y = currentYRotation, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
			transform.position.Set (transform.position.x, currentHeight, transform.position.z);
				
			transform.LookAt (cameraLookAtTarget);
		}
	}*/

} //End class
