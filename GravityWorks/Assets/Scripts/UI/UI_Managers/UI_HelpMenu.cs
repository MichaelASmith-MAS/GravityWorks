/* -----------------------------------------------------------------------------------
 * Class Name: UI_HelpMenu
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum HelpTopic { Interact, Movement, EnvironmentalGravity, SelfShoot, Ggun, GgunSelect, GravityDifference, HealthCollectible, ScoreCollectible,
                BreakableSurfaces, Shoot, GravityEffectTimeCollectible, InvertedGravity, FireRateCollectible, PushPull, DeathVolume,
                PressurePlates, IceVolumes, SecurityCameraEnemy, PowerBox, Teleporter, DamageVolume, ExploderEnemy, ShooterEnemy }

public class UI_HelpMenu : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public GameObject topicTitle, topicContent, pausedMenu;

    public Dictionary<string, string> helpTopicList = new Dictionary<string, string>();

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

    private void Start()
    {
        if (helpTopicList.Count < 1)
        {
            PopulateValues();
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

    public void CancelButton ()
    {
        pausedMenu.SetActive(true);
        gameObject.SetActive(false);

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

    public void HelpTitle (string title)
    {
        topicTitle.GetComponentInChildren<Text>().text = title;
        topicContent.GetComponentInChildren<Text>().text = helpTopicList[title];

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

    void PopulateValues ()
    {
        helpTopicList.Add("Interact", "Use the interact button ('F' for keyboard control; 'A' for controller)" +
            "to progress through messages and interact with specified objects in the world. Hold the interact button to auto-scroll through" +
            "the messages on screen.");

        helpTopicList.Add("Movement", "Use 'A' and 'D' on the keyboard or the left joystick on a controller to move the player left" +
            "and right, respecively. Using 'W' and 'S' on the keyboard or the right joystick on a controller will shift the camera up" + 
            "and down to see what is above and below the player.");

        helpTopicList.Add("Environmental Gravity", "Every segment of a level will have a gravity setting that affects the player and the objects around it."+
            "Airlocks will have lights and an information pop up that will inform the player of the gravity they will be entering.");

        helpTopicList.Add("Self Shoot", "The spacebar on a keyboard or the 'A' button on a controller will allow the player to use the gravity gun on themselves." +
            "The resulting effect is dependant on the gGun's selected gravity setting. When the player uses the self shoot, depending on the environmental gravity they're in," +
            "they will either be pushed into the ground or propelled into the air.");

        helpTopicList.Add("Ggun", "The gravity gun selection can be accessed by selecting the 'gGun' button at the bottom of the HUD. " +
            "The gravity gun changes the gravity affect applied to the player if performing a self-shoot, or the gravity applied to an enemy or " +
            "piece of debris, based on the gravity setting selected and the environmental gravity surrounding the player/object.");

        helpTopicList.Add("Ggun Select", "The gravity selection applied to the gravity gun can be changed in a few ways. On a keyboard" +
            " the gravity selection can be changed with the number keys at the top of the keyboard, or by using the scroll wheel on the mouse." +
            "The gravity can also be selected with the mouse on the gGun panel to the left of their UI." +
            " If using a controller, the 'Y' and 'B' buttons will allow the player to go through their gravity settings. The selected gravity is used when shooting " +
            "enemies and debris, or performing a self-shoot.");

        helpTopicList.Add("Gravity Difference", "In the HUD/UI is a small bar with a rock on one end and a cloud on the other. This is the " +
            "difference in gravity between the environmental gravity and the gravity selected for the gravity gun. The closer the blue bar is to " +
            "one end, the heavier or lighter the gun setting is. For example, if the player is currently in Neptune gravity, but has the Earth " +
            "gravity selected in the gun, the bar will be close to the center, but if the player changed their selected gravity to the moon, " +
            "this bar would be closer to the cloud, denoting a much lower gravity has been selected.");

        helpTopicList.Add("Health Collectible", "If damaged, pickups exist that give the player health (See Below)" + 
            "Collecting one gives an instant increase in health, up to the maximum.");

        helpTopicList.Add("Score Collectible", "Score collectibles give 100 points to the player's score. Their overall score can be seen at the top" +
            " of the screen.");

        helpTopicList.Add("Breakable Surfaces", "Orange walls and floors are weak surfaces that can be broken with the appropriate force " +
            "application.");

        helpTopicList.Add("Shoot", "Pressing the shoot button (left click on the mouse; 'X' on a controller) will fire a projectile in the " +
            "direction the player is facing. This shot will change the gravity associated to the object hit based on the Ggun selection " +
            "active when firing.");

        helpTopicList.Add("Gravity Effect Time Collectible", "The gravity effect time collectible will temporarily increase the effect gravity " +
            "has on both the player and objects shot with the gun. (See Below) The pickup will take effect immediately when collected, but will respawn after a short time.");

        helpTopicList.Add("Inverted Gravity", "Purple zones in the world are where gravity is inverted. When in these areas, up is down, and " +
            "down is up. Using a lighter gravity in one of these zones will make you fall, instead of rise.");

        helpTopicList.Add("Fire Rate Collectible", "The fire rate collectible will temporarily decrease the cooldown rate of the gravity gun.(See Below) " +
            "Once collected it will activate, but it will respawn after a short amount of time .");

        helpTopicList.Add("Push / Pull", "When shooting, the gravity gun defaults to only apply a different gravity. The gun shot can also " +
            "apply a directional force. Holding down the 'Q' key on a keyboard or left trigger on a controller will cause a shot object to " +
            "travel left, whereas using 'E' on a keyboard or the right trigger will cause the object to travel right.");

        helpTopicList.Add("Death Volume", "Red areas in the world cause you to immediately lose all of your health when entering, so it's best " +
            "to not.");

        helpTopicList.Add("Dash", "Dashing will move the player quickly in the direction currently traveling for a short boost of speed. Dash " +
            "is used by either holding left shift on a keyboard or the right bumper on a controller. It is subjected to a short cooldown once " +
            "used, so do so wisely.");

        helpTopicList.Add("Pressure Plates", "Pressure plates require specific forces to manipulate. Either jumping on this plate, or causing " +
            "debris to sit on top can push this plate down. Pressure plates are typically used to open doors.");

        helpTopicList.Add("Ice Blocks", "Ice blocks sometimes block your path. These can only be removed by using the heat from a camera enemy.");

        helpTopicList.Add("Security Camera Enemy", "The security camera searches for intruders and will start turning up the heat if you are " +
            "spotted. (See Below for Camera) This temperature increase cannot be withstood for long, so either turn off the powerbox or escape the affected area to " +
            "stop taking damage. Powerboxes are typically placed nearby and are topped with a green light.");

        helpTopicList.Add("Teleporter", "Teleporters are both a traversal method that allows travel between the hub and a level and a checkpoint " +
            "system. Travel through a teleporter, by interacting with it, will auto-save the game and send you back to the hub from within a " +
            "level. If in the hub, you are given a selection of segments you can access. If you have passed some of the segments in a level, " +
            "but not all, you will only be able to select those that you have entered before.");

        helpTopicList.Add("Damage Volume", "Orange areas in the world will deal a significant amount of damage when touched.");

        helpTopicList.Add("Exploding Enemy", "Exploding enemies work like proximity mines, exploding after a short period if you get too close. " +
            "The explosions of these enemies will instantly kill you, causing you to respawn at the last teleporter you came across. (See Below)");

        helpTopicList.Add("Shooter Enemy", "Shooter enemies are small turrets that shoot at you if within their line of sight. Each shot hit " +
            " removes a small portion of health. (See Below)");

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
} // End UI_HelpMenu