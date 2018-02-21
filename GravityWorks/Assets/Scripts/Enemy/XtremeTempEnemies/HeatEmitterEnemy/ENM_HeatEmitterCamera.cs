/* -----------------------------------------------------------------------------------
 * Class Name: TempCamera
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_HeatEmitterCamera : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("HeatEmitter GameObject")]
    public GameObject heatEmitter;

    [Header("Detected Length")]
    public float playerDetectedLength;

    [Header("Laser Start Position")]
    public Transform firePoint;
    public Transform laserEnd;

    [Header("LinRenderer Componenet")]
    public LineRenderer lineRenderer;

    [Header("ECamera Colliders")]
    public Collider POV;
    public Collider camPOV;

    [Header("Timers")]
    [SerializeField]
    private float ResetTime = 10;
    [SerializeField]
    private float waitTime = 5.0f;

    [Header("Rotation and Speed")]
    [SerializeField]
    private float rot = 45;
    [SerializeField]
    private float speed = 0.75f;

    public float timer = 0;

    public bool suspicion, reset, powered, triggered;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    private Vector3 startingPos = new Vector3(0, 0, 0);
    private Vector3 currentPos;
    private Transform target;
    
    private GameObject Base;
    public float cameraActivatedTimer;

  

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
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void Start()
    {
        suspicion = false;
        reset = false;
        triggered = false;

        powered = true;

        cameraActivatedTimer = 0;

        Base = GameObject.Find("Base");
        target = GameObject.FindWithTag(UNA_Tags.player).transform;

        //heatEmitter = GetComponent<Renderer>();
    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons 
    // Date: 10/3/2017
    // ------------------------------------------------------------------------------
    // Purpose:
    // ------------------------------------------------------------------------------

    void Update()
    {
        if (powered == true)
        {
            if (reset == true)
            {
                resetCam();
            }
            else
            {
                if (triggered == true)
                {
                    LookAt();
                }
                else
                {
                    if (suspicion == false)
                    {
                        updateMovement();
                        //Debug.Log("Scanning");
                    }
                    else
                    {
                        updateTimer();
                    }
                }
            }

            if (timer >= ResetTime)
            {
                reset = true;
                suspicion = false;
            }

            if (triggered == true)
            {
                POV.enabled = true;
            }
        }
        Laser();
    }


    // ------------------------------------------------------------------------------
    // Function Name: LookAt
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: Makes the object follow the player
    // ------------------------------------------------------------------------------
    void LookAt()
    {
        if (target != null)
        {
            Vector3 difference = target.position - transform.position;
            float rotationZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) + 90.0f;
            Base.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Resetting IEnumerator
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: This will reset the camera after the wait time
    // ------------------------------------------------------------------------------
    private IEnumerator Resetting()
    {
        yield return new WaitForSeconds(waitTime);
        //Suspicion = false;
        //POV.enabled = false;
        reset = false;
        timer = 0;
        camPOV.enabled = true;
        StopCoroutine(Resetting());

    }

    // ------------------------------------------------------------------------------
    // Function Name: Trigger Colliders
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: These will activate when the player enters the trigger zones
    // ------------------------------------------------------------------------------
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            triggered = true;
            suspicion = false;
            reset = false;
            timer = 0;

            ChangeGradientColorSuspision();
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            cameraActivatedTimer += Time.deltaTime;

            if (cameraActivatedTimer >= playerDetectedLength)
            {
                heatEmitter.SetActive(true);

                ChangeGradientColorAggressive();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            triggered = false;
            suspicion = true;

            cameraActivatedTimer = 0f;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: ChangeGradientColorSuspision
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/10/17
    // ------------------------------------------------------------------------------
    // Purpose: These will change the laser color based on the set state of the camera
    // ------------------------------------------------------------------------------
    void ChangeGradientColorSuspision()
    {
        float alpha = 1.0f;

        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }

    void ChangeGradientColorAggressive()
    {
        float alpha = 1.0f;

        Gradient gradient = new Gradient();

        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
    }
    // ------------------------------------------------------------------------------
    // Function Name: Camera Rotation
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: This function rotates the camera while it is 'searching' for movement/the player
    // ------------------------------------------------------------------------------
    void updateMovement()
    {
        Base.transform.rotation = Quaternion.Euler(0, 0, (rot * Mathf.Sin(Time.time * speed)));
    }


    // ------------------------------------------------------------------------------
    // Function Name: Timer
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: This functions activates the timer when called. 
    // ------------------------------------------------------------------------------

    private void updateTimer()
    {
        timer += Time.deltaTime;
    }

    // ------------------------------------------------------------------------------
    // Function Name: Camera Reset
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/4/17
    // ------------------------------------------------------------------------------
    // Purpose: These functions reset the camera to the starting position after it has been triggered. 
    // ------------------------------------------------------------------------------

    private void CameraReset()
    {
        POV.enabled = false;
        camPOV.enabled = false;
        StartCoroutine(Resetting());
    }

    private void resetCam()
    {
        currentPos = Base.transform.eulerAngles;
        currentPos = new Vector3(
            Mathf.LerpAngle(currentPos.x, startingPos.x, Time.deltaTime * speed),
            Mathf.LerpAngle(currentPos.y, startingPos.y, Time.deltaTime * speed),
            Mathf.LerpAngle(currentPos.z, startingPos.z, Time.deltaTime * speed));
        Base.transform.eulerAngles = currentPos;
        CameraReset();
    }

    // ------------------------------------------------------------------------------
    // Function Name: Laser
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/10/17
    // ------------------------------------------------------------------------------
    // Purpose: controls where the laser starts and where it ends
    // ------------------------------------------------------------------------------
    void Laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, laserEnd.position);


    }

} // End TempCamera