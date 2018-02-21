/* -----------------------------------------------------------------------------------
 * Class Name: ENM_ExTemp
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Controls the extreme Temp Enemy
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ENM_ExTemp : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Movement & TurnSpeed")]
    public float range = 0;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private string playerTag = "Player";

    public GameObject test;
    public Transform partToRotate;
    public LineRenderer lineRenderer;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private bool useLaser;
    private PLR_CharacterMovement targetPlayer;
    private Transform target;
    public Transform firePoint;

    //---------------------------
    //Testing Code
    //---------------------------
    public GameObject bullet;

    [SerializeField]
    [Range(0, 1)]
    float delay = 1.0f;

    public bool shooting = false;

    private float Timer;

    #endregion

    #region Getters/Setters

    public float Range
    {
        get { return range; }
    }

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
  
    void Start()
    {
        useLaser = false;
    }

    void Update()
    {
        //if (UNA_StaticVariables.currentHotTemp <= 0)
        //{
        //    WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
        //}
        Shooting();
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
    void OnTriggerStay(Collider player)
    {
        if (player.gameObject.tag == UNA_Tags.player)
        {
            InvokeRepeating("UpdateTarget", 0f, .5f);

            PlayerTracker();

            //Debug.Log(UNA_StaticVariables.isTempGaugeOn);

            //StartCoroutine(IsTempGaugeOn());
        }
    }

    
    IEnumerator IsTempGaugeOn()
    {
        if (target)
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(true);
            UNA_StaticVariables.isTempGaugeOn = true;
        }
        yield return null;
    }

    void PlayerTracker()
    {
        if (target == null)
        {
            

            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;           
                }
            }
            return;
        }


        TargetLockOn();
    }
    // ------------------------------------------------------------------------------
    // Function Name: UpdateTarget
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/5/17
    // ------------------------------------------------------------------------------
    // Purpose: Checks if there is a player within range, if there is  it will update the players array
    // ------------------------------------------------------------------------------

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }
        
        if (nearestPlayer != null && shortestDistance < range)
        {
            target = nearestPlayer.transform;

            targetPlayer = nearestPlayer.GetComponent<PLR_CharacterMovement>();

            useLaser = true;
        }
        else
        {
            target = null;
            shooting = false;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: TargetLockOn
    // Return types: NA
    // Argument types: NA
    // Author: Jospeh Aranda
    // Date: 10/5/17
    // ------------------------------------------------------------------------------
    // Purpose: Changes the transform of the turret to follow the current target that is within range
    // ------------------------------------------------------------------------------
    void TargetLockOn()
    {
        //targetLockOn
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);

        if (useLaser)
        {
            //Laser();
            //fire.Shooting();
            shooting = true;
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
    void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        StartCoroutine(IsTempGaugeOn());

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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
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
    public void Shot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    public void FireRate()
    {
        if (Timer >= delay)
        {
            Shot();
            Timer = 0;
        }
    }

    public void Shooting()
    {
        if (shooting == true)
        {
            FireRate();
            Timer += Time.deltaTime;
        }
    }

} // End ENM_ExTemp