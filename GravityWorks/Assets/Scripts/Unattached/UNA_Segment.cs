/* -----------------------------------------------------------------------------------
 * Class Name: UNA_Segment
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using System;

[Serializable]
public class UNA_Segment
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

    int segmentNumber;
    bool segmentComplete = false;
    bool segmentPointsCompiled = false;
    int pointsCollected;
    float teleporterX, teleporterY, teleporterZ;
    float timeElapsed;
    Gravity gravitySetting;
    float currentHealth = 100f; //I think health should be restarted every leve
    #endregion

    #region Getters/Setters

    public int SegmentNumber
    {
        get { return segmentNumber; }
    }
    
    public bool SegmentComplete
    {
        get { return segmentComplete; }
        set { segmentComplete = value; }
    }

    public bool SegmentPointsCompiled
    {
        get { return segmentPointsCompiled; }
        set { segmentPointsCompiled = value; }
    }

    public int PointsCollected
    {
        get { return pointsCollected; }
        set { pointsCollected = value; }
    }

    public float TeleporterX
    {
        get { return teleporterX; }
        set { teleporterX = value; }
    }

    public float TeleporterY
    {
        get { return teleporterY; }
        set { teleporterY = value; }
    }

    public float TeleporterZ
    {
        get { return teleporterZ; }
        set { teleporterZ = value; }
    }

    public float TimeElapsed
    {
        get { return timeElapsed; }
        set { timeElapsed = value; }
    }

    public Gravity GravitySetting
    {
        get { return gravitySetting; }
        set { gravitySetting = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    #endregion

    #region Constructors

    public UNA_Segment (int segmentNumber)
    {
        this.segmentNumber = segmentNumber;

        pointsCollected = 0;
        timeElapsed = 0;
        gravitySetting = Gravity.Earth;
    }

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


} // End UNA_Segment