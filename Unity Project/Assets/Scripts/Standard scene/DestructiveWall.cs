using UnityEngine;
using System.Collections;

public class DestructiveWall : MonoBehaviour {

	public GameObject box1;
	public GameObject box2;

	public int direction;

	void Start ()
	{
		RaycastHit hit;
		Ray upRay = new Ray(transform.position, Vector3.forward);
		Ray downRay = new Ray(transform.position, Vector3.back);
		Ray rightRay = new Ray(transform.position, Vector3.right);
		Ray leftRay = new Ray(transform.position, Vector3.left);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
