/* -----------------------------------------------------------------------------------
 * Class Name: WLD_Ice
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_Ice : MonoBehaviour 
{

    #region VARIABLES
    public GameObject heatVolume;
    public float maxTime = 3.0f;

    Vector3 originalScale, depressedFully;
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
	void Start () 
	{
        originalScale = transform.localScale;
        depressedFully = new Vector3(0, 0, transform.localScale.z);

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
	void Update () 
	{
        if (UNA_StaticVariables.currentHotTemp == 0)
        {
            StopAllCoroutines();

            if (transform.localScale != originalScale)
            {
                StartCoroutine(ScaleUp(maxTime));
            }

            else
            {
                StopAllCoroutines();
            }
        }

        if (heatVolume.activeSelf && UNA_StaticVariables.currentHotTemp > 0)
        {
            StartCoroutine(ScaleDown(maxTime));
        }

        //if (UNA_StaticVariables.currentHotTemp >= 1000)
        if (heatVolume.activeSelf && UNA_StaticVariables.currentHotTemp >= 1000)
        {
            Destroy(gameObject);
        }

    }
    //End Update

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
}
// End WLD_Ice