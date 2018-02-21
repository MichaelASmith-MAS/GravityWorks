/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ExplodeContactDestroy
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 11/29/2017
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_ExplodeContactDestroy : MonoBehaviour 
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

    private void OnCollisionEnter(Collision collision)
    {
        if (/*collision.gameObject.tag == UNA_Tags.player*/ collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            GetComponent<WLD_HealthDmg>().ChangeHealth(-(GetComponent<WLD_HealthDmg>().Health));
        }

    }

} // End ENM_ExplodeContactDestroy