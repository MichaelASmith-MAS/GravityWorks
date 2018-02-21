/* -----------------------------------------------------------------------------------
 * Class Name: PLR_Guides
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

public class PLR_Guides : MonoBehaviour 
{

    #region VARIABLES
    public GameObject[] objectives;
    [HideInInspector]
    public int curObjectiveNo = 0;

    GameObject player;
    Vector3 origDistanceFromPlayer;

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
        player = WLD_GameController.player;
        origDistanceFromPlayer = new Vector3 (0, -1.5f, -2.6f);
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
        if (curObjectiveNo >= objectives.Length)
        {
            Destroy(gameObject);
        }

        transform.position = player.transform.position + origDistanceFromPlayer;

        transform.rotation = Quaternion.LookRotation(objectives[curObjectiveNo].transform.position - player.transform.position);

        if (!objectives[curObjectiveNo].GetComponent<Collider>().enabled)
        {
            objectives[curObjectiveNo].GetComponent<Collider>().enabled = true;
        }
	}
//End Update
}
// End PLR_Guides