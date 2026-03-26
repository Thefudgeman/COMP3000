using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGameplayInstructions : MonoBehaviour
{
    public GameObject gameplayInstructions;
    public GameObject seeInstructionsQ;

    private void Start()
    {
        if(PlayerPrefs.GetInt("ShownQ") == 1)
        {
            seeInstructionsQ.SetActive(false);
        }
    }

    public void OpenInstructions()
    {
        gameplayInstructions.SetActive(true);
    }

    public void CloseInstructions()
    {
        gameplayInstructions.SetActive(false);
    }

    public void OpenSeeInstructions()
    {
        seeInstructionsQ.SetActive(true);
    }

    public void CloseSeeInstructions()
    {
        seeInstructionsQ.SetActive(false);
        PlayerPrefs.SetInt("ShownQ", 1);
    }
}
