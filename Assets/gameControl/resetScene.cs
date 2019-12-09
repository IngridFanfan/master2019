using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class resetScene : MonoBehaviour
{
    private float resetTime;
    public Text resetText;
    public Text reset;

    private bool simple = false;
    private bool path = false;
    private bool maze = false;

    private void Start()
    {
        if (GetComponent<visitMarkerRing>())
        {
            simple = true;
            path = false;
            maze = false;
        }
        if (GetComponent<exitControl>())
        {
            path = true;
            simple = false;
            maze = false;
        } 
        if (GetComponent<mazePlay>())
        {
            maze = true;
            simple = false;
            path = false;
        }
    }

    void Update()
    {
        if(simple) resetTime = GetComponent<visitMarkerRing>().resetTime;
        if(path) resetTime = GetComponent<exitControl>().resetTime;
        if (maze) resetTime = GetComponent<mazePlay>().resetTime;

        if (resetTime != 0.0f) setResetText();
        else {
            resetText.text = "";
            reset.text = "";
        } 
    }

    void setResetText()
    {

        if (Math.Round(resetTime, 0) != 0) resetText.text = "Reset in : " + Math.Round(resetTime, 0).ToString() + " s";
        else {
            resetText.text = "";
            reset.text = "Reset";
        } 

    }
}
