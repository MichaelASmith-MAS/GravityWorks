/* -----------------------------------------------------------------------------------
 * Class Name: WLD_GravityLight
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

public class WLD_GravityLight : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    //public int rotateY;
    public ParticleSystem[] particles;
    public Light[] spotLights;

    //Added this for testing with wall 'lights'
    public Renderer rend;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    Color temp;   

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

    void Update()
    {
        ChangeLightColor();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.segment)
        {
            temp = WLD_GameController.gravityImages[other.GetComponent<WLD_SegmentController>().segmentGravity];

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

    void ChangeLightColor()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystem.MainModule main = particles[i].main;

            main.startColor = temp;
          
        }
        for (int i = 0; i < spotLights.Length; i++)
        {
            spotLights[i].color = temp;
        }

        //Added this for testing with wall 'lights'
        if (rend != null)
        {
            rend.material.color = temp;
            rend.material.SetColor("_EmissionColor", temp);
        }
    }
    
} // End WLD_GravityLight