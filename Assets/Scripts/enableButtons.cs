using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class enableButtons : MonoBehaviour
{
    public Button quitButton, playPauseButton, saveButton;
    public TMP_Dropdown dropdown;
    public GameObject setup;
    public TMP_InputField bpm;

    public void enableButton()
    {

        try
        {
            int i;
            for (i = AddSong.Instance.inputField.text.Length - 1; i > 0; i--)
            {
                if (AddSong.Instance.inputField.text[i] == '\\')
                {
                    break;
                }
            }

            var directory = Directory.CreateDirectory("Assets/Music/" + AddSong.Instance.inputField.text.Substring(i + 1, AddSong.Instance.inputField.text.Substring(i + 1).Length - 4));
            Debug.Log(directory.Name);
            File.Copy(AddSong.Instance.inputField.text, directory + "/" + AddSong.Instance.inputField.text.Substring(i + 1));
            File.Create(directory + "/" + AddSong.Instance.inputField.text.Substring(i + 1, AddSong.Instance.inputField.text.Substring(i + 1).Length - 4) + ".txt");
        }
        catch (ArgumentException e)
        {
            System.Windows.Forms.MessageBox.Show("Could not find file: " + AddSong.Instance.inputField.text);
        }
        catch (FileNotFoundException e)
        {
            System.Windows.Forms.MessageBox.Show("Could not find file: " + AddSong.Instance.inputField.text);

        }

        if (dropdown.value == 0)
        {
            GridUI.Instance.beatDivision = 1;
        }
        else if (dropdown.value == 1)
        {
            GridUI.Instance.beatDivision = 2;
        }
        else if (dropdown.value == 2)
        {
            GridUI.Instance.beatDivision = 3;
        }
        else if (dropdown.value == 3)
        {
            GridUI.Instance.beatDivision = 4;
        }


        quitButton.GetComponent<Button>().enabled = true;
        playPauseButton.GetComponent<Button>().enabled = true;
        saveButton.GetComponent<Button>().enabled = true;
        setup.SetActive(false);
        SongControl.Instance.bpm = float.Parse(bpm.text);
        GridUI.Instance.GenerateGrid();
    }
}
