/* -----------------------------------------------------------------------------------
 * Class Name: UI_FinalLevelEndStory
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_FinalLevelEndStory : MonoBehaviour
{

    #region VARIABLES
    //Public Variables
    public float moveSpeed, flashingSpeed;
    public GameObject story, storyText, continueText;
    public Button firstSkipButton;

    //Priave Variables
    float flashTimer;
    bool charAbilities = false, onStartRan = false;
    public static bool isInLanding = true;
    WLD_LevelLoader lL;
    GameObject player;
    PLR_Shoot plr_shoot;
    PLR_CharacterMovement plr_CharacterMovement;
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
    void Start()
    {
        Invoke("OnStart", 0.01f);

    }//End Start	

    
    private void OnStart()
    {
        lL = FindObjectOfType<WLD_LevelLoader>();
        player = WLD_GameController.player;
        plr_shoot = FindObjectOfType<PLR_Shoot>();
        plr_CharacterMovement = player.GetComponent<PLR_CharacterMovement>();

        isInLanding = true;
        

        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        plr_CharacterMovement.enabled = false;
        plr_shoot.enabled = false;
        onStartRan = true;
    }

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
    * 
    */
    void Update()
    {
        if (onStartRan)
        {
            TextFlashing();
            ScrollStoryText();
            InputToContinue();
        }

        if (isInLanding)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanel].SetActive(false);
            WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = false;
            WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelShrink].SetActive(false);
        }

    }
    //End Update


    private void TextFlashing()
    {
        flashTimer += Time.deltaTime;

        if (flashTimer >= flashingSpeed)
        {
            flashTimer = 0;

            if (!continueText.activeSelf)
            {
                continueText.SetActive(true);
            }

            else if (continueText.activeSelf)
            {
                continueText.SetActive(false);
            }
        }
    }

    private void ScrollStoryText()
    {
        if (story.activeSelf && storyText.transform.position.y < 490)
        {
            storyText.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);

            if (storyText.transform.position.z != 0)
            {
                storyText.transform.position = new Vector3(storyText.transform.position.x, storyText.transform.position.y, 0);
            }


        }
    }

    private void InputToContinue()
    {
        Vector3 spawnLocation = Vector3.zero;

        if (story.activeSelf && storyText.transform.position.y >= 200)
        {
            if (Input.GetAxisRaw("Interact") > 0)
            {
                isInLanding = false;

                if (!charAbilities)
                {
                    plr_CharacterMovement.enabled = true;
                    plr_shoot.enabled = true;
                    charAbilities = true;
                }

                lL.LoadLevel(Scenes.Credits, spawnLocation);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void SkipButton()
    {
        Vector3 spawnLocation = Vector3.zero;

        if (!charAbilities)
        {
            plr_CharacterMovement.enabled = true;
            plr_shoot.enabled = true;
            charAbilities = true;
            WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;
            isInLanding = false;

        }

        lL.LoadLevel(Scenes.Credits, spawnLocation);
        this.gameObject.SetActive(false);
    }

    public void TextSpeedButton(int textSpeed)
    {
        moveSpeed = textSpeed;
    }

    public void RestorePlayerAbilities()
    {
        plr_CharacterMovement.enabled = true;
        plr_shoot.enabled = true;
        firstSkipButton.interactable = true;
        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;

        isInLanding = false;

        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;
    }

}
// End UI_Tutorial