/* -----------------------------------------------------------------------------------
 * Class Name: UI_MenuManager
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
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UI_MenuManager : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public EventSystem eventSystems;
    //public EventSystem scoreEventSystem;
    public EventSystem helpEventSystem;
    public EventSystem saveEventSystem;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    //public GameObject pauseMenueventSystems;
    bool isClicked = false;
    public static bool isHelpCkicked = false;
    public static bool isSavedClicked = false;
    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Awake()
    {
        eventSystems = GameObject.Find("GameControllerEventSystem").GetComponent<EventSystem>();
        //scoreEventSystem = GameObject.Find("ScoreEventSystem").GetComponent<EventSystem>();
        helpEventSystem = GameObject.Find("HelpEventSystem").GetComponent<EventSystem>();
        saveEventSystem = GameObject.Find("SaveEventSystem").GetComponent<EventSystem>();
    }
    
    void Start()
    {
        eventSystems.enabled = true;
    }
    void Update()
    {
        DisableEventSystem();
        SwitchEventSystems();
        SwitchHelpEvents();
        SwitchSaveEvents();

        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu] || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.LoadGame]
            || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Controls] || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Credits]
            || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            eventSystems.enabled = false;
            //scoreEventSystem.enabled = false;
            helpEventSystem.enabled = false;
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
    void DisableEventSystem()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu] || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.LoadGame]
            || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Controls] || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Credits]
            || WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            eventSystems.enabled = false;
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
    void SwitchEventSystems()
    {
        if (isClicked)
        {
            //scoreEventSystem.enabled = true;
            eventSystems.enabled = false;
            helpEventSystem.enabled = false;

        }
        else if (!isClicked)
        {
            //scoreEventSystem.enabled = false;
            eventSystems.enabled = true;
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
    void SwitchHelpEvents()
    {
        if (isHelpCkicked)
        {
            helpEventSystem.enabled = true;
            eventSystems.enabled = false;

        }
        else if (!isHelpCkicked)
        {
            helpEventSystem.enabled = false;
           
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
    void SwitchSaveEvents()
    {
        if (isSavedClicked)
        {
            saveEventSystem.enabled = true;
            eventSystems.enabled = false;

        }
        else if (!isSavedClicked)
        {
            saveEventSystem.enabled = false;

        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: NewGameButton, LoadGameButton, ProfileManagerButton, ControlsButton
    //                SettingsButton, CreditsButton, TopScoresButton, QuitButton
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: These functions will hold the information for the buttons in the 
    //          START MENU SCENE.
    // ------------------------------------------------------------------------------

    public void NewGameButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Tutorial].SceneIndex);
    }

    public void LoadGameButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.LoadGame].SceneIndex);

    }
    public void ControlsButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Controls].SceneIndex);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Credits].SceneIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
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
    public void Click()
    {
        isClicked = true;
    }
    public void ReturnClick()
    {
        isClicked = false;
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
    public void HelpClick()
    {
        isHelpCkicked = true;
    }
    public void ReturnHelpClick()
    {
        isHelpCkicked = false;
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
    }
} // End UI_MenuManager