using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_MovingPlatform : MonoBehaviour {

    public GameObject movingPlatform;
    public float moveSpeed = 2.5f;
    public bool moveScreenDown = true, moveScreenUp = false, moveScreenRight = false, moveScreenLeft = false;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            if (moveScreenDown)
            {
                transform.Translate(-Vector3.up * Time.deltaTime * moveSpeed);
            }

            else if (moveScreenUp)
            {
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            }

            else if (moveScreenRight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }

            else if (moveScreenLeft)
            {
                transform.Translate(-Vector3.right * Time.deltaTime * moveSpeed);
            }
        }
    }
}
