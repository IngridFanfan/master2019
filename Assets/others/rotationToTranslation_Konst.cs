using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//===============Do we need some visulization to give user some hints, how much did they already rotate? ==================//

public class rotationToTranslation_Konst : MonoBehaviour
{
    public GameObject tracker;
    public GameObject refPoint; //empty object to record the position transited from tracker rotation
    public float avatarSpeed;
    private float refSpeed;
    private Quaternion prevRotation;
    private Quaternion currentRotation;


    private Quaternion initialSettingRotation;
    private Vector3 refPosition;

    private Vector3 floorPointMult; 

    void Start()
    {

        refSpeed = 3.0f;
        avatarSpeed = 0.1f;
        refPosition = refPoint.transform.position;
    }


    void Update()
    {

        if (Time.frameCount < 3)
        {
            prevRotation = tracker.transform.rotation;
        }
        else
        {

            currentRotation = tracker.transform.rotation;

            toTranslation(prevRotation, currentRotation);

            refPosition = refPoint.transform.position;

            toPosition(refPosition);

            prevRotation = currentRotation;

        }
    }

    void toTranslation(Quaternion from, Quaternion to)
    {
        float sumX = refPoint.transform.position.z;
        float sumZ = refPoint.transform.position.x;

        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;

        if (Mathf.Abs(floorPointMult.z) >= 0.005f || Mathf.Abs(floorPointMult.x) >= 0.005f)
        {

            if (Mathf.Abs(floorPointMult.z) >= Mathf.Abs(floorPointMult.x))
            {
                sumX += floorPointMult.z * refSpeed;
            }
            else
            {
                sumZ += floorPointMult.x * refSpeed;
            }
        }

        refPoint.transform.position = new Vector3(sumZ, transform.position.y, sumX);


    }

    void toPosition(Vector3 vec)
    {

        float posX = transform.position.x;
        float posZ = transform.position.z;

        if(Mathf.Abs(vec.z) >= 3.0f || Mathf.Abs(vec.x) >= 3.0f)
        {
            if(Mathf.Abs(vec.z) >= Mathf.Abs(vec.x))
            {
                posZ = vec.z > 0 ? (posZ + avatarSpeed) : (posZ - avatarSpeed);
            }
            else
            {
                posX = vec.x > 0 ? (posX + avatarSpeed) : (posX - avatarSpeed);
            }
        }
        else if(Mathf.Abs(vec.z) >= 1.0f || Mathf.Abs(vec.x) >= 1.0f)
        {


            if (Mathf.Abs(vec.z) >= Mathf.Abs(vec.x))
            {
                if (vec.z >= 0)
                {
                    posZ += avatarSpeed;
                }
                else
                {
                    posZ -= avatarSpeed;
                }
            }
            else
            {
                if (vec.x >= 0)
                {
                    posX += avatarSpeed;
                }
                else
                {
                    posX -= avatarSpeed;
                }
            }
        }

        transform.position = new Vector3(posX, transform.position.y, posZ);

    }

}
