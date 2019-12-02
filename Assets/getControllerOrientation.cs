using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getControllerOrientation : MonoBehaviour
{
    //public GameObject cam;
    //public Vector3 orientation = Vector3.zero;
    //public Vector3 perpendicular = Vector3.zero;
    //private Vector3 bodyPosition = Vector3.zero;
    //private Vector3 controllerPosition;
    //public float yaw;
    //private float yawVert;

    public Vector3 orientation;
    public Vector3 perpendicular;

    public float yaw;

    private float yawVert;

    // Update is called once per frame
    void Update()
    {
        //bodyPosition = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        //controllerPosition = new Vector3(transform.position.x, 0, transform.position.z);
        //orientation = (controllerPosition - bodyPosition).normalized;

        //yaw = getFullRadDegree(orientation);

        //yawVert = (yaw * Mathf.Rad2Deg + 90) * Mathf.Deg2Rad;
        //perpendicular = new Vector3(Mathf.Sin(yawVert), 0, Mathf.Cos(yawVert)).normalized;

        yaw = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        yawVert = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;

        orientation = new Vector3(Mathf.Sin(yaw), 0, Mathf.Cos(yaw)).normalized;
        perpendicular = new Vector3(Mathf.Sin(yawVert), 0, Mathf.Cos(yawVert)).normalized;

        //Debug.DrawLine(Vector3.zero, orientation * 10, Color.green);
        //Debug.DrawLine(Vector3.zero, perpendicular * 10, Color.blue);
    }

    float getFullRadDegree(Vector3 ort)
    {
        float deg = Mathf.Atan2(ort.z, ort.x) * Mathf.Rad2Deg; //the angle between Vector and X-axis-POSITIVE

        float y = 0;

        if (ort.x < 0 && ort.z >= 0)// IV quadrant
        {
            y = 450 - deg; //360 - (deg - 90)
        }
        else //// I, II & III quadrant
        {
            y = 90 - deg;
        }

        return y * Mathf.Deg2Rad;
    }
}