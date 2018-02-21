using System.Collections;
using System.Collections.Generic;
using UnityEngine;

      [RequireComponent(typeof(Rigidbody))]
public class PLR_Jump : MonoBehaviour {

    public bool enableJump = false;
    public Vector3 jump;
    public float jumpForce = 2.0f;

    public bool isGrounded, jumpButtonEnabled = true;
    Rigidbody rb;
    //Added to test InvertedGravity
    GRV_IndividualGravity player_grv_individualGravity;
    //Finish Add
    UNA_Level currentLevel;

    public bool GETisGrounded
    {
        get { return isGrounded; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 100.0f, 0.0f);
        //Added to test InvertedGravity
        player_grv_individualGravity = this.GetComponent<GRV_IndividualGravity>();
        //Finish Add
    }

    void OnCollisionStay(Collision collider)
    {
        if (collider.collider.tag != UNA_Tags.wall)
        {
            isGrounded = true;
        }
        
    }
    
    private void OnCollisionExit(Collision collider)
    {
        if (collider.collider.tag != UNA_Tags.wall)
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        currentLevel = WLD_GameController.activeLevel;

        if (currentLevel == WLD_GameController.levels[Scenes.Hub])
        {
            Jump();
        }
        else
        {
            return;
        }
    }

    void Jump()
    {
        if ((Input.GetAxis("SelfShoot") > 0 && isGrounded))
        {
            //ORIGINAL BEFORE TESTING FOR INVERTED GRAVITY
            //rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            //isGrounded = false;
            //Finish Original

            if (player_grv_individualGravity.Drag < 0)
            {
                rb.AddForce(jump * -(jumpForce), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            }

            isGrounded = false;
        }
    }

    void SetJumpButtonEnabled()
    {
        if (Input.GetAxis("SelfShoot") > 0)
        {
            jumpButtonEnabled = false;
        }
        if (Input.GetAxis("SelfShoot") == 0)
        {
            jumpButtonEnabled = true;
        }
    }
}

