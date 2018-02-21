/* -----------------------------------------------------------------------------------
 * Class Name: WLD_TextureManipulation
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

public class WLD_TextureManipulation : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Renderer cubeRender;
  
    public float speed = .001f;

    public UI_UIButtonManager ui_BM;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float _dissolveAmmountTemp, _dissolveAmmountTempTwo, resetAmount = 0;
    float baseSpeed = .01f, resetBaseSpeed = .01f;
    GameObject player;
    #endregion

    #region Getters/Setters
    public float DissolveAmountTemp
    {
        get { return _dissolveAmmountTemp; }
        set { _dissolveAmmountTemp = value; }
    }
    

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Awake()
    {
        _dissolveAmmountTemp = cubeRender.material.GetFloat("_DissolveAmount");
    }
    void Start()
    {
        player = WLD_GameController.player;     
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
   
    void Update()
    {
        BurnEffect();
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

    void BurnEffect()
    {
        if (UNA_StaticVariables.isTempGaugeOn && !UI_TemperatureGauge.isCold && !UI_Pause.isPaused)
        {
            if (UNA_StaticVariables.currentHotTemp >= 1000)
            {
                if (_dissolveAmmountTemp <= 1)
                {
                    _dissolveAmmountTemp = baseSpeed += speed;
                }

                cubeRender.material.SetFloat("_DissolveAmount", _dissolveAmmountTemp);

            }
        }
        else if (!UNA_StaticVariables.isTempGaugeOn)
        {
            ResetBurnEffect();
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

    public void ResetBurnEffect()
    {
        if (player.GetComponent<WLD_HealthDmg>().Health >= 100)
        {
            baseSpeed = resetBaseSpeed;
            _dissolveAmmountTemp = resetAmount;
            cubeRender.material.SetFloat("_DissolveAmount", _dissolveAmmountTempTwo);
        }
        else if (ui_BM.IsRestarted)
        {
            baseSpeed = resetBaseSpeed;
            _dissolveAmmountTemp = resetAmount;
            cubeRender.material.SetFloat("_DissolveAmount", _dissolveAmmountTempTwo);
        }    
    }
   
} // End WLD_TextureManipulation