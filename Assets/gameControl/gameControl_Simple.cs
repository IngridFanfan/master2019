using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameControl_Simple : MonoBehaviour
{
    private Text hintMessg;
    private Text countMessg;

    void Start()
    {
        hintMessg = GetComponent<sceneControl_Simple>().hintMessg;
        countMessg = GetComponent<sceneControl_Simple>().countMessg;
        controlGame();
    }

    // Update is called once per frame
    void Update()
    {
        controlGame();

    }

    void controlGame()
    {
        if (hintMessg.text != "" || countMessg.text != "")
        {
            GetComponent<setControlMethod>().disableScript();
        }
        else
        {
            
        }
    }
}
