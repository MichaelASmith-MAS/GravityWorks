/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ColdEffectTrigger
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

public class ENM_ColdEffectPressurePanel : MonoBehaviour
{
    #region VARIABLES
    [SerializeField]
    GameObject coldEmmiter;

    [SerializeField]
    bool risingAction = true, permanentlyDepressed = true;

    [SerializeField]
    float maxTime = 2f;
    [SerializeField]
    [TooltipAttribute("The y-scale value that you want the pressure plate to depress to.")]
    float destinationDepression = 0.25f;

    Vector3 originalScale, depressedFully;
    //float curTime;
    bool completelyDepressed = false, isStart = false;

    #endregion

    #region GETTERS/SETTERS

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    /* ------------------------------------------------------------------------------
    * Function Name: Start
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Used to initialize variables or perform startup processes
    * ------------------------------------------------------------------------------
    */
    void Start()
    {
        originalScale = transform.localScale;
        depressedFully = new Vector3(originalScale.x, destinationDepression, originalScale.z);
    }
    //End Start	

    /* ------------------------------------------------------------------------------
    * Function Name: Update
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Runs each frame. Used to perform frame based checks and actions.
    * ------------------------------------------------------------------------------
    */
    void Update()
    {
        if (transform.localScale.y <= destinationDepression + 0.05f)
        {
            completelyDepressed = true;
            permanentlyDepressed = false;
            
            isStart = true;
        }

        if (!permanentlyDepressed && transform.localScale.y >= destinationDepression + 0.05f)
        {
            completelyDepressed = false;
            isStart = false;
        }

        if (isStart)
        {
            coldEmmiter.SetActive(true);
        }
    }
    //End Update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            StopAllCoroutines();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            StartCoroutine(ScaleDown(maxTime));

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            //Debug.Log("exit");
            isStart = false;
            StopAllCoroutines();

            if (risingAction)
            {
                if (!permanentlyDepressed && !completelyDepressed)
                {
                    StartCoroutine(ScaleUp(maxTime));
                }

                else if (!permanentlyDepressed && completelyDepressed)
                {
                    StartCoroutine(ScaleUp(maxTime));
                }

                else if (permanentlyDepressed && !completelyDepressed)
                {
                    StartCoroutine(ScaleUp(maxTime));
                }

                else if (permanentlyDepressed && completelyDepressed)
                {
                    return;
                }
            }

            else
            {
                return;
            }
        }
    }

    IEnumerator ScaleDown(float time)
    {
        float curTime = 0.0f;
        Vector3 currentScale = transform.localScale;

        do
        {
            this.transform.localScale = Vector3.Lerp(currentScale, depressedFully, curTime / maxTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        while (curTime <= time);
    }

    IEnumerator ScaleUp(float time)
    {
        float curTime = 0.0f;
        Vector3 currentScale = transform.localScale;

        do
        {
            this.transform.localScale = Vector3.Lerp(currentScale, originalScale, curTime / maxTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        while (curTime <= time);
    }
} // End ENM_ColdEffectTrigger