/* -----------------------------------------------------------------------------------
 * Class Name: UI_HubEventSystems
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UI_HubEventSystems : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public EventSystem hub_LLP;
    public EventSystem segmentEventSystems;
    public EventSystem kLshopEventSystem, jAShopEventSystem, jSShopEventSystem, finalShopEventSystem, helpEventSystem;
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
        EventSwitch();
        ShopSegmentSwitch();
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
    void EventSwitch()
    {
        if (UI_HubPause.isPaused)
        {
            segmentEventSystems.enabled = false;
            kLshopEventSystem.enabled = false;
            jAShopEventSystem.enabled = false;
            jSShopEventSystem.enabled = false;
            finalShopEventSystem.enabled = false;
        }
        if (!UI_HubPause.isPaused)
        {
            if (WLD_Teleporter.isInTeleporter)
            {
                segmentEventSystems.enabled = true;
            }
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
    void ShopSegmentSwitch()
    {
        if (segmentEventSystems.enabled)
        {
            hub_LLP.enabled = false;

            kLshopEventSystem.enabled = false;
            jAShopEventSystem.enabled = false;
            jSShopEventSystem.enabled = false;
            finalShopEventSystem.enabled = false;
          
        }
        else
        {
            if (kLshopEventSystem.enabled || jAShopEventSystem.enabled || jSShopEventSystem.enabled || finalShopEventSystem.enabled)
            {
                segmentEventSystems.enabled = false;
            }
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
    public void HelpEventSwitch()
    {
        UI_HubPause.isPaused = false;

        hub_LLP.enabled = false;

        helpEventSystem.enabled = true;
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
    public void HelpCancel()
    {
        UI_HubPause.isPaused = true;
        helpEventSystem.enabled = false;
        hub_LLP.enabled = true;
    }

} // End UI_HubEventSystems