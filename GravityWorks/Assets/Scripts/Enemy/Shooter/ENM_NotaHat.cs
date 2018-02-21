using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENM_NotaHat : MonoBehaviour {

    bool killedPlayer = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == UNA_Tags.player && !killedPlayer)
        {
            other.GetComponent<WLD_HealthDmg>().ChangeHealth(-100);
            killedPlayer = true;
        }
    }
}
