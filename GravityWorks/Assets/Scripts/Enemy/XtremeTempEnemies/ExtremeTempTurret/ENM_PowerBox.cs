/* -----------------------------------------------------------------------------------
 * Class Name: ENM_PowerBox
 * -----------------------------------------------------------------------------------
 * Author: Kayci Lyons
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_PowerBox : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public SphereCollider ExTempEnemyCollider;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    ENM_Camera cam;
    ENM_ExTemp exTemp;
    private Color off = Color.red;
    private Color on = Color.green;
    private bool interactButtonEnabled = true;

    public GameObject bLight;

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
    private void Start()
    {
        cam = FindObjectOfType < ENM_Camera >();
        exTemp = GameObject.FindGameObjectWithTag(UNA_Tags.XTempEnemy).GetComponent<ENM_ExTemp>();
        bLight = GameObject.Find("BoxLight");
        bLight.GetComponent<Renderer>().material.color = on;

    }

    private void Update()
    {
        SetInteractButtonEnabled();
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
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetAxis("Interact") > 0 && interactButtonEnabled)
        {
            cam.powered = false;

            ExTempEnemyCollider.enabled = false;

            exTemp.lineRenderer.enabled = false;

            bLight.GetComponent<Renderer>().material.color = off;

            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
            UNA_StaticVariables.isTempGaugeOn = false;

            //Debug.Log("Powered Down");
        }
    }

    void SetInteractButtonEnabled()
    {
        if (Input.GetAxis("Interact") > 0)
        {
            interactButtonEnabled = false;
        }
        if (Input.GetAxis("Interact") == 0)
        {
            interactButtonEnabled = true;
        }
    }


} // End ENM_PowerBox