/* -----------------------------------------------------------------------------------
 * Class Name: TempHeatEmittor
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

public class ENM_HeatEmittor : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject heatEmitter;
    public BoxCollider heatEmitterCollider;



    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------



    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Update()
    {
        TurnOnUI();

        if (UNA_StaticVariables.currentHotTemp <= 0)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);

        }
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
    void TurnOnUI()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(true);
        UNA_StaticVariables.isTempGaugeOn = true;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Image>().enabled = true;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Slider>().enabled = true;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(true);
    }

    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            heatEmitter.SetActive(false);
            heatEmitterCollider.enabled = false;
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Image>().enabled = false;
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Slider>().enabled = false;
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
            UNA_StaticVariables.isTempGaugeOn = false;
        }
    }
} // End TempHeatEmittor