/* -----------------------------------------------------------------------------------
 * Class Name: WLD_GameController
 * -----------------------------------------------------------------------------------
 * Author: Michael Smith
 * Date: 9/25/2017
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: Maintains all level and segment data structure information; retains enums 
 *          for both the Gravity ans Scenes, as well as the gravity values for use throughout
 *          the game. Cannot be destroyed when switching scenes.
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public enum Gravity { Earth, Mars, Ceres, Uranus, Jupiter, Pluto, Moon, Neptune }
public enum Scenes { Tutorial, JA_0629, KL_0602, JS_1021, /*MS_0211,*/ DW_0713, /*GW_0202,*/ Final, MainMenu, Credits, Controls, LoadGame, Hub, TempFillIn }
public enum UI_GO_Panels {GravityPanel, GravityPanelSmallGo, GravityPanelShrink, GravityActivatedPanel, ShrinkImageFillIn,
                            CurrentGGunSelectionPanel, DmgInputUI, ExtremeTempWarning, TemperatureGuagePanel, PausePanel, MenuPanel, TransitionPanel}
public enum UI_Txt {  CurrentHealthText, SelectedPlanetText, ActivatedText, CurrentGGunSelectionText, TransitionPlanetTxt,
                     CurrentLevelTxt, CurrentTime, CurrentScore, ActivateButtonText, GravityPickUpSliderText, FireRatePickUPSliderText, SaveGameNotificationText, DamageText}

public enum UI_Images { CurrentPlanetImg, FillCurrentHealth, CurrentGGunSelectionImage, FillHot, DamageOverlay }
public enum UI_Buttons {GgunPanEarth, GgunPanMoon, GgunPanCeres, GgunPanMars, GgunPanNeptune, GgunPanUranus, GgunPanJupiter, GgunPanelPluto, RestartButton, SkipButton, GravityPanelSmall }
public enum EnemyTypes { PowerBox, Shooter, Explody }
public enum Animations { GgunPanelAnimator, CeresAnimator, PlutoAnimator, MoonAnimatorn, MarsAnimator, UranusAnimator, EarthAnimator, NeptuneAnimator, JupiterAnimator, FrostOverlay, GgunEnlargeAnimator, GgunShrinkAnimator, GgunOutlineAnimator }

