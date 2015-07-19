using UnityEngine;
using System.Collections;

public class RotationAroundObj : MonoBehaviour
{
	public float speed = 1;
	public Transform target;
	
	void Update()
	{
		transform.LookAt(target);
		transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
	}
}