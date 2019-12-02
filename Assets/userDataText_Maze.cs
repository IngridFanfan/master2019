using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userDataText_Maze : MonoBehaviour
{
    private string userNumber = "";
    private string text = "";

    private float[,] accomplishTimes = new float[4, 2];
    private float[,] collisionCounts = new float[4, 2];

    private int prevConditionOrder = -1;
    private int currentConditionOrder = -1;

    private float prevAccomplishTime = 0;
    private float currentAccomplishTime = 0;

    private float prevCollosionCount = -1;
    private float currentCollosionCount = -1;

    private int round = 0;

    void Start()
    {
        prevConditionOrder = GetComponent<mazePlay>().conditionOrder;
        prevAccomplishTime = GetComponent<mazePlay>().accomplishedTime;
        prevCollosionCount = GetComponent<mazePlay>().collisionCount;

    }

    // Update is called once per frame
    void Update()
    {
        currentConditionOrder = GetComponent<mazePlay>().conditionOrder;

        round = GetComponent<mazePlay>().round;
        currentAccomplishTime = GetComponent<mazePlay>().accomplishedTime;
        currentCollosionCount = GetComponent<mazePlay>().collisionCount;


        if (prevAccomplishTime != currentAccomplishTime && currentAccomplishTime != 0) setTime();
        if (prevCollosionCount != currentCollosionCount && currentCollosionCount != -1) setCount();

        prevAccomplishTime = currentAccomplishTime;
        prevCollosionCount = currentCollosionCount;
        prevConditionOrder = currentConditionOrder;
    }

    void setTime()
    {

        if (prevConditionOrder == 0)
        {
            if (round == 1) accomplishTimes[0, 1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[0, 0] = currentAccomplishTime;

            
        }
        else if (prevConditionOrder == 1)
        {
            if (round == 1) accomplishTimes[1, 1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[1, 0] = currentAccomplishTime;

        }
        else if (prevConditionOrder == 2)
        {
            if (round == 1) accomplishTimes[2, 1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[2, 0] = currentAccomplishTime;

        }
        else if (prevConditionOrder == 3)
        {
            if (round == 1) accomplishTimes[3, 1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[3, 0] = currentAccomplishTime;

        }

    }

    void setCount()
    {

        if (prevConditionOrder == 0)
        {
            if (round == 1) collisionCounts[0, 1] = currentCollosionCount;
            else if (round == 2) collisionCounts[0, 0] = currentCollosionCount;

        }
        else if (prevConditionOrder == 1)
        {
            if (round == 1) collisionCounts[1, 1] = currentCollosionCount;
            else if (round == 2) collisionCounts[1, 0] = currentCollosionCount;
        }
        else if (prevConditionOrder == 2)
        {
            if (round == 1) collisionCounts[2, 1] = currentCollosionCount;
            else if (round == 2) collisionCounts[2, 0] = currentCollosionCount;
        }
        else if (prevConditionOrder == 3)
        {
            if (round == 1) collisionCounts[3, 1] = currentCollosionCount;
            else if (round == 2) collisionCounts[3, 0] = currentCollosionCount;
        }

    }

    private void OnApplicationQuit()
    {
        userNumber = GetComponent<outputTextMaze>().userNumber;
        //print("========" + userNumber);

        text += "userNumber: " + userNumber + "\n";
        text += "--- Accomplished Time: --- \n";
        for (int i = 0; i < accomplishTimes.GetLength(0); i++)
        {
            for (int j = 0; j < accomplishTimes.GetLength(1); j++)
            {
                text += accomplishTimes[i, j].ToString() + "\t";
            }
        }

        text += "\n";

        text += "--- Collision Counts: ---\n";
        for (int i = 0; i < collisionCounts.GetLength(0); i++)
        {
            for (int j = 0; j < collisionCounts.GetLength(1); j++)
            {
                text += collisionCounts[i, j].ToString() + "\t";
            }
        }

        text += "-------------------- --------------------\n";


        System.IO.File.WriteAllText("C:/Users/Fan.Fan/Desktop/userData/txt/mazeTest.txt", text);

    }
}
