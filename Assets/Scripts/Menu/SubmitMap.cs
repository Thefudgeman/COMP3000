using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
        string[] lines = txt.Split("\n");
        if (lines[lines.Length-1].Contains("LeaderboardID:"))
        {
            StreamWriter sw = new StreamWriter(url, false);
            lines[lines.Length-1] = "LeaderboardID:" + System.DateTime.UtcNow.Ticks;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                sw.Write(lines[i]);
                sw.Write("\n");
            }

                sw.Write(lines[lines.Length - 1]);
            

            sw.Close();

        }
        else
        {
            Debug.Log(lines[lines.Length-1]); 
            txt = txt + "\nLeaderboardID:" + System.DateTime.UtcNow.Ticks;
            Debug.Log(url);
            StreamWriter sw = new StreamWriter(url, true);
            sw.Write("\nLeaderboardID:" + System.DateTime.UtcNow.Ticks);
            sw.Close();
        }

    }
}
