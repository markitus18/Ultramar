using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeChilds : MonoBehaviour {

	public bool start;
	public bool fadeIn;
	// Use this for initialization
	void Start ()
	{
		if (start)
			DoFade (fadeIn);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void DoFade(bool fIn)
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(true);
			ImageFade imageF = child.GetComponent<ImageFade>();
			if (imageF)
			{
				imageF.StartFade(fadeIn);
			}
			TextFade textF = child.GetComponent<TextFade>();
			if (textF)
			{
				textF.StartFade(fadeIn);
			}
		}
	}
}
