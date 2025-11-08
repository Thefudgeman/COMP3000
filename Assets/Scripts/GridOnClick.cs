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
        var newNote = Instantiate(note, transform);
        newNote.transform.position = newNote.transform.parent.transform.position;
        newNote.transform.localPosition += new Vector3(0, 23);
        Debug.Log("afasfa");
    }
}
