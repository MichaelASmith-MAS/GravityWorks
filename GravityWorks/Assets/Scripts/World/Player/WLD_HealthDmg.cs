/* -----------------------------------------------------------------------------------
 * Class Name: UNA_HealthDmg
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WLD_HealthDmg : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public float dmgToPlayer;
    public Image healthSliderImage;
    public Text healthText;
    public ParticleSystem ps;

    public Animator healthBarPanel;
    [Header("Impact Thresholds")]
    [Range(15, 30)]
    public float playerImpactThreshold = 15f;
    [Range(5, 20)]
    public float enemyImpactThreshold = 15f;

    public bool canMakeDamageSounds = true;
    public AudioClip[] playerDamageSounds;

    public Animator dmgTextAnim;
    public Animator dmgOverlay;
    public Animator dmgOverlayTurningOFf;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth = 100f;

    GameObject player;
    float startingHealth = 100, startTimer = 1, timer, dmgtimer;
    Color beginHealth = Color.green;
    Color endHealth = Color.red;

    [SerializeField] float impactForce;

    Rigidbody rb;

    bool isHealthChange = false;

    UNA_HashIDs hashIDs;
    Animator anim;
    AudioSource damageAudioSource;

    #endregion

    #region Getters/Setters

    public float DmgToPlayer
    {
        get { return dmgToPlayer; }
    }

    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Start()
    {
        player = WLD_GameController.player;

        UNA_StaticVariables.tempDmgOn = false;
        timer = 0;

        startingHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        WLD_GameController.ui_Texts[UI_Txt.DamageText].enabled = false;
        WLD_GameController.ui_Texts[UI_Txt.CurrentHealthText].color = Color.black;
        //ParticleSystem.EmissionModule em = ps.emission;
        //em.enabled = true;

        if (gameObject == player)
        {
            hashIDs = FindObjectOfType<UNA_HashIDs>();
            anim = player.GetComponent<PLR_CharacterMovement>().anim;
            damageAudioSource = GetComponent<AudioSource>();

        }

    }
    void Update()
    {
        ChangeHealthBar();
        HealthToUI();
        ImpactDamageNotification();
      
        timer += Time.deltaTime;

        if (currentHealth == 0)
        {
            canMakeDamageSounds = false;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: ChangeHealth
    // Return types: void
    // Argument types: float
    // Author: Michael Smith
    // Date: 10/05/2017
    // ------------------------------------------------------------------------------
    // Purpose: Changes health value based on passed float, clamped to ensure the value
    //          cannot get too high.
    // ------------------------------------------------------------------------------

    public void ChangeHealth(float value)
    {
        currentHealth = (int)Mathf.Clamp(currentHealth += value, 0f, maxHealth);

        if (gameObject.tag == UNA_Tags.player)
        {
            if (value < 0)
            {
                anim.SetTrigger(hashIDs.hitTrigger);

                int damageNumber = Random.Range(0, playerDamageSounds.Length - 1);

                if (canMakeDamageSounds)
                {
                    damageAudioSource.PlayOneShot(playerDamageSounds[damageNumber]);
                }

            }

            if (value >= 100 || value == 0)
            {
                return;
            }

            WLD_GameController.ui_Texts[UI_Txt.DamageText].enabled = true;
            WLD_GameController.ui_Texts[UI_Txt.DamageText].text = Mathf.Round(value).ToString();

            if (value > 10)
            {
                WLD_GameController.ui_Texts[UI_Txt.DamageText].color = Color.green;
            }
            else
            {
                WLD_GameController.ui_Texts[UI_Txt.DamageText].color = Color.red;

                PlayOverlayAnimation();
            }

            PlayDmgTextAnim();

            PlayHealthbarAnim();

            if (currentHealth <= 50)
            {
                WLD_GameController.ui_Texts[UI_Txt.CurrentHealthText].color = Color.white;
            }
            else
            {
                WLD_GameController.ui_Texts[UI_Txt.CurrentHealthText].color = Color.black;
            }
        }
    }
    void PlayOverlayAnimation()
    {
        WLD_GameController.ui_Images[UI_Images.DamageOverlay].enabled = true;

        dmgOverlay.SetTrigger("Play");

        if (dmgOverlay.GetCurrentAnimatorStateInfo(0).IsName("DamageOverlay"))
        {
            dmgOverlay.SetTrigger("Off");
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Resets the Dmg Text Animation
    // ------------------------------------------------------------------------------
    void PlayDmgTextAnim()
    {
        dmgTextAnim.SetTrigger("Play");

        if (dmgTextAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerDmgTextTwo"))
        {
            dmgTextAnim.SetTrigger("Idle");
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Resets the health Bar animation
    // ------------------------------------------------------------------------------
    void PlayHealthbarAnim()
    {      
        if (currentHealth >= 25)
        {
            healthBarPanel.SetTrigger("Play");

            if (healthBarPanel.GetCurrentAnimatorStateInfo(0).IsName("HealthBarTurningOff"))
            {
                healthBarPanel.SetTrigger("Idle");
            }
            healthBarPanel.SetBool("IsBelow25", false);
        }
        else
        {
            healthBarPanel.SetBool("IsBelow25", true);
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: ChangeHealth
    // Return types: void
    // Argument types: float
    // Author: Michael Smith
    // Date: 10/05/2017
    // ------------------------------------------------------------------------------
    // Purpose: Changes health value based on passed float, clamped to ensure the value
    //          cannot get too high.
    // ------------------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision)
    {
        impactForce = collision.relativeVelocity.magnitude;

        if (gameObject.tag == UNA_Tags.player)
        {
            if (collision.relativeVelocity.magnitude > playerImpactThreshold)
            {
                float temp = collision.relativeVelocity.magnitude - playerImpactThreshold;

                ChangeHealth(-temp);

                GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Particles/Base/ImpactEffect"), transform.position, Quaternion.identity);
                ParticleSystem.MainModule main = go.GetComponent<ParticleSystem>().main;
                Destroy(go, main.duration);

            }

        }

        else
        {
            if (collision.relativeVelocity.magnitude > enemyImpactThreshold)
            {
                float temp = collision.relativeVelocity.magnitude - enemyImpactThreshold;

                ChangeHealth(-temp);
            }
        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: ChangeHealthBar
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/23/17
    // ------------------------------------------------------------------------------
    // Purpose: Controls the UI health bar
    // ------------------------------------------------------------------------------
    public void ChangeHealthBar()
    {
        if (gameObject.tag == UNA_Tags.explody)
        {
            if (currentHealth <= 4)
            {
                healthSliderImage.enabled = true;
                healthText.enabled = true;
                healthSliderImage.fillAmount = currentHealth / startingHealth;
                healthSliderImage.color = Color.Lerp(endHealth, beginHealth, currentHealth);
            }
            else
            {
                healthSliderImage.enabled = false;
                healthText.enabled = false;

            }
        }
        if (gameObject.tag == UNA_Tags.shooter)
        {
            if (currentHealth <= 9)
            {
                healthSliderImage.enabled = true;
                healthSliderImage.fillAmount = currentHealth / startingHealth;
                healthSliderImage.color = Color.Lerp(endHealth, beginHealth, currentHealth);
            }
            else
            {
                healthSliderImage.enabled = false;
            }
        }

        if (gameObject.tag == UNA_Tags.player)
        {
            WLD_GameController.ui_Images[UI_Images.FillCurrentHealth].fillAmount = currentHealth / startingHealth;

            WLD_GameController.ui_Images[UI_Images.FillCurrentHealth].color = Color.Lerp(endHealth, beginHealth, currentHealth / startingHealth);
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: HealthToUI
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/23/17
    // ------------------------------------------------------------------------------
    // Purpose: Adds the health text to the the ui
    // ------------------------------------------------------------------------------
    public void HealthToUI()
    {
        if (gameObject.tag == UNA_Tags.player)
        {
            WLD_GameController.ui_Texts[UI_Txt.CurrentHealthText].text = "Hp " + currentHealth + " / " + startingHealth.ToString();
        }

        if (healthText != null)
        {
            healthText.text = "Hp " + currentHealth.ToString() + " / " + startingHealth.ToString();

        }
    }



    void ImpactDamageNotification()
    {
        if (gameObject.tag == UNA_Tags.player)      //If we only wanted to display this for the player...if we wanted it to effect enemies too we could just get rid of this line
        {
            if (rb.velocity.magnitude > playerImpactThreshold - 3)
            {


                //ParticleSystem particle = (ParticleSystem)gameObject.GetComponent("ParticleSystem");

                //gameObject.GetComponent<ParticleSystem>().emission.enabled = true;

                ps.Play();
            }

            else if (rb.velocity.magnitude < playerImpactThreshold || currentHealth == 0)
            {
                ps.Stop();

                //gameObject.GetComponent<ParticleSystem>().emission.enabled = false;
            }

        } 
    }
}// End UNA_HealthDmg