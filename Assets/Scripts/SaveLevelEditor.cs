using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLevelEditor : MonoBehaviour
{
    public GameObject gridUI;

    public void SaveChanges()
    {
        string path = Application.dataPath + "/SongTxtFiles/Flamewall.txt";
        StreamWriter sw = new StreamWriter(path, true);
        for (int i = 0; i < gridUI.transform.childCount; i++)
        {
            for (int j = 0; j < gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().noteTimeStamps.Count; j++)
            {
                sw.WriteLine(gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().laneNumber + "," + gridUI.transform.GetChild(i).GetComponent<EditorTimingLine>().noteTimeStamps[j]*1000f);
            }
        }

        //sw.WriteLine("Test");
        sw.Close();
    }
}
