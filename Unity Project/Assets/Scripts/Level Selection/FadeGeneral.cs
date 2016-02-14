using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeGeneral : MonoBehaviour
{

    public bool start = false;
    public bool fadeIn = true;

    public float FadeInDuration = 2.0f;
    public float FadeOutDuration = 2.0f;

    float startTime = 0.0f;
    float maxAlpha;
    // Use this for initialization
    void Start()
    {
        Color myColor = Color.white;
        bool colorFound = false;
        bool isImage = false;
        bool isText = false;
        if (gameObject.GetComponent<Text>())
        {
            myColor = gameObject.GetComponent<Text>().color;
            colorFound = true;
            isText = true;
        }
        else if(gameObject.GetComponent<Image>())
        {
            myColor = gameObject.GetComponent<Image>().color;
            colorFound = true;
            isImage = true;
        }
        if(colorFound)
        {
            maxAlpha = myColor.a;
            if (start)
            {
                if (fadeIn)
                    myColor.a = 0;
                else
                    myColor.a = maxAlpha;
                if (isText)
                    gameObject.GetComponent<Text>().color = myColor;
                else if(isImage)
                    gameObject.GetComponent<Image>().color = myColor;
            }

        }
        else
        {
            Debug.Log("'" + gameObject.name + "' has no color.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            float duration = FadeInDuration;
            if (!fadeIn)
                duration = FadeOutDuration;
            float ratio = (Time.time - startTime) / duration;

            bool isImage = false;
            bool isText = false;

            Color myColor = Color.white;

            //Finding color gameObject variable
            if (gameObject.GetComponent<Image>() != null)
            {
                myColor = gameObject.GetComponent<Image>().color;
                isImage = true;
            }
            else if (gameObject.GetComponent<Text>() != null)
            {
                myColor = gameObject.GetComponent<Text>().color;
                isText = true;
            }

            //Interpolating color through time
            if (fadeIn)
            {
                myColor.a = Mathf.Lerp(0, maxAlpha, ratio);
            }
            else
            {
                myColor.a = Mathf.Lerp(maxAlpha, 0, ratio);
            }

            //Deactivate object if it ends a fade out
            if (Time.time - startTime > duration)
            {
                if (fadeIn)
                {
                    start = false;
                }
                else
                {
                  gameObject.SetActive(false);
                }
            }

            //Changing gameObject color
            if (isImage)
            {
                gameObject.GetComponent<Image>().color = myColor;
            }
            else if (isText)
            {
                gameObject.GetComponent<Text>().color = myColor;
            }
            else
                start = false;
        }
    }

    public void DoFade(bool fIn)
    {
        //Starting fade on each child
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            FadeGeneral fadeScript = child.GetComponent<FadeGeneral>();
            fadeScript.DoFade(fIn);
        }

        //Starting self fade
        fadeIn = fIn;
        start = true;
        startTime = Time.time;

        if (fadeIn)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }
        else
        {
            Color myColor = Color.white;

            if (gameObject.GetComponent<Text>())
            {
                myColor = gameObject.GetComponent<Text>().color;
                maxAlpha = myColor.a;
                gameObject.GetComponent<Text>().color = myColor;
            }
            else if (gameObject.GetComponent<Image>())
            {
                myColor = gameObject.GetComponent<Image>().color;
                maxAlpha = myColor.a;
                gameObject.GetComponent<Image>().color = myColor;
            }
        }
    }
}