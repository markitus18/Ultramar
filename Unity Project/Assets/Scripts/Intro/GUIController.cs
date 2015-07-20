using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour
{

	public float hSliderValue = 10.0F;

	void OnGUI()
	{
		hSliderValue = GUI.HorizontalSlider(new Rect(200, 25, 100, 30), hSliderValue, 0.0F, 10.0F);
	}

	void Update()
	{
		AudioListener.volume = hSliderValue/100.0F;
	}
}
