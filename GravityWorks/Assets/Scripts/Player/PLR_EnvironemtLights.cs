/* -----------------------------------------------------------------------------------
 * Class Name: PLR_EnvironemtLights
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PLR_EnvironemtLights : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Light attenaSpotLight;
    public Light environmentSpotLight;
    public ParticleSystem attenaParticle;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;
    int startRange = 2;
    float timer;
    float startTiner;
    bool isOn = true;

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
        //player = WLD_GameController.player;
    }
    void Update()
    {
        ChangeColor();
        Blink();
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
    void ChangeColor()
    {    
        environmentSpotLight.color = WLD_GameController.gravityImages[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        attenaSpotLight.color = WLD_GameController.gravityImages[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        attenaParticle.startColor = WLD_GameController.gravityImages[GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];

    }

   void Blink()
    {
        timer += Time.deltaTime;

        if (timer <= 1)
        {
            attenaSpotLight.enabled = false;         
        }
        else if (timer >= 1)
        {
            attenaSpotLight.enabled = true;          
        }
        if (timer >= 2)
        {
            timer = startTiner;
        }
    }
} // End PLR_EnvironemtLights