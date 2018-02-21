using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_ShootTrigger : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Light light;
    public GameObject gunMain;
    public float distance;
    public LayerMask mask;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private ENM_Shoot shoot;
    //private Transform target;
    private Color follow = Color.red;
    private Color idle = Color.gray;
    public GameObject sLight;
    private ENM_ShooterLookAt look;
    RaycastHit ray;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

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
    // Purpose: 
    // ------------------------------------------------------------------------------

    private void Start()
    {
        shoot = GetComponentInParent<ENM_Shoot>();
        //target = WLD_GameController.player.transform;
        sLight = transform.GetChild(0).gameObject;
        sLight.GetComponent<Renderer>().material.color = idle;
        look = GetComponent<ENM_ShooterLookAt>();

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
        TriggerShoot();

    }

    // ------------------------------------------------------------------------------
    // Function Name: Trigger
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == UNA_Tags.player)
    //    {
    //        player = other.gameObject;

    //        shoot.shooting = true;
    //        sLight.GetComponent<Renderer>().material.color = follow;
    //        look.triggered = true;
    //        light.enabled = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == UNA_Tags.player)
    //    {
    //        shoot.shooting = false;
    //        sLight.GetComponent<Renderer>().material.color = idle;
    //        look.triggered = false;
    //        light.enabled = false;
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

    void TriggerShoot ()
    {
        //Debug.DrawRay(transform.position, gunMain.transform.forward, Color.red);

        if (Physics.Raycast(transform.position, gunMain.transform.forward, out ray, distance))
        {
            if (ray.collider.tag == UNA_Tags.player)
            {
                shoot.shooting = true;
                sLight.GetComponent<Renderer>().material.color = follow;
                look.triggered = true;
                light.enabled = true;
                
            }
            else
            {
                shoot.shooting = false;
                sLight.GetComponent<Renderer>().material.color = idle;
                look.triggered = false;
                light.enabled = false;

            }
        }
    }

} // End ENM_ShootTrigger