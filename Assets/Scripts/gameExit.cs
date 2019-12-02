using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameExit : MonoBehaviour
{
    private Color notTouched = new Color(0.82f, 0.5f, 0.5f, 0.5f);
    private Color gotTouched = new Color(0.57f, 0.84f, 0.58f, 0.5f);

    private float timer = 0.0f;

    private GameObject ring;
    private GameObject[] arrow;
    private GameObject exitArrow;
    private GameObject wim;
    private float[,] posArray;

    //public GameObject indicator;
    //private GameObject indicators;

    public string collisionInfo = ""; //information about collision
    public string taskInfo = "";//information about reached area;
    private Vector3 diffVec = Vector3.zero;
    private int count = 0;

    public int[] conditionList = new int[4];//the arrangement of condition(controller, tracker-constant, tracker-tilt), should be given for each tester

    private int conditionN = 0;
    public int conditionOrder = -1; //the order of the conditions, decided by the condition array and conditionN;

    private float runTimer = 0; //This should be setted as 0 only when the hint-Message is gone and added by time.
    public float accomplishedTime = 0;

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
        ring = GameObject.Find("markerRing");
        exitArrow = GameObject.Find("exitArrow");
        arrow = GameObject.FindGameObjectsWithTag("indicateArrow");
        wim = GameObject.Find("WIMGroup");

        //conditionOrder = conditionList[conditionN];

    }

    // Update is called once per frame
    void Update()
    {

        hintMessg = GameObject.Find("Avatar").GetComponent<sceneControl>().hintMessg;
        countMessg = GameObject.Find("Avatar").GetComponent<sceneControl>().countMessg;

        dMessg = GameObject.Find("Avatar").GetComponent<interimScene>().doneText;
        tMessg = GameObject.Find("Avatar").GetComponent<interimScene>().transitMessg;
        sMessg = GameObject.Find("Avatar").GetComponent<interimScene>().startText;

        conditionOrder = conditionN <= 3 ? conditionList[conditionN] : conditionList[conditionList.GetLength(0) - 1];

        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        if (conditionDone)
        {
            transform.position = new Vector3(0, 0.8f, 0);
            lagTime();
            conditionN++;
            midfieldScene = true;
            ring.GetComponent<Renderer>().material.SetColor("_Color", Color.white); //reset the color of the ring;
            count = 0;
            GetComponent<setControlMethod>().enterSwitch = true;

            conditionDone = false;

        }

        if (!showingMessage)
        {
            runTimer += Time.deltaTime;
            exitArrow.SetActive(true);
            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].SetActive(true);
            }
            transitingArrows();
            wimControl(); //show the wim or not
        }
        else
        {
            exitArrow.SetActive(false);
            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].SetActive(false);
            }
            wimControl(); 
        }


        if (conditionN == 4)
        {
            accompTask = true;
            midfieldScene = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        timer = 0.0f;
        //arrow = GameObject.FindGameObjectsWithTag("arrow");

        if (ring.GetComponent<Renderer>().material.color != gotTouched)
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", notTouched);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if (timer >= 3.0f)
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", gotTouched);

            setNextTurn(other);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(timer < 3.0f && ring.GetComponent<Renderer>().material.color != gotTouched)
        {
            ring.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            
        }

        taskInfo = "";


    }

    void OnCollisionEnter(Collision collision)
    {

        count++;
        diffVec = collision.gameObject.transform.position - transform.position;
        collisionInfo += "Collision count: " + count + ";\n";
        collisionInfo += "Avatar position: " + transform.position.ToString("F3") + ";\n";
    }

    private void OnCollisionExit(Collision collision)
    {
        collisionInfo = "";

    }

    void setNextTurn(Collider other)
    {

        if (timer >= 5.0f)
        {
            accomplishedTime = runTimer;
            diffVec = other.gameObject.transform.position - transform.position;
            taskInfo += "--- Turn completed with: --- \n";
            taskInfo += "Avatar position:" + transform.position.ToString("F3") + "; \n";
            taskInfo += "Difference Vector: " + diffVec.ToString("F3") + "; \n";
            taskInfo += "Distance: " + diffVec.magnitude + "; \n";
            conditionDone = true;
            runTimer = 0;
            timer = 0;
        }

    }

    IEnumerator lagTime()
    {

        yield return new WaitForSeconds(0.3f);
        accomplishedTime = 0;
    }

    void transitingArrows()
    {
        if (Time.frameCount % 100 < 50)
        {
            exitArrow.transform.Translate(Vector3.up * Time.deltaTime * 5);
            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].transform.Translate(Vector3.up * Time.deltaTime * 2.0f);
            }
        }
        else
        {
            exitArrow.transform.Translate(Vector3.down * Time.deltaTime * 5);
            for (int i = 0; i < arrow.GetLength(0); i++)
            {
                arrow[i].transform.Translate(Vector3.down * Time.deltaTime * 2.0f);
            }
        }
    }

    void wimControl()
    {
        //if (!showingMessage && (conditionOrder == 3 || conditionOrder == 22 || conditionOrder == 2)) wim.SetActive(true);
        if (!showingMessage) wim.SetActive(true);
        else wim.SetActive(false);
    }

}
