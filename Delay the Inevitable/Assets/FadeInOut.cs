using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FadeInOut : MonoBehaviour
{

    public SpriteRenderer sr;

    public bool Running = false;

    public bool FadeIn = false;

    public float totalTime = 4f;

    private float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        if(sr == null)
            sr = GetComponent<SpriteRenderer>();

        timeStart = Time.time;
        if(FadeIn)
        {
            Color color = sr.color;
            color.a = 0;
            sr.color = color;
        }
        else
        {
            Color color = sr.color;
            color.a = 255;
            sr.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Running)
            return;

        float prop = (Time.time - timeStart) / totalTime;

        Color color = sr.color;
        
        if(FadeIn)
        {
            color = new Color(color.r, color.g, color.b, Mathf.SmoothStep(0, 1, prop));
        }
        else if(!FadeIn) 
        {
            color = new Color(color.r, color.g, color.b, Mathf.SmoothStep(1, 0, prop));
        }
        
        sr.color = color;

        if (FadeIn && color.a == 1 || (!FadeIn && color.a == 0))
        {
            Running = false;
        }

    }

    public void DoFadeIn()
    {
        timeStart = Time.time;
        Running = true;

        FadeIn = true;
    }

    public void DoFadeOut()
    {
        timeStart = Time.time;
        Running = true;

        FadeIn = false;
    }
}
