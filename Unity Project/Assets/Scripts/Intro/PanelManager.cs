﻿using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {

	public GameObject panel;

	public void OpenPanel()
	{
		panel.SetActive (true);
	}


}
