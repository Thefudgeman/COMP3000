using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public int laneNumber;
    public float hitTime;
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
    RectTransform size;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = hitTime - (0.7/SongControl.Instance.noteSpeed);
        int multiplier = 1;
        if(laneNumber == 1 || laneNumber ==2 || laneNumber == 3 || laneNumber == 6)
        {
            multiplier = -1;
        }
        noteTapX = transform.parent.transform.position.x +40* multiplier;
        noteTapY = transform.parent.transform.position.y +40* multiplier;

        if (laneNumber == 1 || laneNumber == 5)
        {
          //  transform.Rotate(0, 0, 90);

        }
        else if (laneNumber == 3 || laneNumber == 7)
        {
            transform.Rotate(0, 0, 180);
        }
        if (laneNumber == 6 || laneNumber == 2)
        {
            noteSpawnY = -400;
            transform.Rotate(0, 0, 270);
        }
        else if(laneNumber == 0 || laneNumber == 4)
        {
            noteSpawnY = 400;
            transform.Rotate(0, 0, 90);

        }
        size = this.GetComponent<RectTransform>();
        size.sizeDelta = new Vector2(0, 0);

        timeAlive = 0.7f / SongControl.Instance.noteSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        float spawnDelay = SongControl.Instance.songDelay - (0.7f / SongControl.Instance.noteSpeed);
        double timeSinceInstantiated = spawnDelay > 0 && timeInstantiated < 0
            ? (Time.timeSinceLevelLoad - spawnDelay) + timeInstantiated
            : SongControl.GetSongTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (0.7f * 2 / SongControl.Instance.noteSpeed));
        float sizeScale = timeAlive/(float)timeSinceInstantiated;

        if (timeSinceInstantiated > (0.7/ SongControl.Instance.noteSpeed) + 0.13f) //if player does not hit the object destroy it once it is outside of the margin of error
        {
           // PerformanceManager.Instance.Miss();
            transform.parent.GetComponent<Lane>().noteHitIndex++;
            Destroy(gameObject);
            
        }
        else
        {
            
            if (laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
            {
                transform.localPosition = Vector3.Lerp(Vector3.up * noteSpawnY, Vector3.up * noteDespawnY, t);
            }
            else if (laneNumber == 1 || laneNumber == 7)
            {
                transform.localPosition = Vector3.Lerp(Vector3.left * noteSpawnX, Vector3.left * noteDespawnX, t);

            }
            else if (laneNumber == 3 || laneNumber == 5)
            {
                transform.localPosition = Vector3.Lerp(Vector3.right * noteSpawnX, Vector3.right * noteDespawnX, t);
            }
            size.sizeDelta = new Vector2(162.0f/ sizeScale, 557.0f/ sizeScale);
        }

    }
}
