using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelController : MonoBehaviour
{
    public GameObject tutorialPanel; 

    void Start()
    {
        tutorialPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tutorialPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
