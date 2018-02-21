using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PLR_Shoot : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Bullet")]
    public GameObject Bullet;

    [Header("Effects")]
    public GameObject baseSparks;
    public GameObject baseLight;

    [Header("PowerUp Effects")]
    public GameObject powerUpParticles;

    [Header("Gun Effect")]
    public GameObject gunObject;
    public Light gunLight;

    public float idleTime = 5f;

    public bool paused = false;
    
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] float selfShootTime = 1, alteredSelfShootTime = 0.7f, fireRate = 1.25f, selfFireRate = 1.25f, forceApplication;
    float curTime, selfCurTime;
    Quaternion _lookRotation;
    Vector3 _direction;
    float directionalForce = 0f, timer, startTimer;
    public static bool fireEnabled;
    bool selfShootEnabled = true, isIdle;

    ParticleSystem.MainModule particleMain;
    Color currentGravityColor;

    UNA_HashIDs hashIDs;
    Animator anim;
    AnimatorStateInfo animatorState;
    AnimatorClipInfo[] animState;
    AnimationClip animClip;
    float animTime = 0f;

    Material gunMaterial;
    AudioSource audioSource;

    #endregion

    #region Getters/Setters
    public float GETfireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public float GETselfFireRate
    {
        get { return selfFireRate; }
        set { selfFireRate = value; }
    }

    public float GETcurTime
    {
        get { return curTime; }
        set { curTime = value; }
    }
    #endregion

    #region Constructors



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

    private void Start()
    {
        curTime = fireRate;
        selfCurTime = selfFireRate;

        Material[] tempGunMaterials = gunObject.GetComponent<Renderer>().materials;
        gunMaterial = tempGunMaterials[1];

        audioSource = GetComponent<AudioSource>();

        hashIDs = FindObjectOfType<UNA_HashIDs>();
        anim = WLD_GameController.player.GetComponent<PLR_CharacterMovement>().anim;

    }
    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 9-27-17
    // ------------------------------------------------------------------------------
    // Purpose: Checks to see if a button is pressed and activates the shoot function if it is.
    // ------------------------------------------------------------------------------
    void Update()
    {
        SetShotColor();
        Shoot();
        SetFireEnabled();
        SetSelfShootEnabled();
        ShootIdle();
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

    void SetShotColor ()
    {
        currentGravityColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];

        gunMaterial.color = currentGravityColor;
        gunMaterial.SetColor("_EmissionColor", currentGravityColor);

        gunLight.color = currentGravityColor;

    }
    // ------------------------------------------------------------------------------
    // Function Name: doSpawn
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 9-27-17
    // ------------------------------------------------------------------------------
    // Purpose: Spawns the prefab bullet if the game is not paused.
    // ------------------------------------------------------------------------------
    private void SpawnBullet()
    {

        if (!paused && curTime > fireRate)
        {
            curTime = 0;

            audioSource.Play();

            GameObject go = Instantiate(Bullet, transform.position, transform.rotation);

            go.transform.GetChild(0).GetComponentInChildren<Light>().color = currentGravityColor;

            particleMain = go.transform.GetChild(1).GetComponentInChildren<ParticleSystem>().main;
            particleMain.startColor = currentGravityColor;

            particleMain = go.transform.GetChild(2).GetComponentInChildren<ParticleSystem>().main;
            particleMain.startColor = currentGravityColor;

            //go.GetComponent<Light>().color = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
            //go.GetComponent<ParticleSystem>().startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
            //go.GetComponentInChildren<ParticleSystem>().startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
            //go.transform.GetChild(1).GetComponent<ParticleSystem>().startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];
            //go.transform.GetChild(0).GetComponent<TrailRenderer>().startColor = WLD_GameController.gravityImages[FindObjectOfType<UI_UIButtonManager>().GravitySetting];

            ShootEffect();

            PLR_Bullet bullet = go.GetComponent<PLR_Bullet>();

            bullet.GravitySetting = FindObjectOfType<UI_UIButtonManager>().GravitySetting;

            bullet.DirectionalForce = directionalForce;

            directionalForce = 0;
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
    void ShootEffect()
    {
        GameObject shootEffect = Instantiate(baseSparks, transform.position, transform.rotation);
        GameObject shootEffectLight = Instantiate(baseLight, transform.position, transform.rotation);

        particleMain = shootEffect.GetComponent<ParticleSystem>().main;
        particleMain.startColor = currentGravityColor;

        particleMain = shootEffect.GetComponentInChildren<ParticleSystem>().main;
        particleMain.startColor = currentGravityColor;

        particleMain = shootEffect.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        particleMain.startColor = currentGravityColor;


        shootEffectLight.GetComponent<Light>().color = currentGravityColor;

        Destroy(shootEffect, 2f);
        Destroy(shootEffectLight, .2f);
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
    void Shoot()
    {
        selfCurTime += Time.deltaTime;
        curTime += Time.deltaTime;

        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Hub])
        {
            //if (/*Input.GetKeyDown(KeyCode.Mouse0)*/  /*|| Input.GetKeyDown(KeyCode.JoystickButton10)*/)
            if (Input.GetAxisRaw("Fire") > 0 && fireEnabled)
            {
                isIdle = false;

                anim.SetTrigger(hashIDs.firingTrigger);

                //animatorState = anim.GetCurrentAnimatorStateInfo(0);
                //animState = anim.GetCurrentAnimatorClipInfo(0);
                //animTime = 0f;
                
                //if (Input.GetKey(KeyCode.LeftControl))
                if (Input.GetAxisRaw("PushPhysObj") > 0)
                {
                    directionalForce = -forceApplication;

                }
                //else if (Input.GetKey(KeyCode.LeftAlt))
                else if (Input.GetAxisRaw("PullPhysObj") > 0)
                {
                    directionalForce = forceApplication;
                }

                if (UNA_StaticVariables.isDefaultShoot)
                {
                    SpawnBullet();
                }
                else if (UNA_StaticVariables.isRdyShoot)
                {
                    SpawnBullet();
                    // Debug.Log("Shooting");    
                }
            }
            else if (!fireEnabled)
            {
                isIdle = true;
            }

            
            //if (Input.GetKeyDown(KeyCode.Space))
            if (Input.GetAxis("SelfShoot") > 0 && selfShootEnabled)
            {
                
                if (!paused && selfCurTime > selfFireRate)
                {
                    selfCurTime = 0;

                    audioSource.Play();

                    if (UNA_StaticVariables.isGrvPickedUp)
                    {
                        GetComponentInParent<GRV_IndividualGravity>().GravityGunImpact(FindObjectOfType<UI_UIButtonManager>().GravitySetting, 0, alteredSelfShootTime);
                    }
                    else
                    {
                        GetComponentInParent<GRV_IndividualGravity>().GravityGunImpact(FindObjectOfType<UI_UIButtonManager>().GravitySetting, 0, selfShootTime);
                    }

                }
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
    void ShootIdle()
    {
        if (!isIdle)
        {
            powerUpParticles.SetActive(true);
            timer = startTimer;
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= idleTime)
            {
                powerUpParticles.SetActive(false);
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
    void SetFireEnabled()
    {

        if (Input.GetAxis("Fire") > 0)
        {
            fireEnabled = false;
        }
        if (Input.GetAxis("Fire") == 0)
        {
            fireEnabled = true;
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
    void SetSelfShootEnabled()
    {
        if (Input.GetAxis("SelfShoot") > 0)
        {
            selfShootEnabled = false;
        }
        if (Input.GetAxis("SelfShoot") == 0)
        {
            selfShootEnabled = true;
        }
    }
} // End PLR_Shoot