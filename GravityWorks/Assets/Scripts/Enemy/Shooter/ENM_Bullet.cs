using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ENM_Bullet : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [SerializeField]
    [Range(1, 20)]
    float MoveSpeed = 10.0f;

    [SerializeField]
    float DamageDone;


    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    GameObject player;
    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Start/Update
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 10/11/17
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void Start()
    {  

        player = WLD_GameController.player;
    }


    void Update()
    {
        Move();
    }

    // ------------------------------------------------------------------------------
    // Function Name: Move
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 1-/11/17
    // ------------------------------------------------------------------------------
    // Purpose: This will move the bullet forward from where it's shot
    // ------------------------------------------------------------------------------

    private void Move()
    {
        transform.position += transform.forward * Time.deltaTime * MoveSpeed;
        Destroy(gameObject, 15.0f);
    }

    // ------------------------------------------------------------------------------
    // Function Name: Trigger
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == UNA_Tags.player)
        {
            player.GetComponent<WLD_HealthDmg>().ChangeHealth(DamageDone);

            Destroy(gameObject);
        }
        if(other.tag == UNA_Tags.wall)
        {
            Destroy(gameObject);
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


} // End ENM_Bullet