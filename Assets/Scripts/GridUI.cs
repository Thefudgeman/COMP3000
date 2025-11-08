using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{
    public int width, height;
    public GameObject fullBeat, halfBeat, quarterBeat;

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
                spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * 100);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 - 500, y * -100);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * 100);
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
                spawnedTile.transform.localPosition = new Vector3(x * 100 + 500, y * -100);
            }
        }
    }
}
