using UnityEngine;
using System.Collections;

public class SceneControl : MonoBehaviour
{
	public SlowZoom slowZoom;
	public GameObject title;
	public GameObject playButton;
	// Use this for initialization
	void Start ()
	{
		if (!GameControl.control.introZoom)
		{
			title.SetActive (false);
			playButton.SetActive (false);
			slowZoom.gameObject.transform.position = slowZoom.destination;
			GameControl.control.introZoom = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
