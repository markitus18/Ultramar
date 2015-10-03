using UnityEngine;
using System.Collections;

public class Loading_screen : MonoBehaviour {

    public Texture image;
    Color GuiColor;

    // Use this for initialization
    void Start () {
        GuiColor.a = 1.0f;
	}

    // Update is called once per frame
    void Update()
    {
        if (GuiColor.a > 0)
        {
            GuiColor.a -= 0.01f;
        }
        else
        {
            GuiColor.a = 0.0f;
        }
    }


    void OnGUI()
    {



        GuiColor.r = GuiColor.g = GuiColor.b = 1.0f;
        GUI.color = GuiColor;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image);
    }
}
