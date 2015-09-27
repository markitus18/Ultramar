﻿using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

    Rect ZeroPos;

	// Use this for initialization
	void Start ()
    {
        //public string  Path;
#if !UNITY_EDITOR
        Handheld.PlayFullScreenMovie("Cinematica01.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
        Application.LoadLevel(2);
#endif
#if UNITY_EDITOR
        ZeroPos.x = ZeroPos.y = 0;
        ZeroPos.width = Screen.width;
        ZeroPos.height = Screen.height;
        UnityEngine.Texture

        GUI.DrawTexture(ZeroPos, "Cinematica01.mp4", ScaleMode.ScaleToFit);

#endif

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
