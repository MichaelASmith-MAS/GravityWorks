/* -----------------------------------------------------------------------------------
 * Class Name: CLT_EndPiece
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

public class CLT_EndPiece : MonoBehaviour 
{

    #region VARIABLES
    [SerializeField] private int endPieceID;
	#endregion

	#region GETTERS/SETTERS
    public int GETendPieceID
    {
        get { return endPieceID; }
    }
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
        //if (WLD_GameController.endPieces[endPieceID])
        //{
        //    this.gameObject.SetActive(false);
        //}

        if (WLD_GameController.activeLevel.FinalLevelPieceCollected)
        {
            gameObject.SetActive(false);
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
     
  
}
// End CLT_EndPiece