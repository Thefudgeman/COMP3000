using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridOnClick : MonoBehaviour
{
    public GameObject note;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void click()
    {
        if(!GridUI.Instance.addHoldNote)
        {
            if (transform.childCount == 0)
            {
                var newNote = Instantiate(note, transform);
                newNote.transform.position = newNote.transform.parent.transform.position;
                newNote.transform.localPosition += new Vector3(0, 23);
                newNote.GetComponent<EditorNote>().timeStamp = GetComponentInParent<EditorTimingLine>().timeInstantiated + (0.7f / SongControl.Instance.noteSpeed);
                GetComponentInParent<EditorTimingLine>().noteTimeStamps.Add(newNote.GetComponent<EditorNote>().timeStamp);
            }
            else
            {
                GetComponentInParent<EditorTimingLine>().noteTimeStamps.Remove(transform.GetChild(0).GetComponent<EditorNote>().timeStamp);
                Destroy(this.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            if (transform.childCount == 0)
            {
                if (!GridUI.Instance.headAdded)
                {
                    GridUI.Instance.headTimingLine = transform.gameObject;
                    GridUI.Instance.holdNoteData.headTime = GetComponentInParent<EditorTimingLine>().timeInstantiated + (0.7f / SongControl.Instance.noteSpeed);
                    GridUI.Instance.headAdded = true;
                }
                else
                {
                    GridUI.Instance.tailTimingLine = transform.gameObject;
                    GridUI.Instance.holdNoteData.tailTime = GetComponentInParent<EditorTimingLine>().timeInstantiated + (0.7f / SongControl.Instance.noteSpeed);
                    GridUI.Instance.tailAdded = true;
                }
            }
            else
            {
                HoldNoteData holdNoteData = new HoldNoteData();
                holdNoteData.headTime = GetComponentInChildren<EditorHoldNote>().headHitTime;
                if(GetComponentInChildren<EditorHoldNote>().tailHitTime != -1)
                {
                    holdNoteData.tailTime = GetComponentInChildren<EditorHoldNote>().tailHitTime;
                }
                HoldNoteData holdNoteDatas = GetComponentInParent<EditorTimingLine>().holdNoteTimeStamps.First(x => x.headTime == GetComponentInChildren<EditorHoldNote>().headHitTime);
                GetComponentInParent<EditorTimingLine>().holdNoteTimeStamps.Remove(holdNoteDatas);
                Debug.Log("Destroying");

                Destroy(this.transform.GetChild(0).gameObject);
            }
           
        }
            Debug.Log("afasfa");
    }
}
