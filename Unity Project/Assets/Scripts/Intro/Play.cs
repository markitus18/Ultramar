using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{
	void Start()
	{
	}

	void Update()
	{
	}

	void OnTouchDown()
	{
		Debug.Log ("Play");
		Application.LoadLevel (1);
	}
}
