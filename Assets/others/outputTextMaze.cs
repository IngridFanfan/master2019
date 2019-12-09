using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class outputTextMaze : MonoBehaviour
{
    private string text = "++++++++++++++++++++ Maze Test ++++++++++++++++++++\n\n";
    public string userNumber;

    private string prevMethod = ""; //the previous control method: touchPad or tracker
    private string currentMethod = ""; //the current control method

    private string prevPathInfo = ""; //the previous path info
    private string currentPathInfo = ""; //the current path info

    private string prevCollisionInfo = "";//the informatin about collision
    private string currentCollisionInfo = "";

    private string prevTaskInfo = "";//the information about accomplished task.
    private string currentTaskInfo = "";

    private float prevAccomplishedTime = 0; //the complished time 
    private float currentAccomplishedTime = 0;

    void Start()
    {
        text += "Test Time: " + System.DateTime.Now + "\n";
        prevMethod = GetComponent<setControlMethod>().controlMethod;
        prevPathInfo = GetComponent<setScenePath>().pathInfo;
        prevCollisionInfo = GetComponent<mazePlay>().collisionInfo;
        prevTaskInfo = GetComponent<mazePlay>().taskInfo;
        prevAccomplishedTime = GetComponent<mazePlay>().accomplishedTime;
        Debug.Log("Maze Test");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("=================== !!! Please enter User Number !!! ===================");
        Debug.LogWarning("===========================================================");
        Debug.LogWarning("===========================================================");
    }

    // Update is called once per frame
    void Update()
    {
        currentPathInfo = GetComponent<setScenePath>().pathInfo;
        currentMethod = GetComponent<setControlMethod>().controlMethod;
        currentCollisionInfo = GetComponent<mazePlay>().collisionInfo;
        currentTaskInfo = GetComponent<mazePlay>().taskInfo;
        currentAccomplishedTime = GetComponent<mazePlay>().accomplishedTime;

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

        //write the path information in text
        if (currentPathInfo != "" && currentPathInfo != prevPathInfo)
        {
            //print(currentPathInfo);
            writeToText(currentPathInfo);
        }

        //write the information about marker ring, like vector and distance.
        if (currentCollisionInfo != "" && prevCollisionInfo != currentCollisionInfo) writeToText(currentCollisionInfo);

        if (currentTaskInfo != "" && prevTaskInfo != currentTaskInfo) writeToText(currentTaskInfo);

        if (currentAccomplishedTime != 0 && prevAccomplishedTime != currentAccomplishedTime)
        {
            //print("accomplish time: " + currentAccomplishedTime);
            writeToText("Accomplished Time: " + currentAccomplishedTime.ToString("F3") + "s \n" + "-------------------------------------------" + "\n");
        }

        prevMethod = currentMethod;
        prevPathInfo = currentPathInfo;
        prevCollisionInfo = currentCollisionInfo;
        prevAccomplishedTime = currentAccomplishedTime;

    }

    void writeToText(string s)
    {
        text += s + "\n";
    }

    private void OnApplicationQuit()
    {

        System.IO.File.WriteAllText("C:/Users/Fan.Fan/Desktop/userData/mazeTest_summarised.txt", text);

    }
}
