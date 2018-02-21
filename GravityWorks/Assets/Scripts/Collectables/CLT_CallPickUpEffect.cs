/* -----------------------------------------------------------------------------------
 * Class Name: CLT_CallPickUpEffect
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

public class CLT_CallPickUpEffect : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject gravityCollectedEffect;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    Color32 mars = new Color32(0, 255, 128, 255);
    Color32 ceres = new Color32(255, 255, 204, 255);
    Color32 jupiter = new Color32(102, 0, 102, 255);
    Color32 neptune = new Color32(0, 0, 102, 255);
    Color32 pluto = new Color32(204, 255, 153, 255);
    Color32 uranus = new Color32(0, 102, 254, 255);


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
    void OnTriggerEnter(Collider planet)
    {
        if (planet.tag == UNA_Tags.player)
        {
            if (gameObject.tag != UNA_Tags.pickUpFireRate && gameObject.tag != UNA_Tags.pickUpGravEffTime)
            {
                GameObject gravityEffect = Instantiate(gravityCollectedEffect, transform.position, transform.rotation);

                switch (this.gameObject.tag)
                {
                    case UNA_Tags.mars:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = mars;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = mars;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = mars;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = mars;

                        break;
                    case UNA_Tags.venus:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = ceres;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = ceres;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = ceres;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = ceres;
                        break;
                    case UNA_Tags.uranus:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = uranus;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = uranus;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = uranus;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = uranus;
                        break;
                    case UNA_Tags.neptune:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = neptune;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = neptune;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = neptune;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = neptune;
                        break;
                    case UNA_Tags.jupiter:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = jupiter;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = jupiter;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = jupiter;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = jupiter;
                        break;
                    case UNA_Tags.pluto:
                        gravityEffect.GetComponent<ParticleSystem>().startColor = pluto;
                        gravityEffect.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = pluto;

                        gravityEffect.transform.GetChild(1).GetComponent<Light>().color = pluto;
                        gravityEffect.transform.GetChild(2).GetComponent<Light>().color = pluto;
                        break;
                }



                Destroy(gameObject, 0.01f);

            }
        }
    }

} // End CLT_CallPickUpEffect