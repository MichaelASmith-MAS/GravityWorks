/* -----------------------------------------------------------------------------------
 * Class Name: ENM_Bullet
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
using System.Collections;
using System.Collections.Generic;

public class ENM_BulletTest : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Speed")]
    [SerializeField]
    private float speed = 70f;

    [Header("GameObjects")]
    [SerializeField]
    private GameObject impactEffect;
    [SerializeField]
    private Transform firePoint;

    [Header("Damage")]
    [SerializeField]
    private float explosionRaduis = 0f;
    [SerializeField]
    private int projectileDmg = 20;
    [SerializeField]
    private int shieldDmg = 5;

    [Header("Time")]
    [SerializeField]
    private float destroyEffectTime = 4f;

    [Header("PowerUI")]
    public float addPowerUpPoints = 1f;

    private Transform target;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------



    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void Update()
    {
        ProjectileMove();
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void ProjectileMove()
    {
        if (target == null)
        {
            Destroy(gameObject);

            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancethisframe = speed * Time.deltaTime;

        //if (dir.magnitude <= distancethisframe)
        //{
        //    HitTarget();
        //    return;
        //}

        transform.Translate(dir.normalized * distancethisframe, Space.World);
        transform.LookAt(target);
    }

    //void HitTarget()
    //{
    //    GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
    //    Destroy(effectInstance, destroyEffectTime);


    //    if (explosionRaduis > 0)
    //    {
    //        Explode();
    //    }
    //    else
    //    {
    //        Damage(target);
    //    }

    //    Destroy(gameObject);
    //    return;
    //}

    //void Explode()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRaduis);

    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.tag == "Enemy")
    //        {
    //            Damage(collider.transform);
    //        }
    //    }
    //}

    //void Damage(Transform enemy)
    //{
    //    Enemy e = enemy.GetComponent<Enemy>();
    //    Shield s = enemy.GetComponent<Shield>();


    //    if (e != null && s.shieldDestroyed)
    //    {
    //        e.takeDamage(projectileDmg);

    //        PowerUp.powerUpPoints += addPowerUpPoints;
    //    }

    //    if (s != null)
    //    {
    //        s.shieldTakeDamage(shieldDmg);
    //    }
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRaduis);
    }


} // End ENM_Bullet