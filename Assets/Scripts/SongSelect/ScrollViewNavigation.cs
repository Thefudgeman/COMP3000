using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewNavigation : MonoBehaviour
{
    public GameObject scrollViewContent;
    public static ScrollViewNavigation Instance;
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && index+1 <= scrollViewContent.transform.childCount-1)
        {
            scrollViewContent.transform.GetChild(index + 1).GetComponent<Toggle>().isOn = true;
            scrollViewContent.transform.GetChild(index).GetComponent<ChooseLevel>().onPressed();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && index - 1 >= 0)
        {
            scrollViewContent.transform.GetChild(index - 1).GetComponent<Toggle>().isOn = true;
            scrollViewContent.transform.GetChild(index).GetComponent<ChooseLevel>().onPressed();
            Debug.Log(scrollViewContent.transform.GetChild(index).GetComponentInChildren<TextMeshProUGUI>().text);
        }
    }
}
