
using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddSong : MonoBehaviour, IPointerDownHandler {
    public RawImage output;
    public AudioSource audioSource;
    public static AddSong Instance;
    public TMP_InputField inputField;

    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        Instance = this;
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
        inputField.text = url;
    }

}