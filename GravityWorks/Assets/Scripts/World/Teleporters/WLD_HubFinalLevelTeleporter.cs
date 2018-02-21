/* -----------------------------------------------------------------------------------
 * Class Name: WLD_HubFinalLevelTeleporter
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

public class WLD_HubFinalLevelTeleporter : MonoBehaviour 
{

    #region VARIABLES

    public GameObject /*endPieceIndicator1, endPieceIndicator2,*/ endPieceIndicator3, endPieceIndicator4, 
        endPieceIndicator5, /*endPieceIndicator6*/ endPieceIndicator7, finalLevelDoorUnlock, finalLevelDoorUnlockOne, finalTeleporter;

    private GameObject player;
    private CLT_General clt_General;

    public GameObject final_offTeleporter;
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
        clt_General = player.GetComponent<CLT_General>();
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
        PuzzlePieceDetection();
    }
//End Update

    void PuzzlePieceDetection()
    {

        //endPieceIndicator1.SetActive(WLD_GameController.levels[Scenes.GW_0202].FinalLevelPieceCollected);
        //endPieceIndicator2.SetActive(WLD_GameController.levels[Scenes.MS_0211].FinalLevelPieceCollected);
        endPieceIndicator3.SetActive(WLD_GameController.levels[Scenes.DW_0713].FinalLevelPieceCollected);
        endPieceIndicator4.SetActive(WLD_GameController.levels[Scenes.KL_0602].FinalLevelPieceCollected);
        endPieceIndicator5.SetActive(WLD_GameController.levels[Scenes.JA_0629].FinalLevelPieceCollected);
        //endPieceIndicator6.SetActive(WLD_GameController.levels[Scenes.JS_1021].FinalLevelPieceCollected);
        endPieceIndicator7.SetActive(WLD_GameController.levels[Scenes.Hub].FinalLevelPieceCollected);


        if (WLD_GameController.levels[Scenes.DW_0713].FinalLevelPieceCollected == true &&
        WLD_GameController.levels[Scenes.KL_0602].FinalLevelPieceCollected == true &&
        WLD_GameController.levels[Scenes.JA_0629].FinalLevelPieceCollected == true &&
        //WLD_GameController.levels[Scenes.JS_1021].FinalLevelPieceCollected == true &&
        WLD_GameController.levels[Scenes.Hub].FinalLevelPieceCollected == true)
        {
            Debug.Log("Completed");
            final_offTeleporter.SetActive(false);
            finalTeleporter.SetActive(true);
            WLD_GameController.levels[Scenes.Final].LevelUnlocked = true;
            //finalTeleporter.transform.position = new Vector3(transform.position.x, -16, transform.position.z);
            finalLevelDoorUnlock.SetActive(false);
            finalLevelDoorUnlockOne.SetActive(false);
        }

        else { return; }
    }
}
// End WLD_HubFinalLevelTeleporter