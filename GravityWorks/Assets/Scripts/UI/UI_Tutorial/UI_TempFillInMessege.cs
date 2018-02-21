/* -----------------------------------------------------------------------------------
 * Class Name: UI_TempFillInMessege
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

public class UI_TempFillInMessege : MonoBehaviour 
{
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public GameObject messegePanel;
    public GameObject floor;
    public Text fillInText;
    public string tempFillInText;
    public float waitTime;
    public float destroyTime;
    // ------------------------------------------------------------------------------
    // Protected Variables
    // ------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------
    float timer;
    bool startMessege, stopMessege, dontReplay;

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
        dontReplay = false;
        startMessege = false;
        stopMessege = true;
    }
    void Update()
    {

        TurnOff();
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
    void OnTriggerEnter(Collider player)
    {
        if (!dontReplay)
        {
            if (player.tag == UNA_Tags.player)
            {
                startMessege = true;
                StartText();
                dontReplay = true;
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
    void StartText()
    {
        if (startMessege)
        {
            messegePanel.SetActive(true);
            StartCoroutine(StartLetters());
            stopMessege = true;
        }
    }
    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: Gradually print text
    // ------------------------------------------------------------------------------

    IEnumerator StartLetters()
    {
        foreach (char letter in tempFillInText)
        {
            fillInText.text += letter;

            if (letter == tempFillInText.Last<char>())
            {
                stopMessege = false;
            }
            yield return new WaitForSeconds(waitTime);
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
    void TurnOff()
    {
        if (!stopMessege)
        {
            Debug.Log("is on");
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                messegePanel.SetActive(false);
                fillInText.text = "";
                Destroy(floor);
                Destroy(gameObject);
            }
        }
    }

} // End UI_TempFillInMessege