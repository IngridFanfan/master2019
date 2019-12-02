using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitControl : MonoBehaviour
{
    private Color notTouched = new Color(0.783f, 0.218f, 0.218f, 0.5f); //red
    private Color touching = new Color(0.9f, 0.9f, 0.39f, 0.5f);//yellow
	private Color gotTouched = new Color(0.57f, 0.84f, 0.58f, 0.5f);//green

    private float timer = 0.0f;
    public float resetTime = 0.0f;

    private GameObject ring;
    private GameObject arrow;
    private GameObject[] indicatorArrow;
    private GameObject playSymbol;

    public string collisionInfo = ""; //information about collision
    public string taskInfo = "";//information about reached area;
    private Vector3 diffVec = Vector3.zero;
    private int count = 0;
    public int collisionCount = -1;

    public int[] conditionList = new int[4];//the arrangement of condition(controller, tracker-constant, tracker-tilt), should be given for each tester

    private int conditionN = 0;
    public int conditionOrder = -1; //the order of the conditions, decided by the condition array and conditionN;

    public int[] pathList = new int[4];
    private int pathN = 0;
    public int pathOrder = -1;

    private float runTimer = 0; //This should be setted as 0 only when the hint-Message is gone and added by time.
    public float accomplishedTime = 0;
    public int round = 1;

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    public bool showingMessage = true;

    public bool midfieldScene = false;
    public bool accompTask = false;
    private bool conditionDone = false;

    void Start()
    {

        ring = GameObject.Find("landMarkerRing");
        arrow = GameObject.Find("exitArrow");
        playSymbol = GameObject.Find("playSymbol");

        //Arrow create
        Vector3 initialArrPos = new Vector3(0, 60, 250);
        arrow.transform.SetPositionAndRotation(initialArrPos, Quaternion.Euler(0, 90, 180));

        hintMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Obstacle>().hintMessg;
        countMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Obstacle>().countMessg;

    }

    void Update()
    {

        indicatorArrow = GameObject.FindGameObjectsWithTag("indicateArrow");

        hintMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Obstacle>().hintMessg;
        countMessg = GameObject.Find("Avatar").GetComponent<sceneControl_Obstacle>().countMessg;

        dMessg = GameObject.Find("Avatar").GetComponent<interimScene>().doneText;
        tMessg = GameObject.Find("Avatar").GetComponent<interimScene>().transitMessg;
        sMessg = GameObject.Find("Avatar").GetComponent<interimScene>().startText;


        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        if(conditionDone && round < 2)
        {
            resetTurn();
            round++;
            conditionDone = false;

        }

        if(conditionDone && round == 2)
        {
            resetTurn();
            conditionN++;
            pathN++;
            midfieldScene = true;
            GetComponent<setControlMethod>().enterSwitch = true;
            //GetComponent<setScenePath>().enterSwitch = true;
            round = 1;
            conditionDone = false;
        }

        if (!showingMessage)
        {
            runTimer += Time.deltaTime;
            conditionOrder = conditionN <= 3 ? conditionList[conditionN] : conditionList[conditionList.GetLength(0) - 1];
            pathOrder = pathN <= 3 ? pathList[pathN] : pathList[pathList.GetLength(0) - 1];
            arrow.SetActive(true);
            for (int i = 0; i < indicatorArrow.GetLength(0); i++)
            {
                indicatorArrow[i].SetActive(true);
            }
            symbolControl();
        }
        else
        {
            arrow.SetActive(false);
            for (int i = 0; i < indicatorArrow.GetLength(0); i++)
            {
                indicatorArrow[i].SetActive(false);
            }
            symbolControl();
        }

        if (conditionN == 4)
        {
            accompTask = true;
            midfieldScene = false;
        }

        transitingArrows();

        
    }

    void OnTriggerEnter(Collider other) 
	{

            timer = 0.0f;
            resetTime = 5.0f;

        if (ring.GetComponent<Renderer>().material.color != gotTouched)
            {
                ring.GetComponent<Renderer>().material.SetColor("_Color", touching);
                arrow.GetComponent<Renderer>().material.SetColor("_Color", touching);

            }
        
    }

    void OnTriggerStay(Collider other)
    {
        
       timer += Time.deltaTime;
       resetTime -= Time.deltaTime;

        if (timer >= 3.0f )
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", gotTouched);
            arrow.GetComponent<Renderer>().material.SetColor("_Color", gotTouched);

            //arrow.SetActive (false);

            if (timer >= 5.0f)
            {
                accomplishedTime = runTimer;
                collisionCount = count;
                //print("collisionCount: " + collisionCount);
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
            }

        }


    }

    void OnTriggerExit(Collider other)
    {
            resetTime = 0.0f;

        //if (timer < 5.0f && ring.gameObject.GetComponent<Renderer>().material.color != gotTouched)
        //    {
                ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched);
                arrow.GetComponent<Renderer>().material.SetColor("_Color", notTouched);
                //arrow.SetActive (true);
            //}
        taskInfo = "";
        //}

    }

    void OnCollisionEnter(Collision collision){  

        count++;
        diffVec = collision.gameObject.transform.position - transform.position;
        collisionInfo += "Collision count: " + count + ";\n";
        collisionInfo += "Collision with: " + collision.gameObject.name + ";\n";
        collisionInfo += "Difference Vector: " + diffVec.ToString("F3") + "; \n";
        collisionInfo += "Distance: " + diffVec.magnitude + "; \n";

    }

    private void OnCollisionExit(Collision collision)
    {
        collisionInfo = "";

    }

    IEnumerator lagTime()
    {

        yield return new WaitForSeconds(0.1f);

        accomplishedTime = 0;
        collisionCount = -1;
        GetComponent<setScenePath>().enterSwitch = true;

    }

    void transitingArrows()
    {
        if (Time.frameCount % 100 < 50)
        {
            arrow.transform.Translate(Vector3.up * Time.deltaTime * 7);
            for (int i = 0; i < indicatorArrow.GetLength(0); i++)
            {
                indicatorArrow[i].transform.Translate(Vector3.up * Time.deltaTime * 2.0f);
            }
        }
        else
        {
            arrow.transform.Translate(Vector3.down * Time.deltaTime * 7);
            for (int i = 0; i < indicatorArrow.GetLength(0); i++)
            {
                indicatorArrow[i].transform.Translate(Vector3.down * Time.deltaTime * 2.0f);
            }
        }
    }

    void resetTurn()
    {
        transform.position = new Vector3(0, 0, 0);
        StartCoroutine(lagTime());
        ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the ring;
        arrow.GetComponent<Renderer>().material.SetColor("_Color", notTouched); //reset the color of the arrow;
        count = 0;
    }

    void symbolControl()
    {
        if (!showingMessage && (conditionOrder == 2 || conditionOrder == 3)) playSymbol.SetActive(true);
        else playSymbol.SetActive(false);
    }
}
