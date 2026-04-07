using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void onPressed()
    {
        varsToPass.Instance.path = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        string file = File.ReadAllText(Application.persistentDataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
        string[] lines = file.Split('\n');
        DisplayLeaderboard.Instance.mapId = lines[lines.Length - 1].Substring(lines[lines.Length - 1].IndexOf(":")+1);
    }
}
