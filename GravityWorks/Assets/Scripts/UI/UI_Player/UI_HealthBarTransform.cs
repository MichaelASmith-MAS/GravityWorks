/* -----------------------------------------------------------------------------------
 * Class Name: UI_HealthBarTransform
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

public class UI_HealthBarTransform : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public RectTransform healthBarPanelGO;
    public Transform belowPlayer;
    public Transform abovePlayer;


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
   
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.invertGravity)
        {
            healthBarPanelGO.transform.localPosition = belowPlayer.transform.localPosition;
        }

        if (other.tag == UNA_Tags.wall)
        {
            healthBarPanelGO.transform.localPosition = belowPlayer.transform.localPosition;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == UNA_Tags.invertGravity)
        {
            healthBarPanelGO.transform.localPosition = abovePlayer.transform.localPosition;
        }
        if (other.tag == UNA_Tags.wall)
        {
            healthBarPanelGO.transform.localPosition = abovePlayer.transform.localPosition;
        }
    }
   
} // End UI_HealthBarTransform