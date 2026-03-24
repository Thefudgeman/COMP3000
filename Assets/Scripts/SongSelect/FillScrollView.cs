using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class FillScrollView : MonoBehaviour
{
    string[] directories = Directory.GetDirectories("Assets/Music");
    public Button ScrollViewItem;
    // Start is called before the first frame update
    void Start()
    {
        foreach (string dir in directories)
        {
            string Name = dir.Substring(dir.LastIndexOf('\\') + 1);
            Button Item = Instantiate(ScrollViewItem);
            Item.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name;
            Item.transform.SetParent(transform.GetChild(0).GetChild(0).transform);
            Item.transform.localScale = Vector3.one; //sets scale to 108x larger otherwise
        }
    }
}
