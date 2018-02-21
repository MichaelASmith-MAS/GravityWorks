/* -----------------------------------------------------------------------------------
 * Class Name: UI_Level
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 10/9/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UI_Level : MonoBehaviour 
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
    UNA_Level level;

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
    }

    void Update()
    {
        CurrentLevel();

    }
    // ------------------------------------------------------------------------------
    // Function Name: CurrentLevel
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/1/17
    // ------------------------------------------------------------------------------
    // Purpose: sets the current level text to the segments selected level
    // ------------------------------------------------------------------------------
    void CurrentLevel()
    {
        WLD_GameController.ui_Texts[UI_Txt.CurrentLevelTxt].text = segmentController.CurrentLevel.LevelName;
    }
} // End UI_Level