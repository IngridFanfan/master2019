using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

//controller pressing with velocity, 1-Order => any press will stop moving, with slide to adjust speed
public class touchPad_Const : MonoBehaviour
{
    public SteamVR_Action_Vector2 positionAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("default", "TouchpadTouch");
    public SteamVR_Action_Boolean touchpadClicked = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "Teleport");
    public SteamVR_Input_Sources source = SteamVR_Input_Sources.Any;
    public SteamVR_Input_Sources source_left = SteamVR_Input_Sources.LeftHand;
    public SteamVR_Input_Sources source_right = SteamVR_Input_Sources.RightHand;

    private float sumX;
    private float sumZ;

    private bool clicked = false;//whether the touchepad is clicked. 
    private bool prevClicked = false;
    private bool currentClicked = false;
    private bool clicked_left = false;
    private bool clicked_right = false;
    private Vector2 pos = Vector2.zero; //the touched Position of touched controller currently (egal welche). Difference: it's real time. When not touching the touchpad, this value is zero.
    private Vector2 pos_left = Vector2.zero;
    private Vector2 pos_right = Vector2.zero;
    private Vector2 directionStep;//store the value of touched position. Difference: it only be changed when touchPad be clicked.
    private bool hardBrakingJudge = false; //preparation of hard braking judgement

    private float y;//yaw angle of camera in radian, range: 0 -> 2Pi.
    private float y_left = 0;
    private float y_right = 0;
    private float speed = 0.105f;

    private bool moving = false;//the scene is moving
    private bool allowToBrake = false;//the scene is moving and it is ready for braking
    private bool buttonDown = false;
    private bool buttonUp = false;

    private bool leftController = false;
    private bool rightController = false;

    private Vector2 realStep;

    private void Start()
    {
        //clicked = touchpadClicked.GetState(source);

        prevClicked = clicked;
    }

    void Update()
    {

        y_left = GameObject.Find("Controller (left)").GetComponent<getControllerOrientation>().yaw;
        y_right = GameObject.Find("Controller (right)").GetComponent<getControllerOrientation>().yaw;
        clicked_left = touchpadClicked.GetState(source_left);
        clicked_right = touchpadClicked.GetState(source_right);
        pos_left = positionAction.GetAxis(source_left);
        pos_right = positionAction.GetAxis(source_right);

        //check which controller is be used. The controller used should be allowed to change during the test
        if (pos_left != Vector2.zero)
        {
            leftController = true;
            rightController = false;
        }
        else if (pos_right != Vector2.zero)
        {
            rightController = true;
            leftController = false;
        }

        //valuation, depends on which controller is manipulated
        if (leftController)
        {
            pos = pos_left;
            clicked = clicked_left;
            y = y_left;

        }
        else if (rightController)
        {
            pos = pos_right;
            clicked = clicked_right;
            y = y_right;
        }


        currentClicked = clicked;

        if ((prevClicked != currentClicked) && currentClicked == true) buttonDown = true;
        else if ((prevClicked != currentClicked) && currentClicked == false) buttonUp = true;
        else
        {
            buttonDown = false;
            buttonUp = false;
        }

        toConstPosition();

        prevClicked = currentClicked;
    }

    void toConstPosition()
    {
        sumX = transform.position.x;
        sumZ = transform.position.z;

        //valuation
        if ((Mathf.Abs(pos.x) >= 0.3f || Mathf.Abs(pos.y) >= 0.3f) && buttonDown && !moving)
        {
            directionStep = pos;
            moving = true;
        }

        //movement
        if (moving)
        {
            if (Mathf.Abs(directionStep.x) >= Mathf.Abs(directionStep.y))
            {
                if (pos != Vector2.zero) realStep = new Vector2(pos.x, 0);

                if (directionStep.x >= 0) //right
                {

                    sumX += (realStep.x * 0.25f + 0.75f) * Mathf.Cos(y) * speed;
                    sumZ -= (realStep.x * 0.25f + 0.75f) * Mathf.Sin(y) * speed;

                    if (buttonUp) allowToBrake = true;

                    if (allowToBrake && buttonDown)
                    {
                        moving = false;
                        directionStep = Vector2.zero;
                        realStep = Vector2.zero;
                        allowToBrake = false;
                    }

                }
                else //left
                {

                    sumX -= Mathf.Abs(realStep.x * 0.25f - 0.75f) * Mathf.Cos(y) * speed;
                    sumZ += Mathf.Abs(realStep.x * 0.25f - 0.75f) * Mathf.Sin(y) * speed;

                    if (buttonUp) allowToBrake = true;

                    if (allowToBrake && buttonDown)
                    {
                        moving = false;
                        directionStep = Vector2.zero;
                        realStep = Vector2.zero;
                        allowToBrake = false;
                    }

                }
            }
            else
            {

                if (pos != Vector2.zero) realStep = new Vector2(0, pos.y);

                if (directionStep.y >= 0) //forhead
                {

                    sumX += (realStep.y * 0.25f + 0.75f) * Mathf.Sin(y) * speed;
                    sumZ += (realStep.y * 0.25f + 0.75f) * Mathf.Cos(y) * speed;

                    if (buttonUp) allowToBrake = true;

                    if (allowToBrake && buttonDown)
                    {
                        moving = false;
                        directionStep = Vector2.zero;
                        realStep = Vector2.zero;
                        allowToBrake = false;
                    }

                }
                else //back
                {

                    sumX -= Mathf.Abs(realStep.y * 0.25f - 0.75f) * Mathf.Sin(y) * speed;
                    sumZ -= Mathf.Abs(realStep.y * 0.25f - 0.75f) * Mathf.Cos(y) * speed;

                    if (buttonUp) allowToBrake = true;

                    if (allowToBrake && buttonDown)
                    {
                        moving = false;
                        directionStep = Vector2.zero;
                        realStep = Vector2.zero;
                        allowToBrake = false;
                    }

                }
            }
        }
        


        transform.position = new Vector3(sumX, transform.position.y, sumZ);

    }

    IEnumerator hardBraking()
    {
        yield return new WaitForSeconds(1.0f);
        hardBrakingJudge = false;
    }

}
