using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour
{

	public GameObject finalBox;
	public ConnectBoxes linkerBox;

	void Start()
	{
		gameObject.GetComponent<Collider>().enabled = true;
	}
	void OnTouchUp()
	{
		Debug.Log ("mosue up");
		finalBox.SetActive(true);
		linkerBox.JoinBoxes();
		gameObject.SetActive(false);
	}
}
