/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ChaserRestart
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

public class ENM_ChaserRestart : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Chaser GameObject")]
    public GameObject chaserEnemy;

    [Header("Start Position")]
    public Transform startPosition;

    [Header("Out of Bounds Max Time")]
    public float outOfBoundsMaxTime = 5;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float timer, startTimer = 0;
    bool isOutOfBounds;

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
        isOutOfBounds = false;
    }
    void Update()
    {
        Restart();
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
    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isOutOfBounds = true;
        }
    }

    void Restart()
    {
        if (isOutOfBounds)
        {
            timer += Time.deltaTime;
            if (timer >= outOfBoundsMaxTime)
            {
                chaserEnemy.transform.position = startPosition.position;
                isOutOfBounds = false;
                timer = startTimer;
            }
        }
    }
} // End ENM_ChaserRestart