/* -----------------------------------------------------------------------------------
 * Class Name: WLD_DynamicEnemySpawn
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

public class WLD_DynamicEnemySpawn : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public int spawnTier1PointLimit = 2000, spawnTier2PointLimit = 4000, spawnTier3PointLimit = 6000, spawnTier4PointLimit = 8000, spawnTier5PointLimit = 10000;
    public int tier1SpawnerActivations = 10, tier2SpawnerActivations = 14, tier3SpawnerActivations = 18, tier4SpawnerActivations = 20, tier5SpawnerActivations = 24;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [SerializeField] WLD_EnemySpawner[] spawners;

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

    private void Start()
    {
        spawners = FindObjectsOfType<WLD_EnemySpawner>();

        SelectActiveLevelSpawners();
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

    void SelectActiveLevelSpawners ()
    {
        int points = WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints;

        if (points < spawnTier1PointLimit)
        {
            ActivateSpawners(tier1SpawnerActivations);

        }

        else if (points < spawnTier2PointLimit)
        {
            ActivateSpawners(tier2SpawnerActivations);

        }

        else if (points < spawnTier3PointLimit)
        {
            ActivateSpawners(tier3SpawnerActivations);

        }

        else if (points < spawnTier4PointLimit)
        {
            ActivateSpawners(tier4SpawnerActivations);

        }

        else if (points < spawnTier5PointLimit)
        {
            ActivateSpawners(tier5SpawnerActivations);

        }

        else
        {
            ActivateSpawners(spawners.Length);

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

    void ActivateSpawners (int spawnActivations)
    {
        for (int i = 0; i < spawnActivations; i++)
        {
            int rand;

            do
            {
                rand = Random.Range(0, spawners.Length);

            }
            while (spawners[rand] == null);

            spawners[rand].SpawnEnemy();
            spawners[rand] = null;

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

} // End WLD_DynamicEnemySpawn