/* -----------------------------------------------------------------------------------
 * Class Name: UI_TempTest
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 10/4/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: This controls the Tempurature Gauge
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UI_TemperatureGauge : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Temperature rise speed")]
    public float tempRise;
    [Header("Warning Temp Activated")]
    public float dmgTemp;
    [Header("Rate of Player Damage")]
    public float rateOfDamage;
    [Header("Amount of Heat Damage")]
    public float heatDamage;
    public float coldDamage;

    public Text extremeTempText;

    //public UI_DamagePanel ui_DamagePanel;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private static float timer;
    public static bool isCold;
    public static bool isHeat;
    private float startingTemp = 1000;
    
    private Color beginHot = Color.white;
    private Color endHot = Color.red;
    private Color beginHealth = Color.green;
    private Color endHealth = Color.red;

    Color b = Color.blue;
    Color begin = Color.white;
    Color end = Color.blue;
    GameObject player;
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
    //void OnEnable()
    //{
    //    UI_DamagePanel.Inizialize();
    //}
    void Start()
    {
        UNA_StaticVariables.currentHotTemp = 0;

        player = WLD_GameController.player;

        isCold = false;

    }
    void Update()
    {
        PlayerInput();
        ColdRaisingTemp();
        ExtremeTempWarning();
        TakeDamage();
        ColdTakeDamage();
    }
    
    /* ------------------------------------------------------------------------------
* Function Name: PlayerInput()
* Return types: N/A
* Argument types: N/A
* Author: Joseph Aranda
* Date Created: 10/02/2017
* Last Updated: 10/02/2017
* ------------------------------------------------------------------------------
* Purpose: A temperary placeholder till triggers are made
* ------------------------------------------------------------------------------
*/
    void PlayerInput()
    {
        
        if (UNA_StaticVariables.isTempGaugeOn)
        {
            if (UNA_StaticVariables.currentHotTemp <= 999)
            {
                WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(true);


                RisingTemperature();
            }
        }
        else
        {
            if (!UNA_StaticVariables.isTempGaugeOn)
            {
                if (UNA_StaticVariables.currentHotTemp >= 1)
                {
                    WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(true);

                    LoweringTemperature();
                }
            }
        }
        
    }
    /* ------------------------------------------------------------------------------
* Function Name: RisingTemperature
* Return types: N/A
* Argument types: N/A
* Author: Joseph Aranda
* Date Created: 10/02/2017
* Last Updated: 10/02/2017
* ------------------------------------------------------------------------------
* Purpose: Increases the temp bar
* ------------------------------------------------------------------------------
*/
    public void RisingTemperature()
    {
        UNA_StaticVariables.currentHotTemp += tempRise;

        WLD_GameController.ui_Images[UI_Images.FillHot].fillAmount = UNA_StaticVariables.currentHotTemp / startingTemp;

        if (!isCold)
        {
            isHeat = true;
            WLD_GameController.ui_Images[UI_Images.FillHot].color = Color.Lerp(beginHot, endHot, UNA_StaticVariables.currentHotTemp / startingTemp);

            extremeTempText.color = endHot;
            extremeTempText.text = "WARNING: DAMAGE IMMINENT - EXTREME TEMP.";
        }
    }

    void ColdRaisingTemp()
    {
        if (isCold)
        {
            //coldEffect.SetActive(true);

            WLD_GameController.ui_Images[UI_Images.FillHot].color = Color.Lerp(begin, end, UNA_StaticVariables.currentHotTemp / 1000);

            extremeTempText.color = b;
            extremeTempText.text = "EXTREME COLD CONDITIONS IMMINENT";

            isHeat = false;

        }   
        
    }
  
    /* ------------------------------------------------------------------------------
* Function Name: LoweringTemperature
* Return types: N/A
* Argument types: N/A
* Author: Joseph Aranda
* Date Created: 10/02/2017
* Last Updated: 10/02/2017
* ------------------------------------------------------------------------------
* Purpose: Decreases the temp bar
* ------------------------------------------------------------------------------
*/
    void LoweringTemperature()
    {
        UNA_StaticVariables.currentHotTemp -= tempRise;

        WLD_GameController.ui_Images[UI_Images.FillHot].fillAmount = UNA_StaticVariables.currentHotTemp / startingTemp;
        WLD_GameController.ui_Images[UI_Images.FillHot].color = Color.Lerp(beginHot, endHot, UNA_StaticVariables.currentHotTemp / startingTemp);

        if (!isCold && !isHeat)
        {
            WLD_GameController.ui_Images[UI_Images.FillHot].color = Color.Lerp(begin, end, UNA_StaticVariables.currentHotTemp / startingTemp);
        }

    }

    /* ------------------------------------------------------------------------------
  * Function Name: ExtremeTempWarning
  * Return types: N/A
  * Argument types: N/A
  * Author: JS
  * Date Created: 10/02/2017
  * Last Updated: 10/02/2017
  * ------------------------------------------------------------------------------
  * Purpose: Add-on to TempUpdate to set the UI Warning for extreme temperatures active / inactive
  * ------------------------------------------------------------------------------
  */

    void ExtremeTempWarning()
    {
        if (UNA_StaticVariables.currentHotTemp > dmgTemp)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.ExtremeTempWarning].SetActive(true);
        }
        else
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.ExtremeTempWarning].SetActive(false);
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: TakeDamage()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: Controls the player taking damage over time
    // ------------------------------------------------------------------------------
    void TakeDamage()
    {
        if (!isCold)
        {
            if (UNA_StaticVariables.currentHotTemp >= 1000)
            {
                timer += Time.deltaTime;

                if (timer >= rateOfDamage)
                {
                    player.GetComponent<WLD_HealthDmg>().ChangeHealth(heatDamage);

                    player.GetComponent<WLD_HealthDmg>().ChangeHealthBar();

                    timer = 0;
                }
            }
            else
            {
                timer = 0f;
            }
        }
      
    }
    void ColdTakeDamage()
    {
        if (isCold)
        {
            if (UNA_StaticVariables.currentHotTemp >= 1000)
            {
                timer += Time.deltaTime;

                if (timer >= rateOfDamage)
                {
                    player.GetComponent<WLD_HealthDmg>().ChangeHealth(coldDamage);

                    player.GetComponent<WLD_HealthDmg>().ChangeHealthBar();

                    timer = 0;
                }
            }
            else
            {
                timer = 0f;
            }
        }
    }
} // End UI_TempTest