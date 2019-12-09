using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionCollect : MonoBehaviour
{
    public string collisionInfo = "";
    private int count = 0;
    private Vector3 diffVec = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        count++;
        diffVec = collision.gameObject.transform.position - transform.position;

        collisionInfo += "Collision count: " + count + ";\n";
        collisionInfo += "Collision with: " + collision.gameObject.name + ";\n";
        collisionInfo += "Difference Vector: " + diffVec.ToString("F3") + "; \n";
        collisionInfo += "Distance: " + diffVec.magnitude + "; \n \n";

    }
}
