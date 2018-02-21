using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_ShooterLookAt : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public bool triggered = false;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private Transform target;


    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start & Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void Start()
    {
        //target = WLD_GameController.player.transform;
    }

    private void Update()
    {
        if (triggered)
        {
            //LookAt();
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: LookAt
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons  
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: Rotates the enemy to look at the player
    // ------------------------------------------------------------------------------
    public void LookAt()
    {
        if (target != null)
        {
            Vector3 difference = target.position - transform.position;
            float rotationY = (Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
        }
    }

} // End ShooterLookAt