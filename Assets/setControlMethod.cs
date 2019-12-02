using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class setControlMethod : MonoBehaviour
{
    private int prevNumber = -1;
    private int currentNumber = -1;
    private int switchNumber = -1;
    //private string inputNumber = "";
    public string controlMethod = "";

    private Text hintMessg;
    private Text countMessg;
    private Text dMessg;
    private Text tMessg;
    private Text sMessg;

    private bool allowSwitch = false; //only when there is no text displayed in the screen, the relevant scripts will be applied.
    public bool enterSwitch = true;

    void Start()
    {
        if (GetComponent<duplicateInstance>()) //sample test
        {
            prevNumber = GetComponent<duplicateInstance>().conditionOrder;
        }
        else if (GetComponent<exitControl>())//obstacle test
        {
            prevNumber = GetComponent<exitControl>().conditionOrder;

        }
        else if (GetComponent<mazePlay>())//maze test
        {
            prevNumber = GetComponent<mazePlay>().conditionOrder;
        }
        else
        {
            Debug.LogError("no condition order referenced!");
        }

        //prevNumber = GetComponent<mazePlay>().conditionOrder;

        disableScript();

    }

    void Update()
    {
        if (GetComponent<duplicateInstance>())
        {
            currentNumber = GetComponent<duplicateInstance>().conditionOrder;

        }
        else if (GetComponent<exitControl>())
        {
            currentNumber = GetComponent<exitControl>().conditionOrder;

        }
        else if (GetComponent<mazePlay>())
        {
            currentNumber = GetComponent<mazePlay>().conditionOrder;
        }
        else
        {
            Debug.LogError("no condition order referenced!");
        }

        //currentNumber = GetComponent<mazePlay>().conditionOrder;

        //inputNumber = Input.inputString;

        allowSwitch = checkText();
        //allowSwitch = true;
        //print("allowSwitch: " + allowSwitch);
        if (currentNumber != prevNumber)
        {
            switchNumber = currentNumber;
        }
        //print("enterSwitch: " + enterSwitch);
        //print("===========");
        if (allowSwitch && enterSwitch)
            {
            //    print("+++++++++++++++++++++++++++++++++++++++ enter the switch +++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            //print("switchNumber: " + switchNumber);

            switch (switchNumber)
                {
                    case 0:
                        //case "a":
                        disableScript();
                        GetComponent<touchpadControl>().enabled = true;
                        controlMethod = "=========== a: touchPad constant pressing ===========";
                        enterSwitch = false;
                        break;

                    case 1:
                        //case "b":
                        disableScript();
                        GetComponent<touchPad_Const>().enabled = true;
                        controlMethod = "=========== b: touchPad constant velocity ===========";
                        enterSwitch = false;
                        break;

                    case 2:
                        //case "c":
                        disableScript();
                        GetComponent<rotationToTranslation>().enabled = true;
                        controlMethod = "=========== c: tracker constant rotation ===========";
                        enterSwitch = false;
                        break;

                    case 3:
                        //case "c":
                        disableScript();
                        GetComponent<toPosition_Const>().enabled = true;
                        controlMethod = "=========== d: tracker tilt movement ===========";
                        enterSwitch = false;
                        break;
            }

            }

        if (!allowSwitch) disableScript();

        prevNumber = currentNumber;
            
    }

   public void disableScript()
    {
        GetComponent<touchpadControl>().enabled = false;
        GetComponent<touchPad_Const>().enabled = false;
        GetComponent<rotationToTranslation>().enabled = false;
        GetComponent<toPosition_Const>().enabled = false;
    }

    bool checkText()
    {
        bool b = false;
        if (GetComponent<sceneControl_Simple>())
        {
            hintMessg = GetComponent<sceneControl_Simple>().hintMessg;
            countMessg = GetComponent<sceneControl_Simple>().countMessg;
        }else if (GetComponent<sceneControl_Obstacle>())
        {
            hintMessg = GetComponent<sceneControl_Obstacle>().hintMessg;
            countMessg = GetComponent<sceneControl_Obstacle>().countMessg;
        }else if (GetComponent<sceneControl>())
        {
            hintMessg = GetComponent<sceneControl>().hintMessg;
            countMessg = GetComponent<sceneControl>().countMessg;
        }
        else
        {
            Debug.LogError("No Message referenced!");
        }
        dMessg = GetComponent<interimScene>().doneText;
        tMessg = GetComponent<interimScene>().transitMessg;
        sMessg = GetComponent<interimScene>().startText;

        if (hintMessg.text == "" && countMessg.text == "" && dMessg.text == "" && tMessg.text == "" && sMessg.text == "") b = true;
        return b;
    }


}

//https://answers.unity.com/questions/130311/create-text-file.html