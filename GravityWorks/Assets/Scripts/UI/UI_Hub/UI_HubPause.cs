/* -----------------------------------------------------------------------------------
 * Class Name: UI_HubPause
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UI_HubPause : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("GameObjects")]
    public GameObject hubPausePanel;
    public GameObject topTimesScoresOverlay;
    public GameObject helpOverlay;
    public GameObject saveMenu;
    public GameObject saveButton;

    [Header("EventSystems")]
    public EventSystem eventSystems;
    public EventSystem  helpEventSystem;
    public EventSystem hubSaveEventSystem;

    [Header("MessegeSystems")]
    public WLD_MessageSystem msgSystem;

    [Header("Pause mene buttons")]
    public Button[] menuButtons;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;
    public static bool isPaused = false;
    bool pauseButtonEnabled = true, isScoresEnabled = false;
    bool isSavedClicked = false;

    PLR_CharacterMovement plr_CharacterMovement;
    //PLR_Jump plr_Jump;
    PLR_Shoot plr_Shoot;
    WLD_LevelLoader lL;
    Text hubPointsText;

    int startTemp = 0;
    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    private void Awake()
    {
        lL = FindObjectOfType<WLD_LevelLoader>();

        hubPointsText = GameObject.Find("HubPointsText").GetComponent<Text>();

    }

    void Start()
    {
        Invoke("RunOnStart", 0.1f);

        UNA_StaticVariables.isTempGaugeOn = false;
        UNA_StaticVariables.currentHotTemp = startTemp;

        eventSystems = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        //scoreEventSystem = GameObject.Find("ScoreEventSystems").GetComponent<EventSystem>();
        hubSaveEventSystem = GameObject.Find("HubSaveEventSystem").GetComponent<EventSystem>();

    }

    private void OnLevelWasLoaded(int level)
    {
        lL = FindObjectOfType<WLD_LevelLoader>();
    }

    void Update()
    {
        PauseMenu();
        //SwitchHelpEvents();
        DisablePauseMenuButtons();
        SwitchSaveEvents();

        hubPointsText.text = "Pts " + WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints.ToString();

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
    public void ReturnToMenu()
    {
        Vector3 spawnLocation = Vector3.zero;

        isPaused = false;
        Time.timeScale = 1;
        plr_Shoot.enabled = true;
        plr_CharacterMovement.enabled = true;
        hubPausePanel.SetActive(!hubPausePanel.activeSelf);
        lL.LoadLevel(Scenes.MainMenu, spawnLocation);

        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.MainMenu])
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(false);
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
    public void HelpMenu()
    {
        helpOverlay.SetActive(true);
        hubPausePanel.SetActive(false);
       
    }
    public void HelpOffButtun()
    {
        helpOverlay.SetActive(false);
        hubPausePanel.SetActive(true);
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
    public void PauseMenu()
    {
        if (!lL.loadingScreen.activeSelf)
        {
            if (!isPaused)
            {
                if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled)
                {
                    hubPausePanel.SetActive(!hubPausePanel.activeSelf);

                    eventSystems.enabled = true;

                    if (Time.timeScale == 1 && isPaused == false)
                    {
                        Time.timeScale = 0;
                        isPaused = true;
                        plr_Shoot.enabled = false;
                        //plr_Jump.enabled = false;
                        plr_CharacterMovement.enabled = false;
                    }
                }

                if (saveMenu.activeSelf)
                {
                    saveMenu.SetActive(false);
                    hubPausePanel.SetActive(true);
                    isPaused = true;
                    Time.timeScale = 0;
                    plr_Shoot.enabled = false;
                    plr_CharacterMovement.enabled = false;
                   
                }
                if (!saveMenu.activeSelf)
                {
                    isSavedClicked = false;
                    hubSaveEventSystem.enabled = false;
                    eventSystems.enabled = true;
                }

                if (helpOverlay.activeSelf)
                {
                    isPaused = true;
                    Time.timeScale = 0;
                    plr_Shoot.enabled = false;
                    plr_CharacterMovement.enabled = false;
                }
                else if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu])
                {
                    Time.timeScale = 1;
                }
            }

            else if (isPaused)
            {        
                if (Input.GetAxisRaw("Pause") > 0 && pauseButtonEnabled)
                {
                    if (topTimesScoresOverlay.activeSelf)
                    {
                        return;
                    }

                    else if (!topTimesScoresOverlay.activeSelf && !helpOverlay.activeSelf)
                    {
                        hubPausePanel.SetActive(!hubPausePanel.activeSelf);

                    
                        eventSystems.enabled = false;

                        if (Time.timeScale == 0 && isPaused == true)
                        {
                            Time.timeScale = 1;
                            isPaused = false;

                            if (!msgSystem.show)
                            {
                                plr_Shoot.enabled = true;
                                //plr_Jump.enabled = true;
                                plr_CharacterMovement.enabled = true;
                            }
                        }
                    }

                    if (helpOverlay.activeSelf)
                    {
                        helpOverlay.SetActive(false);
                        hubPausePanel.SetActive(true);

                        helpEventSystem.enabled = false;
                        eventSystems.enabled = true;
                    }

                    else if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu])
                    {
                        Time.timeScale = 1;
                    }
                }
            }
            SetPauseButtonEnabled();
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
    public void UnpauseButton()
    {
        hubPausePanel.SetActive(!hubPausePanel.activeSelf);
        isPaused = false;
        Time.timeScale = 1;

        if (!msgSystem.show)
        {
            plr_Shoot.enabled = true;
            //plr_Jump.enabled = true;
            plr_CharacterMovement.enabled = true;
        }

        Debug.Log("UN-paused");
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
    private void RunOnStart()
    {
        player = WLD_GameController.player;
        plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
        plr_Shoot = FindObjectOfType<PLR_Shoot>();
        //plr_Jump = FindObjectOfType<PLR_Jump>();
        lL = FindObjectOfType<WLD_LevelLoader>();
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
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    //void SwitchHelpEvents()
    //{
    //    if (isScoresEnabled)
    //    {
    //        scoreEventSystem.enabled = true;
    //        eventSystems.enabled = false;

    //    }
    //    else if (!isScoresEnabled)
    //    {
    //        scoreEventSystem.enabled = false;
    //    }
    //} 
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    //public void ClickScores()
    //{
    //    isScoresEnabled = true;
    //    eventSystems.enabled = false;

    //    scoreEventSystem.enabled = true;
    //    helpEventSystem.enabled = false;
    //}
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    //public void ReturnClickScores()
    //{
    //    scoreEventSystem.enabled = false;

    //    eventSystems.enabled = true;

    //    isScoresEnabled = false;
     

    //}
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void SwitchSaveEvents()
    {
        if (isSavedClicked)
        {
            hubSaveEventSystem.enabled = true;
            eventSystems.enabled = false;

        }
        else if (!isSavedClicked)
        {
            hubSaveEventSystem.enabled = false;

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
    public void DisablePauseMenuButtons()
    {
        if (topTimesScoresOverlay.activeSelf)
        {

            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                menuButtons[i].interactable = true;
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
    public void SaveClick()
    {
        isSavedClicked = true;
    }
    public void ReturnSaveClick()
    {
        isSavedClicked = false;
        eventSystems.enabled = true;
    }
} // End UI_HubPause