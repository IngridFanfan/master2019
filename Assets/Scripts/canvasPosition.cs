using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasPosition : MonoBehaviour
{
    public GameObject cam;

    private Vector3 offsetPos;
    private float posY;
    private float rotateY;
    private Quaternion prevRotation;
    private Quaternion currentRotation;

    void Start()
    {
        offsetPos = transform.position - cam.transform.position;
        posY = transform.position.y;
        rotateY = cam.transform.rotation.eulerAngles.y;
        transform.Rotate(0, rotateY, 0);
        prevRotation = cam.transform.rotation;
    }

    void Update()
    {
        currentRotation = cam.transform.rotation;
        rotateY = currentRotation.eulerAngles.y - prevRotation.eulerAngles.y;
        transform.Rotate(0, rotateY, 0);
        transform.position = new Vector3(cam.transform.position.x + offsetPos.x, posY, cam.transform.position.z + offsetPos.z);

        prevRotation = currentRotation;
    }
}
