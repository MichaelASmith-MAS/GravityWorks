using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_InvertedGravity : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            if (other.gameObject.GetComponent<GRV_IndividualGravity>().Drag > 0)
            {
                other.gameObject.GetComponent<GRV_IndividualGravity>().Drag = -1;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<GRV_IndividualGravity>() != null && other.gameObject.GetComponent<GRV_IndividualGravity>().Drag != -1)
        {
            if (other.gameObject.GetComponent<GRV_IndividualGravity>().Drag > 0)
            {
                other.gameObject.GetComponent<GRV_IndividualGravity>().Drag = -1;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<GRV_IndividualGravity>() != null)
        {
            if (other.gameObject.GetComponent<GRV_IndividualGravity>().Drag < 0)
            {
                other.gameObject.GetComponent<GRV_IndividualGravity>().Drag = 1;
            }
        }
    }
}
