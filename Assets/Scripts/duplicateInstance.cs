using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class duplicateInstance : MonoBehaviour
{

    private GameObject[] arrow;
    private GameObject[] ring;
    private GameObject[] centerRing;
    private GameObject playSymbol;
    private bool setActived = false;

    public int currentOrder; //the order of the Gameobject of which should be displayed.
    public int n = 0;


    public int[] conditionList = new int[4];//the arrangement of condition(controller, tracker-constant, tracker-tilt), should be given for each tester
    public int[] ringList = new int[4]; //the arrangement of landMarkerRing, should be given for each tester

    private int conditionN = 0;
    public int conditionOrder = -1; //the order of the conditions, decided by the condition array and conditionN;


    private float timer = 0; //This should be setted as 0 only when the hint-Message is gone and added by time.
    public float accomplishedTime = 0;
    public float sumDistance = -1;
    public int round = 1;

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    public bool showingMessage = true;

    public bool midfieldScene = false;
    public bool accompTask = false;

    private Color notTouched = new Color(0.783f, 0.218f, 0.218f, 0.5f);//red

    void Start()
    {

        arrow = GameObject.FindGameObjectsWithTag("arrow");
        ring = GameObject.FindGameObjectsWithTag("markerRing");
        centerRing = GameObject.FindGameObjectsWithTag("point");
        playSymbol= GameObject.Find("playSymbol");


        hintMessg = GetComponent<sceneControl_Simple>().hintMessg;
        countMessg = GetComponent<sceneControl_Simple>().countMessg;

    }

    void Update()
    {
        hintMessg = GetComponent<sceneControl_Simple>().hintMessg;
        countMessg = GetComponent<sceneControl_Simple>().countMessg;

        dMessg = GetComponent<interimScene>().doneText;
        tMessg = GetComponent<interimScene>().transitMessg;
        sMessg = GetComponent<interimScene>().startText;

        conditionOrder = conditionN <= 3 ? conditionList[conditionN] : conditionList[conditionList.GetLength(0) - 1];

        //n = checkVisited();

        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        //for each single round
        if (n == 4 && round < 3)
        {
            accomplishedTime = timer;
            sumDistance = GetComponent<visitMarkerRing>().roundDistance;
            //if (accomplishedTime != 0) print("++++++++++++++++ accomplishedTime +++++++++++++++" + accomplishedTime);
            n = 0;
            timer = 0;
            GetComponent<visitMarkerRing>().roundDistance = 0;
            //GameObject.Find("Avatar").GetComponent<visitMarkerRing>().markerInfo = "";
            resetVisited(); //the all values of "visited" in landerMarker array will be reseted as false, so the currentOrder will also be reseted as the first value of ringList.
            round++;
        }

        //for each single condition
        if (round == 3) //start a new round for a new controller. All data should be resetted a little later, to pass them through-
        { 
            lagTime();
            midfieldScene = true;
            round = 1;
            conditionN++;
            n = 0;
            timer = 0;
            resetVisited();
            GetComponent<setControlMethod>().enterSwitch = true;
            GetComponent<visitMarkerRing>().roundDistance = 0;

        }


        if (!showingMessage)
        {
            timer += Time.deltaTime;
            currentOrder = n <= 3 ? ringList[n] : ringList[ringList.GetLength(0) - 1];
            symbolControl();
        }
        else
        {
            currentOrder = -1;
            symbolControl();
        }


        //if (!setActived)
        //{
        activeMarkerRing();
            //setActived = true;
        //}

        tranformArrow();

        if (conditionN == 4)
        {
            accompTask = true;
            midfieldScene = false;
        }
    }


    void tranformArrow()
    {
        if (Time.frameCount % 100 < 50)
        {

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].transform.Translate(Vector3.up * Time.deltaTime*3);
            }

        }
        else
        {

            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].transform.Translate(Vector3.down * Time.deltaTime*3);
            }

        }
    }

    //int checkVisited() //check how many objects are marked in "visited", to decide the order of marker should be showed.
    //{
    //    int m = 0;
    //    for (int i = 0; i < landMarkers.GetLength(0); i++)
    //    {
    //        m = (landMarkers[i].cylinder.GetComponent<orderControl>().markerVisited == true) ? (m + 1) : m;
    //    }

    //    return m;
    //}

    //void orderCheck(int m) //mark the marker should be displayed => with attribute "markerOrder = true".
    //{

    //    if (currentOrder != -1)
    //    {
    //        for (int i = 0; i < landMarkers.GetLength(0); i++)
    //        {
    //            if (int.Parse(landMarkers[i].cylinder.name) == m)
    //            {
    //                landMarkers[i].cylinder.GetComponent<orderControl>().markerOrder = true;
    //            }
    //            else
    //            {
    //                landMarkers[i].cylinder.GetComponent<orderControl>().markerOrder = false;
    //            }
    //        }

    //    }


    //}

    //void next() //set the object with "markerOrder = true" active and all others as not active.
    //{
    //    if (currentOrder != -1)
    //    {
    //        for (int i = 0; i < landMarkers.GetLength(0); i++)
    //        {
    //            if (landMarkers[i].cylinder.GetComponent<orderControl>().markerOrder == false)
    //            {
    //                landMarkers[i].cylinder.SetActive(false);
    //            }
    //            else
    //            {
    //                landMarkers[i].cylinder.SetActive(true);
    //            }
    //        }
    //    }


    //}


    void activeMarkerRing()
    {
        deactiveAll();

        for (int i = 0; i < ring.GetLength(0); i++)
        {
            if (i == currentOrder)
            {
                ring[i].SetActive(true);
                arrow[i].SetActive(true);
                centerRing[i].SetActive(true);
            }

        }

    }

    void deactiveAll()
    {

        for (int i = 0; i < ring.GetLength(0); i++)
        {
            ring[i].SetActive(false);
            arrow[i].SetActive(false);
            centerRing[i].SetActive(false);
        }

    }

    void resetVisited()
    {

        for (int i = 0; i < ring.GetLength(0); i++)
        {
            ring[i].GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of all rings;
            arrow[i].GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of all arrow;
            centerRing[i].GetComponent<Renderer>().material.SetColor("_Color", notTouched);//reset the color of all center points
        }
    }

    IEnumerator lagTime()
    {
       
        yield return new WaitForSeconds(0.5f);
        accomplishedTime = 0;
        sumDistance = 0;
    }

    void symbolControl()
    {
        if (!showingMessage && (conditionOrder == 2 || conditionOrder == 3)) playSymbol.SetActive(true);
        else playSymbol.SetActive(false);
    }

}