public class WLD_GameController : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------

    public static Dictionary<Scenes, UNA_Level> levels;

    //Thes are used for the Environment panels Current and Upcoming titles and values
    public static Dictionary<Gravity, float> gravityValues = new Dictionary<Gravity, float>();
    public static Dictionary<Gravity, string> gravityTitles = new Dictionary<Gravity, string>();

    //Thes are used for the save system
    public static Dictionary<Gravity, bool> gravitySettingDictionary;

    //Thes are used for the Environment panels Current and Upcoming PlanetImages
    public static Dictionary<Gravity, Color32> gravityImages = new Dictionary<Gravity, Color32>();
    public static Dictionary<Gravity, GameObject> upcomingGravityImages = new Dictionary<Gravity, GameObject>();

    //Thes are used for all the UI GameObjects, Texts, and Images
    public static Dictionary<UI_GO_Panels, GameObject> ui_GameObjects = new Dictionary<UI_GO_Panels, GameObject>();
    public static Dictionary<UI_Txt, Text> ui_Texts = new Dictionary<UI_Txt, Text>();
    public static Dictionary<UI_Images, Image> ui_Images = new Dictionary<UI_Images, Image>();
    public static Dictionary<UI_Buttons, Button> ui_Button = new Dictionary<UI_Buttons, Button>();

    public static Dictionary<Animations, Animator> animators = new Dictionary<Animations, Animator>();

    public static WLD_GameController controller;
    public static UNA_Level activeLevel;
    public static GameObject player;
    public static GameObject uIButtonManager;
    public static bool[] endPieces;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    [Header("Main Canvas")]
    [SerializeField]
    static GameObject mainUICanvas;

    [Header("UI GravityGunHud GameObjects")]
    [SerializeField]
    private GameObject gravityGunOverViewPanel;
    [SerializeField]
    private Button gravityPanelEnlarge;
    [SerializeField]
    private GameObject gravityPanelEnlargeGo;
    [SerializeField]
    private GameObject gravityPanelShrink;
    [SerializeField]
    private GameObject gravitySelectNotificationPanel;
    [SerializeField]
    private GameObject currentGGunSelectionPanel;

    [Header("UI Temperature GameObjects")]
    [SerializeField]
    private GameObject extremeTempWarning;
    [SerializeField]
    private GameObject temperatureGuagePanel;

    [Header("Transition Panel GameObjects")]
    [SerializeField]
    private GameObject transitionOverlay;

    [Header("UI Gun Panel Buttons")]
    [SerializeField]
    private Button earthButton;
    [SerializeField]
    private Button moonButton;
    [SerializeField]
    private Button marsButton;
    [SerializeField]
    private Button ceresButton;
    [SerializeField]
    private Button jupiterButton;
    [SerializeField]
    private Button uranusButton;
    [SerializeField]
    private Button neptuneButton;
    [SerializeField]
    private Button plutoButton;

    [Header("UI restart Buttons")]
    [SerializeField]
    private Button restartButton;

    [Header("Tutorial Skip Button")]
    [SerializeField]
    private Button tutorialButton;

    [Header("UI Pause Panel GameObjects")]
    [SerializeField]
    private GameObject pausePanel;

    [Header("UI Hub Panel GameObjects")]
    [SerializeField]
    private GameObject menuPanel;

    [Header("UI transition Text Objects")]
    [SerializeField]
    private Text transitionPlanetText;

    [Header("UI HealthBar Text Objects")]
    [SerializeField]
    private Text currentHealthText;

    [Header("UI GravityGunHud Text Objects")]
    [SerializeField]
    private Text planetText;
    [SerializeField]
    private Text activatedText;
    [SerializeField]
    private Text currentGGunSelectionText;

    [Header("UI CurrentLevel Text Objects")]
    [SerializeField]
    private Text currentLevel;

    [Header("UI Current Time/Score Text Objects")]
    [SerializeField]
    private Text currentTime;
    [SerializeField]
    private Text currentScore;

    [Header("PickUp Slider Text")]
    [SerializeField]
    private Text gravityPickUpSliderText;
    [SerializeField]
    private Text fireRatePickUPSliderText;

    [Header("Save Game Notification Text")]
    [SerializeField]
    private Text saveGameNotificationText;

    [Header("Player Dmg Text")]
    [SerializeField]
    private Text damageText;

    [Header("Player Dmg Overlay")]
    [SerializeField]
    private Image damageOverlay;

    [Header("Transition UI Evnironment Image Objects")]
    [SerializeField]
    private Image currentPlanetImageColor;

    [Header("UI GravityGunHud Image Objects")]
    [SerializeField]
    private Image currentGGunSelectionPlanet;

    [Header("UI HealthBar Image Objects")]
    [SerializeField]
    private Image fillCurrentHealth;

    [Header("UI Tempurature Objects")]
    [SerializeField]
    private Image fillhot;

    [Header("UI Shrink Fill In")]
    [SerializeField]
    private GameObject shrinkFillIn;

    [Header("Ggun Animator")]
    [SerializeField]
    private Animator gGunPanelAnimator;
    [SerializeField]
    private Animator ceresButtonAnimator;
    [SerializeField]
    private Animator plutoButtonAnimator;
    [SerializeField]
    private Animator moonButtonAnimator;
    [SerializeField]
    private Animator marsButtonAnimator;
    [SerializeField]
    private Animator uranusButtonAnimator;
    [SerializeField]
    private Animator earthButtonAnimator;
    [SerializeField]
    private Animator neptuneButtonAnimator;
    [SerializeField]
    private Animator jupiterButtonAnimator;
    [SerializeField]
    private Animator gGunEnlargeAnimator;
    [SerializeField]
    private Animator gGunShrinkAnimator;
    [SerializeField]
    private Animator gGunOutlineAnimator;
   

    [Header("Cold Animations")]
    [SerializeField]
    private Animator coldDamageOverlay;

    [Header("UI PickUp Slider Objects")]
    public static Slider gravityPickUpSlider;
    public static Slider fireRatePickUpSlider;
    public static Image fireRatePickUpSliderImage;
    public static Image gravityPickUpSliderImage;
    public static Button activateButton;

    [Header("UI Help Panels")]
    public static GameObject helpPanel;

    [Header("PlanetGravity")]
    [SerializeField] float earth = 9.8f;
    [SerializeField] float mars = 3.7f;
    [SerializeField] float ceres = 0.3f;
    [SerializeField] float uranus = 8.7f;
    [SerializeField] float jupiter = 23.1f;
    [SerializeField] float pluto = 0.7f;
    [SerializeField] float moon = 1.6f;
    [SerializeField] float neptune = 11.0f;

    [Header("Level Segments")]
    [SerializeField] int tempFillInSegments = 2;
    [SerializeField] int tutorialSegments = 6;
    [SerializeField] int JA_0629Segments = 6;
    [SerializeField] int KL_0602Segments = 6;
    [SerializeField] int JS_1021Segments = 6;
    [SerializeField] int MS_0211Segments = 6;
    [SerializeField] int DW_0713Segments = 6;
    [SerializeField] int GW_0202Segments = 6;
    [SerializeField] int finalLevelSegments = 8;

    [Header("Current G Gun Slider")]
    public static Slider cGGSlider;

    //Thes are used for the Environment Current and Upcoming PlanetImages
    private Color32 earthImage = new Color32(0, 255, 255, 255), marsImage = new Color32(0, 255, 128, 255), ceresImage = new Color32(255, 255, 204, 255),
        uranusImage = new Color32(0, 102, 254, 255), jupiterImage = new Color32(102, 0, 102, 255), plutoImage = new Color32(204, 255, 153, 255),
        neptuneImage = new Color32(0, 0, 102, 255), moonImage = new Color32(102, 255, 102, 255);

    //private GameObject upEarthImage, upMarsImage, upUranusImage, upJupiterImage, upPlutoImage, upNeptuneImage, upMoonImage;
       
    static GameObject[] mainCanvas;

    #endregion

    #region Getters/Setters


    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: Awake
    // Return types: N/A
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/25/2017
    // ------------------------------------------------------------------------------
    // Purpose: Runs before any other script element; ensures this object is the only
    //          game controller in the game, regardless of start point. 
    // ------------------------------------------------------------------------------

    private void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;

        }
        
        else if (controller != this)
        {
            Destroy(gameObject);
        }

        if (levels == null)
        {
            if (BuildGameLevels(levels))
            {
                Debug.Log("Levels and segments built.", this);

            }

            else
            {
                Debug.LogError("A problem was encountered when attempting to build the game levels.", this);
            }

            AttributeGravityValues();
        }

        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[(Scenes)i].SceneIndex == SceneManager.GetActiveScene().buildIndex)
            {
                activeLevel = levels[(Scenes)i];

            }

        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(UNA_Tags.player);

        }

        if (endPieces == null)
        {
            endPieces = new bool[7];

        }

        if (mainUICanvas == null)
        {
            mainUICanvas = GameObject.Find("MainUICanvas");
            
        }

        if (uIButtonManager == null)
        {
            uIButtonManager = GetComponentInChildren<UI_UIButtonManager>().gameObject;

        }

        LocateReferences();

        Debug.Log(activeLevel.LevelName);

        if (activeLevel == levels[Scenes.Controls] || activeLevel == levels[Scenes.Credits] || activeLevel == levels[Scenes.MainMenu] || activeLevel == levels[Scenes.LoadGame])
        {
            player.SetActive(false);

            Camera.main.GetComponent<WLD_CameraFollow_SL>().enabled = false;

            Camera.main.transform.position = new Vector3(0, 0, -16);
            Camera.main.transform.rotation = Quaternion.identity;

        }
        else
        {
            player.SetActive(true);

            Camera.main.GetComponent<WLD_CameraFollow_SL>().enabled = true;

        }

        if (activeLevel == levels[Scenes.Hub] || activeLevel == levels[Scenes.Controls] || activeLevel == levels[Scenes.Credits] || activeLevel == levels[Scenes.MainMenu] || activeLevel == levels[Scenes.LoadGame])
        {
            mainUICanvas.GetComponent<Canvas>().enabled = false;

        }
        else
        {
            mainUICanvas.GetComponent<Canvas>().enabled = true;

        }

        WLD_Teleporter[] teleporters = FindObjectsOfType<WLD_Teleporter>();

        for (int i = 0; i < activeLevel.LevelSegments.Length; i++)
        {
            for (int z = 0; z < teleporters.Length; z++)
            {
                if (activeLevel.LevelSegments[i].SegmentNumber == teleporters[z].segmentNumber)
                {
                    activeLevel.LevelSegments[i].TeleporterX = teleporters[z].transform.position.x;
                    activeLevel.LevelSegments[i].TeleporterY = teleporters[z].transform.position.y;
                    activeLevel.LevelSegments[i].TeleporterZ = teleporters[z].transform.position.z;

                }
            }
        }

        if (gravitySettingDictionary == null)
        {
            AddGravitySettings(gravitySettingDictionary);

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

    private void Start()
    {
        gravityGunOverViewPanel.SetActive(false);
        gravitySelectNotificationPanel.SetActive(false);
        temperatureGuagePanel.SetActive(false);
        pausePanel.SetActive(false);
        gravityPanelShrink.SetActive(false);
        helpPanel.SetActive(false);
        tutorialButton.enabled = false;
        transitionOverlay.SetActive(false);
        currentPlanetImageColor.enabled = false;
        saveGameNotificationText.enabled = false;
        damageText.enabled = false;
        damageOverlay.enabled = false;
        

        if (activeLevel == levels[Scenes.Hub])
        {
            gravityPanelEnlarge.enabled = false;
        }
        else if  (activeLevel == levels[Scenes.MainMenu])
        {
            gravityPanelEnlarge.enabled = false;
        }
        else if (activeLevel == levels[Scenes.Credits])
        {
            gravityPanelEnlarge.enabled = false;
        }
        else if (activeLevel == levels[Scenes.LoadGame])
        {
            gravityPanelEnlarge.enabled = false;
        }
        else if (activeLevel == levels[Scenes.Controls])
        {
            gravityPanelEnlarge.enabled = false;
        }
      


        if (activeLevel != levels[Scenes.Tutorial])
        {
            tutorialButton.enabled = false;
        }
    }

  
    // ------------------------------------------------------------------------------
    // Function Name: BuildGameLevels
    // Return types: bool
    // Argument types: Dictionary<Scenes, UNA_Level>
    // Author: Michael Smith
    // Date: 9/25/2017
    // ------------------------------------------------------------------------------
    // Purpose: Collects scene index information from build settings; Creates level and 
    //          segment data structure. Adds levels to static dictionary. Allows for loading
    //          level data.
    // ------------------------------------------------------------------------------

    public bool BuildGameLevels (Dictionary<Scenes, UNA_Level> LevelData)
    {
        int[] sceneIndex = new int[14];

        if (LevelData == null)
        {
            Debug.Log("World built from scratch");

            levels = new Dictionary<Scenes, UNA_Level>();

            sceneIndex[0] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/MenuScenes/Main Menu.unity");
            sceneIndex[1] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/MenuScenes/Controls.unity");
            sceneIndex[2] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/MenuScenes/Credits.unity");
            sceneIndex[3] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/MenuScenes/LoadGame.unity");

            sceneIndex[4] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/PreLevel/Tutorial.unity");
            sceneIndex[5] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/PreLevel/Hub.unity");

            sceneIndex[6] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/JA_0629.unity");
            sceneIndex[7] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/KL_0602.unity");
            sceneIndex[8] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/JS_1021.unity");
            sceneIndex[9] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/MS_0211.unity");
            sceneIndex[10] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/DW_0713.unity");
            sceneIndex[11] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/GW_0202.unity");
            sceneIndex[12] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Levels/FinalLevel.unity");
            sceneIndex[13] = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/PreLevel/TempFillIn.unity");


            levels.Add(Scenes.MainMenu, new UNA_Level("Main Menu", sceneIndex[0], 0));
            levels.Add(Scenes.Controls, new UNA_Level("Controls", sceneIndex[1], 0));
            levels.Add(Scenes.Credits, new UNA_Level("Credits", sceneIndex[2], 0));
            levels.Add(Scenes.LoadGame, new UNA_Level("LoadGame", sceneIndex[3], 0));

            levels.Add(Scenes.Tutorial, new UNA_Level("Tutorial", sceneIndex[4], tutorialSegments));
            levels.Add(Scenes.Hub, new UNA_Level("Hub", sceneIndex[5], 1));
            levels.Add(Scenes.TempFillIn, new UNA_Level("TempFillIn", sceneIndex[13], tempFillInSegments));

            levels.Add(Scenes.JA_0629, new UNA_Level("JA_0629", sceneIndex[6], JA_0629Segments));
            levels.Add(Scenes.KL_0602, new UNA_Level("KL_0602", sceneIndex[7], KL_0602Segments));
            levels.Add(Scenes.JS_1021, new UNA_Level("JS_1021", sceneIndex[8], JS_1021Segments));
            //levels.Add(Scenes.MS_0211, new UNA_Level("MS_0211", sceneIndex[9], MS_0211Segments));
            levels.Add(Scenes.DW_0713, new UNA_Level("DW_0713", sceneIndex[10], DW_0713Segments));
            //levels.Add(Scenes.GW_0202, new UNA_Level("GW_0202", sceneIndex[11], GW_0202Segments));
            levels.Add(Scenes.Final, new UNA_Level("FINAL", sceneIndex[12], finalLevelSegments));

            levels[Scenes.DW_0713].LevelUnlocked = true;
            levels[Scenes.Final].LevelUnlocked = true;

            if (levels.Count > 1)
            {
                Debug.Log(levels.Count);

                return true;
            }
        }

        else
        {
            Debug.Log("Loaded");

            levels = new Dictionary<Scenes, UNA_Level>();

            levels = LevelData;

            if (levels.Count > 1)
            {
                Debug.Log(levels.Count);

                return true;
            }
            
        }

        return false;
    }

    // ------------------------------------------------------------------------------
    // Function Name: AttributeGravityValues
    // Return types: void
    // Argument types: N/A
    // Author: Michael Smith
    // Date: 9/25/2017
    // ------------------------------------------------------------------------------
    // Purpose: Associates all gravity values to enum types through a static dictionary
    // ------------------------------------------------------------------------------

    void AttributeGravityValues ()
    {
        gravityValues.Add(Gravity.Jupiter, jupiter);
        gravityValues.Add(Gravity.Neptune, neptune);
        gravityValues.Add(Gravity.Earth, earth);
        gravityValues.Add(Gravity.Uranus, uranus);
        gravityValues.Add(Gravity.Mars, mars);
        gravityValues.Add(Gravity.Moon, moon);
        gravityValues.Add(Gravity.Pluto, pluto);
        gravityValues.Add(Gravity.Ceres, ceres);

        gravityTitles.Add(Gravity.Earth, "Earth");
        gravityTitles.Add(Gravity.Jupiter, "Jupiter");
        gravityTitles.Add(Gravity.Mars, "Mars");
        gravityTitles.Add(Gravity.Moon, "Moon");
        gravityTitles.Add(Gravity.Neptune, "Neptune");
        gravityTitles.Add(Gravity.Pluto, "Pluto");
        gravityTitles.Add(Gravity.Uranus, "Uranus");
        gravityTitles.Add(Gravity.Ceres, "Ceres");

        gravityImages.Add(Gravity.Earth, earthImage);
        gravityImages.Add(Gravity.Jupiter, jupiterImage);
        gravityImages.Add(Gravity.Mars, marsImage);
        gravityImages.Add(Gravity.Moon, moonImage);
        gravityImages.Add(Gravity.Neptune, neptuneImage);
        gravityImages.Add(Gravity.Pluto, plutoImage);
        gravityImages.Add(Gravity.Uranus, uranusImage);
        gravityImages.Add(Gravity.Ceres, ceresImage);

        ui_GameObjects.Add(UI_GO_Panels.GravityPanel, gravityGunOverViewPanel);
        ui_GameObjects.Add(UI_GO_Panels.GravityPanelSmallGo, gravityPanelEnlargeGo);
        ui_GameObjects.Add(UI_GO_Panels.GravityPanelShrink, gravityPanelShrink);
        ui_GameObjects.Add(UI_GO_Panels.GravityActivatedPanel, gravitySelectNotificationPanel);
        ui_GameObjects.Add(UI_GO_Panels.CurrentGGunSelectionPanel, currentGGunSelectionPanel);
        ui_GameObjects.Add(UI_GO_Panels.ExtremeTempWarning, extremeTempWarning);
        ui_GameObjects.Add(UI_GO_Panels.TemperatureGuagePanel, temperatureGuagePanel);
        ui_GameObjects.Add(UI_GO_Panels.PausePanel, pausePanel);
        ui_GameObjects.Add(UI_GO_Panels.TransitionPanel, transitionOverlay);
        ui_GameObjects.Add(UI_GO_Panels.ShrinkImageFillIn, shrinkFillIn);

        animators.Add(Animations.GgunPanelAnimator, gGunPanelAnimator);
        animators.Add(Animations.GgunEnlargeAnimator, gGunEnlargeAnimator);
        animators.Add(Animations.GgunShrinkAnimator, gGunShrinkAnimator);
        animators.Add(Animations.GgunOutlineAnimator, gGunOutlineAnimator);

        animators.Add(Animations.CeresAnimator, ceresButtonAnimator);
        animators.Add(Animations.PlutoAnimator, plutoButtonAnimator);
        animators.Add(Animations.MoonAnimatorn, moonButtonAnimator);
        animators.Add(Animations.MarsAnimator, marsButtonAnimator);
        animators.Add(Animations.UranusAnimator, uranusButtonAnimator);
        animators.Add(Animations.EarthAnimator, earthButtonAnimator);
        animators.Add(Animations.NeptuneAnimator, neptuneButtonAnimator);
        animators.Add(Animations.JupiterAnimator, jupiterButtonAnimator);
        animators.Add(Animations.FrostOverlay, coldDamageOverlay);

        ui_Texts.Add(UI_Txt.TransitionPlanetTxt, transitionPlanetText);
        ui_Texts.Add(UI_Txt.CurrentHealthText, currentHealthText);
        ui_Texts.Add(UI_Txt.SelectedPlanetText, planetText);
        ui_Texts.Add(UI_Txt.ActivatedText, activatedText);
        ui_Texts.Add(UI_Txt.CurrentGGunSelectionText, currentGGunSelectionText);
        ui_Texts.Add(UI_Txt.CurrentLevelTxt, currentLevel);
        ui_Texts.Add(UI_Txt.CurrentTime, currentTime);
        ui_Texts.Add(UI_Txt.CurrentScore, currentScore);
        ui_Texts.Add(UI_Txt.FireRatePickUPSliderText, fireRatePickUPSliderText);
        ui_Texts.Add(UI_Txt.GravityPickUpSliderText, gravityPickUpSliderText);
        ui_Texts.Add(UI_Txt.SaveGameNotificationText,saveGameNotificationText);
        ui_Texts.Add(UI_Txt.DamageText, damageText);

        ui_Images.Add(UI_Images.CurrentPlanetImg, currentPlanetImageColor);
        ui_Images.Add(UI_Images.FillCurrentHealth, fillCurrentHealth);
        ui_Images.Add(UI_Images.CurrentGGunSelectionImage, currentGGunSelectionPlanet);
        ui_Images.Add(UI_Images.FillHot, fillhot);
        ui_Images.Add(UI_Images.DamageOverlay, damageOverlay);
        //ui_Images.Add(UI_Images.ShrinkImageFillIn, shrinkFillIn);

        ui_Button.Add(UI_Buttons.GgunPanEarth, earthButton);
        ui_Button.Add(UI_Buttons.GgunPanMoon, moonButton);
        ui_Button.Add(UI_Buttons.GgunPanMars, marsButton);
        ui_Button.Add(UI_Buttons.GgunPanCeres, ceresButton);
        ui_Button.Add(UI_Buttons.GgunPanNeptune, neptuneButton);
        ui_Button.Add(UI_Buttons.GgunPanJupiter, jupiterButton);
        ui_Button.Add(UI_Buttons.GgunPanelPluto, plutoButton);
        ui_Button.Add(UI_Buttons.GgunPanUranus, uranusButton);
        ui_Button.Add(UI_Buttons.RestartButton, restartButton);
        ui_Button.Add(UI_Buttons.GravityPanelSmall, gravityPanelEnlarge);

        ui_Button.Add(UI_Buttons.SkipButton, tutorialButton);
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

    void LocateReferences ()
    {

        gravityGunOverViewPanel = GameObject.Find("GravityGunOverViewPanel");
        gravityPanelEnlargeGo = GameObject.Find("GravityPanelEnlarged");
        gravityPanelShrink = GameObject.Find("GravityPanelShrink");
        gravitySelectNotificationPanel = GameObject.Find("GravitySelectNotificationPanel");
        currentGGunSelectionPanel = GameObject.Find("CurrentGgunSelection");
        extremeTempWarning = GameObject.Find("ExtremeTempText");
        temperatureGuagePanel = GameObject.Find("TempGuagePanel");
        pausePanel = GameObject.Find("PausePanel");
        menuPanel = GameObject.Find("MenuPanel");
        transitionOverlay = GameObject.Find("TransitionPanel");
        helpPanel = GameObject.Find("HelpPanel");

        transitionPlanetText = GameObject.Find("TransitionPlanetText").GetComponent<Text>();
        currentHealthText = GameObject.Find("CurrentHealthText").GetComponent<Text>();
        planetText = GameObject.Find("PlanetText").GetComponent<Text>();
        activatedText = GameObject.Find("ActivatedText").GetComponent<Text>();
        currentGGunSelectionText = GameObject.Find("CurrentGgunSelectionText").GetComponent<Text>();
        currentLevel = GameObject.Find("CurrentLevelText").GetComponent<Text>();
        currentTime = GameObject.Find("CurrentTime").GetComponent<Text>();
        currentScore = GameObject.Find("CurrentScore").GetComponent<Text>();
        gravityPickUpSliderText = GameObject.Find("GravityPickUpSliderText").GetComponent<Text>();
        fireRatePickUPSliderText = GameObject.Find("FireRatePickUPSliderText").GetComponent<Text>();
        damageText = GameObject.Find("DmgText").GetComponent<Text>();

        currentPlanetImageColor = GameObject.Find("CurrentGravityImage").GetComponent<Image>();
        currentGGunSelectionPlanet = GameObject.Find("CurrentGgunSelectionPlanet").GetComponent<Image>();
        fillCurrentHealth = GameObject.Find("FillCurrentHealth").GetComponent<Image>();
        fillhot = GameObject.Find("FillHot").GetComponent<Image>();
        gravityPickUpSliderImage = GameObject.Find("GravityPickUpSliderImage").GetComponent<Image>();
        fireRatePickUpSliderImage = GameObject.Find("FireRatePickUPSliderImage").GetComponent<Image>();
        damageOverlay = GameObject.Find("DamageOverlay").GetComponent<Image>();
        shrinkFillIn = GameObject.Find("ShrinkButtonFillIn");

        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        tutorialButton = GameObject.Find("SkipTutorialButton").GetComponent<Button>();
        gravityPanelEnlarge = GameObject.Find("GravityPanelEnlarged").GetComponent<Button>();

        gravityPickUpSlider = GameObject.Find("GravityPickUpSlider").GetComponent<Slider>();
        fireRatePickUpSlider = GameObject.Find("FireRatePickUPSlider").GetComponent<Slider>();
        cGGSlider = GameObject.Find("CompareSlider").GetComponent<Slider>();

        gGunPanelAnimator = GameObject.Find("GravityGunOverViewPanel").GetComponent<Animator>();

        ceresButtonAnimator = GameObject.Find("CeresButton").GetComponent<Animator>();
        plutoButtonAnimator = GameObject.Find("PlutoButton").GetComponent<Animator>();
        moonButtonAnimator = GameObject.Find("MoonButton").GetComponent<Animator>();
        marsButtonAnimator = GameObject.Find("MarsButton").GetComponent<Animator>();
        uranusButtonAnimator = GameObject.Find("UranusButton").GetComponent<Animator>();
        earthButtonAnimator = GameObject.Find("EarthButton").GetComponent<Animator>();
        neptuneButtonAnimator = GameObject.Find("NeptunButton").GetComponent<Animator>();
        jupiterButtonAnimator = GameObject.Find("JupiterButton").GetComponent<Animator>();
        coldDamageOverlay = GameObject.Find("ColdDamageOverlay").GetComponent<Animator>();

        gGunEnlargeAnimator = GameObject.Find("GravityPanelEnlarged").GetComponent<Animator>();
        gGunShrinkAnimator = GameObject.Find("GravityPanelShrinked").GetComponent<Animator>();
        gGunOutlineAnimator = GameObject.Find("GgunPanelOutline").GetComponent<Animator>();
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

    public static void AddGravitySettings (Dictionary<Gravity, bool> dictionary)
    {
        if (dictionary != null)
        {
            gravitySettingDictionary = new Dictionary<Gravity, bool>(dictionary);

        }
        else
        {
            gravitySettingDictionary = new Dictionary<Gravity, bool>();

            gravitySettingDictionary.Add(Gravity.Earth, true);
            gravitySettingDictionary.Add(Gravity.Moon, false);
            gravitySettingDictionary.Add(Gravity.Jupiter, false);
            gravitySettingDictionary.Add(Gravity.Mars, false);
            gravitySettingDictionary.Add(Gravity.Neptune, false);
            gravitySettingDictionary.Add(Gravity.Pluto, false);
            gravitySettingDictionary.Add(Gravity.Uranus, true);
            gravitySettingDictionary.Add(Gravity.Ceres, false);

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

} // End WLD_GameController