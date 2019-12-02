using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPosAndRotation : MonoBehaviour
{
    public GameObject tracker;

    void Update()
    {
        transform.rotation = tracker.transform.rotation;
    }
}
