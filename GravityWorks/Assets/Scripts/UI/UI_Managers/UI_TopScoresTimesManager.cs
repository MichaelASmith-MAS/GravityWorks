/* -----------------------------------------------------------------------------------
 * Class Name: UI_TopScoresTimesManager
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_TopScoresTimesManager : MonoBehaviour 
{

    #region VARIABLES

    [SerializeField] private Scenes levelRef;
    [SerializeField] private int segmentNo;

    private Text time, score;
    private string now;

    #endregion

    #region GETTERS/SETTERS

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    /* ------------------------------------------------------------------------------
    * Function Name: Start
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Used to initialize variables or perform startup processes
    * ------------------------------------------------------------------------------
    */
    void Start () 
	{
        time = this.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        score = this.transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();
    }//End Start	
	
    /* ------------------------------------------------------------------------------
    * Function Name: Update
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Runs each frame. Used to perform frame based checks and actions.
    * ------------------------------------------------------------------------------
    */
	void Update () 
	{
        UpdateTopScore();
        UpdateTopTime();
    }
    //End Update

    void UpdateTopTime()
    {
        float sec = WLD_GameController.levels[levelRef].LevelSegments[segmentNo].TimeElapsed % 60;
        float min = WLD_GameController.levels[levelRef].LevelSegments[segmentNo].TimeElapsed / 120;

        now = string.Format("{0:00}:{1:00}", min, sec);

        time.text = "" + now;
    }

    void UpdateTopScore()
    {
        score.text = "" + WLD_GameController.levels[levelRef].LevelSegments[segmentNo].PointsCollected;
    }
}
// End UI_TopScoresTimesManager