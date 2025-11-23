using System.Collections;
using System.Collections.Generic;
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
        if(transform.childCount == 0)
        {
            var newNote = Instantiate(note, transform);
            newNote.transform.position = newNote.transform.parent.transform.position;
            newNote.transform.localPosition += new Vector3(0, 23);
            newNote.GetComponent<EditorNote>().timeStamp = GetComponentInParent<EditorTimingLine>().timeInstantiated + (0.7f/ SongControl.Instance.noteSpeed);
            GetComponentInParent<EditorTimingLine>().noteTimeStamps.Add(newNote.GetComponent<EditorNote>().timeStamp);
        }
        else
        {
            GetComponentInParent<EditorTimingLine>().noteTimeStamps.Remove(transform.GetChild(0).GetComponent<EditorNote>().timeStamp);
            Destroy(this.transform.GetChild(0).gameObject);
        }

            Debug.Log("afasfa");
    }
}
