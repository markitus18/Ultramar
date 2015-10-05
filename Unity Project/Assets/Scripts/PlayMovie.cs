using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

#if UNITY_EDITOR
    public MovieTexture movie;
#endif
    public System.String Title;
    public int cinematicNumber;

    // Use this for initialization
    void Start()
    {
        //public string  Path;
#if !UNITY_EDITOR
        if (cinematicNumber == 1 && GameControl.control.firstKinematic == true)
        {
                Handheld.PlayFullScreenMovie(Title, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
                GameControl.control.firstKinematic = false;
        }
        else if (cinematicNumber == 2 && GameControl.control.secondKinematic == true)
        {
                Handheld.PlayFullScreenMovie(Title, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
                GameControl.control.secondKinematic = false;
        }
        else
        {
            Handheld.PlayFullScreenMovie(Title, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
        }
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
			if (toLoad == 11)
			{
				GameControl.control.Save ();
           		Application.LoadLevel(1);
			}
			if (toLoad == 13)
			{
				GameControl.control.Save ();
				Application.LoadLevel(6);
			}
        }
#endif
    }
}
