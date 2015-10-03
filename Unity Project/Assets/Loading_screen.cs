using UnityEngine;
using System.Collections;

public class Loading_screen : MonoBehaviour {

    public Texture image;
    public float tickOpacityLoose = 0.01f;
    CameraOrbit CameraScript;
    Color GuiColor;
    bool start;

    // Use this for initialization
    void Start () {
        GuiColor.a = 1.0f;
        CameraScript = GameObject.Find("Main Camera").GetComponent<CameraOrbit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start==false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
            {
                start = true;
                CameraScript.loading = false;
            }
        }
        if (start == true)
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
