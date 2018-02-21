/* -----------------------------------------------------------------------------------
 * Class Name: UI_LoadGames
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
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class UI_LoadGames : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public string[] savedGames;
    public Button loadGameButton, deleteGameButton, cancelButton;
    public RectTransform savedGamesTransform;
    public GameObject savedGamesPanelObject;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    string gameSelected;
    List<GameObject> savedGameButtons = new List<GameObject>();
    WLD_LevelLoader levelLoader;

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

    private void Start()
    {
        levelLoader = FindObjectOfType<WLD_LevelLoader>();

        LocateSavedGames();

        loadGameButton.onClick.AddListener(LoadSavedGame);
        deleteGameButton.onClick.AddListener(DeleteSavedGame);
        cancelButton.onClick.AddListener(CancelSelection);

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

    void LocateSavedGames ()
    {
        savedGames = Directory.GetFiles(Application.persistentDataPath + "/", "*.blr", SearchOption.TopDirectoryOnly);
        if (savedGames.Length != 0)
        {
            Debug.Log(savedGames.Length);

            gameSelected = savedGames[0];

            for (int i = 0; i < savedGames.Length; i++)
            {
                string saveGamePath = savedGames[i];
                string[] savedGame = savedGames[i].Split('/');

                string gameName = savedGame[savedGame.Length - 1];

                savedGame = gameName.Split('.');

                gameName = savedGame[0];

                GameObject tempButton = Instantiate(Resources.Load("Prefabs/UI_Prefabs/SavedGameButton")) as GameObject;

                tempButton.transform.SetParent(savedGamesTransform, false);

                tempButton.GetComponentInChildren<Text>().text = gameName;

                tempButton.GetComponent<Button>().onClick.AddListener(() => { gameSelected = saveGamePath; });

                savedGameButtons.Add(tempButton);

            }
        }

        //ScrollRect rect = GetComponentInChildren<ScrollRect>();
        //rect.SetLayoutVertical();

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

    public void LoadSavedGame ()
    {
        if (savedGames.Length != 0)
        {
            Dictionary<Scenes, UNA_Level> saveData;
            Dictionary<Gravity, bool> gravitySettings;
            bool[] endPieces;
            float playerHealth;
            int overallPoints;

            bool loadEffective = UNA_SaveLoadController.LoadPlayer(gameSelected, out saveData, out endPieces, out playerHealth, out overallPoints, out gravitySettings);

            if (loadEffective)
            {
                Debug.Log("Game " + gameSelected + " Loaded.");

                WLD_GameController.controller.BuildGameLevels(saveData);
                WLD_GameController.endPieces = endPieces;
                WLD_GameController.player.GetComponent<WLD_HealthDmg>().Health = playerHealth;
                WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints = overallPoints;
                WLD_GameController.gravitySettingDictionary = gravitySettings;

                levelLoader.LoadLevel(Scenes.Hub, new Vector3(0, 0));
            }

            else
            {
                Debug.LogError("The game could not be loaded.");

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

    public void DeleteSavedGame ()
    {
        if (savedGames.Length != 0)
        {
            File.Delete(gameSelected);

            for (int i = 0; i < savedGameButtons.Count; i++)
            {
                Destroy(savedGameButtons[i]);
                
            }

            savedGameButtons = new List<GameObject>();

            LocateSavedGames();

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

    public void CancelSelection ()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.MainMenu].SceneIndex);

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
} // End UI_LoadGames