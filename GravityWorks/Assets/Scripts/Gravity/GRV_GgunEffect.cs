/* -----------------------------------------------------------------------------------
 * Class Name: GRV_GgunEffect
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GRV_GgunEffect : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    //ParticleSystem subEmitterOne;

    Renderer childOne, childTwo;
    

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
        //subEmitterOne = transform.GetChild(0).GetComponent<ParticleSystem>();

        childOne = transform.GetChild(0).GetComponent<Renderer>();
        childTwo = transform.GetChild(1).GetComponent<Renderer>();
        
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

    public void ApplyProperties (Gravity gravitySetting, float timeAlive)
    {
        Color effectColor = WLD_GameController.gravityImages[gravitySetting];

        effectColor.a = .25f;

        //ParticleSystem.MainModule mainMain = GetComponent<ParticleSystem>().main;
        //ParticleSystem.MainModule subMain = subEmitterOne.main;

        //mainMain.startColor = effectColor;
        //subMain.startColor = effectColor;
        //Debug.Log("Apply Gravity: " + gravitySetting.ToString() + "\nColor: " + effectColor.ToString());
        

        childOne.material.SetColor("_TintColor", effectColor);
        childTwo.material.SetColor("_TintColor", effectColor);

        //childOne.material.color = effectColor;
        //childTwo.material.color = effectColor;

        Destroy(gameObject, timeAlive);

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

} // End GRV_GgunEffect