using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Animator sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        sceneChange.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }

    public void ToMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    public void ToSongSelect()
    {

        StartCoroutine(LoadScene(1));
    }

    public void ToLevelEditor()
    {

        StartCoroutine(LoadScene(2));
    }

    public void ToGameplay()
    {

        StartCoroutine(LoadScene(3));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
