/* -----------------------------------------------------------------------------------
 * Class Name: UNA_SaveLoadController
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UNA_SaveLoadController
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

    public static string profileName = "";

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

    public static void ChangeProfileName (string name)
    {
        profileName = name;
        
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

    public static void SavePlayer (Dictionary<Scenes, UNA_Level> saveData, bool[] endPieces, float playerHealth, int overallPoints, Dictionary<Gravity, bool> gravitySettings)
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (profileName == "")
        {
            string[] savedGames = System.IO.Directory.GetFiles(Application.persistentDataPath + "/", "*.blr", SearchOption.TopDirectoryOnly);

            if (savedGames.Length > 1)
            {

                for (int i = 0; i < savedGames.Length; i++)
                {
                    string[] splitString = savedGames[i].Split('/', '.');

                    for (int z = 0; z < splitString.Length; z++)
                    {
                        if (splitString[z].Contains("AutoSave "))
                        {
                            ChangeProfileName(splitString[splitString.Length - 2]);
                            break;
                        }

                    }

                    if (profileName != "")
                    {
                        break;

                    }

                }
            }

            if (profileName == "")
            {
                string currentDateTime = DateTime.Now.ToString();

                string[] tempString = currentDateTime.Split('/', ':');

                profileName = "AutoSave " + string.Join("-", tempString);

            }
            
        }

        FileStream stream = new FileStream(Application.persistentDataPath + "/" + profileName + ".blr", FileMode.Create);

        SaveData data = new SaveData(saveData, endPieces, playerHealth, overallPoints, gravitySettings);

        bf.Serialize(stream, data);
        stream.Close();

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

    public static bool LoadPlayer (string gamePath, out Dictionary<Scenes, UNA_Level> saveData, out bool[] endPieces, out float playerHealth, out int overallPoints, out Dictionary<Gravity, bool> gravitySettings)
    {
        SaveData data;

        if (File.Exists(gamePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(gamePath, FileMode.Open);

            data = bf.Deserialize(stream) as SaveData;

            stream.Close();

            saveData = data.saveData;
            endPieces = data.endPieces;
            playerHealth = data.playerHealth;
            overallPoints = data.overallPoints;
            gravitySettings = data.gravitySettings;

            return true;

        }

        else
        {
            saveData = new Dictionary<Scenes, UNA_Level>();
            gravitySettings = new Dictionary<Gravity, bool>();
            endPieces = new bool[7];
            playerHealth = 0f;
            overallPoints = 0;

            return false;
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

} // End UNA_SaveLoadController

[Serializable]
public class SaveData
{
    public Dictionary<Scenes, UNA_Level> saveData;
    public Dictionary<Gravity, bool> gravitySettings;
    public bool[] endPieces;
    public float playerHealth;
    public int overallPoints;

    public SaveData ()
    {
        saveData = new Dictionary<Scenes, UNA_Level>();
        gravitySettings = new Dictionary<Gravity, bool>();
        endPieces = new bool[7];
        playerHealth = 0;
        overallPoints = 0;

    }

    public SaveData (Dictionary<Scenes, UNA_Level> saveData, bool[] endPieces, float playerHealth, int overallPoints, Dictionary<Gravity, bool> gravitySettings)
    {
        this.saveData = saveData;
        this.endPieces = endPieces;
        this.playerHealth = playerHealth;
        this.overallPoints = overallPoints;
        this.gravitySettings = gravitySettings;

    }

}