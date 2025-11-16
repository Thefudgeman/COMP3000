using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.TerrainTools;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    public int width, height;
    public GameObject fullBeat, halfBeat, quarterBeat, quarterBeat2;
    public AudioSource music;
    public List<double> timeStamps = new List<double>();
    public List<EditorTimingLine> editorTimingLines = new List<EditorTimingLine>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateGrid()
    {
        double heightf = ((double)music.clip.samples / music.clip.frequency) / 250;
        var spawnedTile = new GameObject();
        int beatNum = 0;
        for(int x = 0; x< width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                   spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
              //  spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * 100 + 100);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 2;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = -500;
            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
            //    spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * -100 -100);
                spawnedTile.transform.Rotate(0, 0, 180);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 0;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = -500;
            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
          //      spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * 100 + 100);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 6;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = 500;
            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
          //      spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * -100 - 100);
                spawnedTile.transform.Rotate(0, 0, 180);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 4;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = 500;
            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
             //   spawnedTile.transform.localPosition = new Vector3(y * 100 + 600, x * -100);
                spawnedTile.transform.Rotate(0, 0, 270);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 7;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = 500;

            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
             //   spawnedTile.transform.localPosition = new Vector3(y * 100 - 400, x * -100);
                spawnedTile.transform.Rotate(0, 0, 270);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 3;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = -500;
            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
            //   spawnedTile.transform.localPosition = new Vector3(y * -100 - 600, x * 100);
                spawnedTile.transform.Rotate(0, 0, 90);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 1;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = -500;

            }
        }

        beatNum = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (beatNum == 0)
                {
                    spawnedTile = Instantiate(fullBeat, transform);
                }
                else if (beatNum == 1)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
                }
                else if (beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat2, transform);
                }
                else
                {
                    spawnedTile = Instantiate(halfBeat, transform);
                }
                beatNum++;
                if (beatNum > 3)
                {
                    beatNum = 0;
                }
            //    spawnedTile.transform.localPosition = new Vector3(y * -100 + 400 , x * 100);
                spawnedTile.transform.Rotate(0, 0, 90);
                editorTimingLines.Add(spawnedTile.GetComponent<EditorTimingLine>());
                spawnedTile.GetComponent<EditorTimingLine>().laneNumber = 5;
                spawnedTile.GetComponent<EditorTimingLine>().noteSpawnX = 500;
            }
        }
    }
}
