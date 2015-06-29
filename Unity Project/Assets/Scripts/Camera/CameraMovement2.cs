using UnityEngine;
using System.Collections;

public class CameraMovement2 : MonoBehaviour {
	
		Vector3 hit_position = Vector3.zero;
		Vector3 current_position = Vector3.zero;
		Vector3 camera_position = Vector3.zero;
		float z = 0.0f;
	private int firstTouch;
		
		// Use this for initialization
		void Start () {
			
		}
		
	void Update(){
		if (Input.touchCount == 2) {
			if (Input.touches[0].phase == TouchPhase.Began) {
				hit_position = Input.touches[0].position;
				camera_position = transform.position;
				
			}
			if (Input.touches[0].phase == TouchPhase.Moved || Input.touches[0].phase == TouchPhase.Stationary) {
				current_position = Input.touches[0].position;
				// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
				// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
				current_position.z = hit_position.z = camera_position.y;
			
				// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
				// anyways.  
				Vector3 direction = Camera.main.ScreenToWorldPoint (current_position) - Camera.main.ScreenToWorldPoint (hit_position);
			
				// Invert direction to that terrain appears to move with the mouse.
				direction = direction * -1;
			
				Vector3 position = camera_position + direction;
			
				transform.position = position;
			}
			if (Input.touches[firstTouch].phase == TouchPhase.Ended || Input.touches[firstTouch].phase == TouchPhase.Canceled)
			{

			}
		}
	}
		
}
