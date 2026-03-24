using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EditorHoldNote : MonoBehaviour
{
    double headTimeInstantiated;
    double tailTimeInstantiated;
    public int laneNumber;
    public float headHitTime;
    public float tailHitTime;
  
    RectTransform headSize;
    RectTransform tailSize;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        headTimeInstantiated = headHitTime - (0.7 / SongControl.Instance.noteSpeed);
        tailTimeInstantiated = tailHitTime - (0.7 / SongControl.Instance.noteSpeed);


    //    headSize = transform.GetChild(0).GetComponent<RectTransform>();
    //    headSize.sizeDelta = new Vector2(0, 0);

     //   tailSize = transform.GetChild(1).GetComponent<RectTransform>();
      //  tailSize.sizeDelta = new Vector2(0, 0);
        timeAlive = 0.7f / SongControl.Instance.noteSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Math.Abs(GetComponentInParent<EditorTimingLine>().timeInstantiated + (0.7f / SongControl.Instance.noteSpeed) - tailTimeInstantiated));
        if (!GetComponentInParent<RawImage>().IsActive() && !(Math.Abs(tailTimeInstantiated - SongControl.GetSongTime()) < 0.7f / SongControl.Instance.noteSpeed))
        {
            transform.GetChild(1).GetComponent<RawImage>().enabled = false;

        }
        else if(Math.Abs(tailTimeInstantiated - SongControl.GetSongTime()) < 0.7f / SongControl.Instance.noteSpeed && GetComponentInParent<RawImage>().IsActive())
        {
            transform.GetChild(1).GetComponent<RawImage>().enabled = true;
        }
    }
}
