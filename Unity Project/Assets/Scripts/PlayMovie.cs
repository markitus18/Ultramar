using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //public string  Path;

        Handheld.PlayFullScreenMovie("Cinematica01.mp4", Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
        Application.LoadLevel(2);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
