/* -----------------------------------------------------------------------------------
 * Class Name: WLD_FallingPlatforms
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

public class WLD_FallingPlatforms : MonoBehaviour 
{

    #region VARIABLES
    [SerializeField] float timeTillFall = 0.25f;
    float destroyTime;
    Rigidbody rb;
    GRV_IndividualGravity grv_IndividualGravity;
    #endregion

    #region GETTERS/SETTERS

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    /* ------------------------------------------------------------------------------
    * Function Name: 
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: 
    * ------------------------------------------------------------------------------
    */

    /* ------------------------------------------------------------------------------
    * Function Name: Start
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: 
    * ------------------------------------------------------------------------------
    */

    private void Start()
    {
        destroyTime = timeTillFall * 10;
        rb = this.GetComponent<Rigidbody>();
        grv_IndividualGravity = this.GetComponent<GRV_IndividualGravity>();

        rb.isKinematic = true;
        grv_IndividualGravity.enabled = false;
    }

    /* ------------------------------------------------------------------------------
    * Function Name: OnCollisionEnter
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Used to initialize variables or perform startup processes
    * ------------------------------------------------------------------------------
    */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == UNA_Tags.player)
        {
            Invoke("StartFalling", timeTillFall);
            //Destroy(gameObject, destroyTime);
        }

        //else if (collision.gameObject.tag == UNA_Tags.wall)
        //{
        //    Destroy(gameObject);
        //}

    }
    //End OnCollisionEnter	

    void StartFalling()
    {
        rb.isKinematic = false;
        grv_IndividualGravity.enabled = true;
    }

}
// End WLD_FallingPlatforms