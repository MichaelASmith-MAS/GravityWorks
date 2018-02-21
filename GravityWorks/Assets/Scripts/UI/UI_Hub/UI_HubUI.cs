/* -----------------------------------------------------------------------------------
 * Class Name: UI_HubUI
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: controls the hub ui
 * -----------------------------------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UI_HubUI : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Gameobjects")]
    public GameObject menuPanel;
    public GameObject[] segmentNumber;
    public GameObject shopPanel;
  
    [Header("Buttons")]
    public Button segmentOneButton;
    public Button[] segmentButtons;

    [Header("Renderers")]
    public Renderer teleporterRenderer;

    public Material startingMaterial;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private int levelSeg;
    private WLD_LevelLoader wld_LevelLoader;
    private WLD_Teleporter wld_Teleporter;
    private int i;
    EventSystem ev;

    bool isShopping;

    float r = 0f / 255.0f;
    float g = 213f / 255.0f;
    float b = 255f / 255.0f;
    float a = 25f / 255.0f;

    //float offR = 200f / 255f;
    //float offG = 0f / 255f;
    //float offB = 0f / 255f;
    //float offA = 25f / 255f;
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

    void Start()
    {
        wld_Teleporter = GetComponent<WLD_Teleporter>();

        levelSeg = WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelSegments.Length;

        teleporterRenderer = GetComponent<Renderer>();

        segmentButtons = new Button[segmentNumber.Length];


        for (int i = 0; i < segmentNumber.Length; i++)
        {
            segmentButtons[i] = segmentNumber[i].GetComponent<Button>();

            segmentNumber[i].SetActive(false);
            segmentButtons[i].interactable = false;
        }

        shopPanel.SetActive(false);

        ev = GameObject.Find("SegmentEventSystems").GetComponent<EventSystem>();


    }
       
    // ------------------------------------------------------------------------------
    // Function Name: SelectSegment
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/12/17 
    // ------------------------------------------------------------------------------
    // Purpose: passes in a segment number
    // ------------------------------------------------------------------------------
    public void SelectSegment(int i)
    {
        wld_Teleporter.LoadLevelSegment(i);
    }

    // ------------------------------------------------------------------------------
    // Function Name: SelectSegment
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/12/17 
    // ------------------------------------------------------------------------------
    // Purpose: controls what panels are shown depending on how many segments are in a level
    // ------------------------------------------------------------------------------
    //&& wld_Teleporter.levelToLoad == Scenes.JS_1021
    private void OnTriggerEnter(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            if (WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelUnlocked)
            {
                ev.enabled = true;

                for (int index = 0; index < WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelSegments.Length; index++)
                {
                    int tempIndex = index + 1;

                    segmentNumber[index].SetActive(true);

                    segmentButtons[index].onClick.AddListener(() => 
                    {
                        Debug.Log("Segment " + tempIndex + " of teleporter " + wld_Teleporter.levelToLoad.ToString() + " clicked.");
                        wld_Teleporter.LoadLevelSegment(tempIndex);
                        WLD_CameraFollow_SL.inTeleporter = false;
                        UI_Pause.isPaused = true;

                    });
                }

                if (!WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelSegments[0].SegmentComplete)
                {
                    segmentButtons[0].interactable = true;

                }

                else
                {
                    segmentButtons[0].interactable = true;

                    for (int i = 0; i < WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelSegments.Length; i++)
                    {
                        try
                        {
                            segmentButtons[i + 1].interactable = WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelSegments[i].SegmentComplete;
                        }
                        catch (System.NullReferenceException)
                        {

                        }
                    }
                }
            }
            else
            {
                if (!WLD_GameController.levels[wld_Teleporter.levelToLoad].LevelUnlocked)
                {
                    shopPanel.SetActive(true);

                    shopPanel.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, -6);

                }
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

    void OnTriggerStay(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            menuPanel.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, -6);

            shopPanel.transform.position = new Vector3(player.transform.position.x - 2, player.transform.position.y, -6);


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

    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            ev.enabled = false;

            for (int i = 0; i < segmentNumber.Length; i++)
            {
                segmentNumber[i].SetActive(false);
                segmentButtons[i].interactable = false;

                segmentButtons[i].onClick.RemoveAllListeners();

            }

            shopPanel.SetActive(false);
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


} // End UI_HubUI