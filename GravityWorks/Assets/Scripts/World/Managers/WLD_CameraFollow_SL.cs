/* -----------------------------------------------------------------------------------
 * Class Name: WLD_CameraFollow_SL
 * -----------------------------------------------------------------------------------
 * Author: (Added by) Josh Schramm
 * Date: 9/26/17
 * Credit: Lauge, S. 09/29/2016. 2D Platformer Controller (Unity 5). Retrieved from
 *          https://www.youtube.com/playlist?list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz
 * -----------------------------------------------------------------------------------
 * Purpose: This script will specify how the camera should follow the player.
 * -----------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WLD_CameraFollow_SL : MonoBehaviour {

    #region VARIABLES
    public Collider target;
    public Vector2 focusAreaSize;
    public float zCamDist, verticalOffset, vertLookAhead = 5, lookAheadDstX, lookSmoothTimeX, verticalSmoothTime, camY_PosToFinal = 11, camX_PosToFinal = 24, camTimeToFinal = 2;

    public FocusArea focusArea;
    float currentLookAheadX, targetLookAheadX, lookAheadDirX, smoothLookVelocityX = 0.0f, smoothVelocityY = 0.0f, originalVertOffset;
    bool lookAheadStopped;
    public static bool inTeleporter;
    GameObject player;

    private string currentLevel, previousLevel;
    private bool finalLevelUnlocked = false, firstTimeToHubSinceFinalLevelUnlocked = true;
    #endregion

    void Start()
    {
        target = WLD_GameController.player.GetComponent<Collider>();
        player = WLD_GameController.player;

        focusArea = new FocusArea(target.bounds, focusAreaSize);
        originalVertOffset = verticalOffset;
        currentLevel = WLD_GameController.activeLevel.LevelName;
        previousLevel = currentLevel;

        if (zCamDist == 0)
        {
            zCamDist = this.transform.position.z;
        }

        inTeleporter = false;
    }

    private void OnLevelWasLoaded(int level)
    {
        target = WLD_GameController.player.GetComponent<Collider>();
        focusArea = new FocusArea(target.bounds, focusAreaSize);
    }

    private void Update()
    {

        //CheckForLevelChange();
        FinalLevelUnlockedCutscene();

        if (!finalLevelUnlocked || !firstTimeToHubSinceFinalLevelUnlocked)
        {
            if (!inTeleporter)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    verticalOffset += vertLookAhead;
                    verticalOffset = Mathf.Clamp(verticalOffset, originalVertOffset - vertLookAhead, originalVertOffset + vertLookAhead);
                }
                else if (Input.GetAxis("Vertical") < 0)
                {
                    verticalOffset -= vertLookAhead;
                    verticalOffset = Mathf.Clamp(verticalOffset, originalVertOffset - vertLookAhead, originalVertOffset + vertLookAhead);
                }
                else
                {
                    verticalOffset = originalVertOffset;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (!finalLevelUnlocked || !firstTimeToHubSinceFinalLevelUnlocked)
        {
            focusArea.Update(target.bounds);

            Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                lookAheadStopped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4;
                }
            }

            currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

            focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
            focusPosition += Vector2.right * currentLookAheadX;

            if (float.IsNaN(focusPosition.x) || float.IsNaN(focusPosition.y))
            {
                if (WLD_GameController.player.activeSelf)
                {
                    focusPosition = new Vector2(WLD_GameController.player.transform.position.x, WLD_GameController.player.transform.position.y + 2.5f);
                }
                else
                {
                    focusPosition = new Vector2(0, 2.5f);
                }
            }

            transform.position = (Vector3)focusPosition + Vector3.forward * zCamDist;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    public struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;

            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

    private void CheckForLevelChange()
    {
        currentLevel = WLD_GameController.activeLevel.LevelName;

        if (previousLevel != currentLevel)
        {
            focusArea = new FocusArea(target.bounds, focusAreaSize);

            //print("Camera's vertical offset is: " + verticalOffset + "   |||   BUT, the current y pos of the camera is: " + transform.position.y);

            previousLevel = currentLevel;
        }
    }

    private void FinalLevelUnlockedCutscene()
    {
        if (WLD_GameController.activeLevel == WLD_GameController.levels[Scenes.Hub])
        {
            if (WLD_GameController.levels[Scenes.DW_0713].FinalLevelPieceCollected == true &&
                WLD_GameController.levels[Scenes.KL_0602].FinalLevelPieceCollected == true &&
                WLD_GameController.levels[Scenes.JA_0629].FinalLevelPieceCollected == true &&
                WLD_GameController.levels[Scenes.Hub].FinalLevelPieceCollected == true)
            {
                finalLevelUnlocked = true;

                if (firstTimeToHubSinceFinalLevelUnlocked)
                {
                    StartCoroutine(CamToFinalLevel());
                }
            }
        }
    }

    IEnumerator CamToFinalLevel()
    {
        float curTime = 0.0f;
        Vector3 currentPos = transform.position;
        Vector3 desPos = new Vector3(camX_PosToFinal, camY_PosToFinal, transform.position.z);

        player.GetComponent<PLR_CharacterMovement>().enabled = false;

        do
        {
            transform.position = Vector3.Lerp(currentPos, desPos, curTime / camTimeToFinal);
            curTime += Time.deltaTime;
            yield return null;
        }
        while (transform.position != desPos);

        transform.position = desPos;
        yield return new WaitForSeconds(2);

        do
        {
            transform.position = Vector3.Lerp(currentPos, desPos, curTime / camTimeToFinal);
            curTime -= Time.deltaTime;
            yield return null;
        }
        while (transform.position != currentPos);

        firstTimeToHubSinceFinalLevelUnlocked = false;
        player.GetComponent<PLR_CharacterMovement>().enabled = true;
        StopAllCoroutines();
    }

}
