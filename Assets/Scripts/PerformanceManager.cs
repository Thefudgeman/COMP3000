using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{

    public static PerformanceManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(double hitError)
    {
        Debug.Log(hitError);
    }

    public void Miss()
    {

    }
}
