using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLR_Dash : MonoBehaviour

{
    public float maxDash = 20f;
    public float dashTimer;
    public float dashCoolDownTimer;
    //public Vector2 savedVelocity;     //<- Commented out because it became obsolete when referencing the PLR_CharMove scripts variables
    public DashState dashState;
    public float dashRate = 2;
    public TrailRenderer playerTrail;

    public AudioClip dashSound;

    public Slider dashCoolDownSlider;
    //Added the two below to refernce the PLR_CharacterMovement script and its moveSpeed getters/setters
    private float originalSpeed;
    private PLR_CharacterMovement charMove;
    private bool dashEnabled = true;
    Animator anim;
    UNA_HashIDs hashIDs;
    float animatorSpeed;

    AudioSource audioSource;

    private void Start()
    {
        charMove = GetComponent<PLR_CharacterMovement>();
        originalSpeed = charMove.MovingSpeed;

        anim = charMove.anim;
        hashIDs = FindObjectOfType<UNA_HashIDs>();

        animatorSpeed = anim.speed;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DashBehaviour();
        SetDashEnabled();
        DashPlayerFeedBack();
    }

    void DashBehaviour()
    {
        switch (dashState)
        {
            case DashState.Ready:
                //I think the line below was preventing you from entering the behavior, also set up input for what we called out in our GDD
                //var isDashKeyDown = Input.GetKeyDown(KeyCode.F;
                //if (/*Input.GetKeyDown(KeyCode.F)*/
                //    Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton0))
                if (Input.GetAxis("Dash") > 0 && dashEnabled)
                {
                    playerTrail.enabled = true;
                    //if (isDashKeyDown)
                    //{

                    //savedVelocity = GetComponent<Rigidbody>().velocity;
                    //GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x * 3f, GetComponent<Rigidbody>().velocity.y);

                    //I removed the above two lines to directly access the and modify the PLR_CharacterMovment scripts moveSpeed variable
                    charMove.MovingSpeed = originalSpeed * dashRate;
                    anim.speed *= 1.3f;

                    audioSource.PlayOneShot(dashSound);

                    dashState = DashState.Dashing;
                }
                break;

            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    playerTrail.enabled = true;

                    dashTimer = maxDash;
                    //GetComponent<Rigidbody>().velocity = savedVelocity;
                    if (!UI_TemperatureGauge.isCold)
                    {
                        charMove.MovingSpeed = originalSpeed;
                        anim.speed = animatorSpeed;
                    }
                      //Removed the above to replace with the PLR_CharMove's original speed
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                playerTrail.enabled = false;

                dashCoolDownTimer -= Time.deltaTime;
                if (dashCoolDownTimer <= 0)
                {
                    dashCoolDownTimer = 1;
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    void SetDashEnabled()
    {
            if (Input.GetAxis("Dash") > 0)
            {
                dashEnabled = false;
            }
            if (Input.GetAxis("Dash") == 0)
            {
                dashEnabled = true;
            }
    }

    void DashPlayerFeedBack()
    {
        dashCoolDownSlider.value = dashCoolDownTimer;
    }

}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown

}

