/* -----------------------------------------------------------------------------------
 * Class Name: WLD_PressurePlate
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

public class WLD_PressurePlate : MonoBehaviour 
{

    #region VARIABLES
    [SerializeField]
    GameObject door;

    [SerializeField]
    bool vertical = false, risingAction = true, permanentlyDepressed = true;

    [SerializeField]
    float maxTime = 2f, doorScalingTime = 3;
    [SerializeField]
    [Tooltip("The y-scale value that you want the pressure plate to depress to.")]
    float destinationDepression = 0.25f;

    Vector3 originalScale, depressedFully;
    Vector3 doorOrgScale, doorFinalScale;
    //float curTime;
    bool completelyDepressed = false;
    float currentTime, adjDoorScalingTime = 0.75f;
    Renderer rend;
    Color orgColor;

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
        depressedFully = new Vector3(originalScale.x, destinationDepression, originalScale.z);
        doorOrgScale = door.transform.localScale;
        rend = GetComponent<Renderer>();
        orgColor = rend.material.color;

        if (vertical)
        {
            doorFinalScale = new Vector3(doorOrgScale.x, 0.25f, doorOrgScale.z);
        }
        else
        {
            doorFinalScale = new Vector3(0.25f, doorOrgScale.y, doorOrgScale.z);
        }
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
        if (transform.localScale.y <= destinationDepression + 0.05f)
        {
            completelyDepressed = true;
            rend.material.color = Color.black;
            StartCoroutine(ScaleDownDOOR(doorScalingTime));
            //door.SetActive(false);
        }

        if (!permanentlyDepressed && transform.localScale.y >= destinationDepression + 0.05f)
        {
            completelyDepressed = false;
            rend.material.color = orgColor;
            //door.SetActive(true);
        }
        
    }
    //End Update

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            StopAllCoroutines();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            StartCoroutine(ScaleDown(maxTime));

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            StopAllCoroutines();

            if (risingAction)
            {
                if (!permanentlyDepressed && !completelyDepressed)
                {
                    StartCoroutine(ScaleUp(maxTime));
                    StartCoroutine(ScaleUpDOOR(adjDoorScalingTime));
                }

                else if (!permanentlyDepressed && completelyDepressed)
                {
                    StartCoroutine(ScaleUp(maxTime));
                    StartCoroutine(ScaleUpDOOR(adjDoorScalingTime));
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

    IEnumerator ScaleDown (float time)
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

    IEnumerator ScaleDownDOOR(float time)
    {
        float curTime = 0.0f;
        Vector3 currentScale = door.transform.localScale;
        
        do
        {
            door.transform.localScale = Vector3.Lerp(currentScale, doorFinalScale, curTime / doorScalingTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        while (curTime <= time && completelyDepressed);
    }

    IEnumerator ScaleUpDOOR(float time)
    {
        float curTime = 0.0f;
        Vector3 currentScale = door.transform.localScale;

        do
        {
            door.transform.localScale = Vector3.Lerp(currentScale, doorOrgScale, curTime / adjDoorScalingTime);
            curTime += Time.deltaTime;
            yield return null;
        }
        while (curTime <= time);
    }

}
// End WLD_PressurePlate