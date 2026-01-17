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

        public void SetLaneNumber(string laneNumberS)
        {
            laneNumber = Convert.ToInt32(laneNumberS);
        }
        public void SetTimeStamp(string timeStampS)
        {
            timeStamp = Convert.ToInt32(timeStampS);
        }
    }
    public void SaveChanges()
    {
        File.Create(Application.dataPath + "/SongTxtFiles/Flamewall.txt").Close();
        string path = Application.dataPath + "/SongTxtFiles/Flamewall.txt";
        StreamWriter sw = new StreamWriter(path, true);
        List<noteToAdd> timestamps = new List<noteToAdd>();
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

        List<noteToAdd> notes = timestamps.OrderBy(x => x.timeStamp).ToList();

        for (int i = 0; i < notes.Count; i++)
        {;
            sw.WriteLine(notes[i].laneNumber + "," + notes[i].timeStamp);
        }

        //sw.WriteLine("Test");
        sw.Close();
    }
}

