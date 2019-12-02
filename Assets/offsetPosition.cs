using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class offsetPosition : MonoBehaviour
{
    public GameObject camTracker;
    public Transform hmd;

    private Vector3 offset;
    private Quaternion rotation;

    void Update()
    {

        if (Time.frameCount < 3) offset = camTracker.transform.position - hmd.position;
        else transform.localPosition = new Vector3(offset.x, camTracker.transform.position.y - 0.05f, offset.z);

        //offset = camTracker.transform.position - hmd.position;
        //transform.localPosition = new Vector3(offset.x, camTracker.transform.position.y - 0.05f, offset.z);


    }
}
