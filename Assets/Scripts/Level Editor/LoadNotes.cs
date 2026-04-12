using SFB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadNotes : MonoBehaviour
{

    public TextAsset txt;
    public GameObject GridUIObject;
    public AudioSource music;

    public void LoadMap()
    {

        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "txt", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(paths[0]));

        }


    }
    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        Debug.Log(loader.url);
        int lastPos = loader.url.LastIndexOf('/');

        int secondLastPos = loader.url.LastIndexOf('/', lastPos - 1);
        int thirdLastPos = loader.url.LastIndexOf('/', secondLastPos - 1);

        string text = File.ReadAllText(Application.persistentDataPath + "/Music/" + loader.url.Substring(secondLastPos+1, loader.url.Substring(secondLastPos + 1).Length));

        string Songurl = Application.persistentDataPath + "/Music/" + loader.url.Substring(secondLastPos + 1, loader.url.Substring(secondLastPos + 1).Length - 4) + ".mp3";

        using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(Songurl, AudioType.MPEG))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                music.clip = clip;
            }
        }
        enableButtons.Instance.loadingMap = true;
        string[] file = File.ReadAllLines(Application.persistentDataPath + "/Music/" + loader.url.Substring(secondLastPos + 1, loader.url.Substring(secondLastPos + 1).Length - 4) + ".txt");

        SongControl.Instance.bpm = float.Parse(file[file.Length - 2].Substring(file[file.Length-2].IndexOf(":")+1));
        enableButtons.Instance.enableButton();
        Debug.Log(GridUIObject.transform.GetChild(0).transform.name);
        foreach (Transform child in GridUIObject.transform)
        {
            GameObject gameObject = child.gameObject;
            gameObject.GetComponent<EditorTimingLine>().text = text;
            gameObject.GetComponent<EditorTimingLine>().loadMap = true;
        }
    }
}