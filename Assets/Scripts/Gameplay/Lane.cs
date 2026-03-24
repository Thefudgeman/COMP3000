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
    List<HoldNote> holdNotes = new List<HoldNote>();
    List<Note> notes = new List<Note>();
    public string LaneNumber;
    public TextAsset txt;
    public GameObject notePrefab;
    public GameObject holdPrefab;
    public AudioSource audioSource;
    int index = 0;
    int holdIndex = 0;
    public string input;
    public float axisNumber;
    private bool axisDown = false;
    private bool holding = false;
    private bool headMissed = false;
    private bool headHit = false;

    public int noteHitIndex = 0;
    public int holdNoteHitIndex = 0;

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
        holdNote.headTime = Convert.ToDouble(data[1])/1000;
        holdNote.tailTime = Convert.ToDouble(data[2].Substring(0,data[2].Length-2))/1000;
        holdTimeStamps.Add(holdNote);
        Debug.Log(holdNote.headTime);
        Debug.Log(holdNote.tailTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (index < timeStamps.Count)
        {
            if (SongControl.GetSongTime() >= timeStamps[index] - (0.7 / SongControl.Instance.noteSpeed))
            {
                var note = Instantiate(notePrefab, transform);
                note.transform.position = note.transform.parent.transform.parent.transform.position;
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().hitTime = (float)timeStamps[index];
                note.GetComponent<Note>().laneNumber = Convert.ToInt32(LaneNumber);
                if (Convert.ToInt32(LaneNumber) < 4)
                {
                    note.GetComponent<Note>().noteSpawnX = -360;
                }
                else
                {
                    note.GetComponent<Note>().noteSpawnX = 360;
                }
                index++;
            }
        }
        if (holdIndex < holdTimeStamps.Count)
        {
            if (SongControl.GetSongTime() >= holdTimeStamps[holdIndex].headTime - ((0.7 / SongControl.Instance.noteSpeed)))
            {
                //holdNote prefab needed
                var note = Instantiate(holdPrefab, transform);
                note.transform.position = note.transform.parent.transform.parent.transform.position;
                holdNotes.Add(note.GetComponent<HoldNote>());
                note.GetComponent<HoldNote>().headHitTime = (float)holdTimeStamps[holdIndex].headTime;
                note.GetComponent<HoldNote>().tailHitTime = (float)holdTimeStamps[holdIndex].tailTime;
                note.GetComponent<HoldNote>().laneNumber = Convert.ToInt32(LaneNumber);


                note.GetComponent<HoldNote>().headHitTime = (float)holdTimeStamps[holdIndex].headTime;
                note.GetComponent<HoldNote>().tailHitTime = (float)holdTimeStamps[holdIndex].tailTime;

                if (Convert.ToInt32(LaneNumber) < 4)
                {
                    note.GetComponent<HoldNote>().noteSpawnX = -360;
                }
                else
                {
                    note.GetComponent<HoldNote>().noteSpawnX = 360;
                }
                holdIndex++;
            }

        }
        if(noteHitIndex < timeStamps.Count)
        {
            if ((timeStamps[noteHitIndex] + 0.13f) * 1000 <= SongControl.GetSongTime())
            {
                PerformanceManager.Instance.Miss();
                Destroy(notes[noteHitIndex].gameObject);
                noteHitIndex++;
            }
            if (Convert.ToInt32(LaneNumber) > 3)
            {
                if (Input.GetKeyDown(input) && SongControl.GetSongTime() - timeStamps[noteHitIndex] > -0.13)
                {
                    double hitError = SongControl.GetSongTime() - timeStamps[noteHitIndex];
                    Debug.Log(SongControl.GetSongTime() - timeStamps[noteHitIndex] + " error");
                    PerformanceManager.Instance.Hit(hitError);
                    Destroy(notes[noteHitIndex].gameObject);
                    noteHitIndex++;
                }
            }
            else
            {
                if ((timeStamps[noteHitIndex] + 0.13f) * 1000 <= SongControl.GetSongTime())
                {
                    PerformanceManager.Instance.Miss();
                    Destroy(notes[noteHitIndex].gameObject);
                    noteHitIndex++;
                }
                if (axisDown && Input.GetAxis(input) == 0)
                {
                    axisDown = false;
                }
                if (((Input.GetAxis(input) < 0 && (Convert.ToInt32(LaneNumber) == 0 || Convert.ToInt32(LaneNumber) == 1)) || (Input.GetAxis(input) > 0 && (Convert.ToInt32(LaneNumber) == 2 || Convert.ToInt32(LaneNumber) == 3))) && !axisDown && SongControl.GetSongTime() - timeStamps[noteHitIndex] > -0.13)
                {
                   double hitError = SongControl.GetSongTime() - timeStamps[noteHitIndex];
                    PerformanceManager.Instance.Hit(hitError);
                    axisDown = true;
                    Destroy(notes[noteHitIndex].gameObject);
                    noteHitIndex++;
                }
            }
        }

        if (holdNoteHitIndex < holdTimeStamps.Count)
        {
            if ((holdTimeStamps[holdNoteHitIndex].headTime + 0.13) <= SongControl.GetSongTime() && !headHit)
            {
                PerformanceManager.Instance.Miss();
                Debug.Log("holdMiss" + (holdTimeStamps[holdNoteHitIndex].headTime + 0.13));
                holdNotes[holdNoteHitIndex].transform.GetChild(0).gameObject.SetActive(false);
                headMissed = true;
            }
            if (Convert.ToInt32(LaneNumber) > 3)
            {
                if (Input.GetKeyDown(input) && SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime > -0.13 && !headHit)
                {
                    double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime;
                    PerformanceManager.Instance.Hit(hitError);
                    Debug.Log(SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime + " errorssdf");
                    headHit = true;
                    holdNotes[holdNoteHitIndex].transform.GetChild(0).gameObject.SetActive(false);

                    holding = true;
                }

                if (((holdTimeStamps[holdNoteHitIndex].tailTime + 0.13) <= SongControl.GetSongTime()))
                {
                    PerformanceManager.Instance.Miss();
                    Destroy(holdNotes[holdNoteHitIndex].gameObject);

                    headHit = false;
                    headMissed = false;
                    holding = false;
                    holdNoteHitIndex++;
                }

                if (holding && Input.GetKeyUp(input))
                {
                    holding = false;

                    if(SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime >= -0.13)
                    {
                        double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime;
                        PerformanceManager.Instance.Hit(hitError);
                    }
                    else if(SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime < 0.13)
                    {
                        PerformanceManager.Instance.Miss();
                    }

                    Destroy(holdNotes[holdNoteHitIndex].gameObject);
                    headHit = false;

                    headMissed = false;
                    holdNoteHitIndex++;

                }


            }
            else
            {
                if (axisDown && Input.GetAxis(input) == 0)
                {
                    axisDown = false;
                }
                if (Input.GetAxis(input) < 0 && !holding && !axisDown && SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime > -0.13 && !headHit)
                {
                    double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime;
                    PerformanceManager.Instance.Hit(hitError);
                    holdNotes[holdNoteHitIndex].transform.GetChild(0).gameObject.SetActive(false);

                    headHit = true;
                    axisDown = true;
                    holding = true;
                }


                if (((holdTimeStamps[holdNoteHitIndex].tailTime + 0.13) <= SongControl.GetSongTime()))
                {
                    PerformanceManager.Instance.Miss();
                    Destroy(holdNotes[holdNoteHitIndex].gameObject);
                    headHit = false;

                    holding = false;
                    headMissed = false;
                    holdNoteHitIndex++;
                }

                if (holding && Input.GetAxis(input) == 0)
                {
                    holding = false;

                    if (SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime > -0.13)
                    {
                        double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime;
                        PerformanceManager.Instance.Hit(hitError);
                    }
                    else if (SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].tailTime < 0.13)
                    {
                        PerformanceManager.Instance.Miss();
                    }
                    Destroy(holdNotes[holdNoteHitIndex].gameObject);
                    headHit = false;

                    headMissed = false;
                    holdNoteHitIndex++;

                }

            }


        }

    }
}

public class HoldNoteData
{
    public double headTime = -1;
    public double tailTime = -1;
}
