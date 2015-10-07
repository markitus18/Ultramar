using UnityEngine;
using System.Collections;

public class FadeGui : MonoBehaviour {

    public Texture image;
    public int ticksToWait = 50;
    public int tickCount;
    public float tickOpacityLoose = 0.01f;
    Color GuiColor;
    bool start;
    public bool fadeOnInput = true;

    // Use this for initialization
    void Start () {
        tickCount = 0;
        GuiColor.a = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (start==false && tickCount > ticksToWait/2 && fadeOnInput == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
            {
                if (GameControl.control.firstKinematic)
                {
                    Application.LoadLevel(11);
                }
                start = true;
            }
        }
        if (start == false && tickCount > ticksToWait / 2 && fadeOnInput == false)
        {
                start = true;
        }
        else if (start == true)
        {
            if (GuiColor.a > 0)
            {
                GuiColor.a -= tickOpacityLoose;
            }
            else
            {
                GuiColor.a = 0.0f;
            }
        }
        else
        {
            tickCount++;
        }
    }

    void LateUpdate()
    {

    }

    void OnGUI()
    {
        GuiColor.r = GuiColor.g = GuiColor.b = 1.0f;
        GUI.color = GuiColor;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image);
    }
}
