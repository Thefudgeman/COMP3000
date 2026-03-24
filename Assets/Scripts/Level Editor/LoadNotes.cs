using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using UnityEngine;

public class LoadNotes : MonoBehaviour
{

    public TextAsset txt;
    public GameObject GridUIObject;

    public void LoadMap()
    {

        enableButtons.Instance.enableButton();
        Debug.Log(GridUIObject.transform.childCount);
        foreach (GameObject gameObject in GridUIObject.transform)
        {
            gameObject.GetComponent<EditorTimingLine>().loadMap = true;
        }
    }
}
