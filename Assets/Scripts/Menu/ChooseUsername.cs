using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseUsername : MonoBehaviour
{

    [SerializeField] TMP_InputField username;
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] RawImage SetUsernameUI;

    private void Start()
    {
        if (PlayerPrefs.GetString("ChosenUsername") == "true")
        {
            SetUsernameUI.gameObject.SetActive(false);
        }
    }

    public void SetName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = username.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetNameSuccess, OnSetNameError);
    }

    void OnSetNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log(result);
        PlayerPrefs.SetString("ChosenUsername", "true");
        SetUsernameUI.gameObject.SetActive(false);
    }

    void OnSetNameError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        errorText.SetText(error.ErrorMessage);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
