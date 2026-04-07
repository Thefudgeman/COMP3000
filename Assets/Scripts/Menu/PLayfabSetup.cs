using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class PLayfabSetup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayFabSettings.TitleId = "F6A69";
        Login();
    }
    void Login()
    {
       
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier, // unique per device
            CreateAccount = true // will create account if it doesn't exist
        };
        PlayFabClientAPI.LoginWithCustomID(request, onRegisterSuccess,  OnRegisterError);
    }

    void OnRegisterError(PlayFabError error)
    {
        Debug.Log("Error");
        Debug.Log(error.GenerateErrorReport());
    }

    void onRegisterSuccess(LoginResult result)
    {
        Debug.Log("registered");
        SetName();
    }

    void SetName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = "name"
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetNameSuccess, OnSetNameError);
    }

    void OnSetNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result);
    }

    void OnSetNameError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
