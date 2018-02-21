/* -----------------------------------------------------------------------------------
 * Class Name: WLD_EnemySpawner
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WLD_EnemySpawner : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    
    

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] EnemyTypes enemyType = EnemyTypes.Shooter;
    [SerializeField] ENM_HeatEmitterSensor matchedCamera;
    [SerializeField] ENM_HeatEmittor matchedHeatEmitter;

    #endregion

    #region Getters/Setters

    public EnemyTypes EnemyType
    {
        get { return enemyType; }
    }

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

    public bool SpawnEnemy ()
    {
        GameObject go;

        switch (enemyType)
        {
            case EnemyTypes.PowerBox:
                go = SpawnEnemyOfType("Prefabs/EnemyPrefabs/PowerBox");

                go.GetComponent<ENM_HeatEmitterPowerBox>().heatEmitter = matchedHeatEmitter.gameObject;
                matchedHeatEmitter.gameObject.SetActive(true);

                go.GetComponent<ENM_HeatEmitterPowerBox>().cameraTrigger = matchedCamera.gameObject;
                matchedCamera.gameObject.SetActive(true);

                return true;

            case EnemyTypes.Shooter:
                go = SpawnEnemyOfType("Prefabs/EnemyPrefabs/Shooter");

                go.transform.GetChild(0).GetChild(0).GetComponent<WLD_HealthDmg>().MaxHealth = Random.Range(5, 10);
                go.transform.GetChild(0).GetChild(0).GetComponent<WLD_HealthDmg>().Health = go.transform.GetChild(0).GetChild(0).GetComponent<WLD_HealthDmg>().MaxHealth;
                go.transform.GetChild(0).GetChild(1).GetComponent<ENM_Shoot>().Delay = Random.Range(1, 2);
                go.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().freezeRotation = true;
                go.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().useGravity = false;

                return true;

            case EnemyTypes.Explody:
                go = SpawnEnemyOfType("Prefabs/EnemyPrefabs/Explody");

                go.GetComponent<Rigidbody>().freezeRotation = true;
                go.GetComponent<Rigidbody>().useGravity = false;

                return true;

            default:
                break;
        }


        return false;
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

    GameObject SpawnEnemyOfType (string path)
    {

        GameObject go = Instantiate(Resources.Load(path), transform.position, Quaternion.identity) as GameObject;
        go.transform.GetChild(0).transform.rotation = transform.rotation;

        if (go != null)
        {
            return go;
        }

        return go;
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
} // End WLD_EnemySpawner