using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SubmitMap : MonoBehaviour
{

    string txt;

    public void OnClick()
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
        // output.texture = loader.texture;
        Debug.Log(loader.url);

        int lastPos = loader.url.LastIndexOf('/');
        int secondLastPos = loader.url.LastIndexOf('/', lastPos - 1);

        txt = File.ReadAllText(Application.persistentDataPath + "/Music/" + loader.url.Substring(secondLastPos + 1, loader.url.Substring(secondLastPos + 1).Length));

        txt =txt + "\nLeaderboardID:" + System.DateTime.UtcNow.Ticks;
        Debug.Log(url);
        StreamWriter sw = new StreamWriter(url, true);
        sw.Write("\nLeaderboardID:" + System.DateTime.UtcNow.Ticks);
        sw.Close();
    }
}
