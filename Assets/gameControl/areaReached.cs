using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaReached : MonoBehaviour
{
    private Color notTouched = new Color(0.82f, 0.5f, 0.5f, 0.5f);
    private Color gotTouched = new Color(0.57f, 0.84f, 0.58f, 0.5f);

    //private GameObject ring;
    private float timer = 0.0f;
    private float lagTimer = 0.0f;

    //markerRing Collision information
    private Vector3 diffVec = Vector3.zero;
    public string markerInfo = "";
    private int round = 0;

    void Start()
    {
        //Renderer rend = GetComponent<Renderer>();
        //rend.material.SetColor("_Color", notTouched);

    }

    // Update is called once per frame
    void Update()
    {

        //timer += Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {
        //reset the count time in each time you enter the trigger
        timer = 0.0f;

        //set the color of the marker to notice than you enter the trigger
        if (other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color != gotTouched)
        {
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", notTouched);
        }

        //This is how you get a child gameObject when a parent-trigger is entered.
        //Debug.Log("child name: " + other.gameObject.transform.GetChild(0).name);

    }

    void OnTriggerStay(Collider other)
    {
        //set the counte time
        timer += Time.deltaTime;

        //if staying in the trigger more than 5 sec, the marker will turn into green
        if (timer >= 3.0f)
        {
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", gotTouched);

            callNext(other);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //Stay in the trigger less than 5 sec.
        if (timer < 5.0f && other.gameObject.GetComponent<Renderer>().material.color != gotTouched)
        {
            other.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            other.gameObject.GetComponent<orderControl>().markerVisited = false;
        }
    }

    void callNext(Collider other)
    {

        if (timer >= 5.0f)
        {
            other.gameObject.GetComponent<orderControl>().markerVisited = true;
            diffVec = other.gameObject.transform.position - transform.position;
            markerInfo += "Step in ring " + other.gameObject.name + ": \n";
            markerInfo += "Round: " + round + "; ";
            markerInfo += "Difference Vector: " + diffVec.ToString("F3") + "; ";
            markerInfo += "Distance: " + diffVec.magnitude + ";";
            transform.position = Vector3.zero;
            timer = 0;
        }
        else
        {
            other.gameObject.GetComponent<orderControl>().markerVisited = false;
        }
    }


}
