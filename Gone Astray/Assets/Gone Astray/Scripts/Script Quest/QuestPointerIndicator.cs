using UnityEngine;
using UnityEngine.UI;

public class QuestPointerIndicator : MonoBehaviour
{
    [Header("Quest Target")]
    [SerializeField] private Transform target;
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private GameObject screenIndicator;
    [SerializeField] private RectTransform pointerRectTransform;
    [SerializeField] private float borderSize = 50f;
    [SerializeField] private Transform player;

    [Header("Arrow Indicator Settings")]
    [SerializeField] private Image arrowIndicatorImage;
    [SerializeField] private Image screenIndicatorImage;
    [SerializeField] private float maxDistance = 100f;

    private bool indicatorEnabled = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            indicatorEnabled = !indicatorEnabled;
            arrowIndicator.SetActive(indicatorEnabled);
            screenIndicator.SetActive(indicatorEnabled);
        }

        if (!indicatorEnabled || target == null || player == null)
        {
            arrowIndicator.SetActive(false);
            screenIndicator.SetActive(false);
            return;
        }

        UpdateIndicator();
    }

    private void UpdateIndicator()
    {

        float distance = Vector3.Distance(player.position, target.position);


        Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(target.position);


        bool isOffScreen = targetScreenPosition.x <= borderSize ||
                           targetScreenPosition.x >= Screen.width - borderSize ||
                           targetScreenPosition.y <= borderSize ||
                           targetScreenPosition.y >= Screen.height - borderSize;

        if (isOffScreen)
        {

            arrowIndicator.SetActive(true);
            screenIndicator.SetActive(false);


            Vector3 cappedScreenPosition = targetScreenPosition;
            cappedScreenPosition.x = Mathf.Clamp(cappedScreenPosition.x, borderSize, Screen.width - borderSize);
            cappedScreenPosition.y = Mathf.Clamp(cappedScreenPosition.y, borderSize, Screen.height - borderSize);


            pointerRectTransform.position = cappedScreenPosition;


            UpdateArrowIndicatorColor(distance);
        }
        else
        {

            screenIndicator.SetActive(true);
            arrowIndicator.SetActive(false);


            screenIndicator.transform.position = targetScreenPosition;


            screenIndicatorImage.color = Color.green;
        }
    }

    private void UpdateArrowIndicatorColor(float distance)
    {

        if (distance > maxDistance)
        {
            arrowIndicatorImage.color = Color.red;
        }
        else
        {
            arrowIndicatorImage.color = Color.yellow;
        }
    }
}
