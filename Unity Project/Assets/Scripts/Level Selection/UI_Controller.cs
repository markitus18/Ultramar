using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour {

	// Use this for initialization
	public GameObject title;
	public GameObject play_image;
	public GameObject play_text;
	public GameObject credits_image;
	public GameObject credits_text;

	public GameObject mission_1_image;
	public GameObject mission_1_text;
	public GameObject mission_2_image;
	public GameObject mission_2_text;

	public float intro_fadeOut_time;
	public float missions_fadeIn_time;

	bool playStarted = false;
	float playTime;
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
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
		//Title fade
		TextFade tit = title.GetComponent<TextFade>();
		tit.StartFade (false, 0, intro_fadeOut_time);

		//Play image and text fade
		ImageFade p_t_i = play_image.GetComponent<ImageFade>();
		p_t_i.StartFade(false, 0, intro_fadeOut_time);

		TextFade p_t_t = play_text.GetComponent<TextFade>();
		p_t_t.StartFade(false, 0, intro_fadeOut_time);
		
		//Credits image and text fade
		ImageFade c_t_i = credits_image.GetComponent<ImageFade>();
		c_t_i.StartFade(false, 0, intro_fadeOut_time);
		
		TextFade c_t_t = credits_text.GetComponent<TextFade>();;
		c_t_t.StartFade(false, 0, intro_fadeOut_time);

		playTime = Time.time;
		playStarted = true;
	}

	void FadeInMissions()
	{
		//Mission I fade
		//Image
		ImageFade m1_i = mission_1_image.GetComponent<ImageFade>();
		m1_i.StartFade (true, 0, missions_fadeIn_time);
		//Text
		TextFade m1_t = mission_1_text.GetComponent<TextFade>();
		m1_t.StartFade (true, 0, missions_fadeIn_time);

		//Mission II fade
		//Image
		ImageFade m2_i = mission_2_image.GetComponent<ImageFade>();
		m2_i.StartFade (true, 0, missions_fadeIn_time);

		TextFade m2_t = mission_2_text.GetComponent<TextFade>();
		m2_t.StartFade (true, 0, missions_fadeIn_time);
	}
}
