using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class controllerControl : MonoBehaviour
{
    public SteamVR_Action_Vector2 positionAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("default", "TouchpadTouch");
    public SteamVR_Action_Boolean getTouchedAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "TouchpadGettouched");
    public SteamVR_Input_Sources source = SteamVR_Input_Sources.Any;
    public Transform player;
    private Vector3 movement;
    private Vector2 pos;
    private Vector2 prevPos;
    private Vector2 currentPos;
    private bool prevGettouched;
    private bool currentGettouched;
    private int frameTimeStart;
    private int remainder;

    private int countX;
    private int countZ;
    private int countPos;
    private int countNeg;

    private void Start()
    {
        prevGettouched = getTouchedAction.GetState(source);
        //Debug.Log("prevGettouched: " + prevGettouched);
        frameTimeStart = 5000; //this isn't right, but, who knows. ╮(╯▽╰)╭

        countX = 0;
        countZ = 0;
        countPos = 0;
        countNeg = 0;
    }

    //remainder: unclick scripts cameraOffset
    void Update()
    {
        pos = positionAction.GetAxis(source);

        currentGettouched = getTouchedAction.GetState(source);

        //=================== displacement version 1.0 =========================
        if (prevGettouched == false && currentGettouched == true)
        {
            frameTimeStart = Time.frameCount;
            remainder = frameTimeStart % 10;
            prevPos = positionAction.GetAxis(source);
            movement = player.position;
            //Debug.Log("initial position: " + movement);

        }

        //filtration, after "touched" the pad, check position in each 10 frames:
        if (Time.frameCount > frameTimeStart)
        {

            if (Time.frameCount % 10 == remainder)
            {
                

                currentPos = positionAction.GetAxis(source);

                float changeX = currentPos.x - prevPos.x;
                float changeY = currentPos.y - prevPos.y;
                float moveX = 0;
                float moveZ = 0;

                //Debug.Log("changeX: " + changeX);
                //Debug.Log("changeY: " + changeY);


                if(Mathf.Abs(changeX) >= 0.005 && Mathf.Abs(changeY) >= 0.005) {
                    
                if(Mathf.Abs(changeX) >= Mathf.Abs(changeY))
                    {
                        countX++;

                        if (changeX >= 0)
                        {
                            print("Right");
                            print("change: " + changeX);
                            countPos++;
                        }
                        else
                        {
                            print("Left");
                            print("change: " + changeX);
                            countNeg++;
                        }

                    }
                    else
                    {
                        
                        countZ++;
                    }
                    Debug.Log("============");

                }
                //else
                //{
                //    Debug.Log("pressing");
                //}
                

                prevPos = currentPos;
            }
        }

        prevGettouched = currentGettouched;


        //=================== velocity version 1.0 (without relevant direction): ===========================

        //if (currentGettouched == true)
        //{
        //    player.position = new Vector3(movement.x + pos.x * 0.05f, movement.y, movement.z + pos.y * 0.05f);

        //    //this also works:
        //    pos = positionAction.axis;

        //}


    }

    void OnApplicationQuit()
    {
        print("countX: " + countX);
        print("countZ: " + countZ);
        print("Up: " + countPos);
        print("Down: " + countNeg);
    }
}

/*
Reference:
 https://valvesoftware.github.io/steamvr_unity_plugin/tutorials/SteamVR-Input.html
 https://lawlorcode.wordpress.com/2019/03/24/steamvr-2-2-input-system-tiny-example/
     */
