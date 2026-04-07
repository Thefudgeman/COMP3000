using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PerformanceManager : MonoBehaviour
{
    public TextMeshProUGUI displayScore;
    public TextMeshProUGUI displayAccuracy;
    public TextMeshProUGUI displayCombo;

    public TextMeshProUGUI resultsScore;
    public TextMeshProUGUI resultsAccuracy;
    public TextMeshProUGUI resultsMaxCombo;
    public TextMeshProUGUI resultsPerfect;
    public TextMeshProUGUI resultsGreat;
    public TextMeshProUGUI resultsOk;
    public TextMeshProUGUI resultsMiss;

    public static PerformanceManager Instance;
    public GameObject Results;
    int score = 0;
    int combo = 0;
    int maxCombo = 0;
    int missCount = 0;
    int perfectCount = 0;
    int okCount = 0;
    int greatCount = 0;
    float accuracy = 0;
    float accuracySum = 0;
    int noteCount = 0;
    public double lastNote = 0;
    bool showingResults = false;
    public int lastNoteFound = 0;
    string mapID = "0";
    int playerLeaderboardScore = -1;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (SongControl.GetSongTime() > lastNote && !showingResults && lastNoteFound == 8)
        {
            showingResults = true;

            resultsScore.SetText(score.ToString());
            resultsAccuracy.SetText(accuracy.ToString());
            resultsMaxCombo.SetText(maxCombo.ToString());
            resultsPerfect.SetText(perfectCount.ToString());
            resultsGreat.SetText(greatCount.ToString());
            resultsOk.SetText(okCount.ToString());
            resultsMiss.SetText(missCount.ToString());


            Results.SetActive(true);
            SubmitScore();
        }
    }

    public void Hit(double hitError)
    {
        Debug.Log("hit");
        hitError = Math.Abs(hitError);
        if (hitError <= 0.05)
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

        if (combo > maxCombo)
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
        displayCombo.text = "x" + combo.ToString();
        displayScore.text = score.ToString();
    }

    public void SubmitScore()
    {
        string file = File.ReadLines(Application.persistentDataPath + "/Music/" + varsToPass.Instance.path + "/" + varsToPass.Instance.path + ".txt").Last();
        mapID = file.Substring(file.IndexOf(":") + 1);

        var getPlayerScore = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string> { mapID }
        };

        PlayFabClientAPI.GetPlayerStatistics(getPlayerScore, OnGetPlayerScore, OnGetScoreError);
    }

    void OnGetPlayerScore(GetPlayerStatisticsResult result)
    {
        foreach (var resultScore in result.Statistics) 
        {
            if(resultScore.StatisticName == mapID)
            {
                Debug.Log(resultScore.Value);
                playerLeaderboardScore = resultScore.Value;
            }
        }
        Debug.Log(playerLeaderboardScore.ToString());
        if (score > playerLeaderboardScore)
        {
            Debug.Log("Submitting Score");
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = mapID,
                    Value = score,

                }
            }
            };
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnScoreSubmitted, OnScoreError);
        }
    }

    void OnGetScoreError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    void OnScoreSubmitted(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("score submitted");
    }

    void OnScoreError(PlayFabError error)
    {
        Debug.Log("error");
        Debug.Log(error.GenerateErrorReport());
    }
}
