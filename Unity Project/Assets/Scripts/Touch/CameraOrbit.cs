using UnityEngine;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{

		//camera variables
		public Transform cameraLookAtTaret = null;
		private int mouseDown;
		private GameObject MiTarget = null;
		private float height = 0.0f;
		private float rotationDamping = 3.0f;
		private float heightDamping = 2.0f;
		private float distance = 2.0f;
		private float yMinLimit = -20;
		private float yMaxLimit = 80;
		private float xMinLimit = -100;
		private float xMaxLimit = 100;
		private float xSpeed = 250;
		private float ySpeed = 120;
		private float x = 0.0f;
		private float y = 0.0f;
		private float distanceMin = 1f;
		private float distanceMax = 4f;
		
		/// <summary>
		/// Here is where all the mouse input takes place, if you hold now mouse button down it will just follow your character and zoom in and out
		/// if you hold down the right mouse button you can rotate around your player and zoom in and out
		/// if you hold both left and right mouse button it will also turn your player
		/// </summary>
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
		if (cameraLookAtTaret == null) {
			return;
		}
			
		if (Input.GetMouseButton (1)) {
			x = ClampAngle (x - (Input.GetAxis ("Mouse Y") * xSpeed * 0.02f), xMinLimit, xMaxLimit);
			y = ClampAngle (y + (Input.GetAxis ("Mouse X") * ySpeed * 0.02f), yMinLimit, yMaxLimit);
			distance = Mathf.Clamp (distance - (Input.GetAxis ("Mouse ScrollWheel") * 5), distanceMin, distanceMax);
				
			transform.eulerAngles = new Vector3 (x, y, 0.0f);
			transform.position = cameraLookAtTaret.position - (transform.forward * distance);
		} else {
			var wantedRotationAngle = cameraLookAtTaret.eulerAngles.y;
			var wantedHeight = cameraLookAtTaret.position.y + height;
				
			var rotationT = rotationDamping * Time.deltaTime;
			var currentXRotation = Mathf.LerpAngle (transform.eulerAngles.x, 0.0f, rotationT);
			var currentYRotation = Mathf.LerpAngle (transform.eulerAngles.y, wantedRotationAngle, rotationT);
			var currentHeight = Mathf.Lerp (transform.position.y, wantedHeight, heightDamping * Time.deltaTime);
				
			transform.eulerAngles = new Vector3 (x = currentXRotation, y = currentYRotation, 0.0f);
			transform.position = cameraLookAtTaret.position - (transform.forward * distance);
			transform.position.Set (transform.position.x, currentHeight, transform.position.z);
				
			transform.LookAt (cameraLookAtTaret);
		}
	}

} //End class
