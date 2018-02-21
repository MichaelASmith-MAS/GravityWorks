/* -----------------------------------------------------------------------------------
 * Class Name: UI_SkipTutorial
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UI_SkipTutorial : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Button skipButton;
    public GameObject skipButtonGameObject;
    public Button gGunPanelEnlarge; 
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;
    PLR_Shoot plr_shoot;
    WLD_LevelLoader lL;

    bool charAbilities = false;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    
    void Update()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Tutorial])
        {
            skipButton.enabled = true;
        }
       
        player = WLD_GameController.player;
        lL = FindObjectOfType<WLD_LevelLoader>();
        plr_shoot = FindObjectOfType<PLR_Shoot>();

        RestoreAbilities();
        ToggleSkipButton();
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
    public void SkipButton()
    {
        Vector3 spawnLocation = Vector3.zero;

        charAbilities = true;

        lL.LoadLevel(Scenes.Hub, spawnLocation);

        WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;


        gGunPanelEnlarge.enabled = true;

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
    void RestoreAbilities()
    {
        if (charAbilities)
        {
            //UI_UIButtonManager.isPaused = true;
            Time.timeScale = 1;

            player.GetComponent<PLR_CharacterMovement>().enabled = true;
            plr_shoot.enabled = true;
            charAbilities = false;
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
    void ToggleSkipButton()
    {
        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Tutorial])
        {
            skipButtonGameObject.SetActive(false);
        }
        else
        {
            skipButtonGameObject.SetActive(true);
        }
    }

} // End UI_SkipTutorial