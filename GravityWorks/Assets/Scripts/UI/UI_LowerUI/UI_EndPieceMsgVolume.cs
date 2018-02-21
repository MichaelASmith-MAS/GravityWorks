/* -----------------------------------------------------------------------------------
 * Class Name: UI_EndPieceMsgVolume
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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class UI_EndPieceMsgVolume : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public float waitTime = .1f;
    public string msgFrom; //who is sending the message?
    public string[] msgBody; //what does the message say?
    public float msgTime; //how long does this message show?
    public bool onlyOnce = false; //do we only want this message to show one time?

    public GameObject endPiece;
    public GameObject endPieceSet;
    public GameObject endPieceOnEffects;
    public GameObject endPieceLights;
    public GameObject endPieceCollectedEffect;
    
    public float textSpeed = 0.02f;
    public int minTextLength = 5;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    UI_EndPieceMsgSystem msg; //local variable to store the message system

    private string str;
    int completedStrNo, animatedTextNo;
    bool messageStart = false, interactButtonEnabled = true, showFullMessage = false, showNextMessage = false, fillInMessage = false, finalMessageFinished = false;
    Quaternion endPieceSetOrgQuat, goOrgQuat;
    AudioSource aS;
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
        goOrgQuat = transform.rotation;
        endPieceSetOrgQuat = endPieceSet.transform.rotation;
        aS = GetComponent<AudioSource>();
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

    void Update()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            msg = GameObject.Find("LevelSpecificUI/EndPieceLoreSystem").GetComponent<UI_EndPieceMsgSystem>();
        }
        else
        {
            //store the message system
            msg = GameObject.FindObjectOfType<UI_EndPieceMsgSystem>();
        }
        
        if (showFullMessage)
        {
            if (interactButtonEnabled && Input.GetAxis("Interact") > 0)
            {
                showNextMessage = true;
            }
        }

        SetInteractButtonEnabled();
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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {        
            msg.msgFrom.text = msgFrom;
            WLD_GameController.player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            aS.Play();

            StartCoroutine(EndPieceCollectedAnimation());
            StartCoroutine(ShowMessage());

            this.GetComponent<Collider>().enabled = false;
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

    void SetMessageStartToFalse()
    {
        messageStart = false;

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

    void SetInteractButtonEnabled()
    {
        if (Input.GetAxis("Interact") > 0)
        {
            interactButtonEnabled = false;
        }
        if (Input.GetAxis("Interact") == 0)
        {
            interactButtonEnabled = true;
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

    void TurnOffMsg()
    {
        if (Input.GetAxis("Interact") > 0)
        {
            msg.show = false;
            Destroy(endPiece);
            StopAllCoroutines();
            //StopCoroutine(StartLetters());

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

    void SetFinalMessageFinishedToTrue()
    {
        finalMessageFinished = true;
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
    IEnumerator FindChildren()
    {
        endPieceSet.SetActive(false);
        endPieceOnEffects.SetActive(false);
        endPieceLights.SetActive(false);

        StopCoroutine(FindChildren());
        yield return new WaitForSeconds(.01f);
    }

    //  OLD MESSAGE SYSTEM
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------
    //IEnumerator StartLetters()
    //{
    //    TurnOffMsg();

    //    for (int i = 0; i < msgBody.Length; i++)
    //    {

    //        msg.msgBody.text = "";

    //        foreach (char letter in msgBody[i])
    //        {
    //            msg.msgBody.text += letter;

    //            if (i >= msgBody.Length - 1 && letter == msgBody[i].Last<char>())
    //            {
    //                do
    //                {
    //                    yield return null;
    //                }
    //                while (Input.GetAxis("Interact") == 0);

    //                if (Input.GetAxis("Interact") > 0)
    //                {
    //                    msg.show = false;                     
    //                    Destroy(endPiece);
    //                }
    //            }

    //            if (letter == msgBody[i].Last<char>())
    //            {
    //                do
    //                {
    //                    yield return null;
    //                }
    //                while (Input.GetAxis("Interact") == 0);
    //            }

    //            yield return new WaitForSeconds(waitTime);
    //        }
    //    }
    //}

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------

    IEnumerator EndPieceCollectedAnimation()
    {
        float curTime = 0.0f, curTime2 = 0.0f;
        float firstPhaseTime = 0.5f, secondPhaseTime = 2, thirdPhaseTime = 3;
        GameObject player = WLD_GameController.player;
        Vector3 currentPos = transform.position;
        Vector3 desPos1 = new Vector3(transform.position.x, player.transform.position.y + 3, transform.position.z);
        Vector3 desPos2 = new Vector3(player.transform.position.x, player.transform.position.y + 2, transform.position.z);

        player.GetComponent<PLR_CharacterMovement>().enabled = false;

        endPieceSet.GetComponent<WLD_RotatePlanet>().enabled = false;
        endPieceSet.transform.rotation = endPieceSetOrgQuat;

        while (curTime < firstPhaseTime)
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * 5);
            transform.position = Vector3.Lerp(currentPos, desPos1, curTime / firstPhaseTime);
            curTime += Time.deltaTime;

            if (finalMessageFinished && Input.GetAxis("Interact") > 0 || Input.GetAxis("Cancel") > 0)
            {
                curTime = secondPhaseTime;
                curTime2 = thirdPhaseTime;
                transform.position = desPos2;
            } 
            yield return null;
        }

        transform.position = desPos1;

        while (curTime <= secondPhaseTime)
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * 5);
            curTime += Time.deltaTime;

            if (finalMessageFinished && Input.GetAxis("Interact") > 0 || Input.GetAxis("Cancel") > 0)
            {
                curTime = secondPhaseTime;
                curTime2 = thirdPhaseTime;
                transform.position = desPos2;
            }
            yield return null;
        }

        while (curTime2 < thirdPhaseTime)
        {
            transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * 2);
            transform.position = Vector3.Lerp(desPos1, desPos2, curTime2 / 3);
            transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0.25f, 0.25f, 0.25f), curTime2 / thirdPhaseTime);
            curTime2 += Time.deltaTime;

            if (finalMessageFinished && Input.GetAxis("Interact") > 0 || Input.GetAxis("Cancel") > 0)
            {
                curTime = secondPhaseTime;
                curTime2 = thirdPhaseTime;
                transform.position = desPos2;
            }
            yield return null;
        }

        transform.rotation = goOrgQuat;

        StartCoroutine(FindChildren());
        GameObject endPieceCE = Instantiate(endPieceCollectedEffect, transform.position, transform.rotation);
        Destroy(endPieceCE, 3f);

        player.GetComponent<PLR_CharacterMovement>().enabled = true;
        StopCoroutine(EndPieceCollectedAnimation());

        yield return null;
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

    IEnumerator ShowMessage()
    {
        for (int i = 0; i < msgBody.Length; i++)
        {
            completedStrNo = i;
            animatedTextNo = 0;
            str = "";
            msg.msgBody.text = "";
            showFullMessage = false;
            showNextMessage = false;
            fillInMessage = false;
            msg.show = true;
            messageStart = true;

            StartCoroutine(AnimateText(msgBody[i]));

            while (!showNextMessage)
            {
                yield return null;
            }

            if (i >= msgBody.Length - 1)
            {
                str = "";
                msg.msgBody.text = str;
                msg.show = false;
                Invoke("SetFinalMessageFinishedToTrue", 0.25f);
                //plr_CharacterMovement.enabled = true;
                //plr_shoot.enabled = true;

                //if (plr_Jump != null)
                //{
                //    plr_Jump.enabled = true;
                //}

                Invoke("SetMessageStartToFalse", 1f);
                aS.Stop();
                StopCoroutine(ShowMessage());
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

    IEnumerator AnimateText(string strComplete)
    {
        while (animatedTextNo <= strComplete.Length && !showFullMessage)
        {
            if (animatedTextNo > minTextLength && !fillInMessage)
            {
                fillInMessage = true;
            }

            else if (animatedTextNo == strComplete.Length)
            {
                showFullMessage = true;
            }

            else if (fillInMessage && Input.GetAxis("Interact") != 0)
            {
                showFullMessage = true;
                animatedTextNo = strComplete.Length;
                msg.msgBody.text = strComplete;
            }

            else
            {
                str += strComplete[animatedTextNo++];
                msg.msgBody.text = str;
            }
            //print("animated text no: " + animatedTextNo + "   |||   string complete: " + strComplete.Length);

            yield return new WaitForSeconds(textSpeed);
        }

        if (showFullMessage)
        {
            animatedTextNo = strComplete.Length;
            msg.msgBody.text = strComplete;
            StopCoroutine(AnimateText(msgBody[completedStrNo]));
        }
    }

} // End UI_EndPieceMsgVolume