using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Image circleOksigen;
    public Image circleSuhuPanas;
    public Image circleSuhuDingin;
    public WarningSign warningSign;
    public GameObject gameOverCanvas;   
    private float suhuPanas = 0f;
    private float suhuDingin = 0f;
    private float oksigen = 100f;
    public float maksimumSuhu = 100f;
    public float maksimumOksigen = 100f; 
    public float kecepatanSuhu = 10f;   
    public float kecepatanPenggunaanOksigen = 5f; 
    public float kecepatanPengisianOksigen = 20f; 
    public float decreaseRate = 0.5f;
    private bool isDead = false;
    private bool inAreaPanas = false;
    private bool inAreaDingin = false;
    private bool inAreaPengisianOksigen = false;
    [SerializeField] private ScriptableRendererFeature fullScreenFire;
    [SerializeField] private ScriptableRendererFeature fullScreenIce;
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material iceMaterial;
    [SerializeField] private float firescreenIntensityStat = 0f;
    [SerializeField] private float icescreenIntensityStat = 0f;
    private int firescreenIntensity = Shader.PropertyToID("_FirescreenIntensity");
    private int icescreenIntensity = Shader.PropertyToID("_IcescreenIntensity");

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }

        firescreenIntensityStat = 0f;
        icescreenIntensityStat = 0f;
        if (fireMaterial != null)
            fireMaterial.SetFloat(firescreenIntensity, firescreenIntensityStat);
        if (iceMaterial != null)
            iceMaterial.SetFloat(icescreenIntensity, icescreenIntensityStat);
    }

    void Update()
    {
        if (!isDead)
    {
        
        if (inAreaPanas)
            suhuPanas += kecepatanSuhu * Time.deltaTime;
        else
            suhuPanas -= kecepatanSuhu * Time.deltaTime;


        if (inAreaDingin)
            suhuDingin += kecepatanSuhu * Time.deltaTime;
        else
            suhuDingin -= kecepatanSuhu * Time.deltaTime;

        
        suhuPanas = Mathf.Clamp(suhuPanas, 0f, maksimumSuhu);
        suhuDingin = Mathf.Clamp(suhuDingin, 0f, maksimumSuhu);

        

    
        if (inAreaPengisianOksigen)
            oksigen += kecepatanPengisianOksigen * Time.deltaTime;
        else
            oksigen -= kecepatanPenggunaanOksigen * Time.deltaTime;

        
        oksigen = Mathf.Clamp(oksigen, 0f, maksimumOksigen);

        
        if (circleOksigen != null)
            circleOksigen.fillAmount = (oksigen / maksimumOksigen) * 0.5f; 
        
        if (circleSuhuPanas != null)
            circleSuhuPanas.fillAmount = (suhuPanas / maksimumSuhu) * 0.25f; 
        
        if (circleSuhuDingin != null)
            circleSuhuDingin.fillAmount = (suhuDingin / maksimumSuhu) * 0.25f; 

        if (inAreaPanas && fireMaterial != null)
            {
                firescreenIntensityStat = suhuPanas / maksimumSuhu;
                fireMaterial.SetFloat(firescreenIntensity, firescreenIntensityStat);
            }
            else if (inAreaDingin && iceMaterial != null)
            {
                icescreenIntensityStat = suhuDingin / maksimumSuhu;
                iceMaterial.SetFloat(icescreenIntensity, icescreenIntensityStat);
            }
            else
            {
                // Gradually decrease the fullscreen intensity if not in hot or cold area
                firescreenIntensityStat = Mathf.Max(0, firescreenIntensityStat - decreaseRate * Time.deltaTime);
                icescreenIntensityStat = Mathf.Max(0, icescreenIntensityStat - decreaseRate * Time.deltaTime);

                // Apply the decreased intensity to both materials to ensure they fade out
                if (fireMaterial != null)
                    fireMaterial.SetFloat(firescreenIntensity, firescreenIntensityStat);
                if (iceMaterial != null)
                    iceMaterial.SetFloat(icescreenIntensity, icescreenIntensityStat);
            }
        
        if (suhuPanas >= maksimumSuhu || suhuDingin >= maksimumSuhu || oksigen <= 0f)
        {
            isDead = true;
            HandleKematian();
        }
    }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AreaPanas"))
        {
            inAreaPanas = true;
            Debug.Log("Masuk ke Area Panas");
            warningSign.TriggerWarning(true);
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = true;
            Debug.Log("Masuk ke Area Dingin");
            warningSign.TriggerWarning(true);
        }
        else if (other.CompareTag("AreaOksigen"))
        {
            inAreaPengisianOksigen = true;
            Debug.Log("Masuk ke Area Pengisian Oksigen");
            warningSign.TriggerWarning(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AreaPanas"))
        {
            inAreaPanas = false;
            Debug.Log("Keluar dari Area Panas");
            warningSign.TriggerWarning(false);
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = false;
            Debug.Log("Keluar dari Area Dingin");
            warningSign.TriggerWarning(false);
        }
        else if (other.CompareTag("AreaOksigen"))
        {
            inAreaPengisianOksigen = false;
            Debug.Log("Keluar dari Area Pengisian Oksigen");
            warningSign.TriggerWarning(false);
        }
    }

    void HandleKematian()
    {
        Debug.Log("Astronot telah mati.");
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        Time.timeScale = 0f; 
    }
}