using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class sceneControl_Obstacle : MonoBehaviour
{
    public Text countMessg;
    public Text hintMessg;
    public Text hintMessg2;
    private bool accompTask;

    private float timeLeft;
    private float timer;

    void Start()
    {
        timeLeft = 5.0f;
        timer = 0.0f;

        SetCountText();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        SetCountText();
    }

    void SetCountText()
    {
        countMessg.text = "Start in : " + Math.Round(timeLeft, 0).ToString() + " s";
        hintMessg.text = "";
        hintMessg2.text = "";
        accompTask = GameObject.Find("Avatar").GetComponent<exitControl>().accompTask;

        if (Math.Round(timeLeft, 0) <= 0)
        {
            countMessg.text = "";
            timer += Time.deltaTime;
            if (timer >= 0 && timer <= 1)
            {
                hintMessg.text = "Start";

            }
            else
            {
                hintMessg.text = "";
            }
        }

        if (accompTask)
        {
            hintMessg.text = "Well Done!";
            hintMessg2.text = "We are going to the next scene";
            GetComponent<setControlMethod>().disableScript();
        }
    }
}
