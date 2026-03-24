using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PerformanceManager : MonoBehaviour
{
    public TextMeshProUGUI displayScore;
    public TextMeshProUGUI displayAccuracy;
    public TextMeshProUGUI displayCombo;
    public static PerformanceManager Instance;
    int score=0;
    int combo = 0;
    int maxCombo = 0;
    int missCount=0;
    int perfectCount=0;
    int okCount=0;
    int greatCount=0;
    float accuracy=0;
    float accuracySum=0;
    int noteCount=0;
    
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
        Debug.Log("hit");
        hitError = Math.Abs(hitError);
        if(hitError <= 0.05)
        {
            score += 1000;
            perfectCount++;
            accuracySum += 100;
            Debug.Log("Perf");
        }
        else if (hitError > 0.05 && hitError <= 0.1)
        {
            score += 500;
            greatCount++;
            accuracySum += 50;
            Debug.Log("Great");
        }
        else
        {
            score += 250;
            okCount++;
            accuracySum += 25;
            Debug.Log("Bad");
        }
        combo++;

        if(combo>maxCombo)
        {
            maxCombo = combo;
        }
        updateValues();
        Debug.Log(score);
    }

    public void Miss()
    {
        Debug.Log("miss");
        missCount++;
        combo = 0;
        updateValues();
        
    }

    public void updateValues()
    {
        noteCount++;
        accuracy = accuracySum / noteCount;
        displayAccuracy.text = accuracy.ToString() + "%";
        displayCombo.text = "x" +combo.ToString();
        displayScore.text = score.ToString();
    }
}
