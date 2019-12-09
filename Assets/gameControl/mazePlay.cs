using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mazePlay : MonoBehaviour
{
    private Color notTouched = new Color(0.783f, 0.218f, 0.218f, 0.5f); //red
    private Color touching = new Color(0.9f, 0.9f, 0.39f, 0.5f);//yellow
    private Color gotTouched = new Color(0.57f, 0.84f, 0.58f, 0.5f);//green

    private float timer = 0.0f;
    public float resetTime = 0.0f;

    private GameObject ring;
    private GameObject exitArrow;
    private GameObject wim;
    public GameObject sephrer;
    public GameObject pointer;

    public GameObject exitWall;


    public int[] conditionList = new int[4];//the arrangement of condition(controller, tracker-constant, tracker-tilt), should be given for each tester
    private int conditionN = 0;
    public int conditionOrder = 0; //the order of the conditions, decided by the condition array and conditionN;

    public int[] pathList = new int[4];
    private int pathN = 0;
    public int pathOrder = -1;

    private bool conditionDone = false;
    public bool midfieldScene = false;
    public bool accompTask = false;

    public string collisionInfo = ""; //information about collision
    public string taskInfo = "";//information about reached area;
    private Vector3 diffVec = Vector3.zero;
    private int count = 0;
    public int collisionCount = -1;

    private float runTimer = 0; //This should be setted as 0 only when the hint-Message is gone and added by time.
    public float accomplishedTime = 0;
    public int round = 1;

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    public bool showingMessage = true;

    

    void Start()
    {
        ring = GameObject.Find("markerRing");
        exitArrow = GameObject.Find("exitArrow");
        wim = GameObject.Find("WIMGroup");

    }

    void Update()
    {

        hintMessg = GetComponent<sceneControl>().hintMessg;
        countMessg = GetComponent<sceneControl>().countMessg;

        dMessg = GetComponent<interimScene>().doneText;
        tMessg = GetComponent<interimScene>().transitMessg;
        sMessg = GetComponent<interimScene>().startText;


        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        if (conditionN == 4)
        {
            accompTask = true;
            midfieldScene = false;
        }

        if (conditionDone && round < 2)
        {
            resetTurn();
            round++;
            conditionDone = false;

        }

        if (conditionDone && round == 2)
        {
            resetTurn();
            conditionN++;
            pathN++;
            midfieldScene = true;
            GetComponent<setControlMethod>().enterSwitch = true;
            GetComponent<setScenePath>().enterSwitch = true;
            round = 1;
            conditionDone = false;
        }

        if (!showingMessage)
        {
            runTimer += Time.deltaTime;
            conditionOrder = conditionN <= 3 ? conditionList[conditionN] : conditionList[conditionList.GetLength(0) - 1];
            pathOrder = pathN <= 3 ? pathList[conditionN] : pathList[pathList.GetLength(0) - 1];
            exitArrow.SetActive(true);
            exitWall.SetActive(true);
            ring.SetActive(true);
            wim.SetActive(true);
            transitingArrows();
            wimControl(); //show the wim or not

        }
        else
        {
            exitArrow.SetActive(false);
            exitWall.SetActive(false);
            ring.SetActive(false);
            wim.SetActive(false);
            wimControl();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        timer = 0.0f;
        resetTime = 5.0f;

        if (ring.GetComponent<Renderer>().material.color != gotTouched)
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", touching);
            exitArrow.GetComponent<Renderer>().material.SetColor("_Color", touching);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;
        resetTime -= Time.deltaTime;

        if (timer >= 3.0f)
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", gotTouched);
            exitArrow.GetComponent<Renderer>().material.SetColor("_Color", gotTouched);

            if (timer >= 5.0f)
            {
                accomplishedTime = runTimer;
                collisionCount = count;

                diffVec = other.gameObject.transform.position - transform.position;

                taskInfo += "--- Turn completed with: --- \n";
                taskInfo += "Round: " + round + "; \n";
                taskInfo += "Avatar position:" + transform.position.ToString("F3") + "; \n";
                taskInfo += "Difference Vector: " + diffVec.ToString("F3") + "; \n";
                taskInfo += "Distance: " + diffVec.magnitude + "; \n";
                conditionDone = true;
                runTimer = 0;
                count = 0;
                timer = 0;

                //if (round < 2)
                //{
                //    setNextTurn();
                //    round++;
                //}

                //if (round == 2)
                //{
                //    setNextTurn();
                //    conditionN++;
                //    midfieldScene = true;
                //    //GetComponent<setControlMethod>().enterSwitch = true;
                //    round = 1;
                //}

                

            }

        }

        //print("time: " + timer);
    }

    private void OnTriggerExit(Collider other)
    {
        resetTime = 0.0f;

        //if (timer < 5.0f && ring.GetComponent<Renderer>().material.color != gotTouched)
        //{
            ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched);
            exitArrow.GetComponent<Renderer>().material.SetColor("_Color", notTouched);

        //}

        taskInfo = "";
    }

    private void OnCollisionEnter(Collision collision)
    {
        count++;
        diffVec = collision.gameObject.transform.position - transform.position;
        collisionInfo += "Collision count: " + count + ";\n";
        collisionInfo += "Collision with: " + collision.gameObject.name + ";\n";
        collisionInfo += "Avatar position: " + transform.position.ToString("F3") + ";\n";
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionInfo = "";
    }

    void setNextTurn()
    {
        //transform.position = new Vector3(0, 0, 0);
        StartCoroutine(lagTime());
        //conditionN++;
        //midfieldScene = true;
        ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the ring;
        exitArrow.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the arrow;
        count = 0;
    }

    void transitingArrows()
    {
        if (Time.frameCount % 100 < 50)
        {
            exitArrow.transform.Translate(Vector3.up * Time.deltaTime * 5);

        }
        else
        {
            exitArrow.transform.Translate(Vector3.down * Time.deltaTime * 5);
        }
    }

    IEnumerator lagTime()
    {

        yield return new WaitForSeconds(0.1f);
        accomplishedTime = 0;
        collisionCount = -1;
        GetComponent<setControlMethod>().enterSwitch = true;

    }

    void wimControl()
    {

        //if (!showingMessage && (conditionOrder == 2 || conditionOrder == 3)) wim.SetActive(true);
        //else wim.SetActive(false);

        if (!showingMessage && (conditionOrder == 2 || conditionOrder == 3))
        {
            sephrer.SetActive(true);
            pointer.SetActive(true);
        }
        else
        {
            sephrer.SetActive(false);
            pointer.SetActive(false);
        }
    }

    void resetTurn()
    {
        transform.position = new Vector3(0, 0.8f, 0);
        StartCoroutine(lagTime());
        ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the ring;
        exitArrow.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the arrow;
        count = 0;
    }
}
