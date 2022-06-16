using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hover_script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public AudioSource song;
    public GameObject Main;
    public GameObject Options;
    private Text text;
    private Color inColor;
    private Color outColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (text != null)
        {
            switch (text.text)
            {
                case "PLAY":
                    SceneManager.LoadScene("Main");
                    break;
                case "OPTIONS":
                    Debug.Log("Opcje");
                    Main.SetActive(false);
                    Options.SetActive(true);
                    break;
                case "EXIT":
                    Application.Quit();
                    break;
            }
        }
    }

  

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);
        if(text != null)
        {
            text.color = inColor;
        }
        if(song != null)
            song.Play();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        image.gameObject.SetActive(false);
        if(text != null)
        {
            text.color = outColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        inColor = Color.black;
        outColor = Color.white;
    }

    
}
