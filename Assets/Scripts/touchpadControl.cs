using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//Constant-Function of controller, 0-Order
public class touchpadControl : MonoBehaviour
{
    public SteamVR_Action_Vector2 positionAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("default", "TouchpadTouch");
    public SteamVR_Action_Boolean touchpadClicked = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "Teleport");
    public SteamVR_Input_Sources source = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources source_left = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources source_right = SteamVR_Input_Sources.RightHand;

    private float sumX;
    private float sumZ;

    private Vector2 pos = Vector2.zero; //the touched Position of touched controller currently (egal welche)
    private Vector2 pos_left = Vector2.zero; 
    private Vector2 pos_right = Vector2.zero; 
    private bool clicked = false;//whether the touchepad is clicked
    private bool clicked_left = false;
    private bool clicked_right = false;

    private float y = 0;//yaw angle of camera in radian, range: 0 -> 2Pi.
    private float y_left = 0;
    private float y_right = 0;

    void Update()
    {

        //camera orientation

        //clicked = touchpadClicked.GetState(source);
        //y = GameObject.Find("Camera").GetComponent<getOrientation>().yaw;
        //pos = positionAction.GetAxis(source);

        //controller orientation -> Any

        y_left = GameObject.Find("Controller (left)").GetComponent<getControllerOrientation>().yaw;
        y_right = GameObject.Find("Controller (right)").GetComponent<getControllerOrientation>().yaw;
        clicked_left = touchpadClicked.GetState(source_left);
        clicked_right = touchpadClicked.GetState(source_right);
        pos_left = positionAction.GetAxis(source_left);
        pos_right = positionAction.GetAxis(source_right);

        if(pos_left != Vector2.zero)
        {
            pos = pos_left;
            clicked = clicked_left;
            y = y_left;

        }else if(pos_right != Vector2.zero)
        {
            pos = pos_right;
            clicked = clicked_right;
            y = y_right;

        }
        else if (pos_left == Vector2.zero && pos_right == Vector2.zero)
        {
            pos = Vector2.zero;
            clicked = false;
            y = 0;
        }

        touchPadPosition();

    }

    void touchPadPosition()
    {
        if ((Mathf.Abs(pos.x) >= 0.3f || Mathf.Abs(pos.y) >= 0.3f) && clicked)//movement condition: Abs(pos) > 0.3 && touchpad get touched.
        {
            sumX = transform.position.x;
            sumZ = transform.position.z;

            if (Mathf.Abs(pos.x) >= Mathf.Abs(pos.y))
            {
                if (pos.x >= 0) //right
                {
                    sumX += Mathf.Abs(pos.x) * Mathf.Cos(y) * 0.13f;
                    sumZ -= Mathf.Abs(pos.x) * Mathf.Sin(y) * 0.13f;
                }
                else //left
                {
                    sumX -= Mathf.Abs(pos.x) * Mathf.Cos(y) * 0.13f;
                    sumZ += Mathf.Abs(pos.x) * Mathf.Sin(y) * 0.13f;
                }
            }
            else
            {
                if (pos.y >= 0) //forhead
                {
                    sumX += Mathf.Abs(pos.y) * Mathf.Sin(y) * 0.13f;
                    sumZ += Mathf.Abs(pos.y) * Mathf.Cos(y) * 0.13f;
                }
                else //back
                {
                    sumX -= Mathf.Abs(pos.y) * Mathf.Sin(y) * 0.13f;
                    sumZ -= Mathf.Abs(pos.y) * Mathf.Cos(y) * 0.13f;
                }

            }

            transform.position = new Vector3(sumX, transform.position.y, sumZ);


        }
    }

}
