using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EditorTimingLine : MonoBehaviour
{
    public List<float> noteTimeStamps = new List<float>();
    public List<HoldNoteData> holdNoteTimeStamps = new List<HoldNoteData>();

    public int laneNumber;
    public Vector3 position;
    public float hitTime;
    public float timeInstantiated;
    public float noteSpawnX;
    public float noteTapX;
    public float noteDespawnX
    {
        get
        {
            return noteTapX - (noteSpawnX - noteTapX);
        }
    }
    public float noteSpawnY;
    public float noteTapY;
    public float noteDespawnY
    {
        get
        {
            return noteTapY - (noteSpawnY - noteTapY);
        }
    }
    public float timeStampIncrement;
    public float times;
    public float timeStampIncrementMultiplayer;
    public int beatDivsion;
    // Start is called before the first frame update
    void Start()
    {
        timeStampIncrement = timeStampIncrementMultiplayer * (1f / (SongControl.Instance.bpm / 60f));
        noteSpawnY = 0;
        if (gameObject.name == "FullBeat(Clone)")
        {
            timeInstantiated = 0f - (0.7f / SongControl.Instance.noteSpeed) + (1f / (SongControl.Instance.bpm / 60f) * times);
        }
        else if (gameObject.name == "HalfBeat(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / 2f - (0.7f / SongControl.Instance.noteSpeed) + (1f / (SongControl.Instance.bpm / 60f) * times);
        }
        else if (gameObject.name =="QuarterBeat(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / beatDivsion - (0.7f / SongControl.Instance.noteSpeed) + (1f / (SongControl.Instance.bpm / 60f) * times);
        }
        else if (gameObject.name == "QuarterBeat2(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / beatDivsion *(beatDivsion-1) - (0.7f / SongControl.Instance.noteSpeed) + (1f / (SongControl.Instance.bpm / 60f) * times);
        }

        if (laneNumber == 6 || laneNumber == 2)
        {
            noteTapY = 400;
        }
        else if (laneNumber == 0 || laneNumber == 4)
        {
            noteTapY = -400;
        }
        if (laneNumber == 7)
        {
            noteTapX = 900;
        }
        else if (laneNumber == 5)
        {
            noteTapX = 100;
        }
        else if (laneNumber == 3)
        {
            noteTapX = -100;
        }
        else if (laneNumber == 1)
        {
            noteTapX = -900;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float spawnDelay = SongControl.Instance.songDelay - (0.7f / SongControl.Instance.noteSpeed);
        double timeSinceInstantiated = spawnDelay > 0 && timeInstantiated < 0
            ? (Time.timeSinceLevelLoad - spawnDelay) + timeInstantiated
            : SongControl.GetSongTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (0.7f * 2 / SongControl.Instance.noteSpeed));
        
        if (timeSinceInstantiated - 0.001 > 0.7 / SongControl.Instance.noteSpeed) //minus one millisecond so if arrow keys are used to go through map the editor line still shows properly
        {
            if(laneNumber < 4) 
            {
                transform.localPosition = new Vector3(-500, 0);
            }
            else
            {
                transform.localPosition = new Vector3(500, 0);
            }
            if (transform.childCount > 0)
            {
                if(transform.GetChild(0).gameObject.GetComponent<EditorHoldNote>() != null) 
                {
                    transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    if(transform.GetChild(0).GetComponent<EditorHoldNote>().tailHitTime < SongControl.GetSongTime())
                    {
                        Destroy(transform.GetChild(0).gameObject);
                    }
                }
                else
                {
                    Destroy(transform.GetChild(0).gameObject);

                }
            }
            timeInstantiated += timeStampIncrement;
            GetComponent<RawImage>().enabled = false;
        }
        else
        {
            if(timeSinceInstantiated - 0.001 > 0 && !GetComponent<RawImage>().enabled) // add one millisecond so that when skipping through song with arrow keys editor line is shown properly
            {
                GetComponent<RawImage>().enabled = true;
            }
            if (laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
            {
                transform.localPosition = Vector3.Lerp(new Vector3(noteSpawnX, noteSpawnY, 0), new Vector3(noteSpawnX, noteDespawnY, 0), t);
            }
            else if (laneNumber == 1 || laneNumber == 7)
            {
                transform.localPosition = Vector3.Lerp(Vector3.left * -noteSpawnX, Vector3.left * -noteDespawnX, t);
            }
            else if (laneNumber == 3 || laneNumber == 5)
            {
                transform.localPosition = Vector3.Lerp(Vector3.right * noteSpawnX, Vector3.right * noteDespawnX, t);
            }
        }

        if (timeInstantiated > SongControl.GetSongTime())
        {
            if (laneNumber == 0 || laneNumber == 4)
            {
                transform.localPosition += new Vector3(0, -400);
            }
            else if (laneNumber == 2 || laneNumber == 6)
            {
                transform.localPosition += new Vector3(0, 400);
            }
            else if (laneNumber == 1 || laneNumber == 5)
            {
                transform.localPosition += new Vector3(400, 0);
            }
            else if (laneNumber == 3 || laneNumber == 7)
            {
                transform.localPosition += new Vector3(-400, 0);
            }
            timeInstantiated -= timeStampIncrement;
        }
    }
}
