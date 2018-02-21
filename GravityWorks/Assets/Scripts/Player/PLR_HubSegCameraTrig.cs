/* -----------------------------------------------------------------------------------
 * Class Name: PLR_HubSegCameraTrig
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

public class PLR_HubSegCameraTrig : MonoBehaviour 
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
    void Start()
    {
        WLD_CameraFollow_SL.inTeleporter = false;

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
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            WLD_CameraFollow_SL.inTeleporter = true;
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            WLD_CameraFollow_SL.inTeleporter = false;
        }
    }

} // End PLR_HubSegCameraTrig