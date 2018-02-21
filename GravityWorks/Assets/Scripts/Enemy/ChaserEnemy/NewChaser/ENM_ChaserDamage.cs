/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ChaserDamage
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

public class ENM_ChaserDamage : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Enemy Damage")]
    public int damageDone = 10;
    public float attackRate = 2;
    public Rigidbody chaserRb;
    public ENM_Movement enm_move;
 
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float attackTime, startAttackTime;
    GameObject player;
    
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
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.bullet)
        {
            chaserRb.isKinematic = false;
            enm_move.isInRange = false;

            StartCoroutine(EnableIsInRange());
        }
    }
    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {        
            HitTarget();
            enm_move.enabled = false;
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            StartCoroutine(EnableMove());

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
    IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(.25f);

        enm_move.enabled = true;

        StopCoroutine(EnableMove());
    }
    IEnumerator EnableIsInRange()
    {
        yield return new WaitForSeconds(2f);

        enm_move.isInRange = true;

        StopCoroutine(EnableMove());
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
    void HitTarget()
    {
        attackTime += Time.deltaTime;

        if (attackTime >= attackRate)
        {
            player.GetComponent<WLD_HealthDmg>().ChangeHealth(damageDone);
            attackTime = startAttackTime;
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
} // End ENM_ChaserDamage