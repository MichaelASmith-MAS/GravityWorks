/* -----------------------------------------------------------------------------------
 * Class Name: ENM_TriggerExit
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
using System.Collections;
using System.Collections.Generic;

public class ENM_PowerBoxTrigger : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject coldEmmiter;
    public ParticleSystem blinkingParticle;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    ParticleSystem coldEffect;
    
    Image coldEffectOverlay;
    GameObject player;

    int minParticles = 0;
    bool  interactButtonEnabled = true;
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
        coldEffect = GameObject.Find("ColdEffect").GetComponent<ParticleSystem>();
        coldEffectOverlay = GameObject.Find("ColdDamageOverlay").GetComponent<Image>();

        player = WLD_GameController.player;
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
    void Update()
    {
        TurnOffUI();
        SetInteractButtonEnabled();
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
    void TurnOffUI()
    {
        if (UNA_StaticVariables.currentHotTemp <= 0)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
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
   
    void OnTriggerEnter()
    {
        if (Input.GetAxis("Interact") > 0 && interactButtonEnabled)
        {
            TurnOff();
        }
    }
    void OnTriggerStay()
    {
        if (Input.GetAxis("Interact") > 0 && interactButtonEnabled)
        {
            TurnOff();
        }
    }
  
    void TurnOff()
    {
        player.GetComponent<PLR_CharacterMovement>().MovingSpeed = 10;
        blinkingParticle.maxParticles = minParticles;
        coldEffect.maxParticles = minParticles;
        coldEffectOverlay.enabled = false;
        UI_TemperatureGauge.isCold = false;

        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
        UNA_StaticVariables.isTempGaugeOn = false;

        coldEmmiter.SetActive(false);
    }

    void SetInteractButtonEnabled()
    {
        if (Input.GetAxis("Interact") > 0)
        {
            interactButtonEnabled = false;
        }
        if (Input.GetAxis("Interact") == 0)
        {
            interactButtonEnabled = true;
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

} // End ENM_TriggerExit