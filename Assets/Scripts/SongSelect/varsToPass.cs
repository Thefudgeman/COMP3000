using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class varsToPass : MonoBehaviour
{
    public string path;
    public static varsToPass Instance;
    public int? scrollviewIndex;
    void Start()
    {
        Instance = this;
    }
}
