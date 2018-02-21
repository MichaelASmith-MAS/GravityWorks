/* -----------------------------------------------------------------------------------
 * Class Name: WLD_SegmentDoorDetection
 * -----------------------------------------------------------------------------------
 * Author: Joshua Schramm
 * Date Created: 10/05/17
 * Last Updated:
 * -----------------------------------------------------------------------------------
 * Purpose: Detect when the player is in the, "DoorCloseVolume," to lock them out of
 *          previous segments.
 * -----------------------------------------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_SegmentDoorDetection : MonoBehaviour 
{

    #region VARIABLES
    [SerializeField] private GameObject physicalDoor, renderSize, doorColliderVolume;
    [SerializeField] private Vector3 doorDestination;
    private Vector3 doorStartingPosition;

    public float doorCloseSpeed = .5f;
    public bool twoWayDoor = false;

    private bool startDoorClose = false;

    public GameObject[] airLockOpenEffect;
    //public GameObject airLockCloseEffect;
    #endregion

    #region GETTERS/SETTERS

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
    void Start () 
	{
        renderSize.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        doorStartingPosition = physicalDoor.transform.localPosition;
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
        if (startDoorClose)
        {
            physicalDoor.transform.localPosition = Vector3.Lerp(physicalDoor.transform.localPosition, doorStartingPosition, doorCloseSpeed);
        }
	}
    //End Update

    private void OnTriggerEnter(Collider other)
    {
        if (twoWayDoor)
        {
            if (other.tag == UNA_Tags.player)
            {
                startDoorClose = false;

                for (int i = 0; i < airLockOpenEffect.Length; i++)
                {
                    airLockOpenEffect[i].SetActive(true);
                }
            }
        }
        if (other.tag == UNA_Tags.player)
        {
            for (int i = 0; i < airLockOpenEffect.Length; i++)
            {
                airLockOpenEffect[i].SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            if (!twoWayDoor)
            {
                doorColliderVolume.SetActive(true);
            }

            physicalDoor.transform.localPosition = Vector3.Lerp(physicalDoor.transform.localPosition, doorDestination, doorCloseSpeed);

           
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (twoWayDoor)
        {
            if (other.tag == UNA_Tags.player)
            {
                startDoorClose = true;

                for (int i = 0; i < airLockOpenEffect.Length; i++)
                {
                    airLockOpenEffect[i].SetActive(false);
                }
            }
            //if (physicalDoor.transform.localPosition != doorStartingPosition)
            //{
            //    physicalDoor.transform.localPosition = Vector3.Lerp(physicalDoor.transform.localPosition, doorStartingPosition, doorCloseSpeed * 25);
            //}
        }
        if (other.tag == UNA_Tags.player)
        {
            for (int i = 0; i < airLockOpenEffect.Length; i++)
            {
                airLockOpenEffect[i].SetActive(false);
            }
        }
    }

    private void CloseDoor()
    {
        physicalDoor.transform.localPosition = Vector3.Lerp(physicalDoor.transform.localPosition, doorStartingPosition, doorCloseSpeed);
    }
}
// End WLD_SegmentDoorDetection