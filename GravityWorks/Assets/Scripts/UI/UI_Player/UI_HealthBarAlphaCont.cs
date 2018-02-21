/* -----------------------------------------------------------------------------------
 * Class Name: UI_PLayerHealthBar
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_HealthBarAlphaCont : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public CanvasGroup healthBarPanel;
    public WLD_HealthDmg healthScript;
    //public GameObject shooterParent;
   
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
        HealthBarState();
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
    void HealthBarState()
    {
        if (healthScript != null)
        {
            if (healthScript.Health >= healthScript.MaxHealth)
            {
                healthBarPanel.alpha = 0;
            }
            else
            {
                healthBarPanel.alpha = 1;
            }

        }

    }
   
} // End UI_PLayerHealthBar