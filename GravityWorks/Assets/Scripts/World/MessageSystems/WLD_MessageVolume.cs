using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WLD_MessageVolume : MonoBehaviour {

    private GameObject player;
    private string str;
    private int completedStrNo, animatedTextNo;
    private bool messageStart = false, interactButtonEnabled = true, showFullMessage = false, showNextMessage = false, fillInMessage = false;
    private PLR_Shoot plr_shoot;
    private PLR_Jump plr_Jump;
    private PLR_CharacterMovement plr_CharacterMovement;
	private WLD_MessageSystem msg; //local variable to store the message system

    public float textSpeed = 0.02f;
    public int minTextLength = 5;
    public string msgFrom; //who is sending the message?
    public string[] msgBody; //what does the message say?
    public float msgTime; //how long does this message show?
    public bool onlyOnce = false; //do we only want this message to show one time?

    bool dontPlayAgain = false;
    // Use this for initialization
    void Start()
    {
        //this.GetComponent<Renderer> ().enabled = false;
        this.GetComponent<Collider>().enabled = false;
        Invoke("OnStart", 0.1f);
    }

    private void Update()
    {
        if (showFullMessage)
        {
            if (interactButtonEnabled && Input.GetAxis("Interact") > 0)
            {
                showNextMessage = true;
            }
        }

        SetInteractButtonEnabled();
    }

    //  OLD ONTRIGGERSTAY LOGIC
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == UNA_Tags.player)
    //    {

    //        if (Input.GetAxisRaw("Interact") > 0 && interactButtonEnabled && !messageStart && !dontPlayAgain)
    //        {
    //            //send a new message
    //            //msg.NewMessage(msgFrom,msgBody[0],msgTime);

    //            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    //            plr_CharacterMovement.enabled = false;
    //            plr_shoot.enabled = false;

    //            if (plr_Jump != null)
    //            {
    //                plr_Jump.enabled = false;
    //            }

    //            msg.msgFrom.text = msgFrom;
    //            msg.msgBody.text = "";
    //            StartCoroutine(StartLetters());
    //            msg.show = true;
    //            messageStart = true;

    //            dontPlayAgain = true;
    //        }

    //        if (Input.GetAxisRaw("Cancel") > 0)
    //        {
    //            StopAllCoroutines();
    //            msg.msgBody.text = "";
    //            msg.show = false;
    //            plr_CharacterMovement.enabled = true;
    //            plr_shoot.enabled = true;

    //            if (plr_Jump != null)
    //            {
    //                plr_Jump.enabled = true;
    //            }

    //            Invoke("SetMessageStartToFalse", 1f);
    //            //Destroy(gameObject);
    //        }
    //    }
    //}

    void OnTriggerExit(Collider player)
    {
        if (player.tag == UNA_Tags.player)
        {
            dontPlayAgain = false;
            msg.msgBody.text = "";
        }
    }

    void OnStart ()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            msg = GameObject.Find("LevelSpecificUI/MessageSystem").GetComponent<WLD_MessageSystem>();
            GetComponent<Collider>().enabled = true;
            player = WLD_GameController.player;
            plr_shoot = FindObjectOfType<PLR_Shoot>();
            plr_Jump = FindObjectOfType<PLR_Jump>();
            plr_CharacterMovement = player.GetComponent<PLR_CharacterMovement>();
        }

        else
        {
            //store the message system
            msg = FindObjectOfType<WLD_MessageSystem>();
            GetComponent<Collider>().enabled = true;
            player = WLD_GameController.player;
            plr_shoot = FindObjectOfType<PLR_Shoot>();
            plr_CharacterMovement = player.GetComponent<PLR_CharacterMovement>();
        }
    }

    void SetMessageStartToFalse()
    {
        messageStart = false;
        
    }

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

    //      OLD MESSAGE SYSTEM COROUTINE
    //IEnumerator StartLetters()
    //{
    //    for (int i = 0; i < msgBody.Length; i++)
    //    {
    //        msg.msgBody.text = "";

    //        foreach (char letter in msgBody[i])
    //        {
    //            msg.msgBody.text += letter;

    //            if (i >= msgBody.Length - 1 && letter == msgBody[i].Last())
    //            {
    //                do
    //                {
    //                    yield return null;
    //                }
    //                while (Input.GetAxis("Interact") == 0);

    //                if (Input.GetAxis("Interact") > 0)
    //                {
    //                    msg.show = false;
    //                    plr_CharacterMovement.enabled = true;
    //                    plr_shoot.enabled = true;

    //                    if (plr_Jump != null)
    //                    {
    //                        plr_Jump.enabled = true;
    //                    }

    //                    Invoke("SetMessageStartToFalse", 1f);
    //                    //Destroy(gameObject);
    //                }
    //            }

    //            else if (letter == msgBody[i].Last())
    //            {
    //                do
    //                {
    //                    yield return null;
    //                }
    //                while (Input.GetAxis("Interact") == 0);
    //            }

    //            yield return new WaitForSeconds(textSpeed);
    //        }
    //    }
    //}

    //  Newly Added to test new text message system

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            if (Input.GetAxisRaw("Interact") > 0 && interactButtonEnabled && !messageStart && !dontPlayAgain)
            {
                player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                plr_CharacterMovement.enabled = false;
                plr_shoot.enabled = false;

                if (plr_Jump != null)
                {
                    plr_Jump.enabled = false;
                }

                msg.msgFrom.text = msgFrom;
                StartCoroutine(ShowMessage ());

                dontPlayAgain = true;
            }

            if (msg.show && Input.GetAxis("Cancel") > 0)
            {
                str = "";
                msg.msgBody.text = str;
                msg.show = false;
                plr_CharacterMovement.enabled = true;
                plr_shoot.enabled = true;

                if (plr_Jump != null)
                {
                    plr_Jump.enabled = true;
                }

                Invoke("SetMessageStartToFalse", 1f);
                StopAllCoroutines();
            }
        }
    }

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
                plr_CharacterMovement.enabled = true;
                plr_shoot.enabled = true;

                if (plr_Jump != null)
                {
                    plr_Jump.enabled = true;
                }

                Invoke("SetMessageStartToFalse", 1f);
                StopAllCoroutines();
            }
        }
    }

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
}
