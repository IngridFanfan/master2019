using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameControl : MonoBehaviour
{
    private Text hintMessg;
    private Text countMessg;

    void Start()
    {
        hintMessg = GetComponent<sceneControl>().hintMessg;
        countMessg = GetComponent<sceneControl>().countMessg;
        controlGame();
    }

    void Update()
    {
        controlGame();
    }

    void controlGame()
    {
        if (hintMessg.text != "" || countMessg.text != "")
        {
            GetComponent<rotationToTranslation>().enabled = false;
            //GetComponent<rotationToTranslation_Konst>().enabled = false;
            GameObject.Find("touchpad").GetComponent<touchpadControl>().enabled = false;
            GameObject.Find("Exit").GetComponent<exitCreate>().enabled = false;
        }
        else
        {
            GetComponent<rotationToTranslation>().enabled = true;
            //GetComponent<rotationToTranslation_Konst>().enabled = true;
            GameObject.Find("touchpad").GetComponent<touchpadControl>().enabled = true;
            GameObject.Find("Exit").GetComponent<exitCreate>().enabled = true;

            //arrow = GameObject.FindGameObjectsWithTag("arrow");
        }
    }
}
