/* -----------------------------------------------------------------------------------
 * Class Name: UI_MainMenuButtonManager
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UI_MainMenuButtonManager : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Buttons")]
    public Button loadButton;

    [Header("GameObjects")]
    public GameObject placeHolderPanel;
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject developerText;

    [Header("EventSystems")]
    public EventSystem eventSystem;
    public EventSystem developerEventSystem;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    WLD_LevelLoader loader;
    //WLD_Teleporter teleporter;
    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Start()
    {
        if (loadButton != null)
        {
            string[] savedGames = System.IO.Directory.GetFiles(Application.persistentDataPath + "/", "*.blr", System.IO.SearchOption.TopDirectoryOnly);

            if (savedGames.Length > 0)
            {
                loadButton.interactable = true;
            }
            else
            {
                loadButton.interactable = false;
            }
        }

        loader = FindObjectOfType<WLD_LevelLoader>();
        //teleporter = FindObjectOfType<WLD_Teleporter>();
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
    public void LevelMenuOn()
    {
        mainMenu.SetActive(false);
        levelMenu.SetActive(true);
        developerText.SetActive(false);

        eventSystem.enabled = false;
        developerEventSystem.enabled = true;
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
    public void LevelMenuOff()
    {
        mainMenu.SetActive(true);
        levelMenu.SetActive(false);
        developerText.SetActive(true);

        eventSystem.enabled = true;
        developerEventSystem.enabled = false;

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
    public void Hub()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.Hub, Vector3.zero);
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
    public void Dw_0713()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.DW_0713, Vector3.zero);
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
    public void JS_1021()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.JS_1021, Vector3.zero);
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
    public void JA0629()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.JA_0629, Vector3.zero);
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
    public void KL_0602()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.KL_0602, Vector3.zero);
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
    public void Final()
    {
        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();

        loader.LoadLevel(Scenes.Final, Vector3.zero);
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
    public void NewGameButton()
    {
        //SceneManager.LoadScene(WLD_GameController.levels[Scenes.Tutorial].SceneIndex);

        if (placeHolderPanel != null)
        {
            placeHolderPanel.SetActive(false);
        }

        Dictionary<Scenes, UNA_Level> temp = null;

        WLD_GameController.controller.BuildGameLevels(temp);
        WLD_GameController.player.GetComponent<WLD_HealthDmg>().ChangeHealth(100);
        WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = 0;
        WLD_GameController.player.GetComponent<PLR_Points>().ResetToZero();
        WLD_GameController.endPieces = null;
        WLD_GameController.AddGravitySettings(null);

        loader.LoadLevel(Scenes.Tutorial, Vector3.zero);

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

} // End UI_MainMenuButtonManager