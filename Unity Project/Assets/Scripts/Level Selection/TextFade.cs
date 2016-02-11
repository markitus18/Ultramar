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
    void Start()
    {
        if (fadeIn)
        {
            Color myColor = gameObject.GetComponent<Text>().color;
            maxAlpha = myColor.a;
            myColor.a = 0;
            gameObject.GetComponent<Text>().color = myColor;
        }
        else
        {
            Color myColor = gameObject.GetComponent<Text>().color;
            maxAlpha = myColor.a;
            myColor.a = maxAlpha;
            gameObject.GetComponent<Text>().color = myColor;
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
            if (Time.time - startTime > duration)
            {
                start = false;
                if (!fadeIn)
                {
                   gameObject.SetActive(false);
                }
			}
			gameObject.GetComponent<Text>().color = myColor;
            if (transform.name == "PlayText")
            {
                Debug.Log("Play text alpha: " + gameObject.GetComponent<Text>().color.a);
            }
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
            if (!gameObject.activeSelf)
                gameObject.SetActive (true);
		}
        else
        {
            Color myColor = gameObject.GetComponent<Text>().color;
            maxAlpha = myColor.a;
            myColor.a = maxAlpha;
            gameObject.GetComponent<Text>().color = myColor;
        }
	}
}
