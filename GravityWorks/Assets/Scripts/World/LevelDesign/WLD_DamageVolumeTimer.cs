/* -----------------------------------------------------------------------------------
 * Class Name: WLD_DamageVolumeTimer
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


[RequireComponent(typeof (WLD_DamageVolume))]
[RequireComponent(typeof (MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class WLD_DamageVolumeTimer : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public float blinkTimer = 0.5f;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    float runningTimer;
    MeshRenderer meshRenderer;
    WLD_DamageVolume damageVolume;
    Collider deathCollider;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 12/05/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs first during scene start; collects component references
    // ------------------------------------------------------------------------------

    private void Start()
    {
        runningTimer = 0f;

        meshRenderer = GetComponent<MeshRenderer>();
        damageVolume = GetComponent<WLD_DamageVolume>();
        deathCollider = GetComponent<Collider>();

    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 12/05/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs every frame; increments timer; checks for timer completion to call
    //          TurnOffComponents function
    // ------------------------------------------------------------------------------

    private void Update()
    {
        runningTimer += Time.deltaTime;

        if (runningTimer >= blinkTimer)
        {
            TurnOffComponents();
            runningTimer = 0f;

        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: TurnOffComponents
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 12/05/2017
    // ------------------------------------------------------------------------------
    // Purpose: Turns off specified components
    // ------------------------------------------------------------------------------

    void TurnOffComponents ()
    {
        meshRenderer.enabled = !meshRenderer.enabled;
        damageVolume.enabled = !damageVolume.enabled;
        deathCollider.enabled = !deathCollider.enabled;

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
} // End WLD_DamageVolumeTimer