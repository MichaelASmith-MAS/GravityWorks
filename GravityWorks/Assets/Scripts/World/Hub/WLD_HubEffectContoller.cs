/* -----------------------------------------------------------------------------------
 * Class Name: WLD_HubEffectContoller
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 11/16/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WLD_HubEffectContoller : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject[] onTeleporters;
    public GameObject[] offTeleporters;
    
    public Light[] lights;
    public ParticleSystem[] particleSystems;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    public static bool isKlOn = false, isJaOn = false, isJsON = false, isFinal = false;

    float offR = 200f / 255f;
    float offG = 0f / 255f;
    float offB = 0f / 255f;
    float offA = 25f / 255f;
    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------\
    void Update()
    {
        TeleporterSwitch();
        JATeleporterSwitch();
        JSTeleporterSwitch();
        //FinalTeleporterSwitch();
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
    void TeleporterSwitch()
    {
        if (!WLD_GameController.levels[Scenes.KL_0602].LevelUnlocked)
        {
            lights[0].color = new Color(offR, offG, offB, offA);
            particleSystems[0].enableEmission = true;
            offTeleporters[0].SetActive(true);
            onTeleporters[0].SetActive(false);
        }
        else
        {
            //if (isKlOn)
            //{
            lights[0].enabled = false;
            //particleSystems[0].enableEmission = false;
            offTeleporters[0].SetActive(false);
            onTeleporters[0].SetActive(true);
            //}
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
    void JATeleporterSwitch()
    {
        if (!WLD_GameController.levels[Scenes.JA_0629].LevelUnlocked)
        {
            lights[1].color = new Color(offR, offG, offB, offA);
            particleSystems[1].enableEmission = true;
            offTeleporters[1].SetActive(true);
            onTeleporters[1].SetActive(false);
        }
        else
        {
            //if (isJaOn)
            //{
            lights[1].enabled = false;
            //particleSystems[1].enableEmission = false;
            offTeleporters[1].SetActive(false);
            onTeleporters[1].SetActive(true);
            //}
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
    void JSTeleporterSwitch()
    {
        if (!WLD_GameController.levels[Scenes.JS_1021].LevelUnlocked)
        {
            lights[2].color = new Color(offR, offG, offB, offA);
            particleSystems[2].enableEmission = true;
            offTeleporters[2].SetActive(true);
            onTeleporters[2].SetActive(false);
        }
        else
        {
            //if (isJsON)
            //{
            lights[2].enabled = false;
            //particleSystems[2].enableEmission = false;
            offTeleporters[2].SetActive(false);
            onTeleporters[2].SetActive(true);
            //}
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
    //void FinalTeleporterSwitch()
    //{
    //    if (!isFinal)
    //    {
    //        lights[3].color = new Color(offR, offG, offB, offA);
    //        particleSystems[3].enableEmission = true;
    //        offTeleporters[3].SetActive(true);
    //        onTeleporters[3].SetActive(false);
    //    }
    //    else
    //    {
    //        if (isFinal)
    //        {
    //            lights[3].enabled = false;
    //            //particleSystems[3].enableEmission = false;
    //            offTeleporters[3].SetActive(false);
    //            onTeleporters[3].SetActive(true);
    //        }
    //    }
    //}
} // End WLD_HubEffectContoller