using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour
{
	public bool fadeIn = true;
	public bool start = true;
	
	public float delayTime = 1.0f;
	public float duration = 2.0f;
	
	float startTime = 0.0f;
	
	// Use this for initialization
	void Start ()
	{
		if (start)
		{
			if(fadeIn)
			{
				Color myColor = gameObject.GetComponent<Image>().color;
				myColor.a = 0;
				gameObject.GetComponent<Image>().color = myColor;
			}
			else
			{
				Color myColor = gameObject.GetComponent<Text>().color;
				myColor.a = 1;
				gameObject.GetComponent<Image>().color = myColor;
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
				Color myColor = gameObject.GetComponent<Image>().color;
				float ratio = (Time.time - startTime - delayTime)/duration;
				
				if (fadeIn)
				{
					myColor.a = Mathf.Lerp(0, 1, ratio);
				}
				else
				{
					myColor.a = Mathf.Lerp(1, 0, ratio);
				}
				if (Time.time > duration)
				{
					//		Destroy (gameObject);
				}
				gameObject.GetComponent<Image>().color = myColor;
			}
		}
	}
	
	// Used to start fade transition
	void StartFade()
	{
		start = true;
		startTime = Time.time;
	}
}
