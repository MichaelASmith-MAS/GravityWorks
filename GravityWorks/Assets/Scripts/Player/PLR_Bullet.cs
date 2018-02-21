using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PLR_Bullet : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [SerializeField]
    [Range(1, 25)]
    float MoveSpeed = 10.0f;

    [SerializeField] float effectTime = 1f;

    [SerializeField] float alteredEffectTime;

    [SerializeField] Gravity gravitySetting = Gravity.Earth;

    [SerializeField] float directionalForce = 0f;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    UI_UIButtonManager gravitySettingHud;


    #endregion

    #region Getters/Setters

    public float GETeffectTime
    {
        get { return effectTime; }
        set { effectTime = value; }
    }

    public Gravity GravitySetting
    {
        get { return gravitySetting; }
        set { gravitySetting = value; }
    }

    public float DirectionalForce
    {
        get { return directionalForce; }
        set { directionalForce = value; }
    }

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: N/A
    // Argument types: N/A
    // Author: 
    // Date: 9-27-17
    // ------------------------------------------------------------------------------
    // Purpose: Calls the HUD
    // ------------------------------------------------------------------------------

    private void Start()
    {
        gravitySettingHud = FindObjectOfType<UI_UIButtonManager>();
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: N/A
    // Argument types: N/A
    // Author: Kayci Lyons 
    // Date: 9-27-17
    // ------------------------------------------------------------------------------
    // Purpose: Moves the object at a constant rate. Destroys it after so much time.
    // ------------------------------------------------------------------------------

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * MoveSpeed;

        if (transform.position.z != 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        Destroy(gameObject, 20.0f);
    }

    // ------------------------------------------------------------------------------
    // Function Name: Destroy Trigger
    // Return types: N/A
    // Argument types: N/A
    // Author:
    // Date: 9-28-17
    // ------------------------------------------------------------------------------
    // Purpose: Destroys the object when it hits a tagged object
    // ------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == UNA_Tags.wall || collision.GetComponent<GRV_IndividualGravity>() != null)
        {
            Destroy(gameObject, 0.01f);
        }

        if (UNA_StaticVariables.isGrvPickedUp)
        {
            if (collision.GetComponent<GRV_IndividualGravity>() != null)
            {
                if (collision.tag != UNA_Tags.player)
                {
                    collision.GetComponent<GRV_IndividualGravity>().GravityGunImpact(gravitySetting, directionalForce, alteredEffectTime);
                }

            }
        }
        else 
        {
            if (collision.GetComponent<GRV_IndividualGravity>() != null)
            {
                if (collision.tag != UNA_Tags.player)
                {
                    collision.GetComponent<GRV_IndividualGravity>().GravityGunImpact(gravitySetting, directionalForce, effectTime);
                }

            }
                

        }

    }


} // End PLR_Bullet