using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOnce : MonoBehaviour {

	public float timeStart = 1.0f;
	public float duration = 2.0f;
	// Use this for initialization
	void Start ()
	{
		Color myColor = gameObject.GetComponent<Text>().color;
		myColor.a = 0;
		gameObject.GetComponent<Text>().color = myColor;
		Debug.Log("Start Game");
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > duration)
		{
	//		Destroy (gameObject);
		}
		if (Time.time > timeStart)
		{
			Color myColor = gameObject.GetComponent<Text>().color;
			float ratio = (Time.time - timeStart)/duration;
			myColor.a = Mathf.Lerp (0, 1, ratio);
			gameObject.GetComponent<Text>().color = myColor;
			Debug.Log("Alpha: " + myColor.a);
		}

	}
}
