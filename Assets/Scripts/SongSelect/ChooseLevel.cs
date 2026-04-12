using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public void onPressed()
    {
        Toggle toggle = GetComponent<Toggle>();
        varsToPass.Instance.path = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        string file = File.ReadAllText(Application.persistentDataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
        string[] lines = file.Split('\n');
        DisplayLeaderboard.Instance.mapId = lines[lines.Length - 1].Substring(lines[lines.Length - 1].IndexOf(":")+1);
        if(toggle.isOn)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
    }
}
