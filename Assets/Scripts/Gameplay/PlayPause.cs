using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPause : MonoBehaviour
{
    public AudioSource audioSource;
    public void OnClick()
    {
        if(audioSource.isPlaying)
        {
            SongControl.Instance.PauseSong();
        }
        else
        {
            SongControl.Instance.StartSong();
        }
    }

}
