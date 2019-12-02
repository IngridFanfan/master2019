using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setPointerColor : MonoBehaviour
{
    private bool forward = false;
    private bool back = false;
    private bool left = false;
    private bool right = false;

    private GameObject[] pointer;

    private int conditionOrder = -1;

    private Color direction = new Color(0.35f, 0.82f, 0.45f, 1.0f);
    private Color nullDirection = new Color(0.55f, 0.75f, 0.896f, 0.4f);

    private int childCount;
    private bool allowChangeColor = false;
    private bool notRotating = true;

    private void Start()
    {
        pointer = GameObject.FindGameObjectsWithTag("pointer");
    }

    void Update()
    {
        if (GameObject.Find("Avatar").GetComponent<duplicateInstance>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<duplicateInstance>().conditionOrder;

        }else if (GameObject.Find("Avatar").GetComponent<exitControl>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<exitControl>().conditionOrder;

        }
        else if (GameObject.Find("Avatar").GetComponent<mazePlay>())
        {
            conditionOrder = GameObject.Find("Avatar").GetComponent<mazePlay>().conditionOrder;

        }
        else
        {
            Debug.LogError("no ConditionOrder refernced!");
        }


        allowChangeColor = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().allowChangeColor;
        notRotating = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().notRotating;

        checkDirection();


        if (conditionOrder == 2)
        {
            if (Time.frameCount % 10 == 0 && (allowChangeColor || notRotating)) setColor();
        }
        else if (conditionOrder == 3) {
            notRotating = false;
            setColor();
        } 

    }

    void checkDirection()
    {
        if (conditionOrder == 2) //rotation to translation
        {
            forward = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().forward;
            back = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().back;
            left = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().left;
            right = GameObject.Find("Avatar").GetComponent<rotationToTranslation>().right;

        }
        else if (conditionOrder == 3)//toPosition_Const
        {
            forward = GameObject.Find("Avatar").GetComponent<toPosition_Const>().forward;
            back = GameObject.Find("Avatar").GetComponent<toPosition_Const>().back;
            left = GameObject.Find("Avatar").GetComponent<toPosition_Const>().left;
            right = GameObject.Find("Avatar").GetComponent<toPosition_Const>().right;
        }
        else
        {
            forward = false;
            back = false;
            left = false;
            right = false;
        }
    }

    void setColor()
    {
        if (notRotating)
        {
            forward = false;
            back = false;
            left = false;
            right = false;
        }

        if (forward)
        {
            resetAllColor();
            //GameObject.Find("pointer_forward").GetComponent<Renderer>().material.SetColor("_Color", direction);
            GameObject present = GameObject.Find("pointer_forward");
            childCount = present.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                present.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", direction);
            }


        }
        else if (back)
        {
            resetAllColor();
            //GameObject.Find("pointer_back").GetComponent<Renderer>().material.SetColor("_Color", direction);
            GameObject present = GameObject.Find("pointer_back");
            childCount = present.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                present.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", direction);
            }
        }
        else if (left)
        {

            resetAllColor();
            //GameObject.Find("pointer_left").GetComponent<Renderer>().material.SetColor("_Color", direction);

            GameObject present = GameObject.Find("pointer_left");
            childCount = present.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                present.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", direction);
            }
        }
        else if (right)
        {
            resetAllColor();

            //GameObject.Find("pointer_right").GetComponent<Renderer>().material.SetColor("_Color", direction);
            GameObject present = GameObject.Find("pointer_right");
            childCount = present.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                present.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", direction);
            }
        } 
        else
        {
            if(conditionOrder == 2)
            {
                StartCoroutine(lagTime());
            }
            else
            {
                resetAllColor();

            }

            
        }
    }

    void resetAllColor()
    {
        for(int i = 0; i < pointer.GetLength(0); i++)
        {
            for(int j = 0; j < pointer[i].transform.childCount; j++)
            {
                pointer[i].transform.GetChild(j).GetComponent<Renderer>().material.SetColor("_Color", nullDirection);
            }
        }

    }

    IEnumerator lagTime()
    {

        yield return new WaitForSeconds(0.1f);
        resetAllColor();


    }
}

