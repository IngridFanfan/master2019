using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Constant-Function of tracker, 0-Order
public class rotationToTranslation : MonoBehaviour
{
    public GameObject tracker;
    private Quaternion prevRotation;
    private Quaternion currentRotation;
    private Vector3 prevPosition;
    private Vector3 currentPosition;

    private bool movingHand = false;
    private bool allowMove = true;
    public bool allowChangeColor = false;
    public bool notRotating = true;
    private bool allowRotationChange = true;

    private float sumX;//position of avatar in X-axis
    private float sumZ;//position of avatar in Z-axis

    //to record how much times tracker rotated in relevant direction, working as an assit in rotation jugement.
    private int countX;
    private int countZ;
    private int countZPos;
    private int countZNeg;
    private int countXPos;
    private int countXNeg;

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

            currentPosition = tracker.transform.position;
            currentRotation = tracker.transform.rotation;

            //jugement of moving hand. 
            Vector3 diffPosition = currentPosition - prevPosition;
            moveHand(diffPosition);

            //rotation around x und z
            toTranslation(prevRotation, currentRotation);

            prevRotation = currentRotation;
            prevPosition = currentPosition;

        }

    }

    void toTranslation(Quaternion from, Quaternion to)
    {
        sumX = transform.position.x;
        sumZ = transform.position.z;
        Vector3 floorPoint = new Vector3(0, -1, 0);
        Quaternion quadDiff = from * Quaternion.Inverse(to);

        Vector3 floorPointMult = quadDiff * floorPoint;
        Vector3 floorPointMultRevise = new Vector3(floorPointMult.x, 0, floorPointMult.z); //rotation vector in XoZ plate

        //revised Vectors on orientation and right direction
        Vector3 reviseZ = Vector3.Project(floorPointMultRevise, cameraOrient); //component of rotation vector on camera orientation, the magnitude is same function as Mathf.Abs(floorPointMult.z)
        Vector3 reviseX = Vector3.Project(floorPointMultRevise, cameraRight); //component of rotation vector on camera right direction

        //the angles between these two Vectors, to decide the sign of revised Vectors. With two values: 0 (ungenau) or 180 (ungenau).
        float angleZ = Vector3.Angle(cameraOrient, reviseZ); //the angles between these two Vectors, to decide the sign of revised Vectors. With two values:  0 (ungenau) or 180 (ungenau). 0->forward, positive, 180->backward 
        float angleX = Vector3.Angle(cameraRight, reviseX); //0-> right, 180->left

        allowMove = Mathf.Abs(floorPointMult.z) >= 0.005f || Mathf.Abs(floorPointMult.x) >= 0.005f ? true : false;
        allowChangeColor = Mathf.Abs(floorPointMult.z) >= 0.06f || Mathf.Abs(floorPointMult.x) >= 0.06f ? true : false; 
        allowRotationChange = Mathf.Abs(floorPointMult.z) <= 0.015f || Mathf.Abs(floorPointMult.x) <= 0.015f ? true : false;
        notRotating = Mathf.Abs(floorPointMult.z) <= 0.001f || Mathf.Abs(floorPointMult.x) <= 0.001f ? true : false;
        //<===== 
        //should notice that, the threshold "allowRotationChange" is stricter than "allowMove", because count is just an assit, 
        //we cannot let it become a conclusive judement condition. Especially when a little hand shaking will arouse the changes of count values.
        //======>

        if (allowMove && !movingHand)
        {

            if (reviseZ.magnitude >= reviseX.magnitude || countZ >= countX + 5)//X or Z
            {
                countZ++;

                if (angleZ <= 90 || countZPos >= countZNeg + 5) //Z-Positive -> y = 1
                {
                    countZPos++;
                    forward = true;
                    back = false;
                    left = false;
                    right = false;
                    //the movement of camera, in any direction possible
                    sumX += floorPointMultRevise.magnitude * Mathf.Sin(y) * 3.3f;
                    sumZ += floorPointMultRevise.magnitude * Mathf.Cos(y) * 3.3f;

                }
                else if (angleZ > 90 || countZNeg >= countZPos + 5) //Z-Negative -> y = -1
                {
                    countZNeg++;
                    back = true;
                    forward = false;
                    left = false;
                    right = false;
                    sumX -= floorPointMultRevise.magnitude * Mathf.Sin(y) * 3.3f;
                    sumZ -= floorPointMultRevise.magnitude * Mathf.Cos(y) * 3.3f;
                }

            }
            else if (reviseX.magnitude > reviseZ.magnitude || countX >= countZ + 5)
            {
                countX++;
                if (angleX <= 90 || countXPos >= countXNeg + 5) //X-Positive -> x = 1
                {
                    countXPos++;
                    right = true;
                    forward = false;
                    back = false;
                    left = false;
                    sumX += floorPointMultRevise.magnitude * Mathf.Cos(y) * 3.3f;
                    sumZ -= floorPointMultRevise.magnitude * Mathf.Sin(y) * 3.3f;
                }
                else if (angleX > 90 || countXNeg >= countXPos + 5) //X-Negative -> x = -1
                {
                    countXNeg++;
                    left = true;
                    forward = false;
                    back = false;
                    right = false;
                    sumX -= floorPointMultRevise.magnitude * Mathf.Cos(y) * 3.3f;
                    sumZ += floorPointMultRevise.magnitude * Mathf.Sin(y) * 3.3f;
                }
            }
        }
        else
        {
            forward = false;
            back = false;
            left = false;
            right = false;
        }

        if (allowRotationChange)//reset all count values when stop rotating
        {
            countX = 0;
            countZ = 0;
            countZPos = 0;
            countZNeg = 0;
            countXPos = 0;
            countXNeg = 0;

           
        }

        transform.position = new Vector3(sumX, transform.position.y, sumZ);

    }

    bool moveHand(Vector3 vec)
    {
        float vecLength = vec.magnitude;

        movingHand = vecLength >= 0.007f ? true : false;
        return movingHand;
    }

}
