/* -----------------------------------------------------------------------------------
 * Class Name: UI_Time
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 10/10/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Projects the Time data onto the UI.
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Time : MonoBehaviour 
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
    WLD_SegmentController segmentController;

    GameObject player;

    string now;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Start()
    {
        segmentController = GetComponent<WLD_SegmentController>();

        player = WLD_GameController.player;

        UNA_StaticVariables.isRunning = true;
    }

    void Update()
    {
        CurrentTimeUI();

        segmentController.RunTimer();
    }
   
    // ------------------------------------------------------------------------------
    // Function Name: CurrentTime
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/1/17
    // ------------------------------------------------------------------------------
    // Purpose: Takes the timer from the segment controller then converts it 
    //          to minutes, seconds while projecting to the UI
    // ------------------------------------------------------------------------------
    void CurrentTimeUI()
    {
        float sec = segmentController.timer % 60;
        float min = segmentController.timer / 120;
        
        //now = string.Format("{0:00}:{1:00}", min, sec);

        //WLD_GameController.ui_Texts[UI_Txt.CurrentTime].text = now;
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
} // End UI_Time