using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;


public class ENM_Chaser : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public bool chase = false;

    public bool countDown = false;

    public float Timer = 0;

    public float chaseLimit = 5;

    [SerializeField]
    float speed = 5;

    [SerializeField]
    float DamageDone;

    public bool dealingDamage = false;

    [SerializeField]
    float startSpeed = 1;

    [SerializeField]
    [Range(1,5)]
    float timeToDamage;

    public Transform[] points;

    private int wayPointIndex = 0;

    public GameObject Chaser;

    public Collider colOne;

    public Collider colTwo;

    public Transform target;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    Transform player;

    private Color angry = Color.red;

    private Color patrol = Color.gray;

    WLD_HealthDmg damage;

    private float damageTimer = 0;

    private float MinDist = 1.5f;

    private bool hit = false;

    private float startY;

    WLD_HealthDmg hp;

    private bool grounded = false;

    GameObject playerHealth;

    float playerHP;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

#region Functions
    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: Initializes
    // ------------------------------------------------------------------------------
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(UNA_Tags.player).transform;
        //target = ENM_WayPoints.waypoints[0];
        //damage = GameObject.FindGameObjectWithTag(UNA_Tags.player).GetComponent<WLD_HealthDmg>();
        startY = transform.position.y;
        hp = this.GetComponent<WLD_HealthDmg>();
        playerHealth = WLD_GameController.player;
        //playerHP = playerHealth.GetComponent<WLD_HealthDmg>().Health;
    }

    void CheckPlayerHealth()
    { 
        playerHP = playerHealth.GetComponent<WLD_HealthDmg>().Health;
        Debug.Log(playerHP);
            if (playerHP <= 0)
            {
                chase = false;
                Debug.Log("Dead");
                colOne.enabled = false;
                colTwo.enabled = false;
                StartCoroutine(deathDelay());
            }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void Update()
    {
        ChaserLogic();
        Death();
        //CheckPlayerHealth();
    }

    // ------------------------------------------------------------------------------
    // Function Name: ChaserLogic
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: This function controls the chaser logic 
    // ------------------------------------------------------------------------------
    public void ChaserLogic()
    {
        if (grounded) {
            if (chase == true)
            {
                Chase();
                CheckPlayerHealth();
                GetComponent<Renderer>().material.color = angry;
            } else
                {
                if (hit == false)
                    {
                        GetComponent<Renderer>().material.color = patrol;
                        Movement();
                    }
                }
        }else
            {
                Airtime();
            }

        if (countDown == true)
        {
            updateTimer();
        }

        if (Timer >= chaseLimit)
        {
            chase = false;
        }

        if (dealingDamage == true)
        {
            updateDamageTimer();
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Trigger
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: Triggers things
    // ------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            chase = true;
            countDown = false;
            Timer = 0;
            dealingDamage = true;
        }

        if(other.tag == UNA_Tags.wall)
        {
            chase = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            doDamage(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            countDown = true;
            dealingDamage = false;
        }
    }

    private void OnCollisionStay(Collision collider)
    {
        if(collider.collider.tag != UNA_Tags.wall)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collider)
    {
        if (collider.collider.tag != UNA_Tags.wall)
        {
           grounded = false;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Chase
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: When called, moves the enemy towards the player
    // ------------------------------------------------------------------------------

    private void Chase()
    {
        Vector3 toTarget = player.transform.position - transform.position;

        transform.LookAt(player);
        if(Vector3.Distance(transform.position, player.position) >= MinDist)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            //transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private void Airtime()
    {
        if (chase)
        {
            hit = true;
            chase = false;
            if (grounded)
            {
                chase = true;
                hit = false;
            }
        }
        else
        {
            if (grounded)
            {
                hit = false;
            }
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Movement
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons, Joseph Aranda
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Moves the object between waypoints.
    // ------------------------------------------------------------------------------

    void Movement()
    {
        transform.LookAt(points[wayPointIndex]);
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
        speed = startSpeed;
        target = points[wayPointIndex];
    }

    // ------------------------------------------------------------------------------
    // Function Name: GetNextWaypoint
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons, Joseph Aranda
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Grabs the next waypoint for the object to move to.
    // ------------------------------------------------------------------------------

    void GetNextWaypoint()
    {
        Vector3 newWayPoint = points[wayPointIndex].transform.position;
        if (wayPointIndex < points.Length-1)
        {
            wayPointIndex++;
        }
        else
        {
            wayPointIndex = 0;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: DeathTimer
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Destroys the game object upon death/when health reaches zero.
    // ------------------------------------------------------------------------------

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.09f);
        //Destroy(gameObject);
        Destroy(Chaser);
    }

    // ------------------------------------------------------------------------------
    // Function Name: Death
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Checks to see if the object's heath is at zero
    // ------------------------------------------------------------------------------

    private void Death()
    {
        if (hp.Health <= 0)
        {
            StartCoroutine(DeathTimer());
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: doDamage
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: When called, it will do damage to the player
    // ------------------------------------------------------------------------------

    public void doDamage()
    {
        if (damageTimer >= timeToDamage)
        {
            //damage.ChangeHealth(-DamageDone);
            playerHealth.GetComponent<WLD_HealthDmg>().ChangeHealth(DamageDone);
            damageTimer = 0;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: UpdateTimer
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: When called, it runs like a timer.
    // ------------------------------------------------------------------------------

    private void updateTimer()
    {
        Timer += Time.deltaTime;
    }

    // ------------------------------------------------------------------------------
    // Function Name: UpdateDamageTimer
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: When called, it runs a timer
    // ------------------------------------------------------------------------------

    private void updateDamageTimer()
    {
        damageTimer += Time.deltaTime;
    }

    IEnumerator deathDelay()
    {
        yield return new WaitForSeconds(1);
        colOne.enabled = true;
        colTwo.enabled = true;
    }

    // ------------------------------------------------------------------------------
    // Function Name: hitDelay
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Pauses the object's movement if hit by a player bullet.
    // ------------------------------------------------------------------------------

    IEnumerator hitDelay()
    {

        yield return new WaitForSeconds(1);
        hit = false;
        StopCoroutine(hitDelay());
    }

    #endregion
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------


} // End ENM_Chaser