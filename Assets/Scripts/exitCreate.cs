using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitCreate : MonoBehaviour
{
    public GameObject arrow;
    public GameObject indicator;

    private Vector3 initialArrPos;

    private GameObject exitArrow;
    private GameObject indicators;
    private float[,] posArray;


    void Start()
    {

        //Arrow create
        initialArrPos = GameObject.Find("Cylinder").GetComponent<Transform>().position;
        exitArrow = Instantiate(arrow, new Vector3(initialArrPos.x, 7.0f, initialArrPos.z), Quaternion.identity) as GameObject;
        exitArrow.transform.Rotate(Vector3.right, 180f);
        //arrow.transform.SetPositionAndRotation(initialArrPos, Quaternion.Euler(0, 0, 180));

        //indicator creats:
        posArray = new float[10, 2] { { 0f, -1.5f }, { 0f, 4.5f }, { 0f, 10.5f }, { 0f, 16.5f }, { 3f, 18f }, { 9f, 18f }, { 12.0f, 19.5f }, { 12f, 24f }, { 18f, 24f }, { 24f, 24f } };

        for (int i = 0; i < posArray.GetLength(0); i++)
        {

            indicators = Instantiate(indicator, new Vector3(posArray[i, 0], -0.09f, posArray[i, 1]), Quaternion.identity) as GameObject;
            indicators.name = "Index " + i;
            indicators.transform.Rotate(Vector3.up, -90f);
            indicators.transform.Rotate(Vector3.right, -90f);

            if (i < 4 || i == 6)
            {
                indicators.transform.Rotate(Vector3.forward, -90f);
            }
        }
    }

    void Update()
    {
        if (Time.frameCount % 100 < 50)
        {
            transitingForward();
        }
        else
        {
            transitingBack();
        }
    }

    void transitingForward()
    {
        if (exitArrow != null && indicators != null)
        {
            exitArrow.transform.Translate(Vector3.up * Time.deltaTime);

            for (int i = 0; i < posArray.GetLength(0); i++)
            {
                GameObject.Find("Index " + i).transform.Translate(Vector3.up * Time.deltaTime * 2.0f);
            }
        } 

        
    }

    void transitingBack()
    {

        if (exitArrow != null && indicators != null)
        {
            exitArrow.transform.Translate(Vector3.down * Time.deltaTime);

            for (int i = 0; i < posArray.GetLength(0); i++)
            {
                GameObject.Find("Index " + i).transform.Translate(Vector3.down * Time.deltaTime * 2.0f);
            }
        }
            

        
    }
}
