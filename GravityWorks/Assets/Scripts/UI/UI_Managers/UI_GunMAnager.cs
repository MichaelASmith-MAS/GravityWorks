/* -----------------------------------------------------------------------------------
 * Class Name: UI_GunMAnager
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_GunMAnager : MonoBehaviour 
{


    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    UI_UIButtonManager ui_ButtonManager;
    int currentGunSetting = 1;
    bool decrement_gGunEnabled = true, increment_gGunEnabled = true;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void Start()
    {
        ui_ButtonManager = GetComponent<UI_UIButtonManager>();
    }
    void Update()
    {
        GgunEnable();
        HandleIncrementDecrementOf_gGunSetting();
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void GgunEnable()
    {
        WLD_GameController.ui_Button[UI_Buttons.GgunPanCeres].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Ceres];
        WLD_GameController.ui_Button[UI_Buttons.GgunPanelPluto].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Pluto];
        WLD_GameController.ui_Button[UI_Buttons.GgunPanMoon].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Moon];
        WLD_GameController.ui_Button[UI_Buttons.GgunPanMars].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Mars];
        WLD_GameController.ui_Button[UI_Buttons.GgunPanNeptune].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Neptune];
        WLD_GameController.ui_Button[UI_Buttons.GgunPanJupiter].interactable = WLD_GameController.gravitySettingDictionary[Gravity.Jupiter];
        
        if (WLD_GameController.gravitySettingDictionary[Gravity.Ceres])
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentGunSetting = 1;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Pluto])
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentGunSetting = 2;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Moon])
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                currentGunSetting = 3;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Mars])
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                currentGunSetting = 4;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Uranus])
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                currentGunSetting = 5;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Earth])
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                currentGunSetting = 6;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Neptune])
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                currentGunSetting = 7;
                Increment_gGunSetting(currentGunSetting);
            }
        }
        if (WLD_GameController.gravitySettingDictionary[Gravity.Jupiter])
        {
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                currentGunSetting = 8;
                Increment_gGunSetting(currentGunSetting);
            }
        }
    }

    void HandleIncrementDecrementOf_gGunSetting()
    {
        if (Input.GetAxis("CycleGravUp") > 0 && increment_gGunEnabled)
        {
            currentGunSetting++;
            //Debug.Log("Current gGun Setting is now: " + currentGunSetting);
            Increment_gGunSetting(currentGunSetting);
        }
        if ((Input.GetAxis("CycleGravDown") > 0 && decrement_gGunEnabled) || Input.GetAxis("CycleGravUp") < 0)
        {
            currentGunSetting--;
            //Debug.Log("Current gGun Setting is now: " + currentGunSetting);
            Decrement_gGunSetting(currentGunSetting);
        }

        SetIncrement_gGunEnabled();
        SetDecrement_gGunEnabled();
    }

    void Increment_gGunSetting(int gunSetting)
    {
        switch (gunSetting)
        {
            case 1:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Ceres])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanCeres].interactable = true;
                    ui_ButtonManager.Ceres();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            case 2:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Pluto])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanelPluto].interactable = true;
                    ui_ButtonManager.Pluto();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            case 3:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Moon])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanMoon].interactable = true;
                    ui_ButtonManager.EarthsMoon();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            case 4:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Mars])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanMars].interactable = true;
                    ui_ButtonManager.Mars();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            case 5:
                ui_ButtonManager.Uranus();
                break;

            case 6:
                ui_ButtonManager.Earth();
                break;

            case 7:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Neptune])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanNeptune].interactable = true;
                    ui_ButtonManager.Neptune();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            case 8:              
                if (WLD_GameController.gravitySettingDictionary[Gravity.Jupiter])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanJupiter].interactable = true;
                    ui_ButtonManager.Jupiter();
                }
                else
                {
                    currentGunSetting++;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;

            default:
                if (currentGunSetting > 8)
                {
                    currentGunSetting = 1;
                    Increment_gGunSetting(currentGunSetting);
                }
                break;
        }
    }

    void Decrement_gGunSetting(int gunSetting)
    {
        switch (gunSetting)
        {
            case 1:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Ceres])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanCeres].interactable = true;
                    ui_ButtonManager.Ceres();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            case 2:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Pluto])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanelPluto].interactable = true;
                    ui_ButtonManager.Pluto();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            case 3:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Moon])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanMoon].interactable = true;
                    ui_ButtonManager.EarthsMoon();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            case 4:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Mars])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanMars].interactable = true;
                    ui_ButtonManager.Mars();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            case 5:
                ui_ButtonManager.Uranus();
                break;

            case 6:
                ui_ButtonManager.Earth();            
                break;

            case 7:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Neptune])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanNeptune].interactable = true;
                    ui_ButtonManager.Neptune();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            case 8:
                if (WLD_GameController.gravitySettingDictionary[Gravity.Jupiter])
                {
                    WLD_GameController.ui_Button[UI_Buttons.GgunPanJupiter].interactable = true;
                    ui_ButtonManager.Jupiter();
                }
                else
                {
                    currentGunSetting--;
                    Decrement_gGunSetting(currentGunSetting);
                }
                break;

            default:
                if (currentGunSetting <= 0)
                {
                    currentGunSetting = 8;
                    Decrement_gGunSetting(currentGunSetting);
                }
                //print("DEFAULT CASE RAN - Set back to Earth");
                break;
        }
    }

    void SetIncrement_gGunEnabled()
    {
        if (Input.GetAxis("CycleGravUp") > 0)
        {
            increment_gGunEnabled = false;
        }
        if (Input.GetAxis("CycleGravUp") == 0)
        {
            increment_gGunEnabled = true;
        }
    }

    void SetDecrement_gGunEnabled()
    {
        if (Input.GetAxis("CycleGravDown") > 0)
        {
            decrement_gGunEnabled = false;
        }
        if (Input.GetAxis("CycleGravDown") == 0)
        {
            decrement_gGunEnabled = true;
        }
    }

} // End UI_GunMAnager