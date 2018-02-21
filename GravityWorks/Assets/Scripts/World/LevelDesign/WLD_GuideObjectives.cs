/* -----------------------------------------------------------------------------------
 * Class Name: WLD_GuideObjectives
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

public class WLD_GuideObjectives : MonoBehaviour 
{

    #region VARIABLES

    public bool checkpointObjective = false;
    public int setObjectiveNo;

    PLR_Guides plr_Guides;

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
        plr_Guides = FindObjectOfType<PLR_Guides>();
        gameObject.GetComponent<Renderer>().enabled = false;

        if (!checkpointObjective)
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }
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

	}
    //End Update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            if (checkpointObjective)
            {
                plr_Guides.curObjectiveNo = setObjectiveNo;
                plr_Guides.curObjectiveNo++;
            }
            else
            {
                plr_Guides.curObjectiveNo++;
            }

            //print("Current Objective number is now: " + plr_Guides.curObjectiveNo);
            Destroy(gameObject);
        }
    }
}
// End WLD_GuideObjectives