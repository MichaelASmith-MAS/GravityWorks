/* -----------------------------------------------------------------------------------
 * Class Name: UI_TutorialEventSystem
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

public class UI_TutorialEventSystem : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Button firstSkipButton;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    public static bool isTutOn ;
    EventSystem playTutorialEventSystem;
    EventSystem optionEventSystem;
   
    EventSystem gameControllerEventSystems;

    
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
        playTutorialEventSystem = GameObject.Find("PlayTutorialEventSystem").GetComponent<EventSystem>();
        optionEventSystem = GameObject.Find("OptionEventSystem").GetComponent<EventSystem>();
        gameControllerEventSystems = GameObject.Find("GameControllerEventSystem").GetComponent<EventSystem>();
        isTutOn = true;

        firstSkipButton.interactable = false;

        gameControllerEventSystems.enabled = false;
    }
   void Update()
    {
        if (optionEventSystem.enabled)
        {
            gameControllerEventSystems.enabled = false;
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
   
    public void FirstTutClick()
    {
        playTutorialEventSystem.enabled = false;
        optionEventSystem.enabled = true;
        
    }
    public void SecTutClick()
    {
        isTutOn = false;
        optionEventSystem.enabled = false;
        gameControllerEventSystems.enabled = true;
    }

    public void SkipTutorial()
    {
        optionEventSystem.enabled = false;
        gameControllerEventSystems.enabled = true;
        isTutOn = false;
    }
} // End UI_TutorialEventSystem