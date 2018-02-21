/* -----------------------------------------------------------------------------------
 * Class Name: WLD_SegmentCost
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: #DATE#
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class WLD_SegmentCost : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Level Cost")]
    public int jA_0629_Cost;
    public int kL_0602_Cost;
    public int jS_1021_Cost;

    [Header("Buttons")]
    public Button buyJAButton;
    public Button buyKLButton;
    public Button buyJSButton;

    [Header("GameObjects")]
    public GameObject jaGO;
    public GameObject klGO;
    public GameObject jsGO;
    public GameObject shopMenu;

    [Header("Text")]
    public Text costText;
    public Text walletText;

    [Header("EventSystems")]
    public EventSystem kLshopEventSystem;
    public EventSystem jAShopEventSystem;
    public EventSystem jSShopEventSystem;
    public EventSystem finalShopEventSystem;
    public EventSystem eventSystems;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    GameObject player;
    WLD_Teleporter teleporter;
    UI_HubUI hub_ui;
    public static bool canBuyJA = true, canBuyKL = true, canBuyJS = true;

    #endregion

    #region Getters/Setters



    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    void Start()
    {
        player = WLD_GameController.player;

        teleporter = GetComponent<WLD_Teleporter>();
        hub_ui = GetComponent<UI_HubUI>();

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
    //void Update()
    //{
    //    ShowText();
    //}
    //void ShowText()
    //{       
    //    walletText.text = "Pts " + WLD_GameController.player.GetComponent<PLR_Points>().OverallPoints.ToString();
    //}

    void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            eventSystems.enabled = false;
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
    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {

            if (teleporter.levelToLoad == Scenes.JA_0629 && !WLD_GameController.levels[Scenes.JA_0629].LevelUnlocked)
            {
                costText.text = "Pts req. " + jA_0629_Cost.ToString();     

                buyJAButton.interactable = !WLD_GameController.levels[Scenes.JA_0629].LevelUnlocked;
                buyKLButton.interactable = false;
                buyJSButton.interactable = false;

                jAShopEventSystem.enabled = !WLD_GameController.levels[Scenes.JA_0629].LevelUnlocked;

                kLshopEventSystem.enabled = false;
                jSShopEventSystem.enabled = false;
                finalShopEventSystem.enabled = false;

                jaGO.SetActive(!WLD_GameController.levels[Scenes.JA_0629].LevelUnlocked);
                klGO.SetActive(false);
                jsGO.SetActive(false);

                //player.GetComponent<PLR_Jump>().enabled = false;
            }
            else if (teleporter.levelToLoad == Scenes.KL_0602 && !WLD_GameController.levels[Scenes.KL_0602].LevelUnlocked)
            {
                costText.text = "Pts req. " + kL_0602_Cost.ToString();

                buyKLButton.interactable = !WLD_GameController.levels[Scenes.KL_0602].LevelUnlocked;
                buyJAButton.interactable = false;
                buyJSButton.interactable = false;

                kLshopEventSystem.enabled = !WLD_GameController.levels[Scenes.KL_0602].LevelUnlocked;

                jAShopEventSystem.enabled = false;
                jSShopEventSystem.enabled = false;
                finalShopEventSystem.enabled = false;

                jaGO.SetActive(false);
                klGO.SetActive(!WLD_GameController.levels[Scenes.KL_0602].LevelUnlocked);
                jsGO.SetActive(false);

                //player.GetComponent<PLR_Jump>().enabled = false;
            }
            else if (teleporter.levelToLoad == Scenes.JS_1021 && !WLD_GameController.levels[Scenes.JS_1021].LevelUnlocked)
            {
                costText.text = "Pts req. " + jS_1021_Cost.ToString();

                buyJSButton.interactable = !WLD_GameController.levels[Scenes.JS_1021].LevelUnlocked;
                buyJAButton.interactable = false;
                buyKLButton.interactable = false;

                jSShopEventSystem.enabled = !WLD_GameController.levels[Scenes.JS_1021].LevelUnlocked;

                kLshopEventSystem.enabled = false;
                jAShopEventSystem.enabled = false;
                finalShopEventSystem.enabled = false;

                jaGO.SetActive(false); klGO.SetActive(false); jsGO.SetActive(true);

                //player.GetComponent<PLR_Jump>().enabled = false;
            }
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
    void OnTriggerExit()
    {
        if (player.tag == UNA_Tags.player)
        {
            eventSystems.enabled = true;

            kLshopEventSystem.enabled = false;
            jAShopEventSystem.enabled = false;
            jSShopEventSystem.enabled = false;
            finalShopEventSystem.enabled = false;
            eventSystems.enabled = false;
            buyJAButton.onClick.RemoveAllListeners();
            buyKLButton.onClick.RemoveAllListeners();
            buyJSButton.onClick.RemoveAllListeners();

            //player.GetComponent<PLR_Jump>().enabled = true;
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
    public void JA_0629()
    {
        if (player.GetComponent<PLR_Points>().OverallPoints >= jA_0629_Cost && canBuyJA)
        {
            WLD_HubEffectContoller.isJaOn = true;

            player.GetComponent<PLR_Points>().PointRemoval(jA_0629_Cost);

            WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked = true;

            shopMenu.SetActive(!WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked);
      
            canBuyJA = !WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked;

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
    public void KL_0602()
    {
        if (player.GetComponent<PLR_Points>().OverallPoints >= kL_0602_Cost && canBuyKL)
        {
            WLD_HubEffectContoller.isKlOn = true;

            player.GetComponent<PLR_Points>().PointRemoval(kL_0602_Cost);

            WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked = true;

            shopMenu.SetActive(!WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked);
  
            canBuyKL = !WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked;
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
    public void JS_1021()
    {
        if (player.GetComponent<PLR_Points>().OverallPoints >= jS_1021_Cost && canBuyJS)
        {
            WLD_HubEffectContoller.isJsON = true;

            player.GetComponent<PLR_Points>().PointRemoval(jS_1021_Cost);

            WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked = true;

            shopMenu.SetActive(!WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked);
 
            canBuyJS = !WLD_GameController.levels[teleporter.levelToLoad].LevelUnlocked;
        }
    }
} // End WLD_SegmentCost