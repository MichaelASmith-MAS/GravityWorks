/* -----------------------------------------------------------------------------------
 * Class Name: UI_EnvironmentalGravityUI
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 9/26/17
 * Credit: Joseph Aranda
 * -----------------------------------------------------------------------------------
 * Purpose: This script controls the UI for the Environmental Gravity
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class UI_EnvironmentalGravityUI : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
 
    //[Tooltip("PLace CURRENT segment here. If its the first segment place the first segment")]
    //[Header("Segment")]
    //public BoxCollider currentSegmentVolumn;

    //[Header("Previous Segment")]
    //public float triggerOffTime;
    
   
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    //WLD_SegmentController segmentController;

    //public static bool hasBeen, isEarthGravity;

    //private WLD_PLanetGravity plantGravity;


    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
   
    //void Start()
    //{
    //    segmentController = GetComponent<WLD_SegmentController>();
    //    currentSegmentVolumn = GetComponent<BoxCollider>();

    //    hasBeen = false;
    //    isEarthGravity = true;
    //}
    //void Update()
    //{
    //    SetDefaultGravitySetting();
    //}

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerStay
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith 
    // Date: 9/28/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the trigger which will then update the environmental 
    //          gravity depending on which segment the player enters.
    // ------------------------------------------------------------------------------
    //void OnTriggerStay(Collider player)
    //{
    //    if (player.tag == UNA_Tags.player)
    //    {
    //        hasBeen = true;
    //        isEarthGravity = false;

    //        //WLD_GameController.ui_GameObjects[UI_GO_Panels.EnvironPanel].SetActive(true);
    //        //WLD_GameController.ui_GameObjects[UI_GO_Panels.EmvorironTextPanel].SetActive(true);

    //        if (hasBeen)
    //        {
    //            SetCurrentPlanetPanel();
    //            SetUpcomingPlanetPanel();
    //            SetUpcomingGravityPanel();
    //            SetCurrentPlanetImage();
    //            UpcomingPlanetImage();
    //        }   
    //    }
    //}
    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerExit
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith 
    // Date: 9/28/17
    // ------------------------------------------------------------------------------
    // Purpose: When the player leaves the trigger it will destroy the previous segment
    // ------------------------------------------------------------------------------
    //void OnTriggerExit(Collider player)
    //{
    //    if (player.tag == UNA_Tags.player)
    //    {
    //        hasBeen = false;
    //        isEarthGravity = true;

    //        //currentSegmentVolumn.isTrigger = false;
    //        //WLD_GameController.ui_GameObjects[UI_GO_Panels.EmvorironTextPanel].SetActive(false);
    //        //WLD_GameController.ui_GameObjects[UI_GO_Panels.EnvironPanel].SetActive(false);
    //    }
    //}
    // ------------------------------------------------------------------------------
    // Function Name: SetDefaultGravityPanel
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: Sets the current gravity to earth when not in a segment
    // ------------------------------------------------------------------------------
    //void SetDefaultGravitySetting()
    //{
    //    if (!hasBeen && isEarthGravity)
    //    {
    //        WLD_GameController.ui_Texts[UI_Txt.CurrentPlanetTxt].text = WLD_GameController.gravityTitles[Gravity.Earth].ToString();
    //        WLD_GameController.ui_Texts[UI_Txt.CurrentGValueTxt].text = WLD_GameController.gravityValues[Gravity.Earth].ToString();
    //    }
    //}

    // ------------------------------------------------------------------------------
    // Function Name: SetCurrentPlanetPanel
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the current UI Panel
    // ------------------------------------------------------------------------------
    //void SetCurrentPlanetPanel()
    //{
    //    WLD_GameController.ui_Texts[UI_Txt.CurrentPlanetTxt].text = segmentController.segmentGravity.ToString();
    //    WLD_GameController.ui_Texts[UI_Txt.CurrentGValueTxt].text = WLD_GameController.gravityValues[segmentController.segmentGravity].ToString();
    //}

    // ------------------------------------------------------------------------------
    // Function Name: SetUpcomingPlanetPanel
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the Upcoming planet name UI Panel
    // ------------------------------------------------------------------------------
    //void SetUpcomingPlanetPanel()
    //{
    //    if (segmentController.segmentNumber < segmentController.CurrentLevel.LevelSegments.Length)
    //    {
    //        UNA_Segment upcoming = segmentController.CurrentLevel.LevelSegments[segmentController.segmentNumber];
    //        WLD_GameController.ui_Texts[UI_Txt.UpcomingPlanetTxt].text = WLD_GameController.gravityTitles[upcoming.GravitySetting].ToString();
    //    }
    //}
    // ------------------------------------------------------------------------------
    // Function Name: SetUpcomingGravityPanel
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the Upcoming gravity UI Panel
    // ------------------------------------------------------------------------------
    //void SetUpcomingGravityPanel()
    //{
    //    if (segmentController.segmentNumber < segmentController.CurrentLevel.LevelSegments.Length)
    //    {
    //        UNA_Segment upcoming = segmentController.CurrentLevel.LevelSegments[segmentController.segmentNumber];

    //        WLD_GameController.ui_Texts[UI_Txt.UpcomingGValueTxt].text = WLD_GameController.gravityValues[upcoming.GravitySetting].ToString();
    //    }
    //}
    // ------------------------------------------------------------------------------
    // Function Name: SetCurrentPlanetImage(
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/29/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the color change for the current planet image
    // ------------------------------------------------------------------------------
    //void SetCurrentPlanetImage()
    //{
    //    WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].GetComponent<Image>().color = WLD_GameController.gravityImages[segmentController.segmentGravity];
    //}
    // ------------------------------------------------------------------------------
    // Function Name: UpcomingPlanetImage
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Michael Smith
    // Date: 9/29/17
    // ------------------------------------------------------------------------------
    // Purpose: This controls the color change for the upcoming planet image
    // ------------------------------------------------------------------------------
    //void UpcomingPlanetImage()
    //{
    //    if (segmentController.segmentNumber < segmentController.CurrentLevel.LevelSegments.Length)
    //    {
    //        UNA_Segment upcoming = segmentController.CurrentLevel.LevelSegments[segmentController.segmentNumber];


    //        WLD_GameController.ui_Images[UI_Images.UpcomingPlanetImg].GetComponent<Image>().color = WLD_GameController.gravityImages[upcoming.GravitySetting];

    //    }
    //}
    
} // End UI_EnvironmentalGravityUI