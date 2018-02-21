using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_TeleporterSwitch : MonoBehaviour {

    public Scenes currentLevel;
    public GameObject teleporterOn, teleporterOff;

    bool teleporterReached = false;
    int segmentNumber;
    WLD_Teleporter wld_Teleporter;

    private void Start()
    {
        segmentNumber = teleporterOn.GetComponent<WLD_Teleporter>().segmentNumber;
        wld_Teleporter = teleporterOn.GetComponent<WLD_Teleporter>();

        if (WLD_GameController.player.GetComponent<PLR_Points>().LastSegment != 1)
        {
            if (WLD_GameController.activeLevel.LevelSegments[teleporterOn.GetComponent<WLD_Teleporter>().segmentNumber - 2].SegmentComplete)
            {
                teleporterOn.SetActive(true);
                teleporterOff.SetActive(false);
                teleporterReached = true;
            }
        }
    }

    private void Update()
    {
        if (WLD_GameController.player.GetComponent<PLR_Points>().LastSegment != 1)
        {
            if (WLD_GameController.activeLevel.LevelSegments[teleporterOn.GetComponent<WLD_Teleporter>().segmentNumber - 2].SegmentComplete/*WLD_GameController.levels[currentLevel].LevelSegments[segmentNumber - 2].SegmentComplete*/)
            {
                teleporterOn.SetActive(true);
                teleporterOff.SetActive(false);
                teleporterReached = true;
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (!teleporterReached)
    //    {
    //        if (!WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].SegmentComplete)
    //        {
    //            WLD_GameController.activeLevel.LevelSegments[WLD_GameController.player.GetComponent<PLR_Points>().LastSegment - 1].SegmentComplete = true;

    //            //WLD_GameController.levels[currentLevel].LevelSegments[segmentNumber - 2].SegmentComplete = true;
    //        }
    //    }
    //}

}
