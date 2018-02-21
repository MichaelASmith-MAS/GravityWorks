/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ColdSensorTrigger
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

public class ENM_ColdSensorTrigger : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject coldEmmiter;
    public GameObject coldParticles;
    public float coldSpeed = 5;
    public float startSpeed = 10;
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
    float time, startTime = 0;
    bool isExit;
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
        isExit = false;

        coldParticles.SetActive(false);
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
        DeathReset();
        ExitBufffer();
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
    void DeathReset()
    {
        if (player.GetComponent<WLD_HealthDmg>().Health <= 0)
        {
            UNA_StaticVariables.isTempGaugeOn = false;
           
            UI_TemperatureGauge.isCold = false;

            WLD_GameController.player.GetComponent<PLR_CharacterMovement>().MovingSpeed = startSpeed;

            WLD_GameController.animators[Animations.FrostOverlay].Play("FrostIdle");

            player.GetComponent<PLR_Dash>().enabled = true;

            coldParticles.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider player)
    {        
        if (player.tag == UNA_Tags.player)
        {
            coldParticles.SetActive(true);

            WLD_GameController.animators[Animations.FrostOverlay].enabled = true;
            WLD_GameController.animators[Animations.FrostOverlay].SetTrigger("On");

            player.GetComponent<PLR_CharacterMovement>().moveSpeed = coldSpeed;
            player.GetComponent<PLR_Dash>().enabled = false;

            UNA_StaticVariables.isTempGaugeOn = true;
            UI_TemperatureGauge.isCold = true;
            isExit = false;

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
    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isExit = false;
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
    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isExit = true;
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
    void ExitBufffer()
    {
        if (isExit)
        {
            time += Time.deltaTime;
        }

        if (time >= 1)
        {
            WLD_GameController.player.GetComponent<PLR_CharacterMovement>().MovingSpeed = startSpeed;
            player.GetComponent<PLR_Dash>().enabled = true;

            WLD_GameController.animators[Animations.FrostOverlay].SetTrigger("Off");
            WLD_GameController.animators[Animations.FrostOverlay].SetTrigger("Idle");

            coldParticles.SetActive(false);

            UI_TemperatureGauge.isCold = false;

            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
            UNA_StaticVariables.isTempGaugeOn = false;

            coldEmmiter.SetActive(false);

            isExit = false;
            time = startTime;
        }

    }
  
} // End ENM_ColdSensorTrigger