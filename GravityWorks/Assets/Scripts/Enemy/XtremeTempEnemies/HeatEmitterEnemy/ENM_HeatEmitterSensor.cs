/* -----------------------------------------------------------------------------------
 * Class Name: ENM_HeatEmitterSensor
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

public class ENM_HeatEmitterSensor : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("HeatEmitter GameObject")]
    public GameObject heatEmitter;
    public GameObject camLight;

    [Header("Detected Length")]
    public float playerDetectedLength;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    float timer = 0f;
    bool isSearching, isTriggered;
    float resetTimer = 0f;
    float startTemp = 0;
    Light cameraLight;


    Color triggered = Color.red;
    Color warning = Color.yellow;
    Color searching = Color.green;

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
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void Start()
    {
        isSearching = true;
        isTriggered = false;

        cameraLight = camLight.GetComponentInChildren<Light>();

        UNA_StaticVariables.isTempGaugeOn = false;
        UNA_StaticVariables.currentHotTemp = startTemp;
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons 
    // Date: 10/3/2017
    // ------------------------------------------------------------------------------
    // Purpose:
    // ------------------------------------------------------------------------------

    void Update()
    {
        LightChange();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            isSearching = false;
            isTriggered = true;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: Trigger Colliders
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: These will activate when the player enters the trigger zones
    // ------------------------------------------------------------------------------

    void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            camLight.GetComponent<Renderer>().material.color = warning;
            cameraLight.color = warning;

            timer += Time.deltaTime;

            if (timer >= playerDetectedLength && isTriggered)
            {
                camLight.GetComponent<Renderer>().material.color = triggered;
                cameraLight.color = triggered;

                heatEmitter.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            camLight.GetComponent<Renderer>().material.color = searching;
            cameraLight.color = searching;

            timer = resetTimer;
        }
    }
    void LightChange()
    {
        if (isSearching)
        {
            camLight.GetComponent<Renderer>().material.color = searching;
            cameraLight.color = searching;
        }
    }
} // End ENM_HeatEmitterSensor