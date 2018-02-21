/* -----------------------------------------------------------------------------------
 * Class Name: CLT_RemoveGravityPickup
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

public class CLT_RemoveGravityPickup : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public bool[] planets;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject temp;
    GameObject tempOne;
    GameObject tempTwo;
    GameObject tempThree;
    GameObject tempFour;
    GameObject tempFive;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Awake()
    {
        planets[0] = UNA_StaticVariables.isJupiter;
        planets[1] = UNA_StaticVariables.isCeres;
        planets[2] = UNA_StaticVariables.isMars;
        planets[3] = UNA_StaticVariables.isNeptune;
        planets[4] = UNA_StaticVariables.isUranus;
        planets[5] = UNA_StaticVariables.isPluto;      
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

    void Update()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i])
            {
                if (planets[i] == planets[0])
                {
                    temp = GameObject.Find("JupiterPickUP");
                    Destroy(temp);
                }
                if (planets[i] == planets[1])
                {
                    tempOne = GameObject.Find("CeresPickUp");
                    Destroy(tempOne);
                }
                if (planets[i] == planets[2])
                {
                    tempTwo = GameObject.Find("MarsPickUP");
                    Destroy(tempTwo);
                }
                if (planets[i] == planets[3])
                {
                    tempThree = GameObject.Find("NeptunePickUP");
                    Destroy(tempThree);
                }
                if (planets[i] == planets[4])
                {
                    tempFour = GameObject.Find("MoonPickUP");
                    Destroy(tempFour);
                }
                if (planets[i] == planets[5])
                {
                    tempFive = GameObject.Find("PlutoPickUP");
                    Destroy(tempFive);
                }
            }
        }
    }
        

    } // End CLT_RemoveGravityPickup