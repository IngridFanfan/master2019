using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visitMarkerRing : MonoBehaviour
{
    private Color notTouched = new Color(0.783f, 0.218f, 0.218f, 0.5f);//red
    private Color touching = new Color(0.9f, 0.9f, 0.39f, 0.5f);//yellow
    private Color gotTouched = new Color(0.57f, 0.84f, 0.58f, 0.5f);//green

    private float timer = 0.0f;
    public float resetTime = 0.0f;

    private Vector3 diffVec = Vector3.zero;
    private float distance = 0;
    public float roundDistance = 0;
    public string markerInfo = "";
    public int round = 0;
    private float sTimer = 0.0f;
    private float singleTime;
    private bool showingMessage;


    private bool enteredCenter = false;
    private GameObject[] centerRing;

    private void Start()
    {
        centerRing = GameObject.FindGameObjectsWithTag("centerRing");
        
    }

    private void Update()
    {
        showingMessage = GetComponent<duplicateInstance>().showingMessage;
        if (showingMessage)
        {
            sTimer = 0;
        }
        else
        {
            sTimer += Time.deltaTime;
        }
        round = GetComponent<duplicateInstance>().round;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("outRing"))
        {
            timer = 0.0f;
            resetTime = 5.0f;
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", touching);
            other.gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", touching);
            //print("enter outer");
        }

        if (other.gameObject.CompareTag("centerRing"))
        {
            //other.gameObject.transform.GetComponent<Renderer>().material.SetColor("_Color", touching);
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", touching);

            enteredCenter = true;
            //print("enter center");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("outRing"))
        {
            timer += Time.deltaTime;
            resetTime -= Time.deltaTime;
            if (timer >= 3.0f)
            {
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", gotTouched);
                other.gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", gotTouched);

                if (enteredCenter)
                {
                    for (int i = 0; i < centerRing.GetLength(0); i++)
                    {
                        centerRing[i].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", gotTouched);
                    }
                }

                if (timer >= 5.0f)
                {
                    singleTime = sTimer;
                    //print("time: " + singleTime);
                    diffVec = other.gameObject.transform.position - transform.position;
                    distance = diffVec.magnitude;
                    roundDistance += distance;
                    //print("roundDistance: " + roundDistance);
                    markerInfo += "Step in " + other.gameObject.name + ": \n";
                    markerInfo += "Round: " + round + "; \n";
                    markerInfo += "Difference Vector: " + diffVec.ToString("F3") + "; \n";
                    markerInfo += "Distance: " + diffVec.magnitude + "; \n";
                    markerInfo += "Time: " + singleTime + "s; \n";
                    transform.position = Vector3.zero;
                    sTimer = 0;
                    timer = 0;
                    GetComponent<duplicateInstance>().n++;
                }
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("centerRing"))
        {

            //print("out of the center");
            for (int i = 0; i < centerRing.GetLength(0); i++)
            {
                centerRing[i].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", notTouched);
            }

            enteredCenter = false;
        }

        if (other.gameObject.CompareTag("outRing"))
        {
            //Stay in the trigger less than 5 sec.
            resetTime = 0.0f;
            if (timer < 5.0f && other.gameObject.GetComponent<Renderer>().material.color != gotTouched)
            {
                other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", notTouched);
                other.gameObject.transform.GetChild(1).GetComponent<Renderer>().material.SetColor("_Color", notTouched);
            }

            //print("out of out ring");
        }

        

        //if (other.gameObject.CompareTag("centerRing"))
        //{
        //    other.gameObject.transform.GetComponent<Renderer>().material.SetColor("_Color", notTouched);
        //    enteredCenter = false;
        //    print("out of the center");
        //}

        markerInfo = "";

    }

}
