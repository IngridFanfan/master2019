using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPosition : MonoBehaviour
{
    private int conditionOrder;

    public Transform tracker;
    public Transform controller;

    void Update()
    {
        if (GameObject.Find("Avatar").GetComponent<duplicateInstance>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<duplicateInstance>().conditionOrder;
        }else if (GameObject.Find("Avatar").GetComponent<exitControl>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<exitControl>().conditionOrder;

        }else if (GameObject.Find("Avatar").GetComponent<mazePlay>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<mazePlay>().conditionOrder;

        }

        if (conditionOrder == 2 || conditionOrder == 3)
        {
            transform.localPosition = tracker.position;
        }
        else if (conditionOrder == 0 || conditionOrder == 1)
        {

            transform.localPosition = new Vector3(controller.localPosition.x, controller.localPosition.y + 0.045f, controller.localPosition.z + 0.02f);

        }

    }
}
