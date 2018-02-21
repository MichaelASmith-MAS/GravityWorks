/* -----------------------------------------------------------------------------------
 * Class Name: UNA_HashIDs
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;

public class UNA_HashIDs : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public int hitTrigger;
    public int firingTrigger;
    public int moveBool;
    public int deathBool;

    public int firingState;
    public int deathState;
    public int hitState;
    public int idleState;
    public int walkState;
    public int blankState;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------



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

    private void Awake()
    {
        hitTrigger = Animator.StringToHash("Hit");
        moveBool = Animator.StringToHash("Moving");
        deathBool = Animator.StringToHash("Death");
        firingTrigger = Animator.StringToHash("Firing");

        deathState = Animator.StringToHash("Base Layer.Death");
        hitState = Animator.StringToHash("Base Layer.Hit");
        idleState = Animator.StringToHash("Base Layer.BigIdle");
        walkState = Animator.StringToHash("Base Layer.Walk");
        blankState = Animator.StringToHash("UpperBody.New State");
        firingState = Animator.StringToHash("UpperBody.mixamo_com");

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

} // End UNA_HashIDs