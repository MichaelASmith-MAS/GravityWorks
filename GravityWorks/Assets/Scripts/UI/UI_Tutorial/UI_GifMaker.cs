using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GifMaker : MonoBehaviour {

    public Sprite[] sprites;
    public float timeOnImage = 0.5f;

    SpriteRenderer thisSprite;
    float curTime;
    int curSprite = 0;

    private void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
    }

    void Update ()
    {
        curTime += Time.deltaTime;

	    if (curTime >= timeOnImage)
        {
            curSprite += 1;

            if (curSprite > sprites.Length - 1)
            {
                curSprite = 0;
            }

            thisSprite.sprite = sprites[curSprite];
            curTime = 0;

            //Debug.Log("GIFMAKER RAN!! and is on this number: " + curSprite);
        }
	}
}
