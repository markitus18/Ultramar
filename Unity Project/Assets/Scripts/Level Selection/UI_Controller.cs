using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour {

	// Use this for initialization
	public float startFadeDelay = 1.5f;
	public GameObject InitialUI;

	public GameObject mission_1_image;
	public GameObject mission_1_text;
	public GameObject mission_2_image;
	public GameObject mission_2_text;

	public float intro_fadeOut_time;
	public float missions_fadeIn_time;

    bool firstFade = false;

	float timeStart;

	bool playStarted = false;
	float playTime;
	void Start ()
	{
		timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time - timeStart > startFadeDelay && !firstFade)
		{
            firstFade = true;
			InitialUI.GetComponent<FadeChilds>().DoFade(true);
		}

		if(playStarted)
		{
			if (Time.time - playTime > intro_fadeOut_time)
			{
				FadeInMissions();
				playStarted = false;
			}
		}
	}

	public void OnPlayDown()
	{
		InitialUI.GetComponent<FadeChilds> ().DoFade (true);
		playTime = Time.time;
		playStarted = true;
	}

	void FadeInMissions()
	{
		//Mission I fade
		//Image
		ImageFade m1_i = mission_1_image.GetComponent<ImageFade>();
		m1_i.StartFade (true);
		//Text
		TextFade m1_t = mission_1_text.GetComponent<TextFade>();
		m1_t.StartFade (true);

		//Mission II fade
		//Image
		ImageFade m2_i = mission_2_image.GetComponent<ImageFade>();
		m2_i.StartFade (true);

		TextFade m2_t = mission_2_text.GetComponent<TextFade>();
		m2_t.StartFade (true);
	}
}
