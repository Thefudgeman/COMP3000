using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class SaveLevelEditor : MonoBehaviour
{
    public GameObject gridUI;
    public class noteToAdd
    {
        public int laneNumber;
        public float timeStamp;
    }
    public class holdNoteToAdd
    {
        public int laneNumber;
        public float headTimeStamp;
        public float tailTimeStamp;
    }
    public void SaveChanges()
    {
        File.Create(Application.dataPath + "/SongTxtFiles/Flamewall.txt").Close();
        string path = Application.dataPath + "/SongTxtFiles/Flamewall.txt";
        StreamWriter sw = new StreamWriter(path, true);
        List<noteToAdd> timestamps = new List<noteToAdd>();
        List<holdNoteToAdd> holdTimestamps = new List<holdNoteToAdd>();

        for (int i = 0; i < gridUI.transform.childCount; i++)
        {
            for (int j = 0; j < gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().noteTimeStamps.Count; j++)
            {
                noteToAdd note = new noteToAdd();

                note.laneNumber = gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().laneNumber;
                note.timeStamp = gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().noteTimeStamps[j] * 1000f;
                timestamps.Add(note);
            }
        }

        for (int i = 0; i < gridUI.transform.childCount; i++)
        {
            for (int j = 0; j < gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().holdNoteTimeStamps.Count; j++)
            {
                holdNoteToAdd note = new holdNoteToAdd();

                note.laneNumber = gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().laneNumber;
                note.headTimeStamp = (float)gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().holdNoteTimeStamps[j].headTime * 1000f;
                note.tailTimeStamp = (float)gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().holdNoteTimeStamps[j].tailTime * 1000f;
                holdTimestamps.Add(note);
            }
        }

        List<noteToAdd> notes = timestamps.OrderBy(x => x.timeStamp).ToList();
        List<holdNoteToAdd> holdNotes = holdTimestamps.OrderBy(x=>x.headTimeStamp).ToList();
        Debug.Log("notes");

        for (int i = 0; i < notes.Count; i++)
        {
            Debug.Log(notes[i].timeStamp);
        }
        Debug.Log("holdnotes");
        for (int i = 0; i < holdNotes.Count; i++)
        {
            Debug.Log(holdNotes[i].headTimeStamp);
        }
        int notesIndex = 0;
        int holdNotesIndex = 0;

        while (notesIndex < notes.Count && holdNotesIndex < holdNotes.Count) 
        {
            if ((notes[notesIndex].timeStamp < holdNotes[holdNotesIndex].headTimeStamp))
            {
                sw.WriteLine(notes[notesIndex].laneNumber + "," + notes[notesIndex].timeStamp + ":0");
                notesIndex++;
            }
            else
            {
                sw.WriteLine(holdNotes[holdNotesIndex].laneNumber + "," + holdNotes[holdNotesIndex].headTimeStamp + "," + holdNotes[holdNotesIndex].tailTimeStamp + ":1");
                holdNotesIndex++;
            }
        }
        if (notesIndex < notes.Count)
        {
            for (int i = notesIndex; i < notes.Count; i++)
            {
                sw.WriteLine(notes[notesIndex].laneNumber + "," + notes[notesIndex].timeStamp + ":0");
                notesIndex++;
            }

            //sw.WriteLine("Test");
        }
        if(holdNotesIndex < holdNotes.Count)
        {
            for(int i = holdNotesIndex; i < holdNotes.Count;i++)
            {
                sw.WriteLine(holdNotes[holdNotesIndex].laneNumber + "," + holdNotes[holdNotesIndex].headTimeStamp + "," + holdNotes[holdNotesIndex].tailTimeStamp + ":1");
                holdNotesIndex++;
            }
        }
            sw.Close();
    }
}

