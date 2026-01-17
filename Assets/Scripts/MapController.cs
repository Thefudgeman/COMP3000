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
            Debug.Log((double)SongControl.Instance.audioSource.timeSamples / SongControl.Instance.audioSource.clip.frequency);
            SongControl.Instance.audioSource.timeSamples += (int)(SongControl.Instance.audioSource.clip.frequency * (1 / (SongControl.Instance.bpm / 60) / 4));
            Debug.Log((double)SongControl.Instance.audioSource.timeSamples / SongControl.Instance.audioSource.clip.frequency);
            Debug.Log(1/(SongControl.Instance.bpm/60)/4);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SongControl.Instance.audioSource.timeSamples -= (int)(SongControl.Instance.audioSource.clip.frequency * (1 / (SongControl.Instance.bpm / 60) / 4));
        }
    }
}
