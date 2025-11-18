using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EditorTimingLine : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        timeStampIncrement = (0.7f / SongControl.Instance.noteSpeed);
        noteSpawnY = 0;
        if (gameObject.name == "FullBeat(Clone)")
        {
            timeInstantiated = 0f - (0.7f / SongControl.Instance.noteSpeed);
        }
        else if (gameObject.name == "HalfBeat(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / 2f - (0.7f / SongControl.Instance.noteSpeed);
        }
        else if (gameObject.name =="QuarterBeat(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / 4f - (0.7f / SongControl.Instance.noteSpeed);
        }
        else if (gameObject.name == "QuarterBeat2(Clone)")
        {
            timeInstantiated = 1f / (SongControl.Instance.bpm / 60f) / 4f * 3f - (0.7f / SongControl.Instance.noteSpeed);
        }

        if (laneNumber == 6 || laneNumber == 2)
        {
            noteTapY = 400;
        }
        else if (laneNumber == 0 || laneNumber == 4)
        {
            noteTapY = -400;
        }
        if (laneNumber == 1)
        {
            noteTapX = -900;
        }
        else if (laneNumber == 5)
        {
            noteTapX = 100;
        }
        else if (laneNumber == 3)
        {
            noteTapX = -100;
        }
        else if (laneNumber == 7)
        {
            noteTapX = 900;
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


        if (timeSinceInstantiated > 0.7 / SongControl.Instance.noteSpeed)
        {
            if(laneNumber < 4)
            {
                transform.localPosition = new Vector3(-500, 0);
            }
            else
            {
                transform.localPosition = new Vector3(500, 0);
            }
            timeInstantiated += timeStampIncrement;
        }
        else
        {
            if (laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
            {
                transform.localPosition = Vector3.Lerp(new Vector3(noteSpawnX, noteSpawnY, 0), new Vector3(noteSpawnX, noteDespawnY, 0), t);
            }
            else if (laneNumber == 1 || laneNumber == 7)
            {
                transform.localPosition = Vector3.Lerp(Vector3.left * noteSpawnX, Vector3.left * noteDespawnX, t);
            }
            else if (laneNumber == 3 || laneNumber == 5)
            {
                transform.localPosition = Vector3.Lerp(Vector3.right * noteSpawnX, Vector3.right * noteDespawnX, t);
            }
        }
    }
}
