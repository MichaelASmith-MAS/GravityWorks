using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WLD_DestroyExplosion : MonoBehaviour {

    public float timeToDestory = 5;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, timeToDestory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
