using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;
using System.Collections;

public class DisplayLeaderboard : MonoBehaviour
{


    public GameObject leaderboard;
    public static DisplayLeaderboard Instance;
    public string mapId = "0";
    public GameObject leaderboardItem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        Instance = this;
        updateLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {

    }

    async void updateLeaderboard()
    {

        while (Application.isPlaying)
        {
            await Task.Delay(500);

            var getLeaderboard = new GetLeaderboardRequest
            {
                StatisticName = mapId,
                StartPosition = 0,
                MaxResultsCount = 25
            };

            PlayFabClientAPI.GetLeaderboard(getLeaderboard, OnGetLeaderboard, OnGetLeaderboardError);
          

        }
    }

    void OnGetLeaderboard(GetLeaderboardResult result)
    {
        Debug.Log("Got leaderboard");
        foreach (Transform child in transform.GetChild(0).GetChild(0).transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var score in result.Leaderboard)
        {
            var newLeaderBoardItem = Instantiate(leaderboardItem);
            newLeaderBoardItem.transform.SetParent(transform.GetChild(0).GetChild(0).transform);
            newLeaderBoardItem.transform.localScale = Vector3.one;
            newLeaderBoardItem.GetComponent<LeaderboardItem>().score.SetText(score.StatValue.ToString());
            newLeaderBoardItem.GetComponent<LeaderboardItem>().playerName.SetText(score.DisplayName);
            newLeaderBoardItem.GetComponent<LeaderboardItem>().position.SetText("#" + (score.Position+1));
        }
    }

    void OnGetLeaderboardError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }


}

public class leaderboardScore
{
    public double score;
    public string name;
    
}
