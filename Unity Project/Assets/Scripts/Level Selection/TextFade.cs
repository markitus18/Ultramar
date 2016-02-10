using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
	public bool fadeIn = true;
	public bool start = true;
	
	public float delayTime = 1.0f;
	public float duration = 2.0f;
	
	float startTime = 0.0f;
	float maxAlpha;
	// Use this for initialization
	void Start ()
	{
		maxAlpha = gameObject.GetComponent<Text>().color.a;
		if (start)
		{
			if(fadeIn)
			{
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
	
	// Update is called once per frame
	void Update ()
	{
		if (start)
		{
			if (Time.time - startTime > delayTime)
			{
				Color myColor = gameObject.GetComponent<Text>().color;
				float ratio = (Time.time - startTime - delayTime)/duration;
				
				if (fadeIn)
				{
					myColor.a = Mathf.Lerp(0, maxAlpha, ratio);
				}
				else
				{
					myColor.a = Mathf.Lerp(maxAlpha, 0, ratio);
				}
				if (!fadeIn && Time.time - startTime > duration + delayTime)
				{
					gameObject.SetActive(false);
				}
				gameObject.GetComponent<Text>().color = myColor;
			}
		}
	}
	
	// Used to start fade transition
	public void StartFade(bool fadIn, float timeDelay, float timeDuration)
	{
		fadeIn = fadIn;
		delayTime = timeDelay;
		duration = timeDuration;
		start = true;
		startTime = Time.time;

		if (fadeIn)
			gameObject.SetActive (true);
	}
}
