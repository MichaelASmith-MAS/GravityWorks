/* -----------------------------------------------------------------------------------
 * Class Name: WLD_Teleporter
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 09/29/17
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: Handles teleporter behaviors, calling specific UI elements when necessary
 *          or loading specified level.
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WLD_Teleporter : MonoBehaviour 
{

    #region VARIABLES
    public Scenes levelToLoad; //Ref to what scene you want to load - will need to update to ref specific Teleporter's Segement selection later
    public int segmentNumber;
    public Vector3[] levelTeleporters;
    public bool canUseTeleporter = true, checkPointOnly = false;
    public Vector2 teleportPoint;

    private UNA_Level currentLevel;
    private WLD_LevelLoader lL; //Ref to levelLoader script - will need to update to ref specific Teleporter's Segement selection later
    private PLR_CharacterMovement playerMove;
    private PLR_Jump playerJump;
    private bool startSceneTransition = false, interactButtonEnabled = true; //Needs to be reworked when UI segement selection is put into action.
    private GameObject player;

    int startTemp = 0;

    public static bool isInTeleporter = false;
    #endregion

    #region GETTERS/SETTERS

    public Scenes LevelToLoad
    {
        set { levelToLoad = value; }
    }

    public bool GETcanUseTeleporter
    {
        set { canUseTeleporter = value; }
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
        levelTeleporters = new Vector3[WLD_GameController.activeLevel.LevelSegments.Length];

        for (int i = 0; i < WLD_GameController.activeLevel.LevelSegments.Length; i++)
        {
            levelTeleporters[i] = new Vector3(WLD_GameController.activeLevel.LevelSegments[i].TeleporterX, WLD_GameController.activeLevel.LevelSegments[i].TeleporterY, WLD_GameController.activeLevel.LevelSegments[i].TeleporterZ);

        }


        currentLevel = WLD_GameController.activeLevel;

        lL = FindObjectOfType<WLD_LevelLoader>(); //Ref to levelLoader script - will need to update to ref specific Teleporter's Segement selection later
        player = WLD_GameController.player;
        playerMove = FindObjectOfType<PLR_CharacterMovement>();


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

    private void Update()
    {
        CheckIsPaused();

        SetInteractButtonEnabled();
    }

    //End Update

    private void OnTriggerStay(Collider other)
    {
        WLD_GameController.player.GetComponent<GRV_IndividualGravity>().Drag = 1;

        if (!startSceneTransition && !checkPointOnly)
        {
            if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Final] && canUseTeleporter)
            {
                if ((other.tag == UNA_Tags.player && (Input.GetAxis("Interact") > 0) && interactButtonEnabled))
                {
                    GameObject levelSpecificUI = GameObject.Find("LevelSpecificUI");

                    levelSpecificUI.transform.GetChild(0).gameObject.SetActive(true);

                    isInTeleporter = true;
                    startSceneTransition = true;

                    player.transform.position = new Vector3(0, 0, 0);
                    player.GetComponent<PLR_CharacterMovement>().playerGFX.transform.eulerAngles = new Vector3(0, 0, 0);
                    other.gameObject.GetComponent<WLD_HealthDmg>().ChangeHealth(100);

                    WLD_GameController.uIButtonManager.GetComponent<UI_SaveGame>().SaveGame();
                    //SceneManager.LoadScene(WLD_GameController.levels[Scenes.Credits].SceneIndex);
                }
            }

            else if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Hub] && canUseTeleporter)
            {
                if ((other.tag == UNA_Tags.player && (Input.GetAxis("Interact") > 0) && interactButtonEnabled))
                {
                    isInTeleporter = true;

                    startSceneTransition = true;

                    Vector3 spawnLocation = Vector3.zero;
                    WLD_GameController.uIButtonManager.GetComponent<UI_SaveGame>().SaveGame();
                    
                    lL.LoadLevel(Scenes.Hub, spawnLocation);
                    //playerMove.FreezeCharacter();
                    //JS - I still think we should disable the player from moving in the middle of scene transitions...there may be a better place for it though?
                }
            }
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isInTeleporter = false;

            WLD_CameraFollow_SL.inTeleporter = false;

            WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
        }
    }
    public void LoadLevelSegment (int segNumber)
    {
        Vector3 spawnLocation;

        if (!startSceneTransition)
        {
            if (!WLD_GameController.levels[levelToLoad].LevelSegments[0].SegmentComplete)
            {
                spawnLocation = Vector3.zero; //Resetting player location to origin (and below too)

                startSceneTransition = true;
                //playerMove.FreezeCharacter();
                lL.LoadLevel(levelToLoad, spawnLocation);
            }
            else
            {
                spawnLocation = new Vector3(WLD_GameController.levels[levelToLoad].LevelSegments[segNumber - 1].TeleporterX, WLD_GameController.levels[levelToLoad].LevelSegments[segNumber - 1].TeleporterY, WLD_GameController.levels[levelToLoad].LevelSegments[segNumber - 1].TeleporterZ);

                startSceneTransition = true;
                //playerMove.FreezeCharacter();
                lL.LoadLevel(levelToLoad, spawnLocation);
            }
        }

        //startSceneTransition = true;
        ////playerMove.FreezeCharacter();
        //lL.LoadLevel(levelToLoad, spawnLocation);

    }

    void CheckIsPaused()
    {
        if (UI_Pause.isPaused)
        {
            canUseTeleporter = false;
        }

        else if (!UI_Pause.isPaused)
        {
            if (!canUseTeleporter)
            {
                Invoke("SetCanUseTeleporterToTrue", 0.1f);
            }
        }
    }

    void SetCanUseTeleporterToTrue()
    {
        canUseTeleporter = true;
    }

    void SetInteractButtonEnabled()
    {
        if (Input.GetAxis("Interact") > 0)
        {
            interactButtonEnabled = false;
        }
        if (Input.GetAxis("Interact") == 0)
        {
            interactButtonEnabled = true;
        }
    }

}
// End WLD_Teleporter