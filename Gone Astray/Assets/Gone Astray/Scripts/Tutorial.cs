using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelController : MonoBehaviour
{
    public GameObject tutorialPanel; 

    void Start()
    {
        tutorialPanel.SetActive(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tutorialPanel.SetActive(false);
        }
    }
}
