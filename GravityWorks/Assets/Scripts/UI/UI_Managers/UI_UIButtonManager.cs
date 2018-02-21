/* -----------------------------------------------------------------------------------
 * Class Name: UI_UIManager
 * -----------------------------------------------------------------------------------
 * Author: Joseph Aranda
 * Date: 9/25/17
 * Credit: 
 * -----------------------------------------------------------------------------------
 * Purpose: This script will be holding all the data for the UserInterface buttons
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UI_UIButtonManager : MonoBehaviour
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
 
    [Header("Panel GameObjects")]
    //public GameObject topTimesScoresOverlay;
    public GameObject helpMenu;
    public GameObject saveMenu;
    public WLD_MessageSystem msgSystem;

    [Header("Save Button")]
    public Button saveButton;

    [Header("End Piece Msg")]
    public Image EndPieceLorePanel;
    public Text msgBody;
    public Text msgFrom;
    public Text pressE;

    [Header("Enlarge")]
    public Button gGunEnlargeButton;

    [Header("Restart buttons")]
    public GameObject restartButton;

    [Header("Shoot Effects")]
    public ParticleSystem powerUpParticles;
    public Light powerUpLights;


    [Header("Player Start Speed")]
    public float startSpeed = 10;

    [Header("UI_Pause Script")]
    public UI_Pause ui_Pause;

    [Header("Suit Burn Effect")]
    public WLD_TextureManipulation wld_TextureManipulation;

    public GameObject gGunPanelOverlay;
    public Image gravityButtonEnlarged;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    Gravity gravitySetting;
    int arrayPos = 0;
    float startDissolveValue = 0, resetBaseSpeed = .01f;

    bool isRestarted;
    GameObject player;
    PLR_Shoot plr_Shoot;
    PLR_CharacterMovement plr_CharacterMovement;
   
    UNA_Level currentLevel;
    UI_SaveGame ui_SaveGame;

    WLD_LevelLoader lL;

   

    #endregion

    #region Getters/Setters

    public Gravity GravitySetting
    {
        get { return gravitySetting; }
    }
   
    public bool IsRestarted
    {
        get {return isRestarted; }
        set { isRestarted = value; }
    }
    #endregion

    #region Constructors




    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: KeyBoardMenuScroll()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: Calls the functions every frame
    // ------------------------------------------------------------------------------

    private void OnLevelWasLoaded(int level)
    {
        if (saveButton != null)
        {
            if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Tutorial])
            {
                saveButton.interactable = false;

            }
            else
            {
                saveButton.interactable = true;
            }
        }

        lL = FindObjectOfType<WLD_LevelLoader>();

        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.MainMenu])
        {
            if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.LoadGame])
            {
                if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Controls])
                {
                    if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Credits])
                    {
                        plr_Shoot = FindObjectOfType<PLR_Shoot>();
                        plr_CharacterMovement = FindObjectOfType<PLR_CharacterMovement>();

                        UI_Pause.isPaused = false;
                        Time.timeScale = 1;

                        if (WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText] != null)
                        {
                            WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
                        }

                        if (plr_CharacterMovement != null)
                        {
                            plr_CharacterMovement.enabled = true;
                        }

                        if (plr_Shoot != null)
                        {
                            plr_Shoot.enabled = true;
                        }
                    }
                }
            }
        }
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(false);
            WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelShrink].SetActive(false);

            UI_SkipTutorial skipTut = FindObjectOfType<UI_SkipTutorial>();
            skipTut.enabled = false;
        }
        else
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelSmallGo].SetActive(true);
        }

        HeatZoneReset();
        HealthOverlayReset();
        ResetShrinkButtonFillIn();
        
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: Calls the functions every frame
    // ------------------------------------------------------------------------------
    void Start()
    {
        player = WLD_GameController.player;

        UNA_StaticVariables.isRdyShoot = false;
        UNA_StaticVariables.isDefaultShoot = true;

        UNA_StaticVariables.isCeres = false;
        UNA_StaticVariables.isMars = false;
        UNA_StaticVariables.isNeptune = false;
        UNA_StaticVariables.isUranus = false;
        UNA_StaticVariables.isJupiter = false;
        UNA_StaticVariables.isSaturn = false;
        UNA_StaticVariables.isPluto = false;

        ui_SaveGame = GetComponent<UI_SaveGame>();
    
        arrayPos = 0;
     
        lL = FindObjectOfType<WLD_LevelLoader>();

        UNA_StaticVariables.isShrunk = true;

    }

    void Update()
    {
        TutorialLandingOff();
        KeyBoardInput();      
        NotificationTimer();
        RestartDisabledOnTutorial();
        SkipTutorial();
        //SwitchButtonAnimation();
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Return to menu and Restart Functions
    // ------------------------------------------------------------------------------

    #region ReturnToMenu/Restart Functions

    // ------------------------------------------------------------------------------
    // Function Name: ReturnToMenu
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda, Kayci Lyons    
    // Date: 9/28/17
    // ------------------------------------------------------------------------------
    // Purpose: This function is for the button that will return to the menu. Use this on 
    //          any scenes that has a path back to the start menu. 
    //          For RETURN TO MENU.
    // ------------------------------------------------------------------------------
    public void ReturnToMenu()
    {
        gGunEnlargeButton.enabled = true;

        WLD_GameController.player.transform.position = new Vector3(0f, 0f, 0f);

        Time.timeScale = 1;

        TransitionPanelRestart();
        EndPieceLoreElementsRestart();
        SaveNotificationRestart();
        FireRateRestart();
        HealthOverlayReset();
        ColdEffectRestart();
        HeatZoneReset();

        SceneManager.LoadScene("Main Menu");

        if (currentLevel != WLD_GameController.levels[Scenes.MainMenu])
        {
            WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(false);
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: RestartButton
    // Return types: 
    // Argument types: 
    // Author: Kayci Lyons
    // Date: 10/19/17
    // ------------------------------------------------------------------------------
    // Purpose: When the function is called, it resets the player to the last teleporter they activated.
    // ------------------------------------------------------------------------------
    public void RestartButton()
    {
        for (int i = 0; i < WLD_GameController.levels.Count; i++)
        {
            if (WLD_GameController.levels[(Scenes)i] == WLD_GameController.activeLevel)
            {
                WLD_Teleporter teleporter = FindObjectOfType<WLD_Teleporter>();
                PLR_DeathRespawnCycle reset = FindObjectOfType<PLR_DeathRespawnCycle>();
                teleporter.LevelToLoad = (Scenes)i;
                teleporter.LoadLevelSegment(WLD_GameController.player.GetComponent<GRV_IndividualGravity>().Segment.SegmentNumber);
                reset.PlayerReset();
            }
        }

        ui_Pause.UnpauseButton();
        EndPieceLoreElementsRestart();
        SaveNotificationRestart();
        FireRateRestart();
        HealthOverlayReset();
        ColdEffectRestart();
        HeatZoneReset();
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Restart and Return to Main Menu Button Functions
    // ------------------------------------------------------------------------------
    void TransitionPanelRestart()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(false);
        WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = false;
    }
    void EndPieceLoreElementsRestart()
    {
        msgFrom.text = "";
        msgBody.text = "";
        pressE.text = "";

        EndPieceLorePanel.enabled = false;
    }
    void SaveNotificationRestart()
    {
        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
    }
    void FireRateRestart()
    {
        player.GetComponent<CLT_General>().ResetFireRateStat();

    }
  
    void HealthOverlayReset()
    {
        WLD_GameController.ui_Images[UI_Images.DamageOverlay].enabled = false;
    }
    void ColdEffectRestart()
    {
        UNA_StaticVariables.isTempGaugeOn = false;
        UI_TemperatureGauge.isCold = false;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Image>().enabled = false;
        WLD_GameController.player.GetComponent<PLR_CharacterMovement>().MovingSpeed = startSpeed;
        player.GetComponent<PLR_Dash>().enabled = true;
        WLD_GameController.animators[Animations.FrostOverlay].Play("FrostIdle");
    }
    void HeatZoneReset()
    {
        UNA_StaticVariables.isTempGaugeOn = false;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Image>().enabled = false;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].GetComponentInChildren<Slider>().enabled = false;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TemperatureGuagePanel].SetActive(false);
        WLD_GameController.ui_GameObjects[UI_GO_Panels.ExtremeTempWarning].SetActive(false);
    }
  
    void ResetShrinkButtonFillIn()
    {       
        WLD_GameController.ui_GameObjects[UI_GO_Panels.ShrinkImageFillIn].SetActive(true);
    }
    public void GgunEnlarge()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelShrink].SetActive(true);

        WLD_GameController.animators[Animations.GgunPanelAnimator].SetBool("Open", false);
        WLD_GameController.animators[Animations.GgunOutlineAnimator].SetTrigger("Play");
        
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanel].SetActive(true);

        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = false;
        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].interactable = false;
       
        WLD_GameController.ui_GameObjects[UI_GO_Panels.ShrinkImageFillIn].SetActive(false);

        gravityButtonEnlarged.color = Color.red;

        UNA_StaticVariables.isShrunk = false;
    }
    public void GgunShrink()
    {
        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].enabled = true;
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityPanelShrink].SetActive(false);

        WLD_GameController.ui_Button[UI_Buttons.GravityPanelSmall].interactable = true;

        WLD_GameController.ui_GameObjects[UI_GO_Panels.ShrinkImageFillIn].SetActive(true);

        WLD_GameController.animators[Animations.GgunPanelAnimator].SetBool("Open", true);
        WLD_GameController.animators[Animations.GgunOutlineAnimator].SetTrigger("Off");

        gravityButtonEnlarged.color = Color.green;

        UNA_StaticVariables.isShrunk = true;
    }
    #endregion


    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void TutorialLandingOff()
    {
        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.MainMenu])
        {
            if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.LoadGame])
            {
                if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Controls])
                {
                    if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Credits])
                    {
                        SetCurrentSelectedGravityValue();
                        
                        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Hub])
                        {
                            ToggleGravityHud();
                            GGunGameContButton();

                            if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Tutorial])
                            {
                                UI_Tutorial.isInLanding = false;
                            }
                        }
                    }
                }
            }
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void SetCurrentSelectedGravityValue()
    {
        if (!FindObjectOfType<WLD_LevelLoader>().loadingScreen.activeSelf)
        {
            WLD_GameController.cGGSlider.value = WLD_GameController.gravityValues[gravitySetting] - WLD_GameController.gravityValues[WLD_GameController.player.GetComponent<GRV_IndividualGravity>().Segment.GravitySetting];
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    void SwitchButtonAnimation()
    {
        if (gravitySetting != Gravity.Earth && WLD_GameController.animators[Animations.EarthAnimator].GetBool("Earth"))
        {
            WLD_GameController.animators[Animations.EarthAnimator].SetBool("Earth", false);
        }
        if (gravitySetting != Gravity.Moon && WLD_GameController.animators[Animations.MoonAnimatorn].GetBool("Moon"))
        {
            WLD_GameController.animators[Animations.MoonAnimatorn].SetBool("Moon", false);
        }
        if (gravitySetting != Gravity.Ceres && WLD_GameController.animators[Animations.CeresAnimator].GetBool("Ceres"))
        {
            WLD_GameController.animators[Animations.CeresAnimator].SetBool("Ceres", false);
        }        
        if (gravitySetting != Gravity.Mars && WLD_GameController.animators[Animations.MarsAnimator].GetBool("Mars"))
        {
            WLD_GameController.animators[Animations.MarsAnimator].SetBool("Mars", false);
        }
        if (gravitySetting != Gravity.Jupiter && WLD_GameController.animators[Animations.JupiterAnimator].GetBool("Jupiter"))
        {
            WLD_GameController.animators[Animations.JupiterAnimator].SetBool("Jupiter", false);
        }
        if (gravitySetting != Gravity.Uranus && WLD_GameController.animators[Animations.UranusAnimator].GetBool("Uranus"))
        {
            WLD_GameController.animators[Animations.UranusAnimator].SetBool("Uranus", false);
        }
        if (gravitySetting != Gravity.Neptune && WLD_GameController.animators[Animations.NeptuneAnimator].GetBool("Neptune"))
        {
            WLD_GameController.animators[Animations.NeptuneAnimator].SetBool("Neptune", false);
        }
        if (gravitySetting != Gravity.Pluto && WLD_GameController.animators[Animations.PlutoAnimator].GetBool("Pluto"))
        {
            WLD_GameController.animators[Animations.PlutoAnimator].SetBool("Pluto", false);
        }
    }

    #region GgunButtonFunctions
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Holds the Gravity Planets notification panel and sets the gun gravities
    // ------------------------------------------------------------------------------
    public void Earth()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Earth";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Earth];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Earth";
        UNA_StaticVariables.isOn = true;

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Earth];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Earth];

        gravitySetting = Gravity.Earth;

        WLD_GameController.animators[Animations.EarthAnimator].Play("EarthButton");

    }
    public void EarthsMoon()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Moon";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Moon];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Moon";
        UNA_StaticVariables.isOn = true;

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Moon];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Moon];

        WLD_GameController.animators[Animations.MoonAnimatorn].Play("MoonButton");

        gravitySetting = Gravity.Moon;
    }
    public void Ceres()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Ceres";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Ceres];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Ceres";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Ceres];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Ceres];

        WLD_GameController.animators[Animations.CeresAnimator].Play("CeresButton");

        gravitySetting = Gravity.Ceres;
        UNA_StaticVariables.isOn = true;
    }
    public void Mars()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Mars";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Mars];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Mars";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Mars];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Mars];

        WLD_GameController.animators[Animations.MarsAnimator].Play("MarsButton");

        gravitySetting = Gravity.Mars;
        UNA_StaticVariables.isOn = true;
    }
    public void Jupiter()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Jupiter";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Jupiter];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Jupiter";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Jupiter];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Jupiter];

        WLD_GameController.animators[Animations.JupiterAnimator].Play("Jupiter");

        gravitySetting = Gravity.Jupiter;
        UNA_StaticVariables.isOn = true;
    }
    public void Uranus()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Uranus";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Uranus];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Uranus";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Uranus];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Uranus];

        WLD_GameController.animators[Animations.UranusAnimator].Play("UranusButton");

        gravitySetting = Gravity.Uranus;

        UNA_StaticVariables.isOn = true;
    }
    public void Neptune()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);     
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Neptune";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Neptune];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Neptune";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Neptune];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Neptune];

        WLD_GameController.animators[Animations.NeptuneAnimator].Play("NeptuneButton");

        gravitySetting = Gravity.Neptune;
        UNA_StaticVariables.isOn = true;
    }
    public void Pluto()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
        WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Pluto";
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
        WLD_GameController.ui_Images[UI_Images.CurrentGGunSelectionImage].color = WLD_GameController.gravityImages[Gravity.Pluto];
        WLD_GameController.ui_Texts[UI_Txt.CurrentGGunSelectionText].text = "Pluto";

        powerUpParticles.startColor = WLD_GameController.gravityImages[Gravity.Pluto];
        powerUpLights.color = WLD_GameController.gravityImages[Gravity.Pluto];

        WLD_GameController.animators[Animations.PlutoAnimator].Play("PlutoButton");

        gravitySetting = Gravity.Pluto;

        UNA_StaticVariables.isOn = true;

    }

    #endregion

    // ------------------------------------------------------------------------------
    // Function Name: MouseOver, MouseExit
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 10/2/17
    // ------------------------------------------------------------------------------
    // Purpose: Detects if the Mouse pointer is over the gravity gun hud. 
    //          If the mouse is over the hud the player cant shoot
    // ------------------------------------------------------------------------------
    public void MouseOver()
    {
        UNA_StaticVariables.isRdyShoot = false;
        UNA_StaticVariables.isDefaultShoot = false;
    }
    public void MouseExit()
    {
        UNA_StaticVariables.isRdyShoot = true;
    }

    // ------------------------------------------------------------------------------
    // Function Name: ActiveGravityHud()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: Controls the minimizing and maximizing of the G.Gun hud
    //          First line toggles the G.Gun Panel
    //          Second line toggles the minimized version
    //          Third line toggles the minimize button
    // ------------------------------------------------------------------------------
    public void ToggleGravityHud() //For Keyboard
    {
        if (!UI_Pause.isPaused && !UI_Tutorial.isInLanding)
        {
            if (Input.GetKeyDown(KeyCode.G) && UNA_StaticVariables.isShrunk == true)
            {
                GgunEnlarge();
            }
            else if (Input.GetKeyDown(KeyCode.G) && UNA_StaticVariables.isShrunk == false)
            {
                GgunShrink();


            }
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void GGunGameContButton()//For gamePad
    {
        if (!UI_Pause.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button6) && UNA_StaticVariables.isShrunk == true)
            {
                GgunEnlarge();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button6) && UNA_StaticVariables.isShrunk == false)
            {
                GgunShrink();
            }
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: EnlargeButton()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: Controls the minimizing and maximizing of the G.Gun hud
    //          First line toggles the G.Gun Panel
    //          Second line toggles the minimized version
    //          Third line toggles the minimize button
    // ------------------------------------------------------------------------------

    public void EnlargeButton()
    {
        GgunEnlarge();
    }

    // ------------------------------------------------------------------------------
    // Function Name: ShrinkButton()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: Controls the minimizing and maximizing of the G.Gun hud
    //          First line toggles the G.Gun Panel
    //          Second line toggles the minimized version
    //          Third line toggles the minimize button
    // ------------------------------------------------------------------------------

    public void ShrinkButton()
    {
        GgunShrink();
    }

    // ------------------------------------------------------------------------------
    // Function Name: NotificationTimer()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/29/17
    // ------------------------------------------------------------------------------
    // Purpose: Checks if th enotification panel is on, starts the timer and when 
    //          timer is don turns of the notification panel
    // ------------------------------------------------------------------------------

    void NotificationTimer()
    {
        if (UNA_StaticVariables.isOn)
        {
            UNA_StaticVariables.time += Time.deltaTime;
            if (UNA_StaticVariables.time >= 2f)
            {
                UNA_StaticVariables.isOn = false;
                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(false);
                UNA_StaticVariables.time = 0;
            }
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: NewGameButton, LoadGameButton, ProfileManagerButton, ControlsButton
    //                SettingsButton, CreditsButton, TopScoresButton, QuitButton
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/26/17
    // ------------------------------------------------------------------------------
    // Purpose: These functions will hold the information for the buttons in the 
    //          START MENU SCENE.
    // ------------------------------------------------------------------------------

    public void NewGameButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Tutorial].SceneIndex);
    }

    public void LoadGameButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.LoadGame].SceneIndex);
        
    }

    public void ControlsButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Controls].SceneIndex);
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(WLD_GameController.levels[Scenes.Credits].SceneIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
  
    // ------------------------------------------------------------------------------
    // Function Name: KeyBoardInput()
    // Return types: NA
    // Argument types: NA
    // Author: Joseph Aranda
    // Date: 9/27/17
    // ------------------------------------------------------------------------------
    // Purpose: This will activated the buttons from a keyboard input.
    //          First part is if no keys are being pushed skip the rest of the function
    //          Secong part is if the scene is on the main menu these keys can be pressed
    // ------------------------------------------------------------------------------

    void KeyBoardInput()
    {
        if (!Input.anyKeyDown)
        {
            return;
        }
        if (UI_Pause.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 0)
            {
                ui_Pause.UnpauseButton();

            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 1)
            {
                //topTimesScoresOverlay.SetActive(true);
                RestartButton();

            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 2)
            {
                //RestartButton();
                ui_SaveGame.SaveGame();
                WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = true;

            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 3)
            {
                //ui_SaveGame.SaveGame();

                //WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = true;

                ReturnToMenu();

            }
            else if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 4)
            {
                //ReturnToMenu();
                HelpMenu();

            }
            //else if (Input.GetKeyDown(KeyCode.KeypadEnter) && arrayPos == 5)
            //{
            //    //HelpMenu();
            //}
        }    
    }
 
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void RestartDisabledOnTutorial()
    {
        if (WLD_GameController.activeLevel != WLD_GameController.levels[Scenes.Tutorial])
        {
            restartButton.SetActive(true);
        }
        else
        {
            restartButton.SetActive(false);
        }
    }
   
   
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    void SkipTutorial()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Tutorial])
        {
            if (Input.GetKey(KeyCode.Joystick1Button6) && Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                lL = FindObjectOfType<WLD_LevelLoader>();
                Vector3 spawnLocation = Vector3.zero;

                lL.LoadLevel(Scenes.Hub, spawnLocation);

                WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(false);
                WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = false;

                WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
            }
        }
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    public void HelpMenu ()
    {
        helpMenu.SetActive(true);
        WLD_GameController.ui_GameObjects[UI_GO_Panels.PausePanel].SetActive(false);

        WLD_GameController.ui_Texts[UI_Txt.SaveGameNotificationText].enabled = false;
    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph Aranda
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    //public void DisablePauseMenuButtons()
    //{
    //    if (topTimesScoresOverlay.activeSelf)
    //    {

    //        for (int i = 0; i < menuButtons.Length; i++)
    //        {
    //            menuButtons[i].interactable = false;
    //        }
    //    }
    //    else
    //    {
    //        for (int i = 0; i < menuButtons.Length; i++)
    //        {
    //            menuButtons[i].interactable = true;
    //        }
    //    }
        
       
    //}

   
}