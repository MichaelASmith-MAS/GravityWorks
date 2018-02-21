using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WLD_InputDetection : MonoBehaviour
{

    public static bool keyboardPresent;
    public static bool controllerPresent;

    private int Controller = 0;
    private int Keyboard = 1;

    //public bool isImgOn;
    public Image imgK;
    public Image imgC;

    // Use this for initialization
    void Start()
    {
        imgK.enabled = true;
        imgC.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                print("KEYBOARD CONNECTED");
                Keyboard = 1;
                Controller = 0;
            }
            if (names[x].Length == 33)
            {
                print("CONTROLLER CONNECTED");

                Keyboard = 0;
                Controller = 1;
            }
        }

        if (Controller == 1)
        {
            imgC.enabled = true;

            imgK.enabled = false;
            //set 360 UI controls true
            //set PC UI controls false
        }
        else if (Keyboard == 1)
        {
            imgK.enabled = true;

            imgC.enabled = false;
            //set PC UI controls true
            //set 360 UI controls false
        }

        
    }
}