/* -----------------------------------------------------------------------------------
 * Class Name: CLT_EMPTYSCRIPT
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CLT_DestroyPrefab: MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public float respawnTimer = 10f;
   
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------


    bool objectEnabled;
    float startTimer = 0;
    float timer;
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

    private void Start()
    {
        timer = 0;
        objectEnabled = true;


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

    private void Update()
    {
        if (!objectEnabled)
        {
            timer += Time.deltaTime;

            if (timer >= respawnTimer)
            {
                //Debug.Log("Reset object " + gameObject.name);
                ResetPickupObjects(true);
                timer = startTimer;
            }
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

    void OnTriggerEnter(Collider planet)
    {
        if (planet.tag == UNA_Tags.player)
        {
            if (gameObject.tag != UNA_Tags.pickUpFireRate && gameObject.tag != UNA_Tags.pickUpGravEffTime)
            {          
                Destroy(gameObject, 0.01f);
            }

            else
            {
                if (objectEnabled)
                {
                    ResetPickupObjects(false);
                }
            }
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

    void ResetPickupObjects(bool setting)
    {
        gameObject.GetComponent<Collider>().enabled = setting;

        for (int i = 0; i < GetComponentsInChildren<Renderer>().Length; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().enabled = setting;

        }

        objectEnabled = setting;

        timer = 0;
        
    }

} // End CLT_EMPTYSCRIPT