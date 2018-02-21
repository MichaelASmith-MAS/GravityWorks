/* -----------------------------------------------------------------------------------
 * Class Name: PLR_Points
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PLR_Points : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public int deathRemovePointValue = 200;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    

    #endregion

    #region Getters/Setters

    public int OverallPoints { get; set; }
    public int SegmentPoints { get; protected set; }
    public int LastSegment { get; set; }

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

    private void Start()
    {
        OverallPoints = 0;
        SegmentPoints = 0;
        LastSegment = 1;
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

    public void ChangePoints (int points)
    {
        SegmentPoints = (int)Mathf.Clamp(SegmentPoints + points, 0, Mathf.Infinity);
        OverallPoints = (int)Mathf.Clamp(OverallPoints + points, 0, Mathf.Infinity);

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

    public void DeathPointRemoval ()
    {

        OverallPoints = (int)Mathf.Clamp(OverallPoints - SegmentPoints, 0, Mathf.Infinity);
        PointRemoval(deathRemovePointValue);
        SegmentPoints = 0;
        LastSegment = 1;

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

    public void ResetToZero ()
    {
        SegmentPoints = 0;
        
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
    public void PointRemoval(int points)
    {
        OverallPoints = (int)Mathf.Clamp(OverallPoints - points, 0, Mathf.Infinity);

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
} // End PLR_Points