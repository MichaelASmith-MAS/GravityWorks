/* -----------------------------------------------------------------------------------
 * Class Name: UI_DmgCrackOverlay
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

public class UI_DmgCrackOverlay : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Image topLeftCrack;
    public Image bottomRightCrack;
    public Image topRightCrack;
    public Image bottomLeftCrack;
    public Image deathCrack;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    WLD_HealthDmg healthdmg;


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
        healthdmg = GetComponent<WLD_HealthDmg>();
    }
    void Update()
    {
        DamageOverlay();
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
    public void DamageOverlay()
    {
        if (healthdmg.Health <= 99)
        {
            if (healthdmg.Health >= 75)
            {
                topLeftCrack.enabled = true;

                bottomRightCrack.enabled = false;
                topRightCrack.enabled = false;
                bottomLeftCrack.enabled = false;
                deathCrack.enabled = false;
            }
            else if (healthdmg.Health >= 50 && healthdmg.Health <= 75)
            {
                bottomRightCrack.enabled = true;

                topRightCrack.enabled = false;
                bottomLeftCrack.enabled = false;
                deathCrack.enabled = false;
            }
            else if (healthdmg.Health >= 25 && healthdmg.Health <= 50)
            {
                topRightCrack.enabled = true;

                bottomLeftCrack.enabled = false;
                deathCrack.enabled = false;
            }
            else if (healthdmg.Health >= 10 && healthdmg.Health <= 25)
            {
                bottomLeftCrack.enabled = true;
            }
            else if (healthdmg.Health >= 1 && healthdmg.Health <= 10)
            {
                deathCrack.enabled = true;
            }
        }

        else
        {
            topLeftCrack.enabled = false;
            bottomRightCrack.enabled = false;
            topRightCrack.enabled = false;
            bottomLeftCrack.enabled = false;
            deathCrack.enabled = false;
        }
    }

} // End UI_DmgCrackOverlay