using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WLD_MessageSystem : MonoBehaviour {

    public GameObject panel;
    public Text msgFrom;
    public Text msgBody;

    public bool show = false;
    public float showTime = 0;

    private string currentLevel;
    private string previousLevel;
    private string body;

    private void Start()
    {
        currentLevel = WLD_GameController.activeLevel.LevelName;
        previousLevel = currentLevel;
    }

    void Update()
    {
        CheckForLevelChange();

        //if(panel.GetComponent<Image>().enabled != show)
        //{
        //    panel.GetComponent<Image>().enabled = show;
        //}

        if (panel.activeSelf != show)
            panel.SetActive(show);

        //if (showTime > 0)
        //{
        //    showTime -= Time.deltaTime;
        //}
        //else
        //{
        //    show = false;
        //}


    }

    public void NewMessage(string from, string body, float displayTime)
    {
        //fix new lines for Windows
        body = body.Replace("/n", "\n");

        showTime = displayTime;
        msgFrom.text = from;
        msgBody.text = body;
        show = true;
    }

    private void CheckForLevelChange()
    {
        currentLevel = WLD_GameController.activeLevel.LevelName;

        if (previousLevel != currentLevel)
        {
            show = false;
            previousLevel = currentLevel;
        }
    }
}
