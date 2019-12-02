using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTrackerOrientation : MonoBehaviour
{
    public GameObject cam;
    public Vector3 orientation = Vector3.zero;
    public Vector3 perpendicular = Vector3.zero;
    private Vector3 bodyPosition = Vector3.zero;
    private Vector3 trackerPosition;
    public float yaw;
    private float yawVert;

    void Update()
    {
        bodyPosition = new Vector3(cam.transform.localPosition.x, 0, cam.transform.localPosition.z);
        trackerPosition = new Vector3(transform.position.x, 0 ,transform.position.z);
        orientation = (trackerPosition - bodyPosition).normalized;

        yaw = getFullRadDegree(orientation);

        yawVert = (yaw * Mathf.Rad2Deg + 90) * Mathf.Deg2Rad;
        perpendicular = new Vector3(Mathf.Sin(yawVert), 0, Mathf.Cos(yawVert)).normalized;

        //Debug.DrawLine(new Vector3(0,0.01f,0), orientation * 20, Color.red);
        //print("angle: " + yaw * Mathf.Rad2Deg);

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
