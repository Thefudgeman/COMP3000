using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    double headTimeInstantiated;
    double tailTimeInstantiated;
    public int laneNumber;
    public float headHitTime;
    public float tailHitTime;
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
    RectTransform headSize;
    RectTransform tailSize;
    float timeAlive;
    // Start is called before the first frame update
    void Start()
    {
        headTimeInstantiated = headHitTime - (0.7 / SongControl.Instance.noteSpeed);
        tailTimeInstantiated = tailHitTime - (0.7 / SongControl.Instance.noteSpeed);
        noteTapX = transform.parent.transform.parent.transform.position.x;
        noteTapY = transform.parent.transform.parent.transform.position.y;
        

        if (laneNumber == 1 || laneNumber == 5)
        {
            transform.localPosition += new Vector3(-320, 0, 0);

        }
        else if (laneNumber == 3 || laneNumber == 7)
        {
            transform.GetChild(0).Rotate(0, 0, 180);
            transform.GetChild(1).Rotate(0, 0, 180);
            transform.localPosition += new Vector3(320, 0, 0);

        }
        if (laneNumber == 6 || laneNumber == 2)
        {
            transform.GetChild(0).Rotate(0, 0, 270);
            transform.GetChild(1).Rotate(0, 0, 270);
            transform.localPosition += new Vector3(0,320, 0);

            noteSpawnY = -320;

        }
        else if (laneNumber == 0 || laneNumber == 4)
        {
            transform.GetChild(0).Rotate(0, 0, 90);
            transform.GetChild(1).Rotate(0, 0, 90);
            transform.localPosition += new Vector3(0, -320, 0);
            noteSpawnY = 320;
        }


        headSize = transform.GetChild(0).GetComponent<RectTransform>();
        headSize.sizeDelta = new Vector2(0, 0);

        tailSize = transform.GetChild(1).GetComponent<RectTransform>();
        tailSize.sizeDelta = new Vector2(0, 0);

        timeAlive = 0.7f / SongControl.Instance.noteSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        float spawnDelay = SongControl.Instance.songDelay - (0.7f / SongControl.Instance.noteSpeed);
        double tailTimeSinceInstantiated = spawnDelay > 0 && tailTimeInstantiated < 0
    ? (Time.timeSinceLevelLoad - spawnDelay) + tailTimeInstantiated
    : SongControl.GetSongTime() - tailTimeInstantiated;
        float tailT = (float)(tailTimeSinceInstantiated / (0.7f * 2 / SongControl.Instance.noteSpeed));
        float tailSizeScale = timeAlive / (float)tailTimeSinceInstantiated;

        if (tailTimeSinceInstantiated > (0.7 / SongControl.Instance.noteSpeed) + 0.13) //if player does not hit the object destroy it once it is outside of the margin of error
        {
            Destroy(transform.gameObject);
            Debug.Log(SongControl.GetSongTime());
        }
        else
        {

            if (laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
            {
                transform.GetChild(1).localPosition = Vector3.Lerp(Vector3.up * noteSpawnY, Vector3.up * noteDespawnY, tailT);
            }
            else if (laneNumber == 1 || laneNumber == 7)
            {
                transform.GetChild(1).localPosition = Vector3.Lerp(Vector3.left * noteSpawnX, Vector3.left * noteDespawnX, tailT);

            }
            else if (laneNumber == 3 || laneNumber == 5)
            {
                transform.GetChild(1).localPosition = Vector3.Lerp(Vector3.right * noteSpawnX, Vector3.right * noteDespawnX, tailT);
            }

            tailSize.sizeDelta = new Vector2(162.0f / tailSizeScale, 557.0f / tailSizeScale);
        }

        double headTimeSinceInstantiated = spawnDelay > 0 && headTimeInstantiated < 0
            ? (Time.timeSinceLevelLoad - spawnDelay) + headTimeInstantiated
            : SongControl.GetSongTime() - headTimeInstantiated;
        float headT = (float)(headTimeSinceInstantiated / (0.7f * 2 / SongControl.Instance.noteSpeed));
        float headSizeScale = timeAlive / (float)headTimeSinceInstantiated;

        if (headTimeSinceInstantiated >= (0.7 / SongControl.Instance.noteSpeed) + 0.13 ) //if player does not hit the object destroy it once it is outside of the margin of error
        {
            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(transform.GetChild(0).gameObject);
            Debug.Log(SongControl.GetSongTime());
        }
        else
        {

            if (laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
            {
                transform.GetChild(0).localPosition = Vector3.Lerp(Vector3.up * noteSpawnY, Vector3.up * noteDespawnY, headT);
            }
            else if (laneNumber == 1 || laneNumber == 7)
            {
                transform.GetChild(0).localPosition = Vector3.Lerp(Vector3.left * noteSpawnX, Vector3.left * noteDespawnX, headT);

            }
            else if (laneNumber == 3 || laneNumber == 5)
            {
                transform.GetChild(0).localPosition = Vector3.Lerp(Vector3.right * noteSpawnX, Vector3.right * noteDespawnX, headT);
            }

            headSize.sizeDelta = new Vector2(162.0f / headSizeScale, 557.0f / headSizeScale);
        }



    }
}
