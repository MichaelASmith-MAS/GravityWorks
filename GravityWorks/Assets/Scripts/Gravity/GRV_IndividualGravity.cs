/* -----------------------------------------------------------------------------------
 * Class Name: GRV_IndividualGravity
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 9/26/2017
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Controls all gravitational elements with an object
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class GRV_IndividualGravity : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] float segmentGravity = 0, gunGravity = 0, effectiveGravity = 0, drag = 1;

    [SerializeField] int jumpDownForce = 50, mainDownForce = 20;

    [SerializeField] int segNum = 0;

    UNA_Segment segment;
    Rigidbody rb;
    float timeAlive = 0f, timer = 0f;

    ParticleSystem gravityEffectParticle;

    #endregion

    #region Getters/Setters

    public float EffectiveGravity
    {
        get { return effectiveGravity; }
    }

    //Added to test InvertedGravity Areas
    public float Drag
    {
        get { return drag; }
        set { drag = value; }
    }
    //Finish Add

    public float SegmentGravity
    {
        get { return segmentGravity; }
    }

    public float GunGravity
    {
        get { return gunGravity; }
    }

    public int SegNum
    {
        get { return segNum; }
    }

    #endregion

    #region

    public UNA_Segment Segment
    {
        get { return segment; }
    }

    #endregion


    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Awake
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/26/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs at the start of the scene. Collects rigidbody data from game object
    // ------------------------------------------------------------------------------

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/26/2017
    // ------------------------------------------------------------------------------
    // Purpose: Run each frame. Runs timer if timeAlive is greater than 0
    // ------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        if (timeAlive > 0)
        {
            RunTimer();
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: OnTriggerStay
    // Return types: void
    // Argument types: Collider
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs while within a trigger volume. Collects segment info and calculates
    //          effective gravity based on environmental and gravity gun settings
    // ------------------------------------------------------------------------------

    private void OnTriggerStay(Collider other)
    {
        //Added to test InvertedGravity Areas
        //if (other.tag == UNA_Tags.invertGravity && drag > 0)
        //{
        //    drag = -1;
        //}
        //Finish Add

        if (other.tag == UNA_Tags.segment)
        {
            segment = other.GetComponent<WLD_SegmentController>().Segment;
            segmentGravity = WLD_GameController.gravityValues[segment.GravitySetting];

            segNum = segment.SegmentNumber;

            if (gunGravity == 0)
            {
                try
                {
                    effectiveGravity = -(CalculateGravity(segmentGravity) * drag);
                }
                catch (System.NullReferenceException)
                {
                    effectiveGravity = -(CalculateGravity(1));
                    throw;
                }

            }

            //ORIGINAL BEFORE TESTING FOR INVERTED GRAVITY
            //else if (segmentGravity - gunGravity > 0)
            //{
            //    effectiveGravity = -(CalculateGravity(gunGravity) - CalculateGravity(segmentGravity) * drag);
            //}


            //else if (segmentGravity - gunGravity < 0)
            //{
            //    effectiveGravity = -(CalculateGravity(segmentGravity) + CalculateGravity(gunGravity) * drag);
            //}
            //Finish Original

            //Added to test InvertedGravity Areas
            else if (drag > 0)
            {
                if (segmentGravity - gunGravity > 0)
                {
                    effectiveGravity = -(CalculateGravity(gunGravity) - CalculateGravity(segmentGravity));
                }


                else if (segmentGravity - gunGravity < 0)
                {
                    effectiveGravity = -(CalculateGravity(segmentGravity) + CalculateGravity(gunGravity));
                }
            }

            else if (drag < 0)
            {
                effectiveGravity = -(CalculateGravity(segmentGravity) - CalculateGravity(gunGravity));
            }
            //Finsish Add

            ApplyGravity();
        }

    }

    //Added to test InvertedGravity Areas
    void OnTriggerExit(Collider other)
    {
        //if (other.tag == UNA_Tags.invertGravity && drag < 0)
        //{
        //    drag = 1;
        //}
    }
    //Finish Add

    // ------------------------------------------------------------------------------
    // Function Name: CalculateGravity
    // Return types: float
    // Argument types: float
    // Author: Michael Smith
    // Date: 9/26/2017
    // ------------------------------------------------------------------------------
    // Purpose: Calculates and returns force value based on passed gravity, mass, and time squared
    // ------------------------------------------------------------------------------

    float CalculateGravity (float gravity)
    {
        try
        {
            return (gravity * rb.mass * Mathf.Pow(Time.deltaTime, 2f) * mainDownForce);
        }
        catch (System.NullReferenceException)
        {
            return 0;
            throw;
        }

         //return (gravity * rb.mass * Mathf.Pow(Time.deltaTime, 2f) * mainDownForce);


    }

    // ------------------------------------------------------------------------------
    // Function Name: GravityGunImpact
    // Return types: void
    // Argument types: Gravity, float
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Sets gun gravity value based on provided Gravity enum. Sets time alive
    //          to effect time, then resets run timer.
    // ------------------------------------------------------------------------------

    public void GravityGunImpact(Gravity gravityLevel, float directionalForce, float timeAlive)
    {
        gunGravity = WLD_GameController.gravityValues[gravityLevel];
        GameObject go = null;

        if (gameObject.tag == UNA_Tags.player || gameObject.tag == UNA_Tags.enemy)
        {
            go = Instantiate(Resources.Load("Prefabs/Particles/GunEffect/GravityEffect"), transform.position, Quaternion.identity, transform) as GameObject;
        }

        else
        {
            go = Instantiate(Resources.Load("Prefabs/Particles/GunEffect/GravityEffect"), new Vector3(transform.position.x + (transform.localScale.x / 2), transform.position.y + (transform.localScale.y / 2), transform.position.z + (transform.localScale.z / 2)), Quaternion.identity, transform) as GameObject;
        }

        //GameObject go = Instantiate(Resources.Load("Prefabs/Particles/GunEffect/GgunShotEffect"), transform.position, Quaternion.identity, transform) as GameObject;

        //ParticleSystem.ShapeModule shape = go.GetComponent<ParticleSystem>().shape;
        //shape.rotation = new Vector3(-90, 0, 0);
        //shape.scale = transform.localScale;

        if (gameObject.tag == UNA_Tags.player)
        {
            go.transform.localScale = transform.GetChild(1).localScale * 1.7f;

        }

        else if (gameObject.name.Contains("ExplodyGFX"))
        {
            go.transform.localScale = new Vector3(go.transform.localScale.x * 1.7f, go.transform.localScale.y * 3f, go.transform.localScale.z * 1.7f);

        }

        else
        {
            go.transform.localScale = go.transform.localScale * 1.7f;
        }

        go.GetComponent<GRV_GgunEffect>().ApplyProperties(gravityLevel, timeAlive);
        
        Vector3 newDirection = Vector3.left * directionalForce;
        
        rb.AddForce(newDirection, ForceMode.VelocityChange);

        this.timeAlive = timeAlive;
        timer = 0f;
    }

    // ------------------------------------------------------------------------------
    // Function Name: RunTimer
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/27/2017
    // ------------------------------------------------------------------------------
    // Purpose: Tracks a timer. Once this timer equals or surpasses timeAlive, it resets
    //          the gravity value associated from the gun, and sets timeAlive to 0
    // ------------------------------------------------------------------------------

    void RunTimer()
    {
        timer += Time.deltaTime;

        if (timer >= timeAlive)
        {
            gunGravity = 0f;
            timeAlive = 0f;
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

    void ApplyGravity ()
    {
        rb.AddForce((Vector3.up * effectiveGravity), ForceMode.Acceleration);

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

} // End GRV_IndividualGravity