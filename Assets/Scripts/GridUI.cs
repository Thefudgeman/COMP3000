using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    public int width, height;
    public GameObject fullBeat, halfBeat, quarterBeat;
    public AudioSource music;
    public List<double> timeStamps = new List<double>();
    public double bpm;

    // Start is called before the first frame update
    void Start()
    {
        double timem = ((double)music.clip.samples / music.clip.frequency) / 60;
        double beats = timem * bpm * 4;
        Debug.Log(timem);
        double beatTime = 60 / bpm / 4;
        Debug.Log(beatTime);
        for (double i = 0; i < beats; i++)
        {
            timeStamps.Add(beatTime * i);
        }
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
                else if (beatNum == 1 || beatNum == 3)
                {
                   spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * 100 + 100);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * -100 -100);
                spawnedTile.transform.Rotate(0, 0, 180);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * 100 + 100);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * -100 - 100);
                spawnedTile.transform.Rotate(0, 0, 180);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(y * 100 + 600, x * -100);
                spawnedTile.transform.Rotate(0, 0, 270);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(y * 100 - 400, x * -100);
                spawnedTile.transform.Rotate(0, 0, 270);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(y * -100 - 600, x * 100);
                spawnedTile.transform.Rotate(0, 0, 90);
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
                else if (beatNum == 1 || beatNum == 3)
                {
                    spawnedTile = Instantiate(quarterBeat, transform);
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
                spawnedTile.transform.localPosition = new Vector3(y * -100 + 400 , x * 100);
                spawnedTile.transform.Rotate(0, 0, 90);
            }
        }
    }
}
