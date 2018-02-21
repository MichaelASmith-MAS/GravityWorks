/* -----------------------------------------------------------------------------------
 * Class Name: UI_gGunCoolDown
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
using UnityEngine.UI;


public class UI_gGunCoolDown : MonoBehaviour 
{

    #region VARIABLES
    public Slider slider;

    public static GameObject bulletSpawnLocation;

    private PLR_Shoot shootScript;
    private UNA_Level scene;

    public ParticleSystem powerUpParticles;
    public Light powerUpLights;
    #endregion

    #region GETTERS/SETTERS

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    private void Awake()
    {
        bulletSpawnLocation = GameObject.Find("BulletSpawnLocation");
        shootScript = bulletSpawnLocation.GetComponent<PLR_Shoot>();
    }

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
        scene = WLD_GameController.activeLevel;

        //powerUpParticles.startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
        //powerUpLights.color = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];

    }//End Start	
	
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
        if (scene != WLD_GameController.activeLevel)
        {
            powerUpParticles.startSize = 0f;
            powerUpLights.intensity = 0f;

           scene = WLD_GameController.activeLevel;

        }

        if(shootScript.GETcurTime < shootScript.GETfireRate)
        {
            powerUpParticles.startSize = shootScript.GETcurTime / shootScript.GETfireRate * 2;
            powerUpLights.intensity = shootScript.GETcurTime / shootScript.GETfireRate * 2;

            powerUpParticles.startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
            powerUpLights.color = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
        }
	}
//End Update
}
// End UI_gGunCoolDown