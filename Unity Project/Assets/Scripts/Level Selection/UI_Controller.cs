using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour {

	// Use this for initialization
	public float startFadeDelay = 1.5f;
	public GameObject InitialUI;
    public GameObject missions;

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
            InitialUI.SetActive(true);
            InitialUI.GetComponent<FadeGeneral>().DoFade(true);
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
		InitialUI.GetComponent<FadeGeneral>().DoFade (false);
		playTime = Time.time;
		playStarted = true;
	}

	void FadeInMissions()
	{
        missions.SetActive(true);
        missions.GetComponent<FadeGeneral>().DoFade(true);
	}
}
