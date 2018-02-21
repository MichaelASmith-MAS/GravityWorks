/* -----------------------------------------------------------------------------------
 * Class Name: CLT_GravityCollectEffect
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

public class CLT_GravityCollectEffect : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject gravityCollectedEffect;

    public Light constantLight;

    public float growSpeed = .5f;
    public float destroyEffectTime = 2.5f;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------



    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Update()
    {
        ChangeLightSize();

        Destroy(gravityCollectedEffect, destroyEffectTime);


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
    void ChangeLightSize()
    {
        if (constantLight.range <= 4)
        {
            constantLight.range += growSpeed;
        }
        
    }

} // End CLT_GravityCollectEffect