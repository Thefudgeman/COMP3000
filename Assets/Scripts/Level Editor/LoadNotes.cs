using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;

public class LoadNotes : MonoBehaviour
{

    public TextAsset txt;
    public GameObject GridUIObject;

    public void LoadMap()
    {
        enableButtons.Instance.enableButton();
        Debug.Log(GridUIObject.transform.childCount);
        string text = txt.text;
        string[] lines = text.Replace("\r", "").Split('\n');
        foreach (Transform editorLine in GridUIObject.transform)
        {
            for (int i = 0; i < lines.Length-1; i++)
            {
                if (Int32.Parse(lines[i].Substring(0, 1)) == editorLine.GetComponent<EditorTimingLine>().laneNumber)
                {
                    if (Int32.Parse(lines[i].Substring(lines[i].IndexOf(":") + 1, 1)) == 0)
                    {
                        for(double j = editorLine.GetComponent<EditorTimingLine>().timeInstantiated + (0.7f / SongControl.Instance.noteSpeed); j < 10; j+= GridUI.Instance.timestampMultiplier * (1f / (SongControl.Instance.bpm / 60f)))
                        {
                            double timestamp = Convert.ToDouble(lines[i].Substring(lines[i].IndexOf(",") + 1, lines[i].Length - 4)) / 1000;
                            Debug.Log(j + " and " + timestamp);
                            if (j == timestamp)
                            {
                                editorLine.GetComponent<EditorTimingLine>().noteTimeStamps.Add((float) timestamp);
                            }
                        }


                    }
                    else if (Int32.Parse(lines[i].Substring(lines[i].IndexOf(":") + 1, 1)) == 1)
                    {
                        HoldNoteData holdNote = new HoldNoteData();
                        string[] data = lines[i].Split(",");
                        holdNote.headTime = Convert.ToDouble(data[1]) / 1000;
                        holdNote.tailTime = Convert.ToDouble(data[2].Substring(0, data[2].Length - 2)) / 1000;
                        editorLine.GetComponent<EditorTimingLine>().holdNoteTimeStamps.Add(holdNote);
                   //     Debug.Log("pog");

                    }
                }
            }
        }
    }
}
