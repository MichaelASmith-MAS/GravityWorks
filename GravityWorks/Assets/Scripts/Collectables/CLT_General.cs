/* -----------------------------------------------------------------------------------
 * Class Name: CLT_General
 * -----------------------------------------------------------------------------------
 * Author:
 * Date: 
 * Credit:  
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLT_General : MonoBehaviour
{

    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    [Header("Timers")]
    public float pickupDuration;
    public float matchWithPickUpDuration;

    [Header("Points")]
    public int pickUPPoints;

    [Header("Health")]
    public int pickUpHealth;

    [Header("EndPieces")]
    public int noOfEndPiecesCollected = 0;

    [Header("Length of PickUp")]
    public float newFireRateLength;

    [Header("Fire Rate After PickUP")]
    public float newFireRate;

    [Header("The Original FireRate")]
    public float startingFireRate;
    public float startingSelfFireRate;

    [Header("SliderValues")]
    public float pickUpTimerLength;
    public float matchwithPickUpTimer;
    public float startpickUpTimerLength;

    [Header("bulletSpaen")]
    public GameObject bulletSpawnLocation;

    [Header("FireRate Pick Up Slider Colors")]
    public Color beginColor = Color.green;
    public Color endColor = Color.red;

    public Animator energyBarFlash;

    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    private float timer , startTimer;

    float alteredEffectTime = 4f;
    float alteredfireRate = 0.3f;

    GameObject player;

    UI_GunMAnager ui_GunManager;
    CLT_EndPiece clt_endPiece;

    

    bool isFireRatePickedUp;

    #endregion

    #region Getters/Setters

    public int GETnoOfEndPiecesCollected
    {
        get { return noOfEndPiecesCollected; }
    }
    public bool GetIsFireRatePickedUp
    {
        get {return isFireRatePickedUp; }
        set {isFireRatePickedUp = value; }
    }
    #endregion

    #region Constructors



    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // -----------------------------------------------------------------------------

    void Start()
    {
        UNA_StaticVariables.isGrvPickedUp = false;
        isFireRatePickedUp = false;

        player = WLD_GameController.player;

        ui_GunManager = GetComponent<UI_GunMAnager>();
    }

    private void Update()
    {
        GravityPickupTimer();
        FireRateChange();
        FireRatePickupTimer();
    
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // -----------------------------------------------------------------------------

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
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // -----------------------------------------------------------------------------
    void GravityPickupTimer()
    {
        if (UNA_StaticVariables.isGrvPickedUp)
        {
            WLD_GameController.gravityPickUpSliderImage.color = Color.Lerp(endColor, beginColor, pickupDuration / matchWithPickUpDuration);

            pickupDuration -= Time.deltaTime;

            if (pickupDuration <= 0)
            {
                WLD_GameController.ui_Texts[UI_Txt.GravityPickUpSliderText].enabled = false;

                UNA_StaticVariables.isGrvPickedUp = false;
                WLD_GameController.gravityPickUpSliderImage.enabled = false;

                pickupDuration = matchWithPickUpDuration;
            }

            WLD_GameController.gravityPickUpSlider.value = pickupDuration / matchWithPickUpDuration;
        }

    }

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: controls how long the effect will last
    // ------------------------------------------------------------------------------
    void FireRateChange()
    {
        player = GameObject.Find("BulletSpawnLocation");

        if (isFireRatePickedUp)
        {
            timer += Time.deltaTime;

            if (timer <= newFireRateLength)
            {
                player.GetComponent<PLR_Shoot>().GETfireRate = newFireRate;
                player.GetComponent<PLR_Shoot>().GETselfFireRate = newFireRate;

                timer = startTimer;
            }

        }
        else
        {
            player.GetComponent<PLR_Shoot>().GETfireRate = startingFireRate;
            player.GetComponent<PLR_Shoot>().GETselfFireRate = startingSelfFireRate;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: Joseph
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Controls how long the pick up will last
    // ------------------------------------------------------------------------------
    void FireRatePickupTimer()
    {

        if (isFireRatePickedUp)
        {
            pickUpTimerLength -= Time.deltaTime;

            if (pickUpTimerLength <= 0)
            {
                WLD_GameController.ui_Texts[UI_Txt.FireRatePickUPSliderText].enabled = false;
                WLD_GameController.fireRatePickUpSliderImage.enabled = false;

                isFireRatePickedUp = false;

                pickUpTimerLength = startpickUpTimerLength;

                //Debug.Log("Hit");
            }

            WLD_GameController.fireRatePickUpSliderImage.color = Color.Lerp(endColor, beginColor, pickUpTimerLength / matchwithPickUpTimer);
            WLD_GameController.fireRatePickUpSlider.value = pickUpTimerLength / matchwithPickUpTimer;

        }

    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: NA
    // Argument types: NA
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose:  WLD_GameController.gravityValues[player.GetComponent<GRV_IndividualGravity>().Segment.GravitySetting]
    // -----------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case UNA_Tags.pickUpHealth:
                GetComponent<WLD_HealthDmg>().ChangeHealth(pickUpHealth);
                break;

            case UNA_Tags.pickUpPoints:
                GetComponent<PLR_Points>().ChangePoints(pickUPPoints);
            
                break;
            case UNA_Tags.pickUpFireRate:
                //WLD_GameController.activateFireRateButton.interactable = true;
                WLD_GameController.fireRatePickUpSliderImage.enabled = true;
                WLD_GameController.ui_Texts[UI_Txt.FireRatePickUPSliderText].enabled = true;
                isFireRatePickedUp = true;
               pickUpTimerLength = matchwithPickUpTimer;
                break;

            case UNA_Tags.pickUpGravEffTime:
                //WLD_GameController.activateGravityButton.interactable = true;
                WLD_GameController.gravityPickUpSliderImage.enabled = true;
                WLD_GameController.ui_Texts[UI_Txt.GravityPickUpSliderText].enabled = true;
                UNA_StaticVariables.isGrvPickedUp = true;
                pickupDuration = matchWithPickUpDuration;
                break;

            case UNA_Tags.pickUpLore:
                //WLD_GameController.activateButton.interactable = true;
                break;

            case UNA_Tags.pickUpMars:
                UNA_StaticVariables.isMars = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Mars] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Mars";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.pickUpCeres:
                UNA_StaticVariables.isCeres = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Ceres] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Ceres";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.pickUpUranus:
                UNA_StaticVariables.isUranus = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Moon] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Moon";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.pickUpNeptune:
                UNA_StaticVariables.isNeptune = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Neptune] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Neptune";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.pickUpPluto:
                UNA_StaticVariables.isPluto = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Pluto] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Pluto";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.pickUpJupiter:
                UNA_StaticVariables.isJupiter = true;
                WLD_GameController.gravitySettingDictionary[Gravity.Jupiter] = true;

                WLD_GameController.ui_GameObjects[UI_GO_Panels.GravityActivatedPanel].SetActive(true);
                WLD_GameController.ui_Texts[UI_Txt.SelectedPlanetText].text = "Jupiter";
                WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "gGun Setting Collected";
                UNA_StaticVariables.isOn = true;
                Invoke("ResetActivatedText", 2f);
                break;

            case UNA_Tags.endPiece:
                WLD_GameController.activeLevel.FinalLevelPieceCollected = true;

                //vvv Commented out for loop because it was setting all 7 to true after collecting one...?
                clt_endPiece = other.gameObject.GetComponent<CLT_EndPiece>();
                int pickupEndPieceID = clt_endPiece.GETendPieceID;

                for (int i = 0; i < WLD_GameController.endPieces.Length; i++)
                {
                    if (!WLD_GameController.endPieces[pickupEndPieceID])
                    {
                        WLD_GameController.endPieces[pickupEndPieceID] = true;
                        noOfEndPiecesCollected++;
                        //Debug.Log("You have collected " + noOfEndPiecesCollected + " of End Pieces.");
                    }
                }
                break;

            default:
                break;
        }
       

    }
    public void ResetFireRateStat()
    {
        WLD_GameController.fireRatePickUpSliderImage.color = beginColor;
        WLD_GameController.fireRatePickUpSlider.value = pickUpTimerLength;

        bulletSpawnLocation.GetComponent<PLR_Shoot>().GETfireRate = startingFireRate;
        bulletSpawnLocation.GetComponent<PLR_Shoot>().GETselfFireRate = startingSelfFireRate;

        WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(false);
        WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = false;

        WLD_GameController.ui_Texts[UI_Txt.FireRatePickUPSliderText].enabled = false;
        WLD_GameController.fireRatePickUpSliderImage.enabled = false;

        GetIsFireRatePickedUp = false;
    }
    void ResetActivatedText()
    {
        WLD_GameController.ui_Texts[UI_Txt.ActivatedText].text = "Activated";
    }
}
