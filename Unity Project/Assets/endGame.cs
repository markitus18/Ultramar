using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endGame : MonoBehaviour {

    public Texture image;
    public int ticksToWait = 50;
    public int tickCount;
   Color GuiColor;

    // Use this for initialization
    void Start()
    {
        tickCount = 0;
        GuiColor.a = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (tickCount > ticksToWait)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            tickCount++;

        }
    }

    void OnGUI()
    {
        GuiColor.r = GuiColor.g = GuiColor.b = 1.0f;
        GUI.color = GuiColor;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), image);
    }
}
