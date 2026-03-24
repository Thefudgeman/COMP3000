using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void onPressed()
    {
        varsToPass.Instance.path = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        SceneManager.LoadScene(3);

    }
}
