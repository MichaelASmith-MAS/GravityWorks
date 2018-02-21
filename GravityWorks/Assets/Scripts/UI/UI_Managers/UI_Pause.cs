/* -----------------------------------------------------------------------------------
 * Class Name: UI_Pause
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
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UI_Pause : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    [Header("Panel GameObjects")]
    public GameObject helpMenu;
    public GameObject saveMenu;
    public WLD_MessageSystem msgSystem;

    //[Header("Enlarge")]
    //public Button gGunEnlargeButton;

    [Header("Event Systems")]
    public EventSystem saveEventSystem;
    public EventSystem gameControllerEventSystem;
    public EventSystem helpEventSystem;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    PLR_Shoot plr_Shoot;
    PLR_CharacterMovement plr_CharacterMovement;

    public static bool isPaused = false;
    bool pauseButtonEnabled = true;

    UNA_Level currentLevel;


    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: TutorialPauseMenu
    // Return types: NA
    // Argument types: NA
    // Author:  Kayci Lyons
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: This function calls pause or unpause in the tutorial level
    // ------------------------------------------------------------------------------
    void Update()
    {
        TutorialPauseMenu();
        CorePause();
    }

    public void UnpauseButton()
    {
        isPaused = false;
        Time.timeScale = 1;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);

        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(true);

        if (UNA_StaticVariables.isShrunk == false)
        {
            gameObject.GetComponent<UI_UIButtonManager>().GgunShrink();
        }

        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;


        if (!msgSystem.show)
        {
            plr_Shoot.enabled = true;
            plr_CharacterMovement.enabled = true;
        }
        Debug.Log("UN-paused");
    }

    public void TutorialPauseMenu()
    {
        if (!FindObjectOfType<WLD_LevelLoader>().loadingScreen.activeSelf)
        {
            currentLevel = WLD_GameController.activeLevel;
            if (currentLevel != WLD_GameController.levels[Scenes.MainMenu] || currentLevel != WLD_GameController.levels[Scenes.Hub] || currentLevel != WLD_GameController.levels[Scenes.Controls] || currentLevel != WLD_GameController.levels[Scenes.Credits])
            {
                if (currentLevel == WLD_GameController.levels[Scenes.Tutorial])
                {
                    GameObject landingSeq = GameObject.Find("LevelSpecificUI");

                    //if (landingSeq.gameObject.transform.GetChild(0).GetChild(1).gameObject.activeSelf)
                    //{
                    //    return;
                    //}
                    if (!UI_Tutorial.isInLanding)
                    {
                        if (!isPaused)
                        {
                            if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled && currentLevel != WLD_GameController.levels[Scenes.Hub])
                            {
                                plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
                                plr_Shoot = FindObjectOfType<PLR_Shoot>();
                                WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
                                WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                                isPaused = true;
                                Time.timeScale = 0;
                                plr_Shoot.enabled = false;
                                plr_CharacterMovement.enabled = false;
                                WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = false;

                                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(false);

                            }
                        }
                        else if (isPaused)
                        {
                            if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled && currentLevel != WLD_GameController.levels[Scenes.Hub])
                            {
                                WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;
                                WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
                                plr_Shoot = FindObjectOfType<PLR_Shoot>();
                                plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
                                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(true);

                                if (UNA_StaticVariables.isShrunk == false)
                                {
                                    gameObject.GetComponent<UI_UIButtonManager>().GgunShrink();
                                }
                                //if (topTimesScoresOverlay.activeSelf)
                                //{
                                //    return;
                                //}
                                //else if (!topTimesScoresOverlay.activeSelf)
                                //{
                                //    WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                                //    isPaused = false;
                                //    Time.timeScale = 1;
                                //    gGunEnlargeButton.enabled = true;
                                //    WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;

                                //    if (!msgSystem.show)
                                //    {
                                //        plr_Shoot.enabled = true;
                                //        plr_CharacterMovement.enabled = true;
                                //    }
                                //    Debug.Log("UN-paused");
                                //}

                                WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                                isPaused = false;
                                Time.timeScale = 1;

                                WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;

                                if (!msgSystem.show)
                                {
                                    plr_Shoot.enabled = true;
                                    plr_CharacterMovement.enabled = true;
                                }
                                Debug.Log("UN-paused");

                                if (helpMenu.activeSelf)
                                {
                                    helpMenu.SetActive(false);
                                    WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(true);
                                    isPaused = true;
                                    Time.timeScale = 0;
                                    plr_Shoot.enabled = false;
                                    plr_CharacterMovement.enabled = false;
                                }
                                if (!helpMenu.activeSelf)
                                {
                                    UI_MenuManager.isHelpCkicked = false;

                                    helpEventSystem.enabled = false;
                                    gameControllerEventSystem.enabled = true;
                                }
                                if (saveMenu.activeSelf)
                                {
                                    saveMenu.SetActive(false);
                                    WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(true);
                                    isPaused = true;
                                    Time.timeScale = 0;
                                    plr_Shoot.enabled = false;
                                    plr_CharacterMovement.enabled = false;
                                }
                                if (!saveMenu.activeSelf)
                                {
                                    UI_MenuManager.isSavedClicked = false;

                                    saveEventSystem.enabled = false;
                                    gameControllerEventSystem.enabled = true;

                                }
                            }
                        }
                    }
                    
                    SetPauseButtonEnabled();
                }

                if (currentLevel == WLD_GameController.levels[Scenes.MainMenu] && isPaused)
                {
                    isPaused = false;
                    Time.timeScale = 1;
                    plr_Shoot.enabled = true;
                    plr_CharacterMovement.enabled = true;

                }

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
    // Purpose: This function calls pause or unpause in all the other levels
    // ------------------------------------------------------------------------------
    void CorePause()
    {
        if (!FindObjectOfType<WLD_LevelLoader>().loadingScreen.activeSelf)
        {
            currentLevel = WLD_GameController.activeLevel;
            if (currentLevel != WLD_GameController.levels[Scenes.MainMenu] || currentLevel != WLD_GameController.levels[Scenes.Hub] || currentLevel != WLD_GameController.levels[Scenes.Controls] || currentLevel != WLD_GameController.levels[Scenes.Credits])
            {
                if (!isPaused)
                {
                    if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled && currentLevel != WLD_GameController.levels[Scenes.Hub])
                    {
                        plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
                        plr_Shoot = FindObjectOfType<PLR_Shoot>();

                        WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                        isPaused = true;
                        Time.timeScale = 0;
                        plr_Shoot.enabled = false;
                        plr_CharacterMovement.enabled = false;
                        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(false);

                        
                        Debug.Log("Paused");
                    }
                }
                else if (isPaused)
                {
                    if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled && currentLevel != WLD_GameController.levels[Scenes.Hub])
                    {
                        plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
                        plr_Shoot = FindObjectOfType<PLR_Shoot>();
                        //gGunEnlargeButton.enabled = true;
                        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
                        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(true);
                      
                        if (UNA_StaticVariables.isShrunk == false)
                        {
                            gameObject.GetComponent<UI_UIButtonManager>().GgunShrink();
                        }
                        

                        //if (topTimesScoresOverlay.activeSelf)
                        //{
                        //    return;
                        //}
                        //else if (!topTimesScoresOverlay.activeSelf)
                        //{
                        //    WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                        //    isPaused = false;
                        //    Time.timeScale = 1;
                        //    gGunEnlargeButton.enabled = true;
                        //    WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;

                        //    if (!msgSystem.show)
                        //    {
                        //        plr_Shoot.enabled = true;
                        //        plr_CharacterMovement.enabled = true;
                        //    }
                        //    Debug.Log("UN-paused");
                        //}

                        WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(!WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].activeSelf);
                        isPaused = false;
                        Time.timeScale = 1;
                        
                        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;

                        if (!msgSystem.show)
                        {
                            plr_Shoot.enabled = true;
                            plr_CharacterMovement.enabled = true;
                        }
                        Debug.Log("UN-paused");

                        if (helpMenu.activeSelf)
                        {
                            helpMenu.SetActive(false);
                            WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(true);
                            isPaused = true;
                            Time.timeScale = 0;
                            plr_Shoot.enabled = false;
                            plr_CharacterMovement.enabled = false;
                        }
                        if (!helpMenu.activeSelf)
                        {
                            UI_MenuManager.isHelpCkicked = false;

                            helpEventSystem.enabled = false;
                            gameControllerEventSystem.enabled = true;
                        }
                        if (saveMenu.activeSelf)
                        {
                            saveMenu.SetActive(false);
                            WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(true);
                            isPaused = true;
                            Time.timeScale = 0;
                            plr_Shoot.enabled = false;
                            plr_CharacterMovement.enabled = false;
                        }
                        if (!saveMenu.activeSelf)
                        {
                            UI_MenuManager.isSavedClicked = false;

                            saveEventSystem.enabled = false;
                            gameControllerEventSystem.enabled = true;

                        }
                    }

                }
                if (currentLevel != WLD_GameController.levels[Scenes.Tutorial])
                {
                    SetPauseButtonEnabled();
                }
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
    void SetPauseButtonEnabled()
    {
        if (Input.GetAxisRaw("Pause") > 0)
        {
            pauseButtonEnabled = false;
        }
        if (Input.GetAxisRaw("Pause") == 0)
        {
            pauseButtonEnabled = true;
        }
    }

} // End UI_Pause