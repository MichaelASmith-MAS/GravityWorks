/* -----------------------------------------------------------------------------------
 * Class Name: UI_Score
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 10/10/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Projects the score data onto the UI.
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class UI_Score : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Slider energySlider;
    
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;


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
        player = WLD_GameController.player;
    }

    void Update()
    {
        CurrentPointsUI();

    }

    // Function Name: CurrentPointsUI
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/1/17
    // ------------------------------------------------------------------------------
    // Purpose: Takes the score from the PlR_poins script and adds the desired amount
    // ------------------------------------------------------------------------------
    void CurrentPointsUI()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name != "Main Menu" && scene.name != "Hub")
        {
            energySlider.value = player.GetComponent<PLR_Points>().OverallPoints;

            WLD_GameController.ui_Texts[UI_Txt.CurrentScore].text = player.GetComponent<PLR_Points>().OverallPoints.ToString();
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
   

} // End UI_Score