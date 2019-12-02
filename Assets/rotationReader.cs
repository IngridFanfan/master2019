using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationReader : MonoBehaviour
{
    public Transform tracker;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = tracker.rotation;
    }
}
