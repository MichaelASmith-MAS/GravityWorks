/* -----------------------------------------------------------------------------------
 * Class Name: UI_SaveGame
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_SaveGame : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    //[Header("Save Notification")]
    //public Text saveGameNotification;

    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Save Game Panel")]
    public GameObject saveGamePanel;

    [Header("Save Button Panel")]
    public GameObject saveButtonPanel;

    
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    string gameToSave = "";
    [SerializeField] float saveGameTimer = 2f;
    float timer;
    PLR_CharacterMovement plr_CharacterMovement;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    private void Awake()
    {
        timer = 0f;
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

    private void Update()
    {
        //if (saveGameNotification.IsActive())
        //{
        //    if (timer >= saveGameTimer)
        //    {
        //        saveGameNotification.enabled = false;

        //    }

        //}

        if (plr_CharacterMovement == null)
        {
            plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();
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

    public void OpenSaveGamePanel ()
    {
        if (!saveGamePanel.activeSelf)
        {
            saveGamePanel.SetActive(true);

            pausePanel.SetActive(false);

            RegisterSaveGames();

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

    public void CancelLoad ()
    {
        saveGamePanel.SetActive(false);

        if (!plr_CharacterMovement.GETisInTeleporter && Input.GetAxis("Interact") == 0)
        {
            pausePanel.SetActive(true);
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

    public void SaveGame ()
    {
        if (gameToSave != "")
        {
            UNA_SaveLoadController.ChangeProfileName(gameToSave);
        }

        UNA_SaveLoadController.SavePlayer(WLD_GameController.levels, WLD_GameController.endPieces, WLD_GameController.player.GetComponent<WLD_HealthDmg>().Health, WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints, WLD_GameController.gravitySettingDictionary);

        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = true; 

        CancelLoad();

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

    void RegisterSaveGames ()
    {
        string[] savedGames = System.IO.Directory.GetFiles(Application.persistentDataPath + "/", "*.blr", System.IO.SearchOption.TopDirectoryOnly);

        Button[] savedGamesButtons = saveButtonPanel.GetComponentsInChildren<Button>();

        gameToSave = "";

        for (int i = 0; i < savedGamesButtons.Length; i++)
        {
            try
            {
                string[] savedGame = savedGames[i].Split('/');

                string gameName = savedGame[savedGame.Length - 1];

                savedGame = gameName.Split('.');

                gameName = savedGame[0];

                savedGamesButtons[i].GetComponentInChildren<Text>().text = gameName;

                savedGamesButtons[i].onClick.AddListener(() => { gameToSave = gameName; SaveGame(); });

            }
            catch (System.IndexOutOfRangeException)
            {
                savedGamesButtons[i].GetComponentInChildren<Text>().text = "Empty Save";

                string gameName = "Game Save " + (i + 1);

                savedGamesButtons[i].onClick.AddListener(() => { gameToSave = gameName; SaveGame(); });

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

} // End UI_SaveGame