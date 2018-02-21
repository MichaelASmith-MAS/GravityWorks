/* -----------------------------------------------------------------------------------
 * Class Name: WLD_SegmentController
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 9/26/2017
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Controls all aspects of a level segment through a trigger volume
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class WLD_SegmentController : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public Gravity segmentGravity;
    public int segmentNumber;
    public Vector3 range;

    public float timer;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    UNA_Segment segment;
    UNA_Level currentLevel;


    #endregion

    #region Getters/Setters

    public UNA_Segment Segment
    {
        get { return segment; }
    }

    public UNA_Level CurrentLevel
    {
        get { return currentLevel; }
    }

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/26/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs at the start of a scene. Ensures the associated collider is a trigger;
    //          sets the current level tracking to the level provided in the enum selection;
    //          iterates through all segments in the designated level to locate the appropriate
    //          segment data. Sets the gravity of the segment to the selected gravity setting.
    // ------------------------------------------------------------------------------

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;

        currentLevel = WLD_GameController.activeLevel;

        for (int i = 0; i < currentLevel.LevelSegments.Length; i++)
        {
            if (currentLevel.LevelSegments[i].SegmentNumber == segmentNumber)
            {
                segment = currentLevel.LevelSegments[i];
            }
        }

        WLD_Teleporter[] teleporters = FindObjectsOfType<WLD_Teleporter>();

        for (int i = 0; i < teleporters.Length; i++)
        {
            if (teleporters[i].GetComponent<WLD_TeleporterOff>() != null)
            {
                teleporters[i].enabled = false;

            }

        }

        segment.GravitySetting = segmentGravity;

    }

    // ------------------------------------------------------------------------------
    // Function Name: RunTimer
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Increments a timer variable by time. Used for tracking the time elapsed
    //          by the player within this segment
    // ------------------------------------------------------------------------------

    public void RunTimer()
    {
        if (UNA_StaticVariables.isRunning)
        {
            timer += Time.deltaTime;
        }     
    }

    // ------------------------------------------------------------------------------
    // Function Name: StopTimer
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Sets the time elapsed value within the segment data to the timer value
    // ------------------------------------------------------------------------------

    public void StopTimer ()
    {
        segment.TimeElapsed = timer;

    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerExit
    // Return types: NA
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 10/04/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            if (WLD_GameController.player.GetComponent<PLR_Points>().LastSegment != segmentNumber)
            {
                if (WLD_GameController.player.GetComponent<PLR_Points>().LastSegment < segmentNumber)
                {
                    WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].SegmentComplete = true;
                    WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].PointsCollected = WLD_GameController.player.GetComponent<PLR_Points>().SegmentPoints;
                    WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].TimeElapsed = timer;
                    WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

                    currentLevel.CalculatePoints();
                    currentLevel.CalculateTime();
                    WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].CurrentHealth = 0f;

                    WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].CurrentHealth = other.GetComponent<WLD_HealthDmg>().Health;

                    UNA_StaticVariables.isRunning = true;
                }

                WLD_GameController.player.GetComponent<PLR_Points>().LastSegment = segmentNumber;
            }
        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerExit
    // Return types: NA
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 10/04/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            WLD_GameController.player.GetComponent<PLR_Points>().LastSegment = segmentNumber;

        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerExit
    // Return types: NA
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 10/04/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------


} // End WLD_SegmentController