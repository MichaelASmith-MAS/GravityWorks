using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_AudioManager : MonoBehaviour {

    public AudioClip mainMenu, tutorial, hub, coreLevel, finalLevel;

    private AudioSource aS;

    private void Start()
    {
        aS = GetComponent<AudioSource>();

        SelectMusic();
    }

    private void OnLevelWasLoaded(int level)
    {
        SelectMusic();
    }

    void SelectMusic()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.MainMenu] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.LoadGame] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Controls] ||
            WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Credits])
        {
            if (aS.clip != mainMenu)
            {
                aS.clip = mainMenu;
                aS.volume = 0.6f;
                aS.Play();
            }
        }

        else if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Tutorial])
        {
            if (aS.clip != tutorial)
            {
                aS.clip = tutorial;
                aS.volume = 0.05f;
                aS.Play();
            }
        }

        else if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            if (aS.clip != hub)
            {
                aS.clip = hub;
                aS.volume = 0.6f;
                aS.Play();
            }
        }

        else if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Final])
        {
            if (aS.clip != finalLevel)
            {
                aS.clip = finalLevel;
                aS.volume = 0.15f;
                aS.Play();
            }
        }

        else
        {
            if (aS.clip != coreLevel)
            {
                aS.clip = coreLevel;
                aS.volume = 0.05f;
                aS.Play();
            }
        }
    }

}
