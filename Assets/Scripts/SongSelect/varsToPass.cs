using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class varsToPass : MonoBehaviour
{
    public string path;
    public static varsToPass Instance;
    void Start()
    {
        Instance = this;
    }
}
