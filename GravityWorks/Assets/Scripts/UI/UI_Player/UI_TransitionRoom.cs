/* -----------------------------------------------------------------------------------
 * Class Name: UI_TransitionRoom
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_TransitionRoom : MonoBehaviour 
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
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == "Transition")
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(true);
            WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = true;

            WLD_GameController.ui_Texts[UI_Txt.TransitionPlanetTxt].text = WLD_GameController.gravityTitles[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        }
     
    }
    void OnTriggerStay(Collider player)
    {
        if (player.tag == "Transition")
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(true);
            WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = true;

            WLD_GameController.ui_Texts[UI_Txt.TransitionPlanetTxt].text = WLD_GameController.gravityTitles[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        }
        if (player.tag == UNA_Tags.segment)
        {
            WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].color = WLD_GameController.gravityImages[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag == "Transition")
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(false);
            WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = false;
        }
    }

} // End UI_TransitionRoom