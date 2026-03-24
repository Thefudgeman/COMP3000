using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SongControl : MonoBehaviour
{

    public AudioSource audioSource;
    public static SongControl Instance;
    public float songDelay;
    public float noteSpeed;
    public float bpm;
    public bool editor;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        if(!editor)
        {
            Invoke(nameof(StartSong), songDelay);
        }
        StartCoroutine(OutputRoutine(Application.dataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".mp3"));
    }
    private IEnumerator OutputRoutine(string path)
    {
        var loader = new WWW(path);
        yield return loader;
        // output.texture = loader.texture;
        AudioClip audio = loader.GetAudioClip();
        audioSource.clip = audio;
    }

    public void StartSong()
    {
        audioSource.Play();
    }

    public void PauseSong()
    {

        audioSource.Pause();
    }

    public static double GetSongTime() //get a more accurate value for the audio source play time
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
