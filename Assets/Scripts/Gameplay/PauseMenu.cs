using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))&& pauseMenu.activeSelf == false && SongControl.Instance.audioSource.isPlaying)
        {
            pauseMenu.SetActive(true);
            music.Pause();
        }
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        music.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(3);
    }
}
