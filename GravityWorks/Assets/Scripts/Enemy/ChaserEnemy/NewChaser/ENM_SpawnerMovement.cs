/* -----------------------------------------------------------------------------------
 * Class Name: ENM_SpawnerMovement
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

public class ENM_SpawnerMovement : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("WayPoints")]
    public Transform[] points;
    public Transform firstWayPoint;

    public GameObject newChaserEnemy;

    [Header("Movement Speed")]
    public float speed = 2;
    public float startSpeed = 2;

    [Header("Back to patrol Time")]
    public float stopChasingTime = 5;

    [Header("Enemy Damage")]
    public int damageDone = 10;
    public float attackRate = 2;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    int wayPointIndex = 0;
    float timer, startTimer, attackTime, startAttackTime;

    Color returning = Color.blue;
    Color chase = Color.red;
    Color patrol = Color.green;

    GameObject player;
    Rigidbody rb;

    Transform chaseTarget, wayPointZero, wayPointOne;


    public bool isStopChasing, isInRange, isBackToPatrol;
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
        chaseTarget = GameObject.Find("EnemyTarget").GetComponent<Transform>();

    }

    void Start()
    {
        isStopChasing = true;
        isInRange = false;
        isBackToPatrol = false;

        player = WLD_GameController.player;

        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        BackToPatrol();
        Patrol();
        Chase();
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
        }
    }
    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isStopChasing = false;
            isInRange = true;
        }
    }
    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            isBackToPatrol = true;

            newChaserEnemy.GetComponent<Renderer>().material.color = returning;

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
    void BackToPatrol()
    {
        if (isBackToPatrol)
        {
            timer += Time.deltaTime;

            if (timer >= stopChasingTime)
            {
                rb.isKinematic = false;

                isStopChasing = true;
                isInRange = false;
                isBackToPatrol = false;

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
    void Chase()
    {
        if (isInRange)
        {
            rb.isKinematic = true;

            newChaserEnemy.GetComponent<Renderer>().material.color = chase;

            speed = 5;

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
    void Patrol()
    {
        if (!isInRange && isStopChasing)
        {
            newChaserEnemy.GetComponent<Renderer>().material.color = patrol;

            speed = startSpeed;

            Vector3 dir = firstWayPoint.position - transform.position;

            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, firstWayPoint.position) <= 0.2f)
            {
                GetNextWaypoint();
            }

            firstWayPoint = points[wayPointIndex];
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
    void GetNextWaypoint()
    {
        if (wayPointIndex < points.Length - 1)
        {
            wayPointIndex++;
        }
        else
        {
            wayPointIndex = 0;
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
} // End ENM_SpawnerMovement