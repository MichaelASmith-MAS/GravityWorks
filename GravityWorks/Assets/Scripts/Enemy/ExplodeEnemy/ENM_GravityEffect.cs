﻿/* -----------------------------------------------------------------------------------
 * Class Name: ENM_GravityEffect
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

public class ENM_GravityEffect : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Rigidbody rb;


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
        if (other.tag == UNA_Tags.bullet)
        {
            rb.isKinematic = false;
        }
    }
} // End ENM_GravityEffect