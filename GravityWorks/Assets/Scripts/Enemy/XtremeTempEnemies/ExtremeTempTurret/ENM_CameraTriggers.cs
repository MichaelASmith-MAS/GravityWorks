using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_CameraTriggers : MonoBehaviour 
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
    private ENM_Camera cam;
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
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void Start()
    {
        cam = FindObjectOfType<ENM_Camera>();
        camLight = GameObject.Find("CamLight");
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
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
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: This function activates the trigger the before it's aware of the player
    // ------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == UNA_Tags.player)
        {
            cam.triggered = true;
            cam.suspicion = false;
            cam.reset = false;
            cam.timer = 0;
            cam.POV.enabled = true;
            //Debug.Log("Entering First");
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


} // End ENM_CameraTriggers