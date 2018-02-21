using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BilboardTrigger : MonoBehaviour {

    public GameObject Gif;
    public GameObject Warning;
    public Collider trigger;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == UNA_Tags.player)
        {
            Gif.SetActive(false);
            Warning.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(turnOff());
    }

    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(1.5f);
        Warning.SetActive(false);
        trigger.enabled = false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
