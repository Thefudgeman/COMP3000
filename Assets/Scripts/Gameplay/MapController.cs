using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SongControl.Instance.audioSource.timeSamples += (int)(SongControl.Instance.audioSource.clip.frequency * (1 / (SongControl.Instance.bpm / 60) / GridUI.Instance.beatDivision));
            Debug.Log((double)SongControl.Instance.audioSource.timeSamples / SongControl.Instance.audioSource.clip.frequency);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && SongControl.GetSongTime() > 0)
        {
            SongControl.Instance.audioSource.timeSamples -= (int)(SongControl.Instance.audioSource.clip.frequency * (1 / (SongControl.Instance.bpm / 60) / GridUI.Instance.beatDivision));
            Debug.Log((double)SongControl.Instance.audioSource.timeSamples / SongControl.Instance.audioSource.clip.frequency);
        }
    }
}
