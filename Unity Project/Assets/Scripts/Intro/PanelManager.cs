using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {
	
	public GameObject panel;
	public GameObject musicButton;
	bool panelOpened = false;

	public void OpenPanel()
	{
		musicButton.SetActive (true);
		panelOpened = true;
	}

	public void ClosePanel()
	{
		musicButton.SetActive (false);
		panelOpened = false;
	}

	public void DeployedClicked()
	{
		if (!panelOpened)
			OpenPanel();
		else
			ClosePanel ();
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
