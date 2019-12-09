using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toPosition_Const_New : MonoBehaviour
{
    public GameObject tracker;
    //public Transform pointMarker;
    public float speed;
    private Quaternion prevRotation;
    private Quaternion currentRotation;

    private float globalSumX = 0;
    private float thresholdNegX = -0.5f;
    private float thresholdPosX = 0.5f;

    private float globalSumZ = 0;
    private float thresholdNegZ = -0.5f;
    private float thresholdPosZ = 0.5f;

    private bool allowMoving = true;

    private float sumX;


    private void Start()
    {


    }
    void Update()
    {
        //transform.rotation = tracker.gameObject.transform.rotation;
        if (Time.frameCount < 3)
        {
            prevRotation = tracker.transform.rotation;

        }
        else
        {

            currentRotation = tracker.transform.rotation;

            //rotation around x und z
            toTranslation(prevRotation, currentRotation);

            prevRotation = currentRotation;

        }


    }

    void toTranslation(Quaternion from, Quaternion to)
    {

        float sumX = transform.position.z;
        float sumZ = transform.position.x;

        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;

        globalSumX += floorPointMult.z;
        globalSumZ += floorPointMult.x;

        if (allowMoving)
        {
            if (globalSumX < -0.5f) //z-negative //BTW: this is actually negative direction, because we're facing to the screem.
            {
                print("z -");
                if (floorPointMult.z > 0) thresholdNegX += floorPointMult.z;
                if (thresholdNegX > 0 || Mathf.Abs(globalSumZ) > 0.5f)
                {
                    //StartCoroutine(Example());
                    StartCoroutine(ExampleX());
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.2f);
            }
            else if (globalSumX > 0.5f) //z-positive
            {
                print("z +");
                if (floorPointMult.z < 0) thresholdPosX += floorPointMult.z;
                if (thresholdPosX < 0 || Mathf.Abs(globalSumZ) > 0.5f)
                {
                    StartCoroutine(ExampleX());
                }

                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.2f);
            }

            if (globalSumZ < -0.5f) //x-negative
            {
                print("x -");
                if (floorPointMult.x > 0) thresholdNegZ += floorPointMult.x;
                if (thresholdNegZ > 0 || (globalSumX < -0.5f || globalSumX > 0.5f))
                {
                    StartCoroutine(ExampleZ());
                }

                transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            }
            else if (globalSumZ > 0.5f) //x-positive
            {
                print("x +");
                if (floorPointMult.x < 0) thresholdPosZ += floorPointMult.x;
                if (thresholdPosZ < 0 || (globalSumX < -0.5f || globalSumX > 0.5f))
                {
                    StartCoroutine(ExampleZ());
                }

                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
            }

            if ((globalSumX >= -0.5f && globalSumX <= 0.5f) && Mathf.Abs(globalSumZ) <= 0.5f)
            {
                //if (globalSumX * floorPointMult.z > 0 || globalSumZ * floorPointMult.x > 0)
                //{
                //    rotationToTranslation(from, to);
                //}
                //else if (globalSumX * floorPointMult.z < 0)
                //{
                //    StartCoroutine(ExampleX());

                //}
                //else if (globalSumZ * floorPointMult.x < 0)
                //{
                //    StartCoroutine(ExampleZ());
                //}

                rotationToTranslation(from, to);
            }

        }

    }



    //IEnumerator Example()
    //{
    //    allowMoving = false;
    //    yield return new WaitForSeconds(0.1f);
    //    thresholdNegX = -0.5f;
    //    thresholdPosX = 0.5f;
    //    globalSumX = 0;
    //    thresholdPosZ = 0.5f;
    //    thresholdNegZ = -0.5f;
    //    globalSumZ = 0;
    //    allowMoving = true;

    //}

    IEnumerator ExampleX()
    {
        allowMoving = false;
        yield return new WaitForSeconds(0.1f);
        thresholdNegX = -0.5f;
        thresholdPosX = 0.5f;
        globalSumX = 0;
        allowMoving = true;

    }

    IEnumerator ExampleZ()
    {
        allowMoving = false;
        yield return new WaitForSeconds(0.1f);
        thresholdPosZ = 0.5f;
        thresholdNegZ = -0.5f;
        globalSumZ = 0;
        allowMoving = true;

    }

    void rotationToTranslation(Quaternion from, Quaternion to)
    {
        float sumX = transform.position.z;
        float sumZ = transform.position.x;

        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;

        if (Mathf.Abs(floorPointMult.z) >= 0.005f || Mathf.Abs(floorPointMult.x) >= 0.005f)
        {


            if (Mathf.Abs(floorPointMult.z) >= Mathf.Abs(floorPointMult.x))
            {
                sumX += floorPointMult.z * speed * 2.0f;
            }
            else
            {
                sumZ += floorPointMult.x * speed * 1.0f;
            }
        }

        transform.position = new Vector3(sumZ, transform.position.y, sumX);
    }
}
