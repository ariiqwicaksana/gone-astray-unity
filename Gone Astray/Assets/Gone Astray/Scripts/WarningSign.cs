using System.Collections; 
using UnityEngine; 

public class WarningSign : MonoBehaviour
{
    public GameObject warningSign; 
    public float blinkInterval = 1f; 
    private Coroutine blinkCoroutine;

    private void Start()
    {
        warningSign.SetActive(false); 
    }

    private IEnumerator BlinkWarningSign()
    {
        while (true)
        {
            warningSign.SetActive(!warningSign.activeSelf); 
            yield return new WaitForSeconds(blinkInterval); 
        }
    }

    public void TriggerWarning(bool active)
    {
        if (active)
        {
            warningSign.SetActive(true); 
            if (blinkCoroutine == null)
            {
                blinkCoroutine = StartCoroutine(BlinkWarningSign());
            }
        }
        else
        {
            warningSign.SetActive(false); 
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
            }
        }
    }
}
