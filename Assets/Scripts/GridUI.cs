using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.UIElements;

public class GridUI : MonoBehaviour
{
    public int width, height;
    public GameObject fullBeat, halfBeat, quarterBeat, quarterBeat2;
    public AudioSource music;
    public List<double> timeStamps = new List<double>();
    public List<EditorTimingLine> editorTimingLines = new List<EditorTimingLine>();
    public int beatDivision;

    // Start is called before the first frame update
    void Start()
    {
       Invoke(nameof(GenerateGrid), 0.01f); //wait for SongControl.Instance to be created first to get bpm and noteSpeed
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        var spawnedTile = new GameObject();
        int beatNum = 0;
        float timeDiff = 1f / (SongControl.Instance.bpm / 60f) / (float)beatDivision;
        float distance = (0.7f / SongControl.Instance.noteSpeed) / timeDiff;
        float times = 400f / distance * (float)beatDivision;
        times = 400f / times;
        float timesInt = Mathf.Ceil(times);
        Debug.Log(timeDiff);
        Debug.Log(distance);

        Debug.Log(times);
        //0.7s = 400p   0.05s = 28.57142

        beatNum = 0;
        for(int l = 0; l < timesInt; l++)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(beatDivision == 4)
                    {
                        if (beatNum == 0)
                        {
                            spawnedTile = Instantiate(fullBeat, transform);
                        }
                        else if (beatNum == 1)
                        {
                            spawnedTile = Instantiate(quarterBeat, transform);
                        }
                        else if (beatNum == 2)
                        {
                            spawnedTile = Instantiate(halfBeat, transform);
                        }
                        else if (beatNum == 3)
                        {
                            spawnedTile = Instantiate(quarterBeat2, transform);
                        }
                    }
                    else if(beatDivision == 3)
                    {
                        if (beatNum == 0)
                        {
                            spawnedTile = Instantiate(fullBeat, transform);
                        }
                        else if (beatNum == 1)
                        {
                            spawnedTile = Instantiate(quarterBeat, transform);
                        }
                        else if (beatNum == 2)
                        {
                            spawnedTile = Instantiate(quarterBeat2, transform);
                        }
                    }
                    else if (beatDivision == 2)
                    {
                        if (beatNum == 0)
                        {
                            spawnedTile = Instantiate(fullBeat, transform);
                        }
                        else if (beatNum == 1)
                        {
                            spawnedTile = Instantiate(halfBeat, transform);
                        }
                    }
                    else if (beatDivision == 1)
                    {
                        if (beatNum == 0)
                        {
                            spawnedTile = Instantiate(fullBeat, transform);
                        }
                    }

                        beatNum++;
                    if (beatNum >= beatDivision)
                    {
                        beatNum = 0;
                    }
                    SetNoteProperties(spawnedTile, timesInt, i, l, beatDivision);
                }
            }
        }
       


    }

    void SetNoteProperties(GameObject spawnedTile, float timesInt, int laneNumber, int l, int beatDivision)
    {
        editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
        spawnedTile.GetComponent<EditorTimingLine>().laneNumber = laneNumber;
        spawnedTile.GetComponent<EditorTimingLine>().times = l;
        spawnedTile.GetComponent<EditorTimingLine>().timeStampIncrementMultiplayer = timesInt;
        spawnedTile.GetComponent<EditorTimingLine>().beatDivsion = beatDivision;
        if (laneNumber < 4)
        {
            spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = -500;
        }
        else
        {
            spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = 500;
        }

        if (laneNumber == 1 || laneNumber == 5)
        {
            spawnedTile.transform.Rotate(0, 0, 90);
        }
        else if (laneNumber == 0 || laneNumber == 4)
        {
            spawnedTile.transform.Rotate(0, 0, 180);
        }
        else if (laneNumber == 3 || laneNumber == 7)
        {
            spawnedTile.transform.Rotate(0, 0, 270);
        }
    }
}
