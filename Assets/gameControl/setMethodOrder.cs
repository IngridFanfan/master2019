using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setMethodOrder : MonoBehaviour
{

    public int[] conditionList = new int[3];

    private GameObject ring;
    private GameObject arrow;

    private int conditionN = 0;
    public int conditionOrder = 0;

    private float timer = 0; 
    public float accomplishedTime = 0;

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    public bool showingMessage = true;

    public bool midfieldScene = false;
    public bool accompTask = false;

    void Start()
    {
        hintMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Simple>().hintMessg;
        countMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Simple>().countMessg;
        arrow = GameObject.FindGameObjectWithTag("arrow");
        ring = GameObject.FindGameObjectWithTag("markerRing");
    }

    void Update()
    {
        hintMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Simple>().hintMessg;
        countMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Simple>().countMessg;

        dMessg = GameObject.Find("Avatar").GetComponent<interimScene>().doneText;
        tMessg = GameObject.Find("Avatar").GetComponent<interimScene>().transitMessg;
        sMessg = GameObject.Find("Avatar").GetComponent<interimScene>().startText;

        conditionOrder = conditionN <= 2 ? conditionList[conditionN] : conditionList[conditionList.GetLength(0) - 1];

        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        if(ring.GetComponent<Renderer>().material.color == new Color())
        {

        }
    }
}
