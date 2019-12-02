using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRelativePos : MonoBehaviour
{
    public Transform avatar;
    public GameObject controller;
    public GameObject tracker;

    private Vector3 prevPosition;
    private Vector3 currentPosition;

    private float rotateY;

    private float prevRotation;
    private float currentRotation;

    private int conditionOrder;

    void Start()
    {
        conditionOrder = GameObject.Find("Avatar").GetComponent<mazePlay>().conditionOrder;

        prevPosition = avatar.position;

        if(conditionOrder == 2 || conditionOrder == 3)
        {
            rotateY = tracker.GetComponent<getTrackerOrientation>().yaw * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -rotateY);
            prevRotation = tracker.GetComponent<getTrackerOrientation>().yaw;

        }else if(conditionOrder == 0 || conditionOrder == 1)
        {
            rotateY = controller.GetComponent<getControllerOrientation>().yaw * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -rotateY);
            prevRotation = controller.GetComponent<getControllerOrientation>().yaw;
        }
        

    }

    void Update()
    {
        conditionOrder = GameObject.Find("Avatar").GetComponent<mazePlay>().conditionOrder;

        currentPosition = avatar.position;

        Vector3 changedPos = currentPosition - prevPosition;
        transform.position = transform.position + changedPos * 0.0006f;
        prevPosition = currentPosition;

        if (conditionOrder == 2 || conditionOrder == 3)
        {
            currentRotation = tracker.GetComponent<getTrackerOrientation>().yaw;
            rotateY = (currentRotation - prevRotation) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -rotateY);

        }
        else if (conditionOrder == 0 || conditionOrder == 1)
        {
            currentRotation = controller.GetComponent<getControllerOrientation>().yaw;
            rotateY = (currentRotation - prevRotation) * Mathf.Rad2Deg;
            transform.Rotate(0, 0, -rotateY);
        }

        

        prevRotation = currentRotation;

    }
}
