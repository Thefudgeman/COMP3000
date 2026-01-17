using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public List<double> timeStamps = new List<double>();
    public List<HoldNoteData> holdTimeStamps = new List<HoldNoteData>();

    List<Note> notes = new List<Note>();
    public string input;
    public string LaneNumber;
    public TextAsset txt;
    public GameObject notePrefab;
    public AudioSource audioSource;
    int index = 0;
    int holdIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        string text = txt.text;
        string[] lines = text.Replace("\r", "").Split('\n');
        for(int i = 0; i < lines.Length-1; i++)
        {
            if (lines[i].Substring(0,1) == LaneNumber)
            {
                if (lines[i].Substring(lines[i].IndexOf(":")+1,1) == "0")
                {
                    Debug.Log(lines[i]);
                    Debug.Log(lines[i].Substring(0, 1));
                    AddNote(lines[i]);
                }
                else if (lines[i].Substring(lines[i].IndexOf(":")+1, 1) == "1")
                {
                    AddHoldNote(lines[i]);
                }
                
            }
        }
    }

    void AddNote(string Line)
    {
        Debug.Log(Line.Substring(Line.IndexOf(",") + 1, Line.Length - 2));
        timeStamps.Add(Convert.ToDouble(Line.Substring(Line.IndexOf(",")+1, Line.Length - 4))/1000);
    }

    void AddHoldNote(string Line)
    {
        HoldNoteData holdNote = new HoldNoteData();
        string[] data = Line.Split(",");
        holdNote.headTime = Convert.ToDouble(data[1]);
        holdNote.tailTime = Convert.ToDouble(data[2].Substring(0,data[2].Length-2));
        holdTimeStamps.Add(holdNote);
        Debug.Log(holdNote.headTime);
        Debug.Log(holdNote.tailTime);
        Debug.Log("ert");
    }

    // Update is called once per frame
    void Update()
    {
        if (index < timeStamps.Count)
        {
            if (SongControl.GetSongTime() >= timeStamps[index] - (0.7/SongControl.Instance.noteSpeed))
            {
                var note = Instantiate(notePrefab, transform);
                note.transform.position = note.transform.parent.transform.parent.transform.position;
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().hitTime = (float)timeStamps[index];
                note.GetComponent<Note>().laneNumber = Convert.ToInt32(LaneNumber);
                if(Convert.ToInt32(LaneNumber) < 4)
                {
                    note.GetComponent<Note>().noteSpawnX = -400;
                }
                else
                {
                    note.GetComponent<Note>().noteSpawnX = 400;
                }
                index++;
            }
        }
        if (holdIndex < holdTimeStamps.Count)
        {
            if (SongControl.GetSongTime() >= holdTimeStamps[holdIndex].headTime - ((0.7 / SongControl.Instance.noteSpeed)))
            {
                //holdNote prefab needed

                if (Convert.ToInt32(LaneNumber) < 4)
                {
                 //   holdNote.GetComponent<Note>().noteSpawnX = -400;
                }
                else
                {
               //     holdNote.GetComponent<Note>().noteSpawnX = 400;
                }
                holdIndex++;
            }
           
        }
    }
}

public class HoldNoteData
{
    public double headTime;
    public double tailTime;
}
