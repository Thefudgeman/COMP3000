using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class FillScrollView : MonoBehaviour
{
    string[] directories;
    public Toggle ScrollViewItem;
    public ToggleGroup ToggleGroup;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        directories = Directory.GetDirectories(Application.persistentDataPath + "/Music");
        foreach (string dir in directories)
        {
            string Name = dir.Substring(dir.LastIndexOf('\\') + 1);
            Toggle Item = Instantiate(ScrollViewItem);
            Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
            Item.transform.SetParent(transform.GetChild(0).GetChild(0).transform);
            Item.transform.localScale = Vector3.one; //sets scale to 108x larger otherwise
            Item.group = ToggleGroup;
            Item.GetComponent<ChooseLevel>().audioSource = audioSource;
            StartCoroutine(OutputRoutine(Name, Item));
        }
    }

    private IEnumerator OutputRoutine(string name, Toggle Item)
    {
        var loader = new WWW(Application.persistentDataPath + "/Music/" + name + "/" + name + ".mp3");
        yield return loader;
        // output.texture = loader.texture;
        AudioClip audio = loader.GetAudioClip();
        Item.GetComponent<ChooseLevel>().clip = audio;
    }
}
