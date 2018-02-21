/* -----------------------------------------------------------------------------------
 * Class Name: ENM_GoInvisible
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

public class ENM_GoInvisible : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public ENM_Movement enm_Movement;
    public Rigidbody rb;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    MeshRenderer meshRender;
   
    
    float timer, startTimer;
    public static bool isHit;
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
    }

    //void Update()
    //{
    //    BackToNormal();
    //}
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == UNA_Tags.bullet)
        {
            rb.isKinematic = false;

            StartCoroutine(RestartAfterBullet());
        }
        if (player.gameObject.tag == UNA_Tags.wall)
        {
            rb.isKinematic = false;
        }
    }
    void OnCollisionStay(Collision player)
    {
        if (player.gameObject.tag == UNA_Tags.wall)
        {
            rb.isKinematic = false;
        }
    }
    void OnCollisionExit(Collision player)
    {
        if (player.gameObject.tag == UNA_Tags.wall)
        {
            rb.isKinematic = true;
        }
    }
   
    IEnumerator RestartAfterBullet()
    {
        yield return new WaitForSeconds(4);

        rb.isKinematic = true;

        StopCoroutine(RestartAfterBullet());

    }
} // End ENM_GoInvisible