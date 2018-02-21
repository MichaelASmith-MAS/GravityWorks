using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_Explode : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [SerializeField]
    float DamageDone;

    public GameObject explodyParent;
    public GameObject explodyGFX;

    public Collider TriggerCollider;

    public Collider ExplosionCollider;

    public Color start;

    public Color finish;

    public AudioClip playerDetectedSound;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    WLD_HealthDmg damage;

    private bool exploding = false;

    private bool boom = false;

    bool explosionDrawn = false;

    bool playerSeen = false;

    bool SeenAlready = false;

    WLD_HealthDmg hp;

    GameObject player;

    Renderer meshRenderer;

    Animator anim;

    int activated;
    Shader tempShader;
    Light enemyLight;

    AudioSource audioSource;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    #region Functions
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
        damage = WLD_GameController.player.GetComponent<WLD_HealthDmg>();

        meshRenderer = explodyGFX.GetComponent<Renderer>();
        enemyLight = explodyGFX.GetComponentInChildren<Light>();
        anim = explodyParent.GetComponentInChildren<Animator>();
        
        tempShader = meshRenderer.material.shader;

        enemyLight.enabled = false;

        meshRenderer.material.SetColor("_TintColor", start);
        
        hp = GetComponentInParent<WLD_HealthDmg>();

        player = WLD_GameController.player;

        audioSource = GetComponent<AudioSource>();

    }

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    private void Update()
    {
        if(exploding == true)
        {
            Color tempColor = Color.Lerp(start, finish, Mathf.PingPong(Time.time, 1));

            enemyLight.color = tempColor;

            meshRenderer.material.SetColor("_TintColor", tempColor);
        }

        if (transform.position != explodyParent.transform.position)
        {
            transform.position = explodyParent.transform.position;
        }

        Death();
    }

    // ------------------------------------------------------------------------------
    // Function Name: DeathTimer
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Destroys the game object upon death/when health reaches zero.
    // ------------------------------------------------------------------------------

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(0.09f);
        Destroy(explodyParent);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            //Debug.Log("Exploding!");
            StartCoroutine(Explode());
            exploding = true;
            TriggerCollider.enabled = false;
            ExplosionCollider.enabled = true;

            enemyLight.enabled = true;
            anim.SetBool("Activated", true);

            playerSeen = true;
            PlayerTriggered();

            //audioSource.PlayOneShot(playerDetectedSound);

        }
    }

    private void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.GetComponent<WLD_HealthDmg>() != null)
            {
                Vector3 rayDirection = other.transform.position - transform.position;

                //Debug.DrawRay(transform.position, rayDirection, Color.red);

                if (boom == true)
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(transform.position, rayDirection, out hitInfo))
                    {

                        hitInfo.transform.GetComponent<WLD_HealthDmg>().ChangeHealth(-DamageDone);

                        //if (hitInfo.transform.tag == UNA_Tags.player)
                        //{
                        //    player.GetComponent<WLD_HealthDmg>().ChangeHealth(-DamageDone);

                        //}
                    }

                    boom = false;
                }

            }

        }
        catch (System.NullReferenceException)
        {
            //Debug.LogError("There is no damage script on " + other.name);
            
        }
                
    }

    void PlayerTriggered()
    {
        if (!SeenAlready)
        {
            if (playerSeen)
            {
                audioSource.PlayOneShot(playerDetectedSound);
                SeenAlready = true;
            }
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Death
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Checks to see if the object's heath is at zero
    // ------------------------------------------------------------------------------

    private void Death()
    {
        if(hp.Health <= 0)
        {
            StartCoroutine(DeathTimer());
            ExplosionCollider.enabled = true;
            boom = true;
            DrawExplosion();
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: IENumerator
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/11/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(2);
        boom = true;
        DrawExplosion();

        yield return new WaitForSeconds(0.05f);
        Destroy(explodyParent);
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/11/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void DrawExplosion ()
    {
        if (!explosionDrawn)
        {
            GameObject go = Instantiate(Resources.Load("Imports/SimpleParticlePack/Resources/Explosions/Explosion01a"), transform.position, transform.rotation) as GameObject;

            //Destroy(go, go.GetComponent<ParticleSystem>().time);

            explosionDrawn = true;
        }
        
    }

    #endregion
} // End ENM_Explode