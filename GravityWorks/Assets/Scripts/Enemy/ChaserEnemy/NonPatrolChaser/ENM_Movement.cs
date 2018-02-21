/* -----------------------------------------------------------------------------------
 * Class Name: ENM_Movement
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

public class ENM_Movement : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject newChaseEnemy;

    [Header("Chaser GameObject")]
    public GameObject chaserEnemy;

    [Header("Start Position")]
    public Transform startPosition;

    [Header("Movement Speed")]
    public float speed = 5;

    [Header("Back to patrol Time")]
    public float stopChasingTime = 5;

    public bool isInRange;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float timer, attackTime, startAttackTime, startTimer;

    Color returning = Color.blue;
    Color chase = Color.red;
    Color patrol = Color.green;

    Rigidbody rb;
    WLD_HealthDmg wld_healthDmg;
    Transform chaseTarget, wayPointZero, wayPointOne;
   

    public bool isStopChasing, isBackToPatrol;
    #endregion

    #region Getters/Setters

    public bool Range
    {
        get { return isInRange; }
    }


    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Awake()
    {
        chaseTarget = GameObject.Find("EnemyTarget").GetComponent<Transform>();
    }

    void Start()
    {
        isStopChasing = true;
        isInRange = false;
        isBackToPatrol = false;

        rb = GetComponent<Rigidbody>();
        wld_healthDmg = GetComponent<WLD_HealthDmg>();
    }
    void Update()
    {
        BackToPatrol();
        Chase();
        Death();
        Patrol();
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
    void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isStopChasing = false;
            isInRange = true;

            timer = startTimer;
        }
        if (player.tag == UNA_Tags.invertGravity)
        {
            rb.isKinematic = true;
        }

    }
    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isStopChasing = false;
            isInRange = true;
            rb.isKinematic = false;
        }
        if (player.tag == UNA_Tags.invertGravity)
        {
            rb.isKinematic = true;
        }
    }
    //void OnTriggerExit(Collider player)
    //{
       
    //    if (player.tag == UNA_Tags.invertGravity)
    //    {
    //        rb.isKinematic = true;
    //    }
    //}
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    public void BackToPatrol()
    {
        if (isBackToPatrol)
        {
            timer += Time.deltaTime;

            if (timer >= stopChasingTime)
            {
                newChaseEnemy.GetComponent<Renderer>().material.color = patrol;

                isStopChasing = true;
                isInRange = false;
                rb.isKinematic = true;

                chaserEnemy.transform.position = startPosition.position;

                timer = startTimer;

                isBackToPatrol = false;

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
    void Patrol()
    {
        if (!isInRange)
        {
            newChaseEnemy.GetComponent<Renderer>().material.color = patrol;
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
    void Chase()
    {
        if (isInRange)
        {
            newChaseEnemy.GetComponent<Renderer>().material.color = chase;

            Vector3 dir = chaseTarget.position - transform.position;

            float distancethisframe = speed * Time.deltaTime;

            transform.Translate(dir.normalized * distancethisframe, Space.World);

            transform.LookAt(chaseTarget);
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

    void Death()
    {
        if (wld_healthDmg.Health <= 0)
        {
            Destroy(gameObject, .5f);
        }
    }

} // End ENM_Movement