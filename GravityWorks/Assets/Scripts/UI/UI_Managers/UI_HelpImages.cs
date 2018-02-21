using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HelpImages : MonoBehaviour {
    #region Variables
    // ------------------------------------------------------------------------------
    // Public Variables
    // ------------------------------------------------------------------------------
    public Image exp;
    public Image sht;
    public Image cam;
    public Image frt;
    public Image etim;
    public Image hlth;
    public Image pts;
    public Image tele;
        

    // ------------------------------------------------------------------------------
    // Private Variables
    // ------------------------------------------------------------------------------

    bool explody = false;
    bool shoot = false;
    bool camaoe = false;
    bool fRate = false;
    bool eTime = false;
    bool health = false;
    bool points = false;
    bool teleport = false;

    #endregion

    // ------------------------------------------------------------------------------
    // FUNCTIONS
    // ------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------
    // Function Name: 
    // Return types: 
    // Argument types: 
    // Author: 
    // Date: 
    // ------------------------------------------------------------------------------
    // Purpose: 
    // ------------------------------------------------------------------------------


    // Use this for initialization
    void Start () {
        Checks();
    }
	
	// Update is called once per frame
	void Update () {
        //Checks();
	}

    public void Explody()
    {
        explody = true;

        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void Shoot()
    {
        explody = false;
        shoot = true;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void Cam()
    {
        explody = false;
        shoot = false;
        camaoe = true;
        fRate = false;
        eTime = false;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void FRate()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = true;
        eTime = false;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void ETime()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = true;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void Health()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = true;
        points = false;
        teleport = false;

        Checks();
    }

    public void Points()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = false;
        points = true;
        teleport = false;

        Checks();
    }

    public void None()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = false;
        points = false;
        teleport = false;

        Checks();
    }

    public void Teleporters()
    {
        explody = false;
        shoot = false;
        camaoe = false;
        fRate = false;
        eTime = false;
        health = false;
        points = false;
        teleport = true;

        Checks();
    }

    void Checks()
    {
        if (explody)
        {
            exp.enabled = true;
        }
        else
        {
            exp.enabled = false;
        }

        if (shoot)
        {
            sht.enabled = true;
        }
        else
        {
            sht.enabled = false;
        }

        if (camaoe)
        {
            cam.enabled = true;
        }
        else
        {
            cam.enabled = false;
        }

        if (fRate)
        {
            frt.enabled = true;
        }
        else
        {
            frt.enabled = false;
        }

        if (eTime)
        {
            etim.enabled = true;
        }
        else
        {
            etim.enabled = false;
        }

        if (health)
        {
            hlth.enabled = true;
        }
        else
        {
            hlth.enabled = false;
        }

        if (points)
        {
            pts.enabled = true;
        }
        else
        {
            pts.enabled = false;
        }

        if (teleport)
        {
            tele.enabled = true;
        }
        else
        {
            tele.enabled = false;
        }
    }
}
