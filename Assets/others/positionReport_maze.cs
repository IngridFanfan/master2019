using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class positionReport_maze : MonoBehaviour
{
    private string userNumber = "";
    private string text = "";

    public GameObject avatar;

    public Text hintMessg;
    public Text countMessg;
    public Text dMessg;
    public Text tMessg;
    public Text sMessg;

    private Vector3 avatarPos;

    private bool showingMessage = true;

    private string prevMethod = ""; //the previous control method: touchPad or tracker
    private string currentMethod = ""; //the current control method

    void Start()
    {
        prevMethod = GetComponent<setControlMethod>().controlMethod;

    }
    void Update()
    {
        currentMethod = GetComponent<setControlMethod>().controlMethod;

        if (currentMethod != "" && currentMethod != prevMethod)
        {
            writeToText(currentMethod);
        }

        showingMessage = (hintMessg.text != "" || countMessg.text != "" || dMessg.text != "" || tMessg.text != "" || sMessg.text != "") ? true : false;

        if (!showingMessage && Time.frameCount % 50 == 0)
        {
            avatarPos = avatar.transform.position;
            setPosition();
        }

        prevMethod = currentMethod;

    }


    void setPosition()
    {
        text += "Position: " + avatarPos.ToString() + "\n";
    }

    void writeToText(string s)
    {
        text += s + "\n";
    }

    private void OnApplicationQuit()
    {
        userNumber = GetComponent<outputTextMaze>().userNumber;

        text += "\n \n --- userNumber: " + userNumber + " ---\n";


        System.IO.File.WriteAllText("C:/Users/Fan.Fan/Desktop/userData/positionReport/mazeTest_Position.txt", text);

    }
}
