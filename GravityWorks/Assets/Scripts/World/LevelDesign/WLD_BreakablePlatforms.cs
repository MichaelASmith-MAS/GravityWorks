/* -----------------------------------------------------------------------------------
 * Class Name: WLD_BreakablePlatforms
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
using System.Linq;

public class WLD_BreakablePlatforms : MonoBehaviour 
{

    #region VARIABLES
    [Tooltip("The levels of Gravity in between the current Gravity & the Gravity Gun Setting that will break through the platform.")]
    [Range(0, 7)]    [SerializeField]
    int differnceInGravityNeeded = 3;

    [SerializeField]
    bool topCollision = true;

    GameObject player;
    List<float> gravityFigures = new List<float>();
    float downwardBreaking, upwardBreaking;
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
        Invoke("OnStart", 0.1f);
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

	}
    //End Update

    private void OnCollisionStay(Collision collision)
    {
        //Only run if the object has an individualGravity script attached
        if (collision.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            GRV_IndividualGravity grv_IndividualGravity = collision.gameObject.GetComponent<GRV_IndividualGravity>();

            //Runs a for loop to find the element in the gravityFigures list
            for (int i = 0; i < gravityFigures.Count; i++)
            {
                //Once it finds the gravity figure associated with the segmentGravity of the individualGravity script
                if (gravityFigures[i] == grv_IndividualGravity.SegmentGravity)
                {
                    if (i - differnceInGravityNeeded > 0)
                    {
                        upwardBreaking = gravityFigures[i - differnceInGravityNeeded];
                    }
                    else    //Set to 0 if there isn't a gravity that is within the above range
                    {
                        upwardBreaking = 0;
                    }

                    if (i + differnceInGravityNeeded <= gravityFigures.Count - 1)
                    {
                        downwardBreaking = gravityFigures[i + differnceInGravityNeeded];
                    }
                    else    //Set just outside of gGun settings if a gravity isn't within the range above.
                    {
                        downwardBreaking = gravityFigures[gravityFigures.Count - 1] + 1;
                    }
                    //Debug.Log("Found a match and it is: " + gravityFigures[i] + " | downwardBreaking is: " + downwardBreaking + " | upwardBreaking is: " + upwardBreaking);
                }
            }
            
            //Destroying the platform from ABOVE
            if (topCollision && grv_IndividualGravity.GunGravity >= downwardBreaking && grv_IndividualGravity.GunGravity != 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }

            //Destroying the platform from BELOW
            else if (!topCollision && grv_IndividualGravity.GunGravity <= upwardBreaking && grv_IndividualGravity.GunGravity != 0)
            {
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }

    void OnStart()
    {
        player = WLD_GameController.player;
        gravityFigures.Add(0.3f);   //Ceres
        gravityFigures.Add(0.7f);   //Pluto
        gravityFigures.Add(1.6f);   //Moon
        gravityFigures.Add(3.7f);   //Mars
        gravityFigures.Add(8.7f);   //Uranus
        gravityFigures.Add(9.8f);   //Earth
        gravityFigures.Add(11f);    //Neptune
        gravityFigures.Add(23.1f);  //Jupiter

        //print("| Ceres is: " + gravityFigures[0] + "| Pluto is: " + gravityFigures [1] + "| Moon is: " + gravityFigures[2] + 
        //    "| Mars is: " + gravityFigures[3] + "| Uranus is: " + gravityFigures[4] + "| Earth is: " + gravityFigures[5] + 
        //    "| Neptune is: " + gravityFigures[6] + "| Jupiter is: " + gravityFigures[7] + 
        //    "| \n The total number in this list is: " + gravityFigures.Count);
    }
}
// End WLD_BreakablePlatforms