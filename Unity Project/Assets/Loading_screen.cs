using UnityEngine;
using System.Collections;

public class Loading_screen : MonoBehaviour {

    public Texture image;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    
    void OnGUI()
    {
        Color GuiColor;
        GuiColor.r = GuiColor.g = GuiColor.b = 1.0f;
        GuiColor.a = 0.5f;
        GUI.color = GuiColor;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image);

        /*if (GuiColor.a > 0)
        {
            GuiColor.a -= 0.01f;
        }
        else
        {
            GuiColor.a = 0.0f;
        }*/
    }
}
