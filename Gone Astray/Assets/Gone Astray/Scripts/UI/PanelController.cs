using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PanelController : MonoBehaviour
{
    public CanvasGroup panel1;
    public CanvasGroup panel2;
    public CanvasGroup panel3; // Main Menu Panel
    public CanvasGroup panel4;
    public CanvasGroup panel5;

    public CanvasGroup settingPanel; // Setting Panel
    public CanvasGroup levelSelectorPanel; // Level Selector Panel
    public Button settingButton;
    public Button backButton;
    public Button playButton;

    public TextMeshProUGUI loadingText; // Reference for the loading text in Panel 2
    private bool activatePanel5 = false;
    public float fadeDuration = 1f; // Duration for fade in/out for all panels

    private void Start()
    {
        StartCoroutine(ShowPanels());

        // Set initial states for main menu, setting, and level selector panels
        panel3.alpha = 1; // Main Menu Panel starts visible
        settingPanel.alpha = 0;
        levelSelectorPanel.alpha = 0;
        settingPanel.gameObject.SetActive(false);
        levelSelectorPanel.gameObject.SetActive(false);

        // Add button listeners
        settingButton.onClick.AddListener(() => StartCoroutine(SwitchToSettingPanel()));
        backButton.onClick.AddListener(() => StartCoroutine(SwitchToMainMenuPanel()));
        playButton.onClick.AddListener(() => StartCoroutine(SwitchToLevelSelectorPanel()));
    }

    private IEnumerator ShowPanels()
    {
        // Fade in Panel 1 at the start
        panel1.alpha = 0;
        yield return StartCoroutine(FadePanel(panel1, 1, 1f));
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadePanel(panel1, 0, 1f));

        // Show Panel 2 with loading dots animation
        yield return StartCoroutine(FadePanel(panel2, 1, 1f));
        StartCoroutine(AnimateLoadingText());
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(FadePanel(panel2, 0, 1f));

        // Activate and fade in Main Menu Panel (Panel 3) after Panel 2 fades out
        panel3.gameObject.SetActive(true);  // Ensure Panel 3 is active
        yield return StartCoroutine(FadePanel(panel3, 1, 1f));  // Fade in Panel 3

        // Start looping fade on Panel 4 for the "Press to Start" message
        StartCoroutine(LoopPanel4Fade());
    }

    private IEnumerator SwitchToSettingPanel()
    {
        yield return StartCoroutine(FadePanel(panel3, 0, fadeDuration)); // Fade out Main Menu Panel
        panel3.gameObject.SetActive(false); // Deactivate Main Menu Panel after fading out
        settingPanel.gameObject.SetActive(true);   // Activate Setting Panel
        yield return StartCoroutine(FadePanel(settingPanel, 1, fadeDuration));  // Fade in Setting Panel
    }

    private IEnumerator SwitchToMainMenuPanel()
    {
        yield return StartCoroutine(FadePanel(settingPanel, 0, fadeDuration));  // Fade out Setting Panel
        settingPanel.gameObject.SetActive(false); // Deactivate Setting Panel after fading out
        panel3.gameObject.SetActive(true); // Activate Main Menu Panel
        yield return StartCoroutine(FadePanel(panel3, 1, fadeDuration)); // Fade in Main Menu Panel
    }

    private IEnumerator SwitchToLevelSelectorPanel()
    {
        yield return StartCoroutine(FadePanel(panel3, 0, fadeDuration)); // Fade out Main Menu Panel
        panel3.gameObject.SetActive(false); // Deactivate Main Menu Panel
        levelSelectorPanel.gameObject.SetActive(true); // Activate Level Selector Panel
        yield return StartCoroutine(FadePanel(levelSelectorPanel, 1, fadeDuration)); // Fade in Level Selector Panel
    }

    private IEnumerator ReturnToMainMenuFromLevelSelector()
    {
        yield return StartCoroutine(FadePanel(levelSelectorPanel, 0, fadeDuration)); // Fade out Level Selector Panel
        levelSelectorPanel.gameObject.SetActive(false); // Deactivate Level Selector Panel
        panel3.gameObject.SetActive(true); // Activate Main Menu Panel
        yield return StartCoroutine(FadePanel(panel3, 1, fadeDuration)); // Fade in Main Menu Panel
    }

    private IEnumerator FadePanel(CanvasGroup panel, float targetAlpha, float duration)
    {
        float startAlpha = panel.alpha;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            panel.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }
        panel.alpha = targetAlpha;
    }

    private IEnumerator AnimateLoadingText()
    {
        string baseText = "Loading";
        int dotCount = 1;
        while (panel2.alpha > 0) // Run while Panel 2 is active
        {
            loadingText.text = baseText + new string('.', dotCount);
            dotCount = dotCount < 3 ? dotCount + 1 : 1;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator LoopPanel4Fade()
    {
        while (!activatePanel5)
        {
            yield return StartCoroutine(FadePanel(panel4, 0.5f, 1f));
            yield return StartCoroutine(FadePanel(panel4, 1f, 1f));
        }
    }

    private void Update()
    {
        // Check for Enter key to activate Panel 5
        if (Input.GetKeyDown(KeyCode.Return))
        {
            activatePanel5 = true;
            panel4.gameObject.SetActive(false); // Deactivate Panel 4
            panel5.alpha = 1; // Set Panel 5 alpha to fully visible
            panel5.gameObject.SetActive(true); // Activate Panel 5
        }

        // Check for Escape key to return from Level Selector to Main Menu
        if (Input.GetKeyDown(KeyCode.Escape) && levelSelectorPanel.gameObject.activeSelf)
        {
            StartCoroutine(ReturnToMainMenuFromLevelSelector());
        }
    }
}
