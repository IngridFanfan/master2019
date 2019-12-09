using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getOrientation : MonoBehaviour
{
    public Vector3 orientation;
    public Vector3 perpendicular;

    public float yaw;

    private float yawVert;


    void Start()
    {
        orientation = Vector3.zero;
        perpendicular = Vector3.zero;
        yaw = 0;
        yawVert = 0;
    }

    void Update()
    {
        yaw = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        yawVert = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;

        orientation = new Vector3(Mathf.Sin(yaw), 0, Mathf.Cos(yaw)).normalized;
        perpendicular = new Vector3(Mathf.Sin(yawVert), 0, Mathf.Cos(yawVert)).normalized;

        //Debug.DrawLine(Vector3.zero, orientation * 20, Color.yellow);

    }
}
