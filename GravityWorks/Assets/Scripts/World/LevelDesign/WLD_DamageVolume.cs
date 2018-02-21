/* -----------------------------------------------------------------------------------
 * Class Name: WLD_DamageVolume
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

public class WLD_DamageVolume : MonoBehaviour 
{

    #region VARIABLES
    public bool death = false;

    [SerializeField]
    bool reoccuringDamage = true;

    [SerializeField]
    float damagePerHit, timeToHit;

    WLD_HealthDmg wld_HealthDmg;
    WLD_DamageVolume[] allDamageVolumes;
    float curTime;
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
        wld_HealthDmg = WLD_GameController.player.GetComponent<WLD_HealthDmg>();
        allDamageVolumes = FindObjectsOfType<WLD_DamageVolume>();

	}//End Start	
	

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (other.GetComponent<WLD_HealthDmg>() != null && death == false)
            {
                other.GetComponent<WLD_HealthDmg>().ChangeHealth(-damagePerHit);

                if (other.tag == UNA_Tags.player && damagePerHit == 100)
                {
                    for (int i = 0; i < allDamageVolumes.Length; i++)
                    {
                        allDamageVolumes[i].death = true;
                    }
                }

            }

        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("GameObject " + other.name + " does not have a health damage script.", this);
            
        }

        //if (other.tag == UNA_Tags.player)
        //{
        //    wld_HealthDmg.ChangeHealth(-damagePerHit);

        //    if (!reoccuringDamage)
        //    {
        //        Destroy(gameObject);
        //    }
        //    else return;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.GetComponent<WLD_HealthDmg>() != null && death == false)
            {
                curTime += Time.deltaTime;

                if (curTime >= timeToHit)
                {
                    other.GetComponent<WLD_HealthDmg>().ChangeHealth(-damagePerHit);
                    curTime = 0;
                }
                
            }

        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("GameObject " + other.name + " does not have a health damage script.", this);

        }

        //    if (other.tag == UNA_Tags.player)
        //    {
        //        curTime += Time.deltaTime;

        //        if (curTime >= timeToHit)
        //        {
        //            wld_HealthDmg.ChangeHealth(-damagePerHit);
        //            curTime = 0;
        //        }
        //    }
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            if (other.GetComponent<WLD_HealthDmg>() != null)
            {
                curTime = 0;

            }

        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("GameObject " + other.name + " does not have a health damage script.", this);
            throw;
        }

        //if (other.tag == UNA_Tags.player)
        //{
        //    curTime = 0;
        //}
    }
    //End Update

}
// End WLD_DamageVolume