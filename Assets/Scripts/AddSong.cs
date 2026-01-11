
using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddSong : MonoBehaviour, IPointerDownHandler {
    public RawImage output;
    public AudioSource audioSource;

    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "mp3", false);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(paths[0]));
        }
    }

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        // output.texture = loader.texture;
        Debug.Log(loader.url);
        AudioClip audio = loader.GetAudioClip();
        audioSource.clip = audio;
        Debug.Log(audio.frequency);
        audioSource.Play();
        Debug.Log(loader.url);
        Debug.Log(url);
        int i;
        for (i = url.Length-1; i > 0; i--)
        {
            if (url[i] == '\\')
            {
                break;
            }
        }
        Debug.Log(i);
        File.Copy(url, "Assets/Music/"+url.Substring(i+1));
    }

}