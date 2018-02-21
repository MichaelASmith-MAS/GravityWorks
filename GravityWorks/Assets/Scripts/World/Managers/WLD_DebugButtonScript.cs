/* -----------------------------------------------------------------------------------
 * Class Name: WLD_DebugButtonScript
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

public class WLD_DebugButtonScript : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public int pointsToAdd = 6000, changesInHealth = 25;
    public UI_UIButtonManager ui_ButtonManager;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;
    UI_GunMAnager ui_GunManager;


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
        player = WLD_GameController.player;

        ui_GunManager = GetComponent<UI_GunMAnager>();

    }
    void Update()
    {
        GravityGunPanelShortCut();
        GravityButton();
        FireRateButton();
        EndPieceCollected();
        IncreasePoints();
        AugmentHealth();
    }
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
    // Purpose: Press Q + 3-4
    // ------------------------------------------------------------------------------
    void GravityGunPanelShortCut()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            UNA_StaticVariables.isCeres = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Ceres] = true;
           
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            UNA_StaticVariables.isPluto = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Pluto] = true;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            ui_ButtonManager.EarthsMoon();
            WLD_GameController.gravitySettingDictionary[Gravity.Moon] = true;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            UNA_StaticVariables.isMars = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Mars] = true;
           
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            UNA_StaticVariables.isUranus = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Uranus] = true;
           
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            ui_ButtonManager.Earth();
            WLD_GameController.gravitySettingDictionary[Gravity.Earth] = true;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            UNA_StaticVariables.isNeptune = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Neptune] = true;
           
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha8))
        {
            UNA_StaticVariables.isJupiter = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Jupiter] = true;
        }
        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha9))
        {
            UNA_StaticVariables.isCeres = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Ceres] = true;
            UNA_StaticVariables.isPluto = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Pluto] = true;
            UNA_StaticVariables.isUranus = true;    //It says Uranus...but it's being used for Moon since Uranus is a default now
            WLD_GameController.gravitySettingDictionary[Gravity.Moon] = true;
            UNA_StaticVariables.isMars = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Mars] = true;
            UNA_StaticVariables.isNeptune = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Neptune] = true;
            UNA_StaticVariables.isJupiter = true;
            WLD_GameController.gravitySettingDictionary[Gravity.Jupiter] = true;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Press Q + Z
    // ------------------------------------------------------------------------------
    void GravityButton()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Z))
        {
            UNA_StaticVariables.isGrvPickedUp = true;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Press Q + C
    // ------------------------------------------------------------------------------
    void FireRateButton()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.C))
        {
            WLD_GameController.fireRatePickUpSliderImage.enabled = true;
            WLD_GameController.ui_Texts[UI_Txt.FireRatePickUPSliderText].enabled = true;
            player.GetComponent<CLT_General>().GetIsFireRatePickedUp = true;
            player.GetComponent<CLT_General>().pickUpTimerLength = player.GetComponent<CLT_General>().matchwithPickUpTimer;
            WLD_GameController.fireRatePickUpSliderImage.color = Color.Lerp(player.GetComponent<CLT_General>().endColor, player.GetComponent<CLT_General>().beginColor, player.GetComponent<CLT_General>().pickUpTimerLength / player.GetComponent<CLT_General>().matchwithPickUpTimer);
            WLD_GameController.fireRatePickUpSlider.value = player.GetComponent<CLT_General>().pickUpTimerLength / player.GetComponent<CLT_General>().matchwithPickUpTimer;
        }
    }

    void EndPieceCollected()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.E))
        {
            WLD_GameController.levels[Scenes.DW_0713].FinalLevelPieceCollected = true;
            WLD_GameController.levels[Scenes.KL_0602].FinalLevelPieceCollected = true;
            WLD_GameController.levels[Scenes.JA_0629].FinalLevelPieceCollected = true;
            WLD_GameController.levels[Scenes.JS_1021].FinalLevelPieceCollected = true;
            WLD_GameController.levels[Scenes.Hub].FinalLevelPieceCollected = true;
        }
    }

    void IncreasePoints()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyUp(KeyCode.T))
        {
            player.GetComponent<PLR_Points>().ChangePoints(pointsToAdd);
        }
    }

    void AugmentHealth()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.H))
        {
            player.GetComponent<WLD_HealthDmg>().ChangeHealth(changesInHealth);
        }

        else if (Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.J))
        {
            player.GetComponent<WLD_HealthDmg>().ChangeHealth(-changesInHealth);
        }

    }

} // End WLD_DebugButtonScript