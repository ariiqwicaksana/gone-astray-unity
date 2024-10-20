using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    public Slider sliderSuhuPanas;     
    public Slider sliderSuhuDingin;   
    public Slider sliderOksigen;       
    public Image fillSuhuPanasImage;   
    public Image fillSuhuDinginImage;   
    public GameObject gameOverCanvas;   

    private float suhuPanas = 0f;      
    private float suhuDingin = 0f;     
    public float maksimumSuhu = 100f;   
    public float kecepatanSuhu = 10f;   
    
    private float oksigen = 100f;        
    public float maksimumOksigen = 100f;
    public float kecepatanPenggunaanOksigen = 5f; 
    public float kecepatanPengisianOksigen = 20f; 
    

    
    private bool isDead = false;


    private bool inAreaPanas = false;
    private bool inAreaDingin = false;
    private bool inAreaPengisianOksigen = false;

    void Start()
    {
    
        if (sliderSuhuPanas != null)
        {
            sliderSuhuPanas.minValue = 0f;
            sliderSuhuPanas.maxValue = maksimumSuhu;
            sliderSuhuPanas.value = 0f; 
        }

        
        if (sliderSuhuDingin != null)
        {
            sliderSuhuDingin.minValue = 0f;
            sliderSuhuDingin.maxValue = maksimumSuhu;
            sliderSuhuDingin.value = 0f; 
        }

        
        if (sliderOksigen != null)
        {
            sliderOksigen.minValue = 0f;
            sliderOksigen.maxValue = maksimumOksigen;
            sliderOksigen.value = maksimumOksigen;
        }

        
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (inAreaPanas)
            {
                suhuPanas += kecepatanSuhu * Time.deltaTime; 
            }
            else
            {
                suhuPanas -= kecepatanSuhu * Time.deltaTime;
            }

        
            if (inAreaDingin)
            {
                suhuDingin += kecepatanSuhu * Time.deltaTime; 
            }
            else
            {
                suhuDingin -= kecepatanSuhu * Time.deltaTime; 
            }

        
            suhuPanas = Mathf.Clamp(suhuPanas, 0f, maksimumSuhu);
            suhuDingin = Mathf.Clamp(suhuDingin, 0f, maksimumSuhu);

            if (sliderSuhuPanas != null)
            {
                sliderSuhuPanas.value = suhuPanas;
                
            }

            
            if (sliderSuhuDingin != null)
            {
                sliderSuhuDingin.value = suhuDingin;
    
            }

            if (inAreaPengisianOksigen)
            {
                oksigen += kecepatanPengisianOksigen * Time.deltaTime;
            }
            else
            {
                oksigen -= kecepatanPenggunaanOksigen * Time.deltaTime;
            }

            oksigen = Mathf.Clamp(oksigen, 0f, maksimumOksigen);
            if (sliderOksigen != null)
            {
                sliderOksigen.value = oksigen;
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
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = true;
            Debug.Log("Masuk ke Area Dingin");
        }
        else if (other.CompareTag("AreaOksigen"))
        {
            inAreaPengisianOksigen = true;
            Debug.Log("Masuk ke Area Pengisian Oksigen");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("AreaPanas"))
        {
            inAreaPanas = false;
            Debug.Log("Keluar dari Area Panas");
        }
        else if (other.CompareTag("AreaDingin"))
        {
            inAreaDingin = false;
            Debug.Log("Keluar dari Area Dingin");
        }
        else if (other.CompareTag("AreaOksigen"))
        {
            inAreaPengisianOksigen = false;
            Debug.Log("Keluar dari Area Pengisian Oksigen");
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