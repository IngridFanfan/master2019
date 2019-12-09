using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tilt-Function of tracker, 1-Order
public class toPosition_Const : MonoBehaviour
{
    public GameObject tracker;
    private Quaternion prevRotation;
    private Quaternion currentRotation;
    private Vector3 prevPosition;
    private Vector3 currentPosition;

    private float sumX;//position of avatar in X-axis
    private float sumZ;//position of avatar in Z-axis

    private float speed = 0.05f;

    private float thresholdValue = 0.785f;//threshold value for allowing to move
    private float globalSumX = 0; //record how much the user rotates in this single turn => almost the same function with countX and countZ in constante movement function
    private float accumulatedSumX = 0; //record how much the user rotates in this direction =>decide the "speed" of scene movement
    private float thresholdNegX = -0.5f;//threshod value for hard braking
    private float thresholdPosX = 0.5f;

    private float globalSumZ = 0;
    private float accumulatedSumZ = 0;
    private float thresholdNegZ = -0.5f;
    private float thresholdPosZ = 0.5f;

    private bool movingHand = false;
    private bool allowMoving = true;//when the sum Rotation is over the threshold, will be judge as "allowMoving"
    private bool allowRotationChange = true;
    private bool notBraking = true;//stop movement directly and stay for a tiny while when braking
    private bool hardBrakeJudge = false;//hard braking judgement
    private bool brakePotential = false;//when rotating on the opposite of current moving direction, there is a potential of braking, start to detect hard braking condition
    private bool moveX = false;
    private bool moveZ = false;

    //to record how much times tracker rotated in relevant direction, working as an assit in rotation jugement.
    private int countX;
    private int countZ;
    private int countXPos;
    private int countXNeg;
    private int countZPos;
    private int countZNeg;

    private float y;//yaw angle of camera in radian, range: 0 -> 2Pi.
    private Vector3 cameraOrient;//the orientation Vector of camera
    private Vector3 cameraRight;//the right direction Vector of camera

    public bool forward = false;
    public bool back = false;
    public bool left = false;
    public bool right = false;

    void Update()
    {
        //camera orientation

        //y = GameObject.Find("Camera").GetComponent<getOrientation>().yaw;

        //cameraOrient = GameObject.Find("Camera").GetComponent<getOrientation>().orientation;
        //cameraRight = GameObject.Find("Camera").GetComponent<getOrientation>().perpendicular;

        //tracker orientation

        y = GameObject.Find("Tracker").GetComponent<getTrackerOrientation>().yaw;

        cameraOrient = GameObject.Find("Tracker").GetComponent<getTrackerOrientation>().orientation;
        cameraRight = GameObject.Find("Tracker").GetComponent<getTrackerOrientation>().perpendicular;

        if (Time.frameCount < 3)
        {
            prevRotation = tracker.transform.rotation;
            prevPosition = tracker.transform.position;

        }
        else
        {

            currentRotation = tracker.transform.rotation;
            currentPosition = tracker.transform.position;

            //just move the hand.
            Vector3 diffPosition = currentPosition - prevPosition;
            moveHand(diffPosition);

            //rotation around x und z
            sumX = transform.position.x;
            sumZ = transform.position.z;
            toTranslation(prevRotation, currentRotation);


            prevRotation = currentRotation;
            prevPosition = currentPosition;

        }


    }

    void toTranslation(Quaternion from, Quaternion to)
    {

        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;

        toPosition(floorPointMult);

    }

