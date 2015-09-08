using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {
	
	public GameObject panel;

	public GameObject disabledImage;
	public GameObject soundButton;

	public void OpenPanel()
	{
		panel.SetActive (true);
	}

	public void ClosePanel()
	{
		panel.SetActive(false);
	}

	public void UpdateVolume()
	{

		if (AudioListener.volume == 1)
		{
			AudioListener.volume = 0;
//			soundButton.GetComponent<Image>() = disabledImage;
		}
		else
		{
			AudioListener.volume = 1;
	//		soundButton.GetComponent<Image>() = disabledImage;
		}
	}

}
