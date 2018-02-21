/* -----------------------------------------------------------------------------------
 * Class Name: TempTrigger
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_HeatEmitterCameraTrigger : MonoBehaviour 
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
    private ENM_HeatEmitterCamera cam;

    private GameObject camLight;
    private Color agressive = Color.red;
    private Color suspicion = Color.yellow;
    private Color searching = Color.green;


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
    void Start()
    {
        cam = FindObjectOfType<ENM_HeatEmitterCamera>();
        camLight = GameObject.Find("CamLight");
    }

    private void Update()
    {
        LightChange();
    }

    void LightChange()
    {
        if (cam.triggered == true && cam.suspicion == false)
        {
            camLight.GetComponent<Renderer>().material.color = agressive;
        }
        else if (cam.triggered == false && cam.suspicion == true)
        {
            camLight.GetComponent<Renderer>().material.color = suspicion;
        }
        else if (cam.triggered == false && cam.suspicion == false)
        {
            camLight.GetComponent<Renderer>().material.color = searching;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Trigger
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: This function activates the trigger the before it's aware of the player
    // ------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            cam.triggered = true;
            cam.suspicion = false;
            cam.reset = false;
            cam.timer = 0;
            cam.POV.enabled = true;
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


} // End TempTrigger