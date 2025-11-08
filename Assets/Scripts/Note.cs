using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        timeInstantiated = hitTime - 0.7;
        noteTapX = transform.parent.transform.position.x;
        noteTapY = transform.parent.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        float time = Time.deltaTime / (0.7f * 2);

        if(laneNumber == 0 || laneNumber == 4 || laneNumber == 2 || laneNumber == 6)
        {
           // transform.localPosition += Vector3.Lerp(Vector3.up * noteSpawnY, Vector3.up * noteDespawnY, 4500);

        }
        else if(laneNumber == 1 || laneNumber == 5 || laneNumber == 3 || laneNumber == 7)
        {
          //  transform.localPosition += Vector3.Lerp(Vector3.left * noteSpawnX, Vector3.left * noteDespawnX, 4500);

        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y + 0.5f), 2);
    }
}
