using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTrackerOrient : MonoBehaviour
{
    private Quaternion rotation;
    private Vector3 position;

    public Vector3 line;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        rotation = transform.rotation;
        line = rotation * Vector3.right;
        Debug.DrawLine(position, line * 100, Color.yellow);
    }


}

