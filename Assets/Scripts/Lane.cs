using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public List<double> timeStamps = new List<double>();
    List<GameObject> notes = new List<GameObject>();
    public string input;
    public string LaneNumber;
    public TextAsset txt;
    public GameObject notePrefab;
    public AudioSource audioSource;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        string text = txt.text;
        string[] lines = text.Replace("\r", "").Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Substring(0,1) == LaneNumber)
            {
                Debug.Log(lines[i]);
                Debug.Log(lines[i].Substring(0, 1));
                AddNote(lines[i]);
            }
        }
    }

    void AddNote(string Line)
    {
        Debug.Log(Line.Substring(Line.IndexOf(",") + 1, Line.Length - 2));
        timeStamps.Add(Convert.ToDouble(Line.Substring(Line.IndexOf(",")+1, Line.Length - 2))/1000);
    }

    // Update is called once per frame
    void Update()
    {
        if (index < timeStamps.Count)
        {
            if ((double)audioSource.timeSamples / audioSource.clip.frequency >= timeStamps[index])
            {
                Debug.Log("e");
                var note = Instantiate(notePrefab, transform);
                notes.Add(note);
                index++;
            }
        }
    }
}
