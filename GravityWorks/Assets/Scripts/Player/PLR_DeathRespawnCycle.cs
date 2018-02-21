/* -----------------------------------------------------------------------------------
 * Class Name: PLR_DeathRespawnCycle
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: 
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLR_DeathRespawnCycle : MonoBehaviour 
{
    //Just need to add a ref to Points and deduct the predefined amount of points.
    #region VARIABLES
    [SerializeField] private int deathPoints = 200;
    public GameObject endPieceLorePanel;
    public AudioClip deathSound;

    WLD_HealthDmg wld_HealthDmg;
    PLR_CharacterMovement plr_CharacterMovement;
    WLD_SegmentController wld_SegmeController;
    PLR_Points plr_Points;
    Vector3 respawnLocation = new Vector3 (0,0,0);
    Rigidbody rb;
    GameObject player;
    UI_EndPieceMsgSystem msg;

    UNA_HashIDs hashIDs;
    Animator anim;
    AudioSource audioSource;

    private Color beginHealth = Color.green;
    private Color endHealth = Color.red;
    bool isDead = false, invinsiblity = false;
    bool isDying = false;
    float currentTime, startingMoveSpeed;

    public int segNumber;
    public bool reloadLevel = false;
    public float timeToDie = .2f;

    #endregion

    #region GETTERS/SETTERS

    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    /* ------------------------------------------------------------------------------
    * Function Name: Start
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Used to initialize variables or perform startup processes
    * ------------------------------------------------------------------------------
    */
    void Awake()
    {
        player = WLD_GameController.player;
        audioSource = GetComponent<AudioSource>();

    }
    void Start () 
	{
        wld_HealthDmg = this.GetComponent<WLD_HealthDmg>();
        plr_CharacterMovement = GetComponent<PLR_CharacterMovement>();
        plr_Points = GetComponent<PLR_Points>();
        rb = plr_CharacterMovement.RB;
        startingMoveSpeed = plr_CharacterMovement.MovingSpeed;

        currentTime = 0;

        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            msg = GameObject.Find("LevelSpecificUI/EndPieceLoreSystem").GetComponent<UI_EndPieceMsgSystem>();
        }
        else
        {
            msg = GameObject.FindObjectOfType<UI_EndPieceMsgSystem>();
        }

        hashIDs = FindObjectOfType<UNA_HashIDs>();

        anim = transform.GetChild(1).GetChild(0).GetComponent<Animator>();

        Debug.Log(anim.gameObject.name);

    }//End Start	
	
    /* ------------------------------------------------------------------------------
    * Function Name: Update
    * Return types: N/A
    * Argument types: N/A
    * Author: 
    * Date Created: 
    * Last Updated: 
    * ------------------------------------------------------------------------------
    * Purpose: Runs each frame. Used to perform frame based checks and actions.
    * ------------------------------------------------------------------------------
    */
	void Update () 
	{
        if (invinsiblity)
        {
            Invoke("InvisibilityTest", timeToDie * 2);
        }

        else if (!isDead && !invinsiblity)
        {
            DeathRespawnBehave();
        }

        segNumber = GetComponent<GRV_IndividualGravity>().Segment.SegmentNumber;


    }
    //End Update

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.teleporter || other.tag == UNA_Tags.doorCloseVolume)
        {
            respawnLocation = transform.position;

        }
    }

    void DeathRespawnBehave()
    {
        if (wld_HealthDmg.Health <= 0)
        {
            isDying = true;

            if (isDying && currentTime == 0)
            {
                audioSource.PlayOneShot(deathSound);

            }

            currentTime += Time.deltaTime;

            anim.SetTrigger(hashIDs.deathBool);
            
            if (timeToDie <= currentTime)
            {
                isDead = true;

                currentTime = 0;
                //rb.velocity = Vector3.zero;
                //rb.angularVelocity = Vector3.zero;
                //transform.position = new Vector3(WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterX, WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterY, WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterZ);

                //PlayerReset();
                if (reloadLevel == false)
                {
                    StartCoroutine(Death());
                }
                else if (reloadLevel == true)
                {
                    for (int i = 0; i < WLD_GameController.levels.Count; i++)
                    {
                        if (WLD_GameController.levels[(Scenes)i] == WLD_GameController.activeLevel)
                        {
                            invinsiblity = true;
                            WLD_Teleporter teleporter = FindObjectOfType<WLD_Teleporter>();
                            rb.velocity = Vector3.zero;
                            rb.angularVelocity = Vector3.zero;
                            teleporter.LevelToLoad = (Scenes)i;
                            PlayerReset();
                            teleporter.LoadLevelSegment(WLD_GameController.player.GetComponent<GRV_IndividualGravity>().Segment.SegmentNumber);

                        }
                    }
                }

            }
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.05f);
        PlayerReset();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = new Vector3(WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterX, WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterY, WLD_GameController.activeLevel.LevelSegments[segNumber - 1].TeleporterZ);

        StopCoroutine(Death());
    }

    private void InvisibilityTest()
    {
        invinsiblity = false;
        wld_HealthDmg.ChangeHealth(100);
    }

    public void PlayerReset()
    {
        WLD_GameController.ui_GameObjects[UI_GO_Panels.TransitionPanel].SetActive(false);
        WLD_GameController.ui_Images[UI_Images.CurrentPlanetImg].enabled = false;

        WLD_GameController.ui_Images[UI_Images.FillCurrentHealth].fillAmount = 100;
        WLD_GameController.ui_Images[UI_Images.FillCurrentHealth].color = Color.green;

        GetComponent<UI_HealthBarTransform>().healthBarPanelGO.transform.localPosition = GetComponent< UI_HealthBarTransform >().abovePlayer.transform.localPosition;

        msg.show = false;

        plr_CharacterMovement.moveSpeed = startingMoveSpeed;
        wld_HealthDmg.ChangeHealth(100);
        plr_Points.DeathPointRemoval();
        plr_Points.ResetToZero();

    }

}
// End PLR_DeathRespawnCycle