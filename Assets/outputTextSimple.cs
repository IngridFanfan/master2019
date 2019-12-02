using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class outputTextSimple : MonoBehaviour
{
    private string text = "++++++++++++++++++++ Simple Test ++++++++++++++++++++\n\n";
    public string userNumber = "";

    private string prevMethod = ""; //the previous control method: touchPad or tracker
    private string currentMethod = ""; //the current control method

    private string prevMarkerInfo = ""; //the previous marker ring information which is entered by user
    private string currentMarkerInfo = ""; //the current ring information 

    private float prevAccomplishedTime = 0; //the complished time 
    private float currentAccomplishedTime = 0;  

    //Notice: if we use a choosed order of rings, each will be visited more than one time, please add a information about the visited time.//
    //Notice: each time when accomplished the task with one method, the last two info should be removed, the all setted value should be reseted.


    void Start()
    {
        text += "Test Time: " + System.DateTime.Now + "\n";
        prevMethod = GetComponent<setControlMethod>().controlMethod;
        prevMarkerInfo = GetComponent<visitMarkerRing>().markerInfo;
        prevAccomplishedTime = GetComponent<duplicateInstance>().accomplishedTime;
        Debug.Log("Simple Test");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("=================== !!! Please enter User Number !!! ===================");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("===========================================================");

    }

    void Update()
    {
        currentMethod = GetComponent<setControlMethod>().controlMethod;
        currentMarkerInfo = GetComponent<visitMarkerRing>().markerInfo;
        currentAccomplishedTime = GetComponent<duplicateInstance>().accomplishedTime;


        //write the user number in text
        if (Input.inputString != "")
        {
            userNumber += Input.inputString;
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                writeToText("User Number: " + userNumber + "\n");
                print("userNumber: " + userNumber);
            }
        }

        //write the control method in text
        if (currentMethod != "" && currentMethod != prevMethod)
        {
            print(currentMethod);
            writeToText(currentMethod);
        } 

        //write the information about marker ring, like vector and distance.
        if (currentMarkerInfo != "" && prevMarkerInfo != currentMarkerInfo) writeToText(currentMarkerInfo);

        if (currentAccomplishedTime != 0 && prevAccomplishedTime != currentAccomplishedTime)
        {
            //print("accomplish time: " + currentAccomplishedTime);
            writeToText("Accomplished Time: " + currentAccomplishedTime.ToString("F3") + "s \n" + "-------------------------------------------" + "\n");
        } 

        prevMethod = currentMethod;
        prevMarkerInfo = currentMarkerInfo;
        prevAccomplishedTime = currentAccomplishedTime;

    }

    void writeToText(string s)
    {
        text += s + "\n";
    }

    private void OnApplicationQuit()
    {
        //print("userNumber: " + userNumber);
        //string p1 = "C:/Users/Fan.Fan/Desktop/userData/userDataTrainingTest";
        ////string p1 = "@C:\\Users\\Fan.Fan\\Desktop\\userData\\userDataTrainingTest";
        //string p2 = ".txt";
        //string p3 = String.Concat(p1, userNumber);
        ////string p3 = String.Concat(p1, "999");
        ////string path = String.Concat(p3, p2);
        //string path = String.Concat(p3, p2);
        ////path = path.Replace("\"", "");

        //print(path);


        //print("uuuuuuuser nr:" + userNumber);
        //print(userNumber.GetType());

        ////System.IO.File.WriteAllText(path, text);
       
        System.IO.File.WriteAllText("C:/Users/Fan.Fan/Desktop/userData/trainingTest_summarised.txt", text);

        //FileInfo f1 = new FileInfo(path);
    }
}

//https://answers.unity.com/questions/130311/create-text-file.html
