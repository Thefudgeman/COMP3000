using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpLevelEditor : MonoBehaviour
{
    public GameObject helpMenu;
    public GameObject openingMessage;
    public GameObject setupMenu;
    public Button button;

    void Start()
    {
        if(PlayerPrefs.GetInt("ShownLevelQ") == 1)
        {
            CloseOpeningMessage();
            OpenSetup();
        }
    }

    public void OpenHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
        if(!button.GetComponent<Button>().IsActive())
        {
            setupMenu.SetActive(true);
        }
    }

    public void CloseOpeningMessage()
    {
        openingMessage.SetActive(false);
        PlayerPrefs.SetInt("ShownLevelQ", 1);
    }

    public void OpenSetup()
    {

        setupMenu.SetActive(true);
    }
}
