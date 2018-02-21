using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_SecretWalls : MonoBehaviour {

    public Material mat1, mat2;

    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            rend.material = mat2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            rend.material = mat1;
        }
    }
}
