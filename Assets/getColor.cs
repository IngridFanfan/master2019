using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getColor : MonoBehaviour
{
    public GameObject landMarkerRing;

    void Start()
    {
        GetComponent<Renderer>().material.color = landMarkerRing.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = landMarkerRing.GetComponent<Renderer>().material.color;
    }
}
