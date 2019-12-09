using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPosition : MonoBehaviour
{
    public GameObject cam;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - cam.transform.position;
        print("camera: " + cam.transform.position);
    }

    void LateUpdate()
    {
        transform.position = cam.transform.position + offset;
        print("camera: " + cam.transform.position);
    }
}
