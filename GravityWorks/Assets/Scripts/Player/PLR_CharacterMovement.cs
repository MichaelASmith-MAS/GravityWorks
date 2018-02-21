/* -----------------------------------------------------------------------------------
 * Class Name: PLR_CharacterMovement
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date: 09/28/2017
 * -----------------------------------------------------------------------------------
 * Purpose: Controls character movement based on Horizontal axis input.
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLR_CharacterMovement : MonoBehaviour {

    #region Variables
    public GameObject playerGFX;
    public float moveSpeed = 10f, maxDeltaV = 10f/*, turnSpeed = 2f*/;
    //public string levelToLoad; //Ref to what scene you want to load - will need to update to ref specific Teleporter's Segement selection later

    Rigidbody rb;
    bool isFrozen = false, isInTeleporter = false;

    public float smooth = 1f;
    private Vector3 targetAngles;
    bool movingRight = false;

    //Added to test invertedGravity
    GRV_IndividualGravity player_grv_individualGravity;
    PLR_Shoot plr_Shoot;
    //PLR_Jump plr_Jump;
    private Vector3 verticalAngles;
    bool floatingUp = false;
    private string currentLevel;
    private string previousLevel;
    UNA_HashIDs hashIDs;
    WLD_LevelLoader lL;
    WLD_HealthDmg healthScript;

    public Animator anim;

    //Finish Add
    #endregion

    #region GETTERS / SETTERS
    public bool IsFrozen
    {
        get { return isFrozen; }
    }

    public float MovingSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    public Rigidbody RB
    {
        get { return rb; }
    }

    public bool GETisInTeleporter
    {
        get { return isInTeleporter; }
    }
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //lL = FindObjectOfType<WLD_LevelLoader>(); //Ref to levelLoader script - will need to update to ref specific Teleporter's Segement selection later
        //Added to test InvertedGravity
        player_grv_individualGravity = this.GetComponent<GRV_IndividualGravity>();
        //Finish Add
        //plr_Jump = GetComponent<PLR_Jump>();
        plr_Shoot = FindObjectOfType<PLR_Shoot>();
        healthScript = this.GetComponent<WLD_HealthDmg>();

        rb.isKinematic = false;
        rb.useGravity = false;

        isFrozen = false;

        hashIDs = FindObjectOfType<UNA_HashIDs>();
        anim = playerGFX.GetComponentInChildren<Animator>();

    }

    private void OnLevelWasLoaded(int level)
    {
        lL = FindObjectOfType<WLD_LevelLoader>();

        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.LoadGame] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Controls] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Credits] )
        {
            return;
        }

        else
        {
            moveSpeed = 10f;
            plr_Shoot.enabled = true;
            healthScript.canMakeDamageSounds = true;
        }

    }

    void Start()
    {
        currentLevel = WLD_GameController.activeLevel.LevelName;
        previousLevel = currentLevel;
        
    }
    private void Update()
    {
        CheckForLevelChange();

        if (!isFrozen)
        {
            Move();
        }

        if (healthScript.Health == 0)
        {
            moveSpeed = 0f;
            plr_Shoot.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.teleporter)
        {
            isInTeleporter = true;
            plr_Shoot.enabled = false;
            //plr_Jump.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == UNA_Tags.teleporter)
        {
            isInTeleporter = false;
            plr_Shoot.enabled = true;
            //plr_Jump.enabled = true;
        }
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");

        Vector3 targetDirection = transform.right * h;

        Vector3 targetVelocity = targetDirection * moveSpeed;
        Vector3 deltaVelocity = targetVelocity - rb.velocity;

        deltaVelocity.x = Mathf.Clamp(deltaVelocity.x, -maxDeltaV, maxDeltaV);
        deltaVelocity.z = Mathf.Clamp(deltaVelocity.z, -maxDeltaV, maxDeltaV);
        deltaVelocity.y = 0f;

        rb.AddForce(deltaVelocity, ForceMode.VelocityChange);
        
        if (h != 0)
        {
            if (!anim.GetBool(hashIDs.moveBool))
            {
                anim.SetBool(hashIDs.moveBool, true);
            }
        }
        else
        {
            if (anim.GetBool(hashIDs.moveBool))
            {
                anim.SetBool(hashIDs.moveBool, false);
            }
        }

        //Original (with the exception of && !floatingUp
        if (h < 0 && !movingRight && !floatingUp)
        {
            if (playerGFX.transform.eulerAngles.y != 180)
            {
                targetAngles = playerGFX.transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
                playerGFX.transform.eulerAngles = targetAngles;
            }
            movingRight = true;
        }
        if (h > 0 && movingRight && !floatingUp)
        {
            if (playerGFX.transform.eulerAngles.y != 0)
            {
                targetAngles = playerGFX.transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
                playerGFX.transform.eulerAngles = targetAngles;
            }
            movingRight = false;
        }
        //End Original


        //Added to test InvertedGravity
        if (h > 0 && !movingRight && floatingUp)
        {
            if (playerGFX.transform.eulerAngles.y != 180)
            {
                targetAngles = playerGFX.transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
                playerGFX.transform.eulerAngles = targetAngles;
            }
            movingRight = true;
        }
        if (h < 0 && movingRight && floatingUp)
        {
            if (playerGFX.transform.eulerAngles.y != 0)
            {
                targetAngles = playerGFX.transform.eulerAngles + 180f * Vector3.up; // what the new angles should be
                //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
                playerGFX.transform.eulerAngles = targetAngles;
            }
            movingRight = false;
        }

        if (player_grv_individualGravity.Drag < 0 && !floatingUp)
        {
            verticalAngles = playerGFX.transform.eulerAngles + 180f * Vector3.forward; // what the new angles should be
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, verticalAngles, smooth * Time.deltaTime); // lerp to new angles
            playerGFX.transform.eulerAngles = verticalAngles;
            floatingUp = true;
        }
        if (player_grv_individualGravity.Drag > 0 && floatingUp)
        {
            verticalAngles = playerGFX.transform.eulerAngles + 180f * Vector3.forward; // what the new angles should be
            //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, verticalAngles, smooth * Time.deltaTime); // lerp to new angles
            playerGFX.transform.eulerAngles = verticalAngles;
            floatingUp = false;
        }
        //Finish Add
        
    }

    public void FreezeCharacter()
    {
        isFrozen = !isFrozen;

    }

    private void CheckForLevelChange()
    {
        currentLevel = WLD_GameController.activeLevel.LevelName;

        if (previousLevel != currentLevel)
        {
            plr_Shoot.enabled = true;
            //plr_Jump.enabled = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.angularVelocity = new Vector3(0, 0, 0);

            if (player_grv_individualGravity.Drag < 0)
            {
                player_grv_individualGravity.Drag = 1;
            }

            previousLevel = currentLevel;
        }
    }

}
