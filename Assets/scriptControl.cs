using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptControl : MonoBehaviour
{
    //private string switchOrder = "";
    private int prevSwitchOrder = -1;
    private int currentSwitchOrder = -1;

    void Start()
    {
        prevSwitchOrder = GameObject.Find("Avatar").GetComponent<gameExit>().conditionOrder;
        disableAllScripts();
    }

    void Update()
    {

        currentSwitchOrder = GameObject.Find("Avatar").GetComponent<gameExit>().conditionOrder;

        //switchOrder = Input.inputString;

        if(currentSwitchOrder != prevSwitchOrder)
        {
            print("currentOrder");
            switch (currentSwitchOrder)
            {
                case 0:
                    //case "a":

                    disableAllScripts();
                    GetComponent<touchpadControl>().enabled = true;
                    //controlMethod = "=========== a: touchPad constant pressing ===========";
                    //print("a: touchPad");
                    break;
                case 1:
                    //case "b":
                    disableAllScripts();
                    GetComponent<touchPad_Const>().enabled = true;
                    //controlMethod = "=========== b: touchPad constant velocity ===========";
                    //print("b: constant movement");
                    break;
                case 2:
                    //case "c":
                    disableAllScripts();
                    GetComponent<rotationToTranslation>().enabled = true;
                    //controlMethod = "=========== c: tracker constant rotation ===========";
                    //print("c: tilt movement");
                    break;

                case 3:
                    //case "d":
                    disableAllScripts();
                    GetComponent<toPosition_Const>().enabled = true;
                    //controlMethod = "=========== d: tracker tilt movement ===========";
                    //print("c: tilt movement");
                    break;

            }
        }
        prevSwitchOrder = currentSwitchOrder;
        
    }

    void disableAllScripts()
    {
        GetComponent<touchPad_Const>().enabled = false;
        GetComponent<touchpadControl>().enabled = false;
        GetComponent<rotationToTranslation>().enabled = false;
        GetComponent<toPosition_Const>().enabled = false;


    }
}
