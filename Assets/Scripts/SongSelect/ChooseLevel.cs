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
    public GameObject scrollViewContent;
    Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    public void onPressed()
    {
        if(toggle.isOn)
        {
            varsToPass.Instance.path = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            string file = File.ReadAllText(Application.persistentDataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
            string[] lines = file.Split('\n');
            DisplayLeaderboard.Instance.mapId = lines[lines.Length - 1].Substring(lines[lines.Length - 1].IndexOf(":") + 1);
            varsToPass.Instance.scrollviewIndex = transform.GetSiblingIndex();


            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            audioSource.clip = clip;
            audioSource.Play();

            float height = 256*scrollViewContent.transform.childCount;

            if ((transform.localPosition.y * -1) - 542 < 0)
            {
                scrollViewContent.transform.localPosition = new Vector3(scrollViewContent.transform.localPosition.x, 0, scrollViewContent.transform.localPosition.z);

            }
            else if((transform.localPosition.y * -1) - 542 >= height - 1080)
            {
                Debug.Log(height);
                scrollViewContent.transform.localPosition = new Vector3(scrollViewContent.transform.localPosition.x, height - 1080, scrollViewContent.transform.localPosition.z);
            }
            else
            {
                scrollViewContent.transform.localPosition = new Vector3(scrollViewContent.transform.localPosition.x, (transform.localPosition.y * -1) - 542, scrollViewContent.transform.localPosition.z);
   
            }
            Debug.Log(transform.localPosition.y);
            ScrollViewNavigation.Instance.index = transform.GetSiblingIndex();
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);

        }
    }
}
