using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavingUIFeedback : MonoBehaviour
{

    public GameObject savingMenu;
    public Button done;
    
    public void CloseMenu()
    {
        done.interactable = false;
        savingMenu.SetActive(false);
    }
}
