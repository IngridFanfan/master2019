using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionPositionData_Maze : MonoBehaviour
{
    private string userNumber = "";
    private string text = "";

    private Vector3[,] collisionPositions = new Vector3[8, 20];

    private int prevConditionOrder = -1;
    private int currentConditionOrder = -1;

    private Vector3[] prevCollisionPosition = new Vector3[20];
    private Vector3[] currentCollisionPosition = new Vector3[20];

    private int round = 0;


    //void Start()
    //{
    //    prevConditionOrder = GetComponent<mazePlay>().conditionOrder;
    //    prevCollisionPosition = GetComponent<mazePlay>().collisionPos;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    currentConditionOrder = GetComponent<mazePlay>().conditionOrder;

    //    currentCollisionPosition = GetComponent<mazePlay>().collisionPos;


    //    round = GetComponent<mazePlay>().round;

    //    print("current: " + currentCollisionPosition[0]);
    //    if (prevCollisionPosition != currentCollisionPosition && currentCollisionPosition[0] != new Vector3(0, -20, 0)) setCollisionPosText();

    //    prevConditionOrder = currentConditionOrder;

    //    prevCollisionPosition = currentCollisionPosition;
    //}

    //void setCollisionPosText()
    //{
    //    print("===================== round: =====================" + round);
    //    print("===================== order: =====================" + currentConditionOrder);

    //    //if (prevConditionOrder == 0)
    //    //{
    //    //    if (round == 1)
    //    //    {
    //    //        for(int i = 0; i< collisionPositions.GetLength(1); i++)
    //    //        {
    //    //            collisionPositions[1, i] = currentCollisionPosition[i];
    //    //        }
    //    //    }else if(round == 2)
    //    //    {
    //    //        for (int i = 0; i < collisionPositions.GetLength(1); i++)
    //    //        {
    //    //            collisionPositions[0, i] = currentCollisionPosition[i];
    //    //        }
    //    //    }


    //    //}
    //    //else if (prevConditionOrder == 1)
    //    //{
    //    //    if (round == 1) collisionPositions[1, 1] = currentAccomplishTime;
    //    //    else if (round == 2) collisionPositions[1, 0] = currentAccomplishTime;

    //    //}
    //    //else if (prevConditionOrder == 2)
    //    //{
    //    //    if (round == 1) collisionPositions[2, 1] = currentAccomplishTime;
    //    //    else if (round == 2) collisionPositions[2, 0] = currentAccomplishTime;

    //    //}
    //    //else if (prevConditionOrder == 3)
    //    //{
    //    //    if (round == 1) collisionPositions[3, 1] = currentAccomplishTime;
    //    //    else if (round == 2) collisionPositions[3, 0] = currentAccomplishTime;

    //    //}

    //    //print("from txt 0: " + collisionPositions[0, 1]);
    //    //print("from txt 1: " + collisionPositions[1, 1]);
    //}
}
