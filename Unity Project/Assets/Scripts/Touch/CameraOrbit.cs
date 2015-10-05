using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraOrbit : MonoBehaviour
{

		//camera variables
    public bool loading;

	public Transform cameraLookAtTarget = null;
	[Range(0,80)] public float height = 0.0f;
	public float rotationDamping = 1.0f;
	public float heightDamping = 0.5f;
	[Range(0,150)] public float distance = 30.0f;
	[Range(0,360)]public float startingY = 90;
	[Range(0,90)]public float yMovementLimit = 45;
	[Range(0,80)]public float xMinLimit = 45;
	[Range(0, 80)]public float xMaxLimit = 80;
	public float xSpeed = 75;
	public float ySpeed = 120;
	private float x = 0.0f;
	private float y = 0.0f;
	PlayerController playerController;
    public bool movingCamera = true;

    public int introCameraSlowness = 1000;

    float startingDampingVar = 0;

    public bool readyToStart;

    public float currentY;

	void Start()
	{
        loading = true;
        readyToStart = false;
        movingCamera = true;
		var angles = gameObject.transform.eulerAngles;
		x = angles.x;
		y = angles.y;
        startingDampingVar = rotationDamping;
        playerController = GameObject.FindWithTag ("Player").GetComponent<PlayerController>();
	}
	float ClampAngle(float angle, float min, float max)
	{
		if (angle < 0)
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
		if (Input.GetMouseButton (0) && playerController.lockCam == false && readyToStart == true)
		{
            if (movingCamera == true)
            {
                if (startingY - yMovementLimit < 0)
                {
                    y = ClampAngle(y + 180 + (Input.GetAxis("Mouse X") * ySpeed * 0.02f), startingY + 180 - yMovementLimit, startingY + 180 + yMovementLimit) - 180;
                }
                else if (startingY + yMovementLimit > 360)
                {
                    y = ClampAngle(y - 180 + (Input.GetAxis("Mouse X") * ySpeed * 0.02f), startingY - 180 - yMovementLimit, startingY - 180 + yMovementLimit) + 180;
                }
                else
                {
                    y = ClampAngle(y + (Input.GetAxis("Mouse X") * ySpeed * 0.02f), startingY - yMovementLimit, startingY + yMovementLimit);
                }
                x = ClampAngle(x - (Input.GetAxis("Mouse Y") * xSpeed * 0.02f), xMinLimit, xMaxLimit);

                transform.eulerAngles = new Vector3(x, y, 0.0f);
                transform.position = cameraLookAtTarget.position - (transform.forward * distance);
            }
            else
            {
                movingCamera = true;
            }
        }
		else if (Input.touchCount == 1 && playerController.lockCam  == false && readyToStart == true)
		{

            ////// Touch version of camera orbit goes here
            if (movingCamera == true)
            {
                if (startingY - yMovementLimit < 0)
                {
                    y = ClampAngle(y + 180 + (Input.touches[0].deltaPosition.x * ySpeed * 0.000001f), startingY + 180 - yMovementLimit, startingY + 180 + yMovementLimit) - 180;
                }
                else if (startingY + yMovementLimit > 360)
                {
                    y = ClampAngle(y - 180 + (Input.touches[0].deltaPosition.x * ySpeed * 0.000001f), startingY - 180 - yMovementLimit, startingY - 180 + yMovementLimit) + 180;
                }
                else
                {
                    y = ClampAngle(y + (Input.touches[0].deltaPosition.x * ySpeed * 0.000001f), startingY - yMovementLimit, startingY + yMovementLimit);

                }

                x = ClampAngle(x - (Input.touches[0].deltaPosition.y * xSpeed * 0.000001f), xMinLimit, xMaxLimit);

                transform.eulerAngles = new Vector3(x, y, 0.0f);
                transform.position = cameraLookAtTarget.position - (transform.forward * distance);
            }
            else
            {
                movingCamera = true;
            }
			
			
		}
		else if (loading == false)
		{
            if (startingDampingVar > 0)
            {
                startingDampingVar -= rotationDamping / introCameraSlowness;
            }
            else
            {
                startingDampingVar = 0;
            }
			var rotationT = (rotationDamping - startingDampingVar) * Time.deltaTime;
			var currentXRotation = Mathf.LerpAngle (transform.eulerAngles.x, height, rotationT);
			var currentYRotation = Mathf.LerpAngle (transform.eulerAngles.y, startingY, rotationT);
			
			transform.eulerAngles = new Vector3 (x = currentXRotation, y = currentYRotation, 0.0f);
			transform.position = cameraLookAtTarget.position - (transform.forward * distance);
			
			transform.LookAt (cameraLookAtTarget);

            if (readyToStart == true)
            {
                movingCamera = false;
            }
		}
        if (readyToStart == false)
        {
            if (Mathf.Abs(transform.eulerAngles.y - startingY) < 10)
            {
                movingCamera = false;
                readyToStart = true;
            }
        }
    }

} //End class
