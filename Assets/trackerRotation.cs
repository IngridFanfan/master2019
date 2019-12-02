using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackerRotation : MonoBehaviour
{
    private Quaternion rotation;
    private Vector3 position;

    // Update is called once per frame
    void Update()
    {
        rotation = transform.rotation;
        position = transform.position;

        Vector3 line = rotation * Vector3.left;
        Debug.DrawLine(position, line * 100, Color.yellow);
    }
}
