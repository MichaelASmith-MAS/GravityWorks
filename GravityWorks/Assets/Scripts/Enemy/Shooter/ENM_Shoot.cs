using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_Shoot : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject bullet;
    public GameObject shooterRB, barrel;
    //public AudioClip shootSound;

    [SerializeField]
    [Range(0, 1)]
    float delay = 5.0f;

    public bool shooting = false;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private float Timer;

    WLD_HealthDmg hp;

    //AudioSource audioSource;

    #endregion

    #region Getters/Setters

    public float Delay
    {
        get { return delay; }
        set { delay = value; }
    }

    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    public void Update()
    {
        if (transform.position != shooterRB.transform.position)
        {
            transform.position = shooterRB.transform.position;
        }

        Shooting();
        Death();
    }

    // ------------------------------------------------------------------------------
    // Function Name: Start
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    public void Start()
    {
        hp = shooterRB.GetComponent<WLD_HealthDmg>();

        //audioSource = GetComponent<AudioSource>();

    }

    // ------------------------------------------------------------------------------
    // Function Name: Shot
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: Spawns the bullet when called.
    // ------------------------------------------------------------------------------
    public void Shot()
    {
        GameObject shockeffect = Instantiate(Resources.Load("Prefabs/Particles/GunEffect/EnemyShotEffect"), barrel.transform.position, barrel.transform.rotation) as GameObject;

        Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);

        //audioSource.PlayOneShot(shootSound);

        Destroy(shockeffect, 2f);
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
        Destroy(transform.parent.gameObject);
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
        if (hp.Health <= 0)
        {
            StartCoroutine(DeathTimer());
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: FireRate
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/7/17
    // ------------------------------------------------------------------------------
    // Purpose: Limits the spawn rate of the Shot
    // ------------------------------------------------------------------------------
    public void FireRate()
    {
        if(Timer >= delay)
        {
            Shot();
            Timer = 0;
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: Shooting
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/14/17
    // ------------------------------------------------------------------------------
    // Purpose: Regulates how long until another shot is instantiated
    // ------------------------------------------------------------------------------
    public void Shooting()
    {
        if (shooting == true)
        {
            FireRate();
            Timer += Time.deltaTime;
        }
    }

} // End ENM_Shoot