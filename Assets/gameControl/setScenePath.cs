using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setScenePath : MonoBehaviour
{
    private GameObject[] path;
    private GameObject[] wim;

    private bool mazeScene = false;

    private int prevNumber = -1;
    private int currentNumber = -1;
    private int switchNumber = -1;
    public string pathInfo = "";

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    private bool allowSwitch = false; //only when there is no text displayed in the screen, the relevant scripts will be applied.
    public bool enterSwitch = true;

    void Start()
    {

        path = GameObject.FindGameObjectsWithTag("path");
        wim = GameObject.FindGameObjectsWithTag("wim");

        if (GetComponent<exitControl>()) mazeScene = false;
        else mazeScene = true;

        if(!mazeScene) prevNumber = GetComponent<exitControl>().pathOrder;
        else prevNumber = GetComponent<mazePlay>().pathOrder;

        disableAllPath();

    }

    void Update()
    {
        if (!mazeScene) currentNumber = GetComponent<exitControl>().pathOrder;
        else currentNumber = GetComponent<mazePlay>().pathOrder;

        allowSwitch = checkText();

        if (currentNumber != prevNumber)
        {
            switchNumber = currentNumber;
        }

        if (allowSwitch && enterSwitch)
        {
            disableAllPath();

            for(int i = 0; i < path.GetLength(0); i++)
            {
                if (path[i].name == "path (" + (switchNumber + 1) + ")")
                {
                    path[i].SetActive(true);
                    pathInfo = "------ Path name: " + path[i].name + " ------";
                    enterSwitch = false;
                }
            }

            if (mazeScene)
            {
                for (int i = 0; i < path.GetLength(0); i++)
                {
                    if (wim[i].name == "Walls (" + (switchNumber + 1) + ")")
                    {
                        wim[i].SetActive(true);
                    }
                }
            }

        }

        if (!allowSwitch) disableAllPath();

        prevNumber = currentNumber;

    }

    public void disableAllPath()
    {
        for (int i = 0; i < path.GetLength(0); i++)
        {
            path[i].SetActive(false);
        }

        for (int j = 0; j < wim.GetLength(0); j++)
        {
            wim[j].SetActive(false);
        }
    }

    bool checkText()
    {
        bool b = false;

        if (!mazeScene)
        {
            hintMessg = GetComponent<sceneControl_Obstacle>().hintMessg;
            countMessg = GetComponent<sceneControl_Obstacle>().countMessg;
        }
        else
        {
            hintMessg = GetComponent<sceneControl>().hintMessg;
            countMessg = GetComponent<sceneControl>().countMessg;
        }

        dMessg = GetComponent<interimScene>().doneText;
        tMessg = GetComponent<interimScene>().transitMessg;
        sMessg = GetComponent<interimScene>().startText;

        if (hintMessg.text == "" && countMessg.text == "" && dMessg.text == "" && tMessg.text == "" && sMessg.text == "") b = true;
        return b;
    }
}
