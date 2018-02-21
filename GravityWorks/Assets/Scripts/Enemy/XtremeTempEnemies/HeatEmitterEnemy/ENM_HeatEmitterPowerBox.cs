/* -----------------------------------------------------------------------------------
 * Class Name: TempPowerBox
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

public class ENM_HeatEmitterPowerBox : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("GameObjects")]
    public GameObject heatEmitter;
    public GameObject lineRenderer;

    [Header("Renderers")]
    public Renderer powerBoxLightColor;

    [Header("Light")]
    public Light powerBoxLightOn;

    [Header("BoxCollider")]
    public GameObject cameraTrigger;
    public BoxCollider heatEmitterCollider;

    [Header("Materials")]
    public Material powerBoxOnMat;
    public Material powerBoxOffMat;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    ENM_HeatEmitterCamera cam;
    private Color offColor = Color.red;
    private Color onColor = Color.green;
    GameObject player;

    float startTemp = 0;
    bool interactButtonEnabled = true;
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
        cam = FindObjectOfType<ENM_HeatEmitterCamera>();

        powerBoxLightColor.GetComponent<Renderer>().material = powerBoxOnMat;

        player = WLD_GameController.player;
    }
    void Update()
    {
        TempGaugePanelOFF();
        DeathReset();
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
    void TempGaugePanelOFF()
    {
        if (UNA_StaticVariables.currentHotTemp <= 0)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);

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
    void DeathReset()
    {
        if (player.GetComponent<WLD_HealthDmg>().Health <= 0)
        {
            heatEmitter.SetActive(false);
            UNA_StaticVariables.isTempGaugeOn = false;
            UNA_StaticVariables.currentHotTemp = startTemp;
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
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player && Input.GetAxis("Interact") > 0 && interactButtonEnabled)
        {
            TurnOffAndReset();
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
    void TurnOffAndReset()
    {
        heatEmitter.SetActive(false);

        heatEmitterCollider.enabled = false;
        cameraTrigger.SetActive(false);

        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
        UNA_StaticVariables.isTempGaugeOn = false;

        powerBoxLightColor.GetComponent<Renderer>().material = powerBoxOffMat;
        
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
} // End TempPowerBox