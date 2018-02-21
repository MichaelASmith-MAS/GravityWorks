/* -----------------------------------------------------------------------------------
 * Class Name: CLT_GravityTimePickUpEffect
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

public class CLT_GravityTimePickUpEffect : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Effects GameObjects")]
    public GameObject pickUpOff;
    public GameObject pickUpOn;
    public GameObject offHalo;
    public Light flash;

    [Header("Light Intensity")]
    public float lightFlashIntensity = 15;

    public float hologramGorwSpeed;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float effectTimer;
    float startTimer;
    float respawnTimer = 10;
    float startIntensity = 0, ringStartSize = 5;
    bool objectEnabled, isLightOn;
    Vector3 temp;


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
        effectTimer = 0;
        objectEnabled = true;

        temp = offHalo.transform.localScale;

       
    }
    void Update()
    {
        TurnBackOn();
        EnlargeHologram();
        StopAndResetFlash();
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
    void TurnBackOn()
    {
        if (!objectEnabled)
        {
            effectTimer += Time.deltaTime;

            if (effectTimer <= respawnTimer)
            {
                pickUpOn.SetActive(false);
                pickUpOff.SetActive(true);
            }
            else if (effectTimer >= respawnTimer)
            {
                effectTimer = startTimer;

                objectEnabled = true;
            }
        }
        else if (objectEnabled)
        {
            pickUpOn.SetActive(true);
            pickUpOff.SetActive(false);
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
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            objectEnabled = false;

            pickUpOff.SetActive(true);
            pickUpOn.SetActive(false);
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
    void EnlargeHologram()
    {
        if (!pickUpOn.activeSelf)
        {
            temp = offHalo.transform.localScale;

            if (temp.y <= 1.8f)
            {
                temp.y += effectTimer / effectTimer * hologramGorwSpeed * Time.deltaTime;
            }
            else
            {
                isLightOn = true;
            }
            offHalo.transform.localScale = temp;
        }

        else if (pickUpOn.activeSelf)
        {
            temp = offHalo.transform.localScale;

            temp.y = 0f;

            offHalo.transform.localScale = temp;


            if (isLightOn)
            {
                StartCoroutine(LightFlash());
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
    void StopAndResetFlash()
    {
        if (flash.intensity >= lightFlashIntensity)
        {
            isLightOn = false;

            StopCoroutine(LightFlash());

            flash.intensity = startIntensity;

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
    IEnumerator LightFlash()
    {
        flash.intensity += 1;

        yield return new WaitForSeconds(.01f);

    }
} // End CLT_GravityTimePickUpEffect