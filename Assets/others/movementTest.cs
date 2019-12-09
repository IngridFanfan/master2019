using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementTest : MonoBehaviour
{
    public GameObject tracker;
    private Quaternion prevRotation;
    private Quaternion currentRotation;

    private bool moving = false; //the current movement status.
    private bool preMovement = false;

    private float sumX;
    private float sumZ;

    private void Start()
    {
        Debug.Log("current Time: " + System.DateTime.Now);
        Debug.Log("Test F-10: One round value along X (left): " );
    }

    void Update()
    {
        if (Time.frameCount < 3)
        {
            prevRotation = tracker.transform.rotation;
            preMovement = moving;
        }
        else
        {

            currentRotation = tracker.transform.rotation;

            //rotation around x und z
            toTranslation(prevRotation, currentRotation);

            preMovement = moving;

            prevRotation = currentRotation;

        }
    }

    void toTranslation(Quaternion from, Quaternion to)
    {

        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;

        sumX += floorPointMult.x;
        sumZ += floorPointMult.z;

        Debug.Log("sumX: " + sumX);
        Debug.Log("sumZ: " + sumZ);
        Debug.Log("========");

        //if (Mathf.Abs(floorPointMult.z) >= 0.002f || Mathf.Abs(floorPointMult.x) >= 0.002f )
        //{
        //    moving = true;
        //    print("moving");
        //}
        //else if(Mathf.Abs(floorPointMult.z) <= 0.002f || Mathf.Abs(floorPointMult.x) <= 0.002f)
        //{
        //    moving = false;
        //    print("Not Moving");
        //}


    }

}
