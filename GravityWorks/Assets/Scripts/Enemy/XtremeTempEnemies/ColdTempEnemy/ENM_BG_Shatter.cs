using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENM_BG_Shatter : MonoBehaviour {

    public GameObject[] popOutObjects;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < popOutObjects.Length; i++)
        {
            popOutObjects[i].GetComponent<Rigidbody>().isKinematic = true;
            popOutObjects[i].GetComponent<GRV_IndividualGravity>().enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == UNA_Tags.player)
        {
            for (int i = 0; i < popOutObjects.Length; i++)
            {
                popOutObjects[i].GetComponent<Rigidbody>().isKinematic = false;
                popOutObjects[i].GetComponent<GRV_IndividualGravity>().enabled = true;

                popOutObjects[i].GetComponent<Rigidbody>().AddExplosionForce(10000, popOutObjects[i].transform.position, 0, 20);
                GameObject go = Instantiate(Resources.Load("Imports/SimpleParticlePack/Resources/Explosions/Explosion01b"), popOutObjects[i].transform.position, popOutObjects[i].transform.rotation) as GameObject;
                go.transform.parent = popOutObjects[i].transform;

                Destroy(popOutObjects[i], 4);
                
            }
            
        }
    }
}
