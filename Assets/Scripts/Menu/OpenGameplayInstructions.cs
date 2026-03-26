using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGameplayInstructions : MonoBehaviour
{
    public GameObject gameplayInstructions;
    public GameObject seeInstructionsQ;

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
    }
}
