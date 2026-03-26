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
    }

    public void OpenSetup()
    {
        setupMenu.SetActive(true);
    }
}