    void toPosition(Vector3 vecMult)
    {
        Vector3 floorPointMultRevise = new Vector3(vecMult.x, 0, vecMult.z);//rotation vector in XoZ plate

        //revised Vectors on orientation and right direction
        Vector3 reviseZ = Vector3.Project(floorPointMultRevise, cameraOrient); //component of rotation vector on camera orientation, the magnitude is same function as Mathf.Abs(floorPointMult.z)
        Vector3 reviseX = Vector3.Project(floorPointMultRevise, cameraRight); //component of rotation vector on camera right direction

        //the angles between these two Vectors, to decide the sign of revised Vectors. With two values: 0 (ungenau) or 180 (ungenau).
        float angleZ = Vector3.Angle(cameraOrient, reviseZ); // 0->forward, positive, 180->backward 
        float angleX = Vector3.Angle(cameraRight, reviseX);//0-> right, 180->left

        if (angleZ <= 90) //Z-Positive -> y = 1
        {
            globalSumZ += reviseZ.magnitude;
            accumulatedSumZ += reviseZ.magnitude;

        }
        else if (angleZ > 90) //Z-Negative -> y = -1
        {
            globalSumZ -= reviseZ.magnitude;
            accumulatedSumZ -= reviseZ.magnitude;
        }

        if (angleX <= 90) //Z-Positive -> y = 1
        {
            globalSumX += reviseX.magnitude;
            accumulatedSumX += reviseX.magnitude;

        }
        else if (angleX > 90) //Z-Negative -> y = -1
        {
            globalSumX -= reviseX.magnitude;
            accumulatedSumX -= reviseX.magnitude;
        }


        allowMoving = Mathf.Abs(globalSumX) >= thresholdValue || Mathf.Abs(globalSumZ) >= thresholdValue ? true : false;
        allowRotationChange = vecMult.z <= 0.015f || vecMult.x <= 0.015f ? true : false;
        //<===== 
        //should notice that, the threshold "allowRotationChange" is stricter than "allowMove", because count is just an assit, 
        //we cannot let it become a conclusive judement condition. Especially when a little hand shaking will arouse the changes of count values.
        //======>

        if (allowMoving && !movingHand && notBraking) //notice: the any non-zero values of count will only be produced while "allowMoving", and rotating (=>decided by "allowRotationChange")
        {

            //this part is to decide in which direction user are rotating.
            if (Mathf.Abs(globalSumZ) >= Mathf.Abs(globalSumX))//X or Z
            {
                countZ++;
                if (globalSumZ >= 0)//Z Positive
                {
                    countZPos++;
                }
                else//Z Negative
                {
                    countZNeg++;

                }
            }
            else
            {
                countX++;
                if (globalSumX >= 0)//X Positive
                {
                    countXPos++;

                }
                else//X Negative
                {
                    countXNeg++;

                }
            }

            if (allowRotationChange)
            {
                clearCountAll();//clear up count values
                clearGlobalAll();//clear up both global values
            }

        }


        //this part is about how the scene/camera will move. 
        if ((Mathf.Abs(accumulatedSumZ) >= thresholdValue || countZ >= countX + 5) && moveX == false) //movement condition: rotation in this direction and scene are not moving on the other direction
        {
            moveZ = true;//scene/camera is moving on Z direction

            if (accumulatedSumZ > 0 || countZPos >= countZNeg + 5) //Z-Positive -> y = 1
            {
                //the movement of camera, in any direction possible
                sumX += Mathf.Abs(accumulatedSumZ) * Mathf.Sin(y) * speed;
                sumZ += Mathf.Abs(accumulatedSumZ) * Mathf.Cos(y) * speed;

                forward = true;

            }
            else if (accumulatedSumZ < 0 || countZNeg >= countZPos + 5) //Z-Negative -> y = -1
            {
                sumX -= Mathf.Abs(accumulatedSumZ) * Mathf.Sin(y) * speed;
                sumZ -= Mathf.Abs(accumulatedSumZ) * Mathf.Cos(y) * speed;

                back = true;
            }

            //the sum value of the other direction should be reseted, or, during rotating, this value will be easily accumulated as more than 0.785.
            //But if you rotate the tracker on purpose, this value will be easily accumulated as greater than 0.785 in 9 frames.
            if (Time.frameCount % 10 == 0 && accumulatedSumX < 0.25f) accumulatedSumX = 0;

            //turning:
            if (Mathf.Abs(accumulatedSumX) >= thresholdValue || countX >= countZ + 5)
            {
                StartCoroutine(ExampleZ());
            }

            //hard brake, the situations should be seperared.
            if (accumulatedSumZ > 0)
            {
                //the contineous 10 frames should be tested. If the hard-braking condition is not satisfied, the threshold value should be reseted immediately.

                if (Time.frameCount % 10 == 0 && angleZ > 90)
                {
                    thresholdPosZ = 0.5f; //reset the threshold value. Important! Or, the changed threshold value will be kept.
                    thresholdPosZ -= reviseZ.magnitude;
                    brakePotential = true;
                }

                if (Time.frameCount % 10 > 0 && Time.frameCount % 10 < 9 && brakePotential && angleZ > 90)
                {
                    thresholdPosZ -= reviseZ.magnitude;
                }

                if (Time.frameCount % 10 == 9 && brakePotential && angleZ > 90)
                {

                    if (thresholdPosZ <= 0)
                    {
                        hardBrakeJudge = true;
                        brakePotential = false;
                    }
                    else brakePotential = false;
                }

                if (hardBrakeJudge)
                {
                    StartCoroutine(hardBrake());
                    hardBrakeJudge = false;

                }

            }
            else
            {
                if (Time.frameCount % 10 == 0 && angleZ <= 90)
                {
                    thresholdNegZ = -0.5f;
                    thresholdNegZ += reviseZ.magnitude;
                    brakePotential = true;
                }

                if (Time.frameCount % 10 > 0 && Time.frameCount % 10 < 9 && brakePotential && angleZ <= 90)
                {
                    thresholdNegZ += reviseZ.magnitude;

                }

                if (Time.frameCount % 10 == 9 && brakePotential && angleZ <= 90)
                {

                    if (thresholdNegZ >= 0)
                    {
                        hardBrakeJudge = true;
                        brakePotential = false;
                    }
                    else brakePotential = false;
                }

                if (hardBrakeJudge)
                {
                    StartCoroutine(hardBrake());
                    hardBrakeJudge = false;

                }
            }

        }
        else if ((Mathf.Abs(accumulatedSumX) >= thresholdValue || countX >= countZ + 5) && moveZ == false)
        {

            moveX = true;//scene/camera is moving on X direction

            if (accumulatedSumX > 0 || countXPos >= countXNeg + 5) //X-Positive -> x = 1
            {

                sumX += Mathf.Abs(accumulatedSumX) * Mathf.Cos(y) * speed;
                sumZ -= Mathf.Abs(accumulatedSumX) * Mathf.Sin(y) * speed;

                right = true;
            }
            else if (accumulatedSumX < 0 || countXNeg >= countXPos + 5) //X-Negative -> x = -1
            {

                sumX -= Mathf.Abs(accumulatedSumX) * Mathf.Cos(y) * speed;
                sumZ += Mathf.Abs(accumulatedSumX) * Mathf.Sin(y) * speed;

                left = true;
            }

            if (Time.frameCount % 10 == 0 && accumulatedSumZ < 0.25f) accumulatedSumZ = 0;

            //turning:
            if (Mathf.Abs(accumulatedSumZ) >= thresholdValue || countZ >= countX + 5)
            {
                StartCoroutine(ExampleX());
            }

            //hard brake
            if (accumulatedSumX > 0)
            {
                if (Time.frameCount % 10 == 0 && angleX > 90)
                {
                    thresholdPosX = 0.5f;
                    thresholdPosX -= reviseX.magnitude;
                    brakePotential = true;

                }

                if (Time.frameCount % 10 > 0 && Time.frameCount % 10 < 9 && brakePotential && angleX > 90)
                {
                    thresholdPosX -= reviseX.magnitude;
                }

                if (Time.frameCount % 10 == 9 && brakePotential && angleX > 90)
                {

                    if (thresholdPosX <= 0)
                    {
                        hardBrakeJudge = true;
                        brakePotential = false;
                    }
                    else brakePotential = false;
                }

                if (hardBrakeJudge)
                {
                    StartCoroutine(hardBrake());
                    hardBrakeJudge = false;

                }

            }
            else
            {
                if (Time.frameCount % 10 == 0 && angleX <= 90)
                {
                    thresholdNegX = -0.5f;
                    thresholdNegX += reviseX.magnitude;
                    brakePotential = true;

                }

                if (Time.frameCount % 10 > 0 && Time.frameCount % 10 < 9 && brakePotential && angleX <= 90)
                {
                    thresholdNegX += reviseX.magnitude;

                }

                if (Time.frameCount % 10 == 9 && brakePotential && angleX <= 90)
                {

                    if (thresholdNegX >= 0)
                    {
                        hardBrakeJudge = true;
                        brakePotential = false;
                    }
                    else brakePotential = false;
                }

                if (hardBrakeJudge)
                {
                    StartCoroutine(hardBrake());
                    hardBrakeJudge = false;

                }
            }

        }
        else if (Mathf.Abs(accumulatedSumZ) < thresholdValue && Mathf.Abs(accumulatedSumX) < thresholdValue) //"original point", when (-0.785f -> 0.785f -> soft brake), reset the movement direction
        {
            moveX = false;
            moveZ = false;

            clearDirectionAll();
        }

        transform.position = new Vector3(sumX, transform.position.y, sumZ);

    }

