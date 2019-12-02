using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readTrackerOrient : MonoBehaviour
{

    private float yaw;
    void Update()
    {
        yaw = GameObject.Find("Tracker").GetComponent<getTrackerOrientation>().yaw;
        transform.rotation = Quaternion.Euler(0, yaw * Mathf.Rad2Deg , 0);
    }
}
