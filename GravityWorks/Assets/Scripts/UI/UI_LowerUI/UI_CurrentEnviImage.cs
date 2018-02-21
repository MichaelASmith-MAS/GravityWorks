/* -----------------------------------------------------------------------------------
 * Class Name: UI_CurrentEnviImage
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_CurrentEnviImage : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Image currentEnviPlanetImage;
    public Text currentText;
    public Image fillImage;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    Color32 colorBlack = new Color32(0,0,0,255);
    Color32 colorWhite = new Color32(255, 255, 255, 255);

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
        currentEnviPlanetImage.color = WLD_GameController.gravityImages[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        currentText.text = WLD_GameController.gravityTitles[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting].ToString();

        ChangeCurrentTextColor();
        ChangeGGunTextColro();
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
    void ChangeCurrentTextColor()
    {
        if (currentEnviPlanetImage.color == WLD_GameController.gravityImages[Gravity.Ceres] || currentEnviPlanetImage.color == WLD_GameController.gravityImages[Gravity.Pluto] ||
            currentEnviPlanetImage.color == WLD_GameController.gravityImages[Gravity.Moon] || currentEnviPlanetImage.color == WLD_GameController.gravityImages[Gravity.Mars])
        {
            currentText.color = colorBlack;
        }
        else
        {
            currentText.color = colorWhite;
        }

        
    }

    void ChangeGGunTextColro()
    {
        if (WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color == WLD_GameController.gravityImages[Gravity.Uranus] || WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color == WLD_GameController.gravityImages[Gravity.Neptune] ||
           WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color == WLD_GameController.gravityImages[Gravity.Jupiter])
        {
            WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].color = colorWhite;
        }
        else
        {
            WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].color = colorBlack;
        }
    }

} // End UI_CurrentEnviImage