    IEnumerator hardBrake()
    {
        //all the movemnt relevant values should be reseted.
        allowMoving = false;
        notBraking = false;
        moveZ = false;
        moveX = false;
        accumulatedSumX = 0; //especially these judgement values, should be cleared totally.
        accumulatedSumZ = 0;
        clearCountAll();
        clearGlobalAll();
        clearDirectionAll();
        yield return new WaitForSeconds(0.1f);
        accumulatedSumX = 0;
        accumulatedSumZ = 0;
        clearCountAll();
        clearGlobalAll();
        thresholdPosX = 0.5f;
        thresholdNegX = -0.5f;
        thresholdPosZ = 0.5f;
        thresholdNegZ = -0.5f;
        notBraking = true;

    }


    IEnumerator ExampleX()
    {
        allowMoving = false;
        notBraking = false;
        moveZ = false;
        moveX = false;
        clearDirectionAll();
        accumulatedSumX = 0; //Two times reseted, important!
        yield return new WaitForSeconds(0.1f);
        accumulatedSumX = 0;//Two times reset are needed. One for avoiding reset the other globalSum value. Another one for braking.
        notBraking = true;

    }

    IEnumerator ExampleZ()
    {
        allowMoving = false;
        notBraking = false;
        moveX = false;
        moveZ = false;
        clearDirectionAll();
        accumulatedSumZ = 0;
        yield return new WaitForSeconds(0.1f);
        accumulatedSumZ = 0;
        notBraking = true;

    }

    bool moveHand(Vector3 vec)
    {
        float vecLength = vec.magnitude;

        movingHand = vecLength >= 0.07f ? true : false;
        return movingHand;
    }

    void clearCountAll()
    {
        countX = 0;
        countZ = 0;
        countZPos = 0;
        countZNeg = 0;
        countXPos = 0;
        countXNeg = 0;
    }

    void clearGlobalAll()
    {
        globalSumX = 0;
        globalSumZ = 0;

    }

    void clearDirectionAll()
    {
        forward = false;
        back = false;
        left = false;
        right = false;
    }

}
