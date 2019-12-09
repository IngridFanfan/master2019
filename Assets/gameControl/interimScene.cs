using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interimScene : MonoBehaviour
{
    public Text transitMessg; //the transit message to give the user hint and time to change the condition
    public Text doneText;
    public Text startText; //There should be "Start" as well, because in the open scene there is a "Start", if there isn't start, maybe could cause some delay in sense.

    private float timer = 0.0f;
    private float timeLeft = 10.0f;
    private float timeLeftDone = 2.0f;

    private bool allowToCountDown = false;

    void Start()
    {
        if (GetComponent<duplicateInstance>())
        {
            allowToCountDown = GetComponent<duplicateInstance>().midfieldScene;

        }
        else if (GetComponent<exitControl>())
        {
            allowToCountDown = GetComponent<exitControl>().midfieldScene;
        }
        else if (GetComponent<mazePlay>())
        {
            allowToCountDown = GetComponent<mazePlay>().midfieldScene;
        }
        else
        {
            print("no object referenced!");
        }

        //allowToCountDown = GetComponent<mazePlay>().midfieldScene;


    }

    void Update()
    {
        if (GetComponent<duplicateInstance>())
        {
            allowToCountDown = GetComponent<duplicateInstance>().midfieldScene;
        }
        else if (GetComponent<exitControl>())
        {
            allowToCountDown = GetComponent<exitControl>().midfieldScene;
        }
        else if (GetComponent<mazePlay>())
        {
            allowToCountDown = GetComponent<mazePlay>().midfieldScene;
        }
        else
        {
            Debug.LogError("no object referenced!");
        }

        //allowToCountDown = GetComponent<mazePlay>().midfieldScene;


        if (allowToCountDown)
        {
            timeLeftDone -= Time.deltaTime;

            if (timeLeftDone >= 0)
            {
                doneText.text = "Good job!";
                transitMessg.text = "";
                startText.text = "";
            }
            else
            {
                doneText.text = "";
                timeLeft -= Time.deltaTime;
                SetCountText();
            }

        }
        else
        {
            timer = 0.0f;
            timeLeft = 10.0f;
            timeLeftDone = 2.0f;
            doneText.text = "";
            transitMessg.text = "";
            startText.text = "";
        }
        

    }

    void SetCountText()
    {
        transitMessg.text = "Next turn will started in: " + Math.Round(timeLeft, 0).ToString() + " s";


        if (Math.Round(timeLeft, 0) <= 0)
        {
            transitMessg.text = "";
            timer += Time.deltaTime;
            if (timer >= 0 && timer <= 1)
            {
                startText.text = "Start";

            }
            else
            {
                startText.text = "";
                if (GetComponent<duplicateInstance>())
                {
                    GetComponent<duplicateInstance>().midfieldScene = false;
                }
                else if (GetComponent<exitControl>())
                {
                    GetComponent<exitControl>().midfieldScene = false;
                }
                else if (GetComponent<mazePlay>())
                {
                    GetComponent<mazePlay>().midfieldScene = false;
                }
                else
                {
                    Debug.LogError("interimScene reset error!");
                }

            }
        }
    }
}
