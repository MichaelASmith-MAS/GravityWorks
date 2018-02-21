using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WLD_LevelLoader : MonoBehaviour
{

    //https://www.youtube.com/watch?v=YMj2qPq9CP8

    public float maxTime = 3;
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    float progress = 0, curTime = 0, clampedTime;

    public void LoadLevel(Scenes levelName, Vector3 spawnLocation)
    {
        //Disable all player inputs during this time; and restore them after new scene loads.
        WLD_GameController.player.GetComponent<GRV_IndividualGravity>().enabled = false;
        //Debug.Log(WLD_GameController.player.GetComponent<GRV_IndividualGravity>().enabled);
        StartCoroutine(LoadAsynchronously(levelName, spawnLocation));
    }

    IEnumerator LoadAsynchronously(Scenes levelName, Vector3 spawnLocation)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(WLD_GameController.levels[levelName].SceneIndex);
        loadingScreen.SetActive(true);

        operation.allowSceneActivation = false;
        //Debug.Log(WLD_GameController.player.GetComponent<GRV_IndividualGravity>().enabled);

        while (!operation.isDone)
        {
            curTime += Time.deltaTime;
            progress = Mathf.Clamp01(operation.progress / .9f);
            clampedTime = Mathf.Clamp01(curTime / maxTime);

            if (progress < clampedTime)
            {
                //print("Up Here, and the clampedTime is: " + clampedTime + "   |||   AND, progress is: " + progress);
                slider.value = progress;
                progressText.text = Mathf.Round(progress * 100f) + "%";
                if (progress == 1.0f)
                {
                    WLD_GameController.player.transform.position = spawnLocation;

                    Camera.main.GetComponent<WLD_CameraFollow_SL>().focusArea.Update(WLD_GameController.player.GetComponent<Collider>().bounds);
                    Camera.main.transform.position = (Vector3)Camera.main.GetComponent<WLD_CameraFollow_SL>().focusArea.center + Vector3.forward * Camera.main.GetComponent<WLD_CameraFollow_SL>().zCamDist;

                    WLD_GameController.player.GetComponent<GRV_IndividualGravity>().enabled = true;
                    WLD_GameController.player.GetComponent<PLR_DeathRespawnCycle>().IsDead = false;
                    WLD_GameController.player.GetComponent<PLR_Points>().LastSegment = 1;

                    operation.allowSceneActivation = true;

                }
            }
            else
            {
                //print("Down Here, and the clampedTime is: " + clampedTime + "   |||   AND, progress is: " + progress);
                slider.value = clampedTime;
                progressText.text = Mathf.Round(slider.value * 100) + "%";
                if (slider.value == 1.0f)
                {
                    WLD_GameController.player.transform.position = spawnLocation;

                    Camera.main.GetComponent<WLD_CameraFollow_SL>().focusArea.Update(WLD_GameController.player.GetComponent<Collider>().bounds);
                    Camera.main.transform.position = (Vector3)Camera.main.GetComponent<WLD_CameraFollow_SL>().focusArea.center + Vector3.forward * Camera.main.GetComponent<WLD_CameraFollow_SL>().zCamDist;

                    WLD_GameController.player.GetComponent<GRV_IndividualGravity>().enabled = true;
                    WLD_GameController.player.GetComponent<PLR_DeathRespawnCycle>().IsDead = false;
                    WLD_GameController.player.GetComponent<PLR_Points>().LastSegment = 1;

                    operation.allowSceneActivation = true;

                }
            }

            yield return null;
        }


    }

}
