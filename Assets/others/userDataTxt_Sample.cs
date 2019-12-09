using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class userDataTxt_Sample : MonoBehaviour
{

    private string userNumber = "";
    private string text = "";

    private float[,] accomplishTimes = new float[4, 2];
    private float[,] sumDistances = new float[4, 2];

    private int conditionOrder = -1;

    private float prevAccomplishTime = 0;
    private float currentAccomplishTime = 0;

    private float prevSumDistance = -1;
    private float currentSumDistance = -1;

    private int round = 0;


    void Start()
    {
        prevAccomplishTime = GetComponent<duplicateInstance>().accomplishedTime;
        prevSumDistance = GetComponent<duplicateInstance>().sumDistance;

    }

    // Update is called once per frame
    void Update()
    {

        conditionOrder = GetComponent<duplicateInstance>().conditionOrder;
        round = GetComponent<duplicateInstance>().round;
        currentAccomplishTime = GetComponent<duplicateInstance>().accomplishedTime;
        currentSumDistance = GetComponent<duplicateInstance>().sumDistance;


        if(prevAccomplishTime != currentAccomplishTime && currentAccomplishTime != 0) setTime();
        if(prevSumDistance != currentSumDistance && currentSumDistance != -1) setDistance();

        prevAccomplishTime = currentAccomplishTime;
        prevSumDistance = currentSumDistance;
    }

    void setTime() {
        //print("round: " + round);
        //print("order: " + conditionOrder);
        //print("time: " + currentAccomplishTime);

        if (conditionOrder == 0)
        {
            if (round == 1) accomplishTimes[0,1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[0,0] = currentAccomplishTime;

            //print("round: " + round);
        }
        else if (conditionOrder == 1)
        {
            if (round == 1) accomplishTimes[1,1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[1,0] = currentAccomplishTime;
        }
        else if (conditionOrder == 2)
        {
            if (round == 1) accomplishTimes[2,1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[2,0] = currentAccomplishTime;
        }
        else if (conditionOrder == 3)
        {
            if (round == 1) accomplishTimes[3,1] = currentAccomplishTime;
            else if (round == 2) accomplishTimes[3,0] = currentAccomplishTime;
        }

    }

    void setDistance()
    {
        //print("round: " + round);
        //print("order: " + conditionOrder);
        //print("distance: " + currentSumDistance);

        if (conditionOrder == 0)
        {
            if (round == 1) sumDistances[0, 1] = currentSumDistance;
            else if (round == 2) sumDistances[0, 0] = currentSumDistance;
        }
        else if (conditionOrder == 1)
        {
            if (round == 1) sumDistances[1, 1] = currentSumDistance;
            else if (round == 2) sumDistances[1, 0] = currentSumDistance;
        }
        else if (conditionOrder == 2)
        {
            if (round == 1) sumDistances[2, 1] = currentSumDistance;
            else if (round == 2) sumDistances[2, 0] = currentSumDistance;
        }
        else if (conditionOrder == 3)
        {
            if (round == 1) sumDistances[3, 1] = currentSumDistance;
            else if (round == 2) sumDistances[3, 0] = currentSumDistance;
        }

    }

    private void OnApplicationQuit()
    {
        userNumber = GetComponent<outputTextSimple>().userNumber;
        //print("========" + userNumber);

        text += "userNumber: " + userNumber + "\n";
        text += "--- Accomplished Time: --- \n";
        for(int i = 0; i < accomplishTimes.GetLength(0); i++)
        {
            for (int j = 0; j < accomplishTimes.GetLength(1); j++)
            {
                text += accomplishTimes[i,j].ToString() + "\t";
            }
        }

        text += "\n";

        text += "--- Sum Distance: ---\n";
        for (int i = 0; i < sumDistances.GetLength(0); i++)
        {
            for (int j = 0; j < sumDistances.GetLength(1); j++)
            {
                text += sumDistances[i, j].ToString() + "\t";
            }
        }

        text += "-------------------- --------------------\n";


        System.IO.File.WriteAllText("C:/Users/Fan.Fan/Desktop/userData/txt/trainingTest.txt", text);

    }
}
