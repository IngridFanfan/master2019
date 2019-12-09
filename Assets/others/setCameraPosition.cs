using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCameraPosition : MonoBehaviour
{
    public Transform avatar;
    void Start()
    {
        transform.position = avatar.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = avatar.position;
    }
}
