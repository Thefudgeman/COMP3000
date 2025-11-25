using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLevelEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveChanges()
    {
        string path = Application.dataPath + "/SongTxtFiles/Flamewall.txt";
        StreamWriter sw = new StreamWriter(path, true);
        sw.WriteLine("Test");
        sw.Close();
    }
}
