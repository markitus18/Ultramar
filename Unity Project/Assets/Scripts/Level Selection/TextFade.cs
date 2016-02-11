using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
	public bool fadeIn = true;
	public bool start = true;

	public float FadeInDuration = 2.0f;
	public float FadeOutDuration = 2.0f;
	
	float startTime = 0.0f;
	float maxAlpha;

	// Use this for initialization
	void Start ()
	{
		maxAlpha = gameObject.GetComponent<Text>().color.a;
		if (start)
		{
			StartFade (fadeIn);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (start)
		{
			float duration = FadeInDuration;
			if (!fadeIn)
				duration = FadeOutDuration;

			Color myColor = gameObject.GetComponent<Text>().color;
			float ratio = (Time.time - startTime)/duration;
			
			if (fadeIn)
			{
				myColor.a = Mathf.Lerp(0, maxAlpha, ratio);
			}
			else
			{
				myColor.a = Mathf.Lerp(maxAlpha, 0, ratio);
			}
			if (!fadeIn && Time.time - startTime > duration)
			{
				gameObject.SetActive(false);
			}
			gameObject.GetComponent<Text>().color = myColor;
		}
	}
	
	// Used to start fade transition
	public void StartFade(bool fadIn)
	{
		fadeIn = fadIn;
		start = true;
		startTime = Time.time;

		if(fadeIn)
		{
			gameObject.SetActive (true);
			Color myColor = gameObject.GetComponent<Text>().color;
			myColor.a = 0;
			gameObject.GetComponent<Text>().color = myColor;
		}
		else
		{
			Color myColor = gameObject.GetComponent<Text>().color;
			myColor.a = maxAlpha;
			gameObject.GetComponent<Text>().color = myColor;
		}
	}
}
