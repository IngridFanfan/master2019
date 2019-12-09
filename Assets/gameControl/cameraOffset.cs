using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOffset : MonoBehaviour
{

    public GameObject tracker;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - tracker.transform.position;
    }

    void LateUpdate()
    {
        transform.position = tracker.transform.position + offset;
    }
}
