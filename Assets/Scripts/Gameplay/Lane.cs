using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Lane : MonoBehaviour
{
    public List<double> timeStamps = new List<double>();
    public List<HoldNoteData> holdTimeStamps = new List<HoldNoteData>();
    List<HoldNote> holdNotes = new List<HoldNote>();
    List<Note> notes = new List<Note>();
    public string LaneNumber;
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
     //   Debug.Log(varsToPass.Instance.path);
     //   txt = File.ReadAllLines(Application.dataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
     //   string text = File.ReadAllLines(Application.dataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
        string[] lines = File.ReadAllLines(Application.persistentDataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt");
        int i = 0;
        while (lines[i].Length != 0)
        {
            if (lines[i].Substring(0,1) == LaneNumber)
            {
                if (lines[i].Substring(lines[i].IndexOf(":")+1,1) == "0")
                {
                    AddNote(lines[i]);
                }
                else if (lines[i].Substring(lines[i].IndexOf(":")+1, 1) == "1")
                {
                    AddHoldNote(lines[i]);
                }
                
            }
            Debug.Log(lines[i]);
            i++;
        }
           StartCoroutine(setLastNoteTime(lines[i - 1]));
    }

    IEnumerator setLastNoteTime(string line)
    {
        yield return new WaitForSeconds(0.1f);
        if (holdTimeStamps.Count == 0 && timeStamps.Count == 0)
        {
            PerformanceManager.Instance.lastNoteFound++;

            yield break;
        }
        else if (holdTimeStamps.Count > 0 && timeStamps.Count == 0)
        {

            if (holdTimeStamps[holdTimeStamps.Count - 1].tailTime > PerformanceManager.Instance.lastNote)
            {
                PerformanceManager.Instance.lastNote = holdTimeStamps[holdTimeStamps.Count - 1].tailTime;
            }

        }
        if (holdTimeStamps.Count == 0 && timeStamps.Count > 0)
        {

            if (timeStamps[timeStamps.Count - 1] > PerformanceManager.Instance.lastNote)
            {
                PerformanceManager.Instance.lastNote = timeStamps[timeStamps.Count - 1];
            }
        }
        else if (timeStamps[timeStamps.Count - 1] > holdTimeStamps[holdTimeStamps.Count - 1].tailTime)
        {

            if (timeStamps[timeStamps.Count - 1] > PerformanceManager.Instance.lastNote)
            {
                PerformanceManager.Instance.lastNote = timeStamps[timeStamps.Count - 1];

            }
        }
        else
        {

            if (holdTimeStamps[holdTimeStamps.Count - 1].tailTime > PerformanceManager.Instance.lastNote)
            {
                PerformanceManager.Instance.lastNote = holdTimeStamps[holdTimeStamps.Count - 1].tailTime;
            }
        }
        PerformanceManager.Instance.lastNoteFound++;

    }

    void AddNote(string Line)
    {
        timeStamps.Add(Convert.ToDouble(Line.Substring(Line.IndexOf(",")+1, Line.Length - 4))/1000);
    }

    void AddHoldNote(string Line)
    {
        HoldNoteData holdNote = new HoldNoteData();
        string[] data = Line.Split(",");
        holdNote.headTime = Convert.ToDouble(data[1])/1000;
        holdNote.tailTime = Convert.ToDouble(data[2].Substring(0,data[2].Length-2))/1000;
        holdTimeStamps.Add(holdNote);
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
            if ((holdTimeStamps[holdNoteHitIndex].headTime + 0.13) <= SongControl.GetSongTime() && !headHit && !headMissed)
            {
                headMissed = true;
            }
            if (Convert.ToInt32(LaneNumber) > 3)
            {
                if(Input.GetKeyDown(input))
                {
                    holding = true;
                }
                if (Input.GetKeyDown(input) && SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime > -0.13 && !headHit)
                {
                    double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime;
                    PerformanceManager.Instance.Hit(hitError);
                    headHit = true;
                    holdNotes[holdNoteHitIndex].transform.GetChild(0).gameObject.SetActive(false);
                }

                if (SongControl.GetSongTime() > holdTimeStamps[holdNoteHitIndex].headTime + 0.13 && !headHit && headMissed)
                {
                    PerformanceManager.Instance.Miss();
                    headHit = true;
                }

                if (((holdTimeStamps[holdNoteHitIndex].tailTime + 0.13) <= SongControl.GetSongTime()))
                {

                    headHit = false;
                    headMissed = false;
                    holding = false;
                    holdNoteHitIndex++;
                }

                if (holding && Input.GetKeyUp(input) && holdTimeStamps[holdNoteHitIndex].headTime <= SongControl.GetSongTime())
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

                if (Input.GetAxis(input) != 0 && SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime > -0.13 && !headHit)
                {
                    double hitError = SongControl.GetSongTime() - holdTimeStamps[holdNoteHitIndex].headTime;
                    PerformanceManager.Instance.Hit(hitError);
                    holdNotes[holdNoteHitIndex].transform.GetChild(0).gameObject.SetActive(false);

                    headHit = true;
                }

                if(SongControl.GetSongTime() > holdTimeStamps[holdNoteHitIndex].headTime + 0.13 && !headHit && headMissed)
                {
                    PerformanceManager.Instance.Miss();
                    headHit = true;
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

                if (holding && Input.GetAxis(input) == 0 && holdTimeStamps[holdNoteHitIndex].headTime <= SongControl.GetSongTime())
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

                if (Input.GetAxis(input) != 0) //must be placed at end or holding will be false when checking for releasing the axis if at the top
                {
                    holding = true;
                }
                else
                {
                    holding = false;
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
