/* -----------------------------------------------------------------------------------
 * Class Name: UNA_Level
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 9/25/2017
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Data structure for game levels
 * -----------------------------------------------------------------------------------
 */

using System;

[Serializable]
public class UNA_Level
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

    UNA_Segment[] levelSegments;
    string levelName;
    int sceneIndex, pointsCollected;
    float timeElapsed, currentHealth;
    UNA_Segment lastCompletedSegment;
    bool levelComplete;
    bool finalLevelPieceCollected;
    bool levelUnlocked;
  

    #endregion

    #region Getters/Setters

    public UNA_Segment[] LevelSegments
    {
        get { return levelSegments; }
    }

    public string LevelName
    {
        get { return levelName; }
    }

    public int SceneIndex
    {
        get { return sceneIndex; }
    }

    public int PointsCollected
    {
        get { return pointsCollected; }
    }

    public float CurrentHealth
    {
        get {return currentHealth; }
        set { currentHealth = value; }
    }

    public float TimeElapsed
    {
        get { return timeElapsed; }
        set { timeElapsed = value; }
    }

    public UNA_Segment LastCompletedSegment
    {
        get { return lastCompletedSegment; }
        set { lastCompletedSegment = value; }
    }

    public bool LevelComplete
    {
        get { return levelComplete; }
        set { levelComplete = value; }
    }

    public bool FinalLevelPieceCollected
    {
        get { return finalLevelPieceCollected; }
        set { finalLevelPieceCollected = value; }
    }

    public bool LevelUnlocked
    {
        get { return levelUnlocked; }
        set { levelUnlocked = value; }
    }

    #endregion

    #region Constructors

    public UNA_Level (string levelName, int sceneIndex, int numSegments)
    {
        this.levelName = levelName;
        this.sceneIndex = sceneIndex;

        pointsCollected = 0;
        timeElapsed = 0f;
        finalLevelPieceCollected = false;
        levelComplete = false;
        levelUnlocked = false;

        if (numSegments > 0)
        {
            levelSegments = new UNA_Segment[numSegments];

            for (int i = 0; i < numSegments; i++)
            {
                levelSegments[i] = new UNA_Segment(i + 1);
            }

        }

        else
        {
            levelSegments = new UNA_Segment[1];

            levelSegments[0] = new UNA_Segment(0);

        }

        lastCompletedSegment = levelSegments[0];
    }


    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: CalculatePoints
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/25/2017
    // ------------------------------------------------------------------------------
    // Purpose: Combines the points from each segment
    // ------------------------------------------------------------------------------

    public void CalculatePoints ()
    {
        for (int i = 0; i < levelSegments.Length - 1; i++)
        {
            if (levelSegments[i].SegmentComplete && !levelSegments[i].SegmentPointsCompiled)
            {
                pointsCollected += levelSegments[i].PointsCollected;
                levelSegments[i].SegmentPointsCompiled = true;

            }
            
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: CalculateTime
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Combines elapsed time from each segment
    // ------------------------------------------------------------------------------

    public void CalculateTime ()
    {
        for (int i = 0; i < levelSegments.Length - 1; i++)
        {
            if (levelSegments[i].SegmentComplete && !levelSegments[i].SegmentPointsCompiled)
            {
                timeElapsed += levelSegments[i].TimeElapsed;
            }
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

    public void CalculateCurrentHealth()
    {
        for (int i = 0; i < levelSegments.Length - 1; i++)
        {
            currentHealth += levelSegments[i].CurrentHealth;
        }
    }

} // End UNA_Level