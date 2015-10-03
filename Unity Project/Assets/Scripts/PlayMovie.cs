using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

#if UNITY_EDITOR
    public MovieTexture movie;
#endif
    public System.String Title;

    // Use this for initialization
    void Start()
    {
        //public string  Path;
#if !UNITY_EDITOR
        Handheld.PlayFullScreenMovie(Title, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
         int toLoad = Application.loadedLevel;
            Application.LoadLevel(toLoad+1);
#endif
#if UNITY_EDITOR
        GetComponent<Renderer>().material.mainTexture = movie;
        GetComponent<AudioSource>().Play();
        movie.Play();

#endif

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (!movie.isPlaying)
        {
            int toLoad = Application.loadedLevel;
			if (toLoad == 1)
				GameControl.control.firstTime = false;
			GameControl.control.Save ();
            Application.LoadLevel(toLoad+1);
        }
#endif
    }
}
