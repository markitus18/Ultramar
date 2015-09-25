using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //public string  Path;

        Handheld.PlayFullScreenMovie("StreamingAssets / Cinematica01.mp4", Color.black, FullScreenMovieControlMode.Minimal, FullScreenMovieScalingMode.AspectFit);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
