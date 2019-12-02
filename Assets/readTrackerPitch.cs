using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readTrackerPitch : MonoBehaviour
{
    public GameObject tracker;
    private float pitch;

    // Update is called once per frame
    void Update()
    {
        pitch = tracker.transform.rotation.eulerAngles.x;
        transform.rotation = Quaternion.Euler(0, pitch, 0);
    }
}